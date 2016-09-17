using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Infra.Identity.Model;
using GestaoDDD.Infra.Identity.Configuration;
using Microsoft.AspNet.Identity;
using GestaoDDD.MVC.Util;
using System.Text;

namespace GestaoDDD.MVC.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoAppService _iServicoApp;
        private readonly ICategoriaAppService _iCategoriaApp;
        private readonly IPrestadorAppService _iPrestadorApp;
        private readonly IServicoPrestadorAppService _iServicoPrestadorApp;
        private readonly ILogAppService _logAppService;
        private readonly IServicoPrestadorAppService _servicoPrestadorAppService;
        private static string msgRetorno = "";
        private ApplicationUserManager _userManager;
        private readonly Utils _utils;


        public ServicoController(IServicoAppService iServicoApp, ICategoriaAppService iCategoriaApp,
            IPrestadorAppService iPrestadorApp, IServicoPrestadorAppService iServicoPrestadorApp, ILogAppService logAppService, IServicoPrestadorAppService servicoPrestadorAppService)
        {
            _iServicoApp = iServicoApp;
            _iCategoriaApp = iCategoriaApp;
            _iPrestadorApp = iPrestadorApp;
            _iServicoPrestadorApp = iServicoPrestadorApp;
            _logAppService = logAppService;
            _servicoPrestadorAppService = servicoPrestadorAppService;
            _utils = new Utils();
        }

        //
        // GET: /Servico/
        public ActionResult ListarTodos()
        {
            var servicos = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());
            foreach (var servico in servicos)
            {
                var categoria = _iCategoriaApp.GetById(servico.cat_Id);
                servico.Categoria = categoria;
            }
            ViewBag.Retorno = msgRetorno;
            return View(servicos.OrderBy(s => s.serv_Nome));
        }


        public ActionResult ServicosCategorias(string cpf, string nome, string celular, string email)
        {
            ViewBag.CategoriaModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_iCategoriaApp.GetAll());
            ViewBag.Cpf = cpf;
            ViewBag.Nome = nome;
            ViewBag.Celular = celular;
            ViewBag.Email = email;

            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());
            return View(servicoViewModel);
        }


        [HttpPost]
        public ActionResult ServicosCategorias(FormCollection collection, string cpf,
            string nome, string celular, string email, bool editarPerfil)
        {
            try
            {
                var sbEmail = new StringBuilder();
                var servicos = new List<Servico>();
                foreach (var col in collection)
                {
                    int servId;
                    Int32.TryParse(col.ToString(), out servId);
                    var servico = _iServicoApp.GetById(servId);

                    sbEmail.Append(servico.serv_Nome);
                    sbEmail.Append(", ");

                    servicos.Add(servico);
                }
                //inserir por email, assim nao tem como duplicar
                var prestador = _iPrestadorApp.GetPorEmail(email);
                _iServicoPrestadorApp.SalvarServicosPrestador(servicos, prestador);
                if (editarPerfil)
                {
                    return RedirectToAction("MeuPerfil", "Prestador", new { usuarioId = prestador.pres_Id });
                }
                else
                {
                    //Enviar email para admins de novo usuario
                    var admins = _iPrestadorApp.GetAdministradores();

                    foreach (var admin in admins)
                    {
                        var corpoNovoUsuario = "Olá, " + _utils.PrimeiraLetraMaiuscula(admin.pres_Nome.Trim()) + ", " + _utils.DefineSaudacao() + "!" + " <br /><br /> Chegou mais um prestador para Agiliza." +
                        " <br /><strong>Nome:</strong>  " + prestador.pres_Nome +
                        " <br /><strong>Email:</strong>  " + prestador.pres_Email +
                        " <br /><strong>Telefone:</strong>  " + prestador.pres_Telefone_Residencial +
                        " <br /><strong>Celular:</strong>  " + prestador.pres_Telefone_Celular +
                        " <br /><strong>Endereço:</strong>  " + prestador.pres_Endereco +
                        " <br /><strong>Serviços:</strong><br />  " + sbEmail.ToString().Substring(0, sbEmail.ToString().Length - 2) + "." +

                        "<br /><br /> <a href=" + '\u0022' + "www.agilizaorcamentos.com.br/Prestador/Index" + '\u0022' + "><strong>Clique aqui</strong></a> para visualizar os prestadores cadastrados. " +
                        "<br /><br /> Att, <br />" +
                        " Equipe Agiliza.";

                        var assuntoNotificacao = "Novo orçamento Cadastrado";
                        var _enviaEmail = new EnviaEmail();
                        var enviouNotificacao = _enviaEmail.EnviaEmailConfirmacao(admin.pres_Email, corpoNovoUsuario, assuntoNotificacao);
                        if (!enviouNotificacao.Key)
                        {
                            var logVm = new LogViewModel();
                            logVm.Mensagem = enviouNotificacao.Value;
                            logVm.Controller = "Enviar Email Notificação para admin de novo prestador";
                            logVm.View = "Enviar email notificação para admin de novo prestador.";
                            var log = Mapper.Map<LogViewModel, Log>(logVm);
                            _logAppService.SaveOrUpdate(log);
                        }
                    }


                    return RedirectToAction("PrestadorCadastroSucesso", "Prestador");
                }
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult Index(string pesquisa_servico, string pesquisa_categoria)
        {
            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());

            if ((!string.IsNullOrEmpty(pesquisa_servico) && (!string.IsNullOrEmpty(pesquisa_categoria))))
            {
                servicoViewModel = servicoViewModel.Where(s => s.serv_Nome.ToUpper().Contains(pesquisa_servico.ToUpper()) && s.Categoria.cat_Nome.ToUpper().Contains(pesquisa_categoria.ToUpper()));
            }
            else if ((string.IsNullOrEmpty(pesquisa_servico) && (!string.IsNullOrEmpty(pesquisa_categoria))))
            {
                servicoViewModel = servicoViewModel.Where(s => s.Categoria.cat_Nome.ToUpper().Contains(pesquisa_categoria.ToUpper()));
            }
            else if ((!string.IsNullOrEmpty(pesquisa_servico) && (string.IsNullOrEmpty(pesquisa_categoria))))
            {
                servicoViewModel = servicoViewModel.Where(s => s.serv_Nome.ToUpper().Contains(pesquisa_servico.ToUpper()));
            }


            ViewBag.CategoriaModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_iCategoriaApp.GetAll());

            return View(servicoViewModel);
        }

        //
        public ActionResult Detalhes(int id)
        {
            var servico = _iServicoApp.GetById(id);
            var servicoViewModel = Mapper.Map<Servico, ServicoViewModel>(servico);
            return View(servicoViewModel);
        }

        //
        // GET: /Servico/Create
        public ActionResult Cadastrar(FormCollection collection)
        {
            ViewBag.cat_Id = new SelectList(_iCategoriaApp.GetAll(), "cat_Id", "cat_Nome");
            return View();
        }

        //
        // POST: /Servico/Create
        [HttpPost]
        public ActionResult Cadastrar(ServicoViewModel servico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var servicoDomain = Mapper.Map<ServicoViewModel, Servico>(servico);
                    var categoria = _iCategoriaApp.GetById(servico.cat_Id);
                    servicoDomain.Categoria = categoria;
                    _iServicoApp.SaveOrUpdate(servicoDomain);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.cat_Id = new SelectList(_iCategoriaApp.GetAll(), "cat_Id", "cat_Nome");
                    return View(servico);
                }
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        //
        // GET: /Categoria/Edit/5

        public ActionResult Editar(int id)
        {
            var servico = _iServicoApp.GetById(id);
            var servicoViewModel = Mapper.Map<Servico, ServicoViewModel>(servico);
            ViewBag.cat_Id = new SelectList(_iCategoriaApp.GetAll(), "cat_Id", "cat_Nome", servico.cat_Id);
            return View(servicoViewModel);
        }

        //
        // POST: /Categoria/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ServicoViewModel servico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var servicoViewModel = Mapper.Map<ServicoViewModel, Servico>(servico);
                    _iServicoApp.Update(servicoViewModel);
                    return RedirectToAction("ListarTodos");
                }
                catch (Exception)
                {
                    //inserir pagina de erro
                    return RedirectToAction("ErroAoCadastrar");
                }
            }
            else
            {
                return View(servico);
            }
        }


        public ActionResult Deletar(int id)
        {
            try
            {
                var servico = _iServicoApp.GetById(id);
                var servicoVm = Mapper.Map<Servico, ServicoViewModel>(servico);
                return View(servicoVm);

                //var servico = _iServicoApp.GetById(id);
                //var servicoPrestador = _servicoPrestadorAppService.GetById(id);
                //if (servicoPrestador != null)
                //    msgRetorno =
                //        "Este serviço não pode ser excluido pois está vinculado aos serviços oferecidos por algum prestador";
                //else
                //{
                //    msgRetorno = "Serviço deletado com sucesso";
                //    _iServicoApp.Remove(servico);    
                //}
                //return RedirectToAction("ListarTodos");

            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Servico";
                logVm.View = "Deletar";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

            //var servicoViewModel = Mapper.Map<Servico, ServicoViewModel>(servico);
            //return View(servicoViewModel);
        }

        //
        // POST: /Categoria/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var servico = _iServicoApp.GetById(id);
            var servicoPrestador = _servicoPrestadorAppService.GetById(id);
            if (servicoPrestador != null)
                msgRetorno =
                    "Este serviço não pode ser excluido pois está vinculado aos serviços oferecidos por algum prestador";
            else
            {
                msgRetorno = "Serviço deletado com sucesso";
                _iServicoApp.Remove(servico);
            }
            return RedirectToAction("ListarTodos");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }

        //retorna os serviços pela categoria
        public JsonResult RServicosPCategoria(string id)
        {
            List<ServicoIdNome> retorno = _iServicoApp.RetornaServicoPelaCategoria(Convert.ToInt32(id));
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ServicosCadastrados()
        {
            return View();
        }
    }
}
