using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using EnumClass = GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.MVC.Util;
using System.Text;


namespace GestaoDDD.MVC.Controllers
{
    public class OrcamentoController : Controller
    {
        private readonly IOrcamentoAppService _orcamentoApp;
        private readonly ICategoriaAppService _categoriaApp;
        private readonly IServicoAppService _servicoApp;
        private readonly IPrestadorAppService _prestadorApp;
        private readonly ICidadeAppService _cidadeApp;
        private readonly ILogAppService _logAppService;

        private EnviaEmail _enviaEmail;

        private static string _msgRetorno = "";
        private static string _userId;

        public OrcamentoController(IOrcamentoAppService orcamentoApp, ICategoriaAppService categoriaApp,
            IServicoAppService servicoApp, IPrestadorAppService prestadorApp, ICidadeAppService cidadeApp,
            ILogAppService logAppService)
        {
            _orcamentoApp = orcamentoApp;
            _categoriaApp = categoriaApp;
            _servicoApp = servicoApp;
            _prestadorApp = prestadorApp;
            _cidadeApp = cidadeApp;
            _logAppService = logAppService;
        }


        public ActionResult ListarTodos()
        {
            var orcamentos =
                Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(
                    _orcamentoApp.RetornaOrcamentosAbertos());
            foreach (var orcamento in orcamentos)
            {
                var servico = _servicoApp.GetById(orcamento.serv_Id);
                if (servico != null)
                    orcamento.NomeServico = servico.serv_Nome;
            }
            ViewBag.Retorno = _msgRetorno;
            return View(orcamentos);
        }

        //
        // GET: /Orcamento/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrcamentoEnviadoSucesso()
        {
            
            return View();
        }

        //
        // GET: /Orcamento/Details/5
        public ActionResult Detalhes(int id, string usuarioId)
        {
            try
            {
                _userId = usuarioId;
                var orcamentoEntity = Mapper.Map<Orcamento, OrcamentoViewModel>(_orcamentoApp.GetById(id));
                var servico = _servicoApp.GetById(orcamentoEntity.serv_Id);
                ViewBag.Servico = servico.serv_Nome;
                ViewBag.UsuarioId = usuarioId;

                return View(orcamentoEntity);
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "Detalhes Get";

                var log = Mapper.Map<LogViewModel, Log>(logVm);


                
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        [HttpPost]

        public ActionResult Detalhes(OrcamentoViewModel orcamentoVm, string usuarioId)
        {
            try
            {
                var orcamentoEntity = Mapper.Map<OrcamentoViewModel, Orcamento>(orcamentoVm);
                orcamentoEntity.Status = EnumClass.EnumStatusOrcamento.Fechado;
                _orcamentoApp.Update(orcamentoEntity);

                return RedirectToAction("BuscaTrabalhos", new { usuarioId });
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "Detalhes Post";
                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }


        }

        //
        // GET: /Orcamento/Cadastrar
        public ActionResult Cadastrar()
        {
            try
            {
                ViewBag.ListaCat = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
                return View();
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "Cadastrar GET";
                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
            
        }

        // POST: /Orcamento/Cadastrar
        [HttpPost]
        public ActionResult Cadastrar(OrcamentoViewModel orcamento, int servico_id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var saudacao = "";
                    var date = DateTime.Now;
                    if (date.Hour > 12 && date.Hour < 18)
                    {
                        saudacao = "boa tarde";
                    }
                    else if (date.Hour > 0 && date.Hour < 12)
                    {
                        saudacao = "bom dia";
                    }
                    else
                    {
                        saudacao = "boa noite";
                    }


                    var orcamentoEntity = Mapper.Map<OrcamentoViewModel, Orcamento>(orcamento);

                    var endereco = orcamento.orc_Endereco;
                    var partes = endereco.Split(',');
                    foreach (var parte in partes.Where(s => s.Contains("-")))
                    {

                        var separar = parte.Split('-');
                        var ufs =
                            " AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA,PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO";
                        if (ufs.Contains(separar[1]))
                        {
                            orcamentoEntity.orc_estado =
                                (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), separar[1]);
                            orcamentoEntity.orc_cidade = separar[0];
                        }
                        else
                            continue;

                    }
                    orcamentoEntity.serv_Id = servico_id;
                    _orcamentoApp.Add(orcamentoEntity);

                    //var corpoCadastro = "Olá " + orcamento.orc_nome_solicitante + " seu orçamento já está cadastrado em nosso sistema, fique atento que logo o prestador entrará em contato com você. Obrigado por nos escolher!" ;

                    var corpoCadastro = "Olá, " + orcamento.orc_nome_solicitante.Trim() + ", " + saudacao + "!" + " <br /><br /> Seu orçamento já está cadastro em nosso sistema." +
                        "<br /> Fique atento que logo o prestador do seu serviço entrará em contato com você."+       
                        "<br /> <a href=" + '\u0022' + "www.agilizaorcamentos.com.br/Orcamento/Cadastrar" + '\u0022' + "><strong>Clique aqui</strong></a> para fazer uma nova solicitação de orçamento para você. " +
                               "<br /><br /> Att, <br />" +
                               " Equipe Agiliza.";

                    var assunto = "Orçamento Enviado";
                    _enviaEmail = new EnviaEmail();
                    var enviou = _enviaEmail.EnviaEmailConfirmacao(orcamentoEntity.orc_email_solicitante, corpoCadastro, assunto);
                    if (!enviou.Key)
                    {
                        var logVm = new LogViewModel();
                        logVm.Mensagem = enviou.Value;
                        logVm.Controller = "Enviar Email";
                        logVm.View = "Cadastrar Orçamento";
                        var log = Mapper.Map<LogViewModel, Log>(logVm);
                        _logAppService.SaveOrUpdate(log);
                    }

                    _enviaEmail.EnviaEmailConfirmacao(orcamentoEntity.orc_email_solicitante, corpoCadastro, assunto);

                    var prestadores  = _orcamentoApp.EnviaEmailParaPrestadoresQueOferecemOServico(orcamentoEntity.serv_Id);
                    foreach(var prestadorID in prestadores)
                    {
                        var prestador = _prestadorApp.GetPorGuid(prestadorID);
                        var envia = _orcamentoApp.EnviaEmailNotificacao(prestador, orcamentoEntity);
                        if (envia.Key)
                        {
                           
                            var corpoNotificacao = "Olá, " + prestador.pres_Nome.Trim() + ", " + saudacao + "!" + " <br /><br /> Chegou mais um orçamento para você." +
                                " <br /> Este orçamento está à uma distância de " + envia.Value.Trim() + ". <br />" +
                                "<br /> <a href=" + '\u0022' + "www.agilizaorcamentos.com.br/Orcamento/BuscaTrabalhos?usuarioId=" + prestador.pres_Id +  '\u0022' + "><strong>Clique aqui</strong></a> para visualizar os orçamentos disponíveis para você. " +
                                "<br /><br /> Att, <br />" +
                                " Equipe Agiliza.";

                            var assuntoNotificacao = "Novo orçamento encontrado";
                            _enviaEmail = new EnviaEmail();
                            var enviouNotificacao = _enviaEmail.EnviaEmailConfirmacao(prestador.pres_Email, corpoNotificacao, assuntoNotificacao);
                            if (!enviou.Key)
                            {
                                var logVm = new LogViewModel();
                                logVm.Mensagem = enviou.Value;
                                logVm.Controller = "Enviar Email Notificação";
                                logVm.View = "Enviar email notificação de novo orçamento.";
                                var log = Mapper.Map<LogViewModel, Log>(logVm);
                                _logAppService.SaveOrUpdate(log);
                            }
                        }
                    }

                    return RedirectToAction("OrcamentoEnviadoSucesso");
                }
                else
                {
                    ViewBag.ListaCat = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
                    return View(orcamento);
                }

            }
            catch (Exception e)
            {

                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "Cadastrar Post";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult Editar(int id)
        {
            var orcamento = _orcamentoApp.GetById(id);



            var orcamentoViewModel = Mapper.Map<Orcamento, OrcamentoViewModel>(orcamento);
            return View(orcamentoViewModel);
        }

        //
        // POST: /\Orcamento/Edit/5

        [HttpPost]
        public ActionResult Editar(OrcamentoViewModel orcamentoVm)
        {
            
                try
                {
                    //var orcamentodomain = Mapper.Map<OrcamentoViewModel, Orcamento>(orcamento);
                    var orcamento = _orcamentoApp.GetById(orcamentoVm.orc_Id);
                   // orcamentodomain.orc_descricao = orcamento.orc_descricao;
                    orcamento.orc_descricao = orcamentoVm.orc_descricao;

                    _orcamentoApp.Update(orcamento);
                    return RedirectToAction("ListarTodos");
                }
                catch (Exception e)
                {
                    var logVm = new LogViewModel();
                    logVm.Mensagem = e.Message;
                    logVm.Controller = "Orçamento";
                    logVm.View = "Editar Post";

                    var log = Mapper.Map<LogViewModel, Log>(logVm);

                    _logAppService.SaveOrUpdate(log);
                    return RedirectToAction("ErroAoCadastrar");
                }
           
        }


        public ActionResult Deletar(int id)
        {
            var orcamento = _orcamentoApp.GetById(id);

            if (orcamento.PrestadorFk != null)
                _msgRetorno = "Este orçamento foi comprado por um prestador, não é possível excluir.";
            else
            {
                _msgRetorno = "Orçamento excluído com sucesso";
                _orcamentoApp.Remove(orcamento);
            }

            return RedirectToAction("ListarTodos");
        }

        //
        // POST: /Orcamento/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var adm_grupo = _orcamentoApp.GetById(id);
            _orcamentoApp.Remove(adm_grupo);

            return RedirectToAction("Index");
        }

        public ActionResult ErroAoCadastrar()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Prestador")]
        public ActionResult BuscaTrabalhosIndex()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Prestador")]
        public ActionResult BuscaTrabalhos(string usuarioId)
        {
            try
            {
                _userId = usuarioId;
                var prestador = _prestadorApp.GetPorGuid(Guid.Parse(usuarioId));
                ViewBag.Nome = prestador.pres_Nome;
                ViewBag.CaminhoFoto = prestador.caminho_foto;
                ViewBag.UsuarioId = prestador.pres_Id;

                //ViewBag.Cidades = new SelectList(_cidadeApp.GetAll(), "Id", "NomeCidade");
                ViewBag.ListaCat = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
                var orcamentoVm = Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>
                    (_orcamentoApp.RetornarOrcamentosComDistanciaCalculada(prestador.pres_latitude,
                        prestador.pres_longitude, prestador.pres_Raio_Recebimento, prestador.pres_Id));

                //var orcamentosAbertos = orcamentoVm.Where(s => s.Status == EnumClass.EnumStatusOrcamento.Aberto);
                string frase;
                var orcamentoViewModels = orcamentoVm as OrcamentoViewModel[] ?? orcamentoVm.ToArray();
                if (orcamentoViewModels.Count() == 1)
                    frase = "Foi encontrado " + orcamentoViewModels.Count().ToString() + " orçamento.";
                else
                    frase = "Foram encontrados " + orcamentoViewModels.Count().ToString() + " orçamentos";
                ViewBag.FraseQtd = frase;

                return View(orcamentoVm);
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "BuscaTraballhos";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");


            }

        }

        [Authorize(Roles = "Admin, Prestador")]
        public PartialViewResult BuscaTrabalhosPagosPartial(string servico, string cidade, string estado)
        {
            try
            {

                var cidades = _cidadeApp.GetById(int.Parse(cidade));
                var estados = (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), estado);
                var retorno = _orcamentoApp.RetornaOrcamentosPagos(Convert.ToInt32(servico), cidades.NomeCidade, estados,
                    _userId);

                var frase = "";
                if (retorno.Count() == 1)
                    frase = "Foi encontrado " + retorno.Count().ToString() + " orçamento para " + cidades.NomeCidade +
                            "-" + estados;
                else
                    frase = "Foram encontrados " + retorno.Count().ToString() + " orçamentos para " + cidades.NomeCidade +
                            "-" + estados;
                ViewBag.FraseQtd = frase;

                return PartialView(Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(retorno));
            }
            catch (Exception)
            {
                return PartialView();
            }
        }

        [Authorize(Roles = "Admin, Prestador")]
        public PartialViewResult BuscaTrabalhosPartial(string servico, string cidade, string estado)
        {
            try
            {

                var cidades = _cidadeApp.GetById(int.Parse(cidade));
                var estados = (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), estado);
                var retorno = _orcamentoApp.RetornaOrcamentos(Convert.ToInt32(servico), cidades.NomeCidade, estados);

                var frase = "";
                if (retorno.Count() == 1)
                    frase = "Foi encontrado " + retorno.Count().ToString() + " orçamento para " + cidades.NomeCidade +
                            "-" + estados;
                else
                    frase = "Foram encontrados " + retorno.Count().ToString() + " orçamentos para " + cidades.NomeCidade +
                            "-" + estados;
                ViewBag.FraseQtd = frase;

                return PartialView(Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(retorno));
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "BuscaTraballhos";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return PartialView();
            }
        }

        [Authorize(Roles = "Admin, Prestador")]
        public ActionResult Pagamento(string token, string amt, string cc, string item_name, string st, string tx)
        {
            try
            {
                if (st.Equals("Completed"))
                {
                    var separarId = item_name.Split('-');

                    var id = separarId[1];

                    var orcamento = _orcamentoApp.GetById(int.Parse(id));
                    var prestador = _prestadorApp.GetPorGuid(Guid.Parse(_userId));

                    orcamento.PrestadorFk = new List<Prestador>();
                    orcamento.PrestadorFk.Add(prestador);

                    prestador.OrcamentoFk = new List<Orcamento>();
                    prestador.OrcamentoFk.Add(orcamento);

                    _prestadorApp.Update(prestador);
                    _orcamentoApp.Update(orcamento);

                    return RedirectToAction("Detalhes", new { id = id, usuarioId = _userId });
                }
                return View();
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "Pagamento";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        [Authorize(Roles = "Admin, Prestador")]
        public ActionResult BuscaTrabalhosPagos(string usuarioId)
        {
            try
            {
                _userId = usuarioId;
                var prestador = _prestadorApp.GetPorGuid(Guid.Parse(usuarioId));
                ViewBag.Nome = prestador.pres_Nome;
                ViewBag.CaminhoFoto = prestador.caminho_foto;
                ViewBag.UsuarioId = prestador.pres_Id;


                //ViewBag.Cidades = new SelectList(_cidadeApp.GetAll(), "Id", "NomeCidade");
                ViewBag.ListaCat = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
                var orcamentoVm =
                    Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(
                        _orcamentoApp.GetOrcamentoPagosPeloPrestador(usuarioId));

                //var orcamentosAbertos = orcamentoVm.Where(s => s.Status == EnumClass.EnumStatusOrcamento.Aberto);
                var frase = "";
                if (orcamentoVm.Count() == 1)
                    frase = "Você possui " + orcamentoVm.Count().ToString() + " orçamento.";
                else
                    frase = "Você possui " + orcamentoVm.Count().ToString() + " orçamentos";
                ViewBag.FraseQtd = frase;

                return View(orcamentoVm);
            }
            catch (Exception e)
            {
                return View();
            }

        }

        [Authorize(Roles = "Admin, Prestador")]
        public byte AtribuirPrestadorOrcamento(string orc, string prestador)
        {
            try
            {
                var orcamento = _orcamentoApp.GetById(Convert.ToInt32(orc));
               var prestadorRecuperado = _prestadorApp.GetPorGuid(Guid.Parse(prestador));
              
                orcamento.PrestadorFk = new List<Prestador>();
                orcamento.PrestadorFk.Add(prestadorRecuperado);

                prestadorRecuperado.OrcamentoFk = new List<Orcamento>();
                prestadorRecuperado.OrcamentoFk.Add(orcamento);

                _prestadorApp.Update(prestadorRecuperado);
               // _orcamentoApp.Update(orcamento);
                return 1;
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Orçamento";
                logVm.View = "AtribuirPrestadorOrcamento";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return 0;
            }

        }
    }

}
