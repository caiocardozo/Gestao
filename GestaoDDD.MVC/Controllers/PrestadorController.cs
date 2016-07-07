using AutoMapper;
using GeoCoordinatePortable;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EnumClass = GestaoDDD.Domain.Entities.NoSql;


namespace GestaoDDD.MVC.Controllers
{
    public class PrestadorController : Controller
    {
        private readonly IPrestadorAppService _prestadorApp;
        private readonly IUsuarioAppService _usuarioApp;
        private readonly IOrcamentoAppService _orcamentoApp;
        private readonly IServicoPrestadorAppService _servicoPrestadorApp;
        private readonly ILogAppService _logAppService;
        private readonly IServicoAppService _servicoAppService;
        private readonly ICategoriaAppService _categoriaApp;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private static string _msgRetorno;
        public PrestadorController(IPrestadorAppService prestadorApp, IOrcamentoAppService orcamentoApp,
            IUsuarioAppService usuarioApp, IServicoPrestadorAppService servicoPrestadorApp, ILogAppService logApp, IServicoAppService servicoApp,
            ICategoriaAppService categoriaApp, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _prestadorApp = prestadorApp;
            _orcamentoApp = orcamentoApp;
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioApp = usuarioApp;
            _servicoPrestadorApp = servicoPrestadorApp;
            _logAppService = logApp;
            _servicoAppService = servicoApp;
            _categoriaApp = categoriaApp;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.Retorno = _msgRetorno;
            var prestadorViewModel =
                Mapper.Map<IEnumerable<Prestador>, IEnumerable<PrestadorUsuarioViewModel>>(_prestadorApp.GetAll());
            return View(prestadorViewModel.Where(s => s.status == EnumClass.EnumStatus.Ativo));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Detalhes(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);

        }


        public ActionResult Cadastrar(FormCollection collection)
        {
            return View();
        }

        //
        // POST: /Prestador/Create

        public ActionResult Create(FormCollection collection)
        {
            return View();
        }


        private void EnviaEmailConfirmacao(ApplicationUser user)
        {
            ////envia o email de confirmação para o usuario]
            var logVm = new LogViewModel();
            logVm.Mensagem = "Gerar email de confirmação";
            logVm.Controller = "Prestador";
            logVm.View = "Create";
            var log = Mapper.Map<LogViewModel, Log>(logVm);
            _logAppService.SaveOrUpdate(log);
            //
            var code = _userManager.GenerateEmailConfirmationToken(user.Id);

            var logVm1 = new LogViewModel();
            logVm1.Mensagem = "Gerou email de confirmação e vai acionar o UrlAction";
            logVm1.Controller = "Prestador";
            logVm1.View = "Create";
            var log1 = Mapper.Map<LogViewModel, Log>(logVm1);
            _logAppService.SaveOrUpdate(log1);
            //
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

            var logVm2 = new LogViewModel();
            logVm2.Mensagem = "acionou o UrlAction e vai pro send email";
            logVm2.Controller = "Prestador";
            logVm2.View = "Create";
            var log2 = Mapper.Map<LogViewModel, Log>(logVm2);
            _logAppService.SaveOrUpdate(log2);
            //
            _userManager.SendEmail(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link:  <a href=" + '\u0022' + callbackUrl + '\u0022' + ">Clique aqui</a>");

            var logVm3 = new LogViewModel();
            logVm3.Mensagem = "saiu do send email";
            logVm3.Controller = "Prestador";
            logVm3.View = "Create";
            var log3 = Mapper.Map<LogViewModel, Log>(logVm3);
            _logAppService.SaveOrUpdate(log3);

        }

        [HttpPost]
        public ActionResult Create(PrestadorUsuarioViewModel prestadorUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var retorno = _userManager.FindByEmail(prestadorUsuario.pres_email);
                    if (retorno != null)
                    {
                        ModelState.AddModelError("pres_email", "Email já cadastrado");
                        return View(prestadorUsuario);
                    }
                    else
                    {
                        var prestador = new Prestador();

                        //primeiro efetua o cadastro do usuario
                        var user = new ApplicationUser
                        {
                            UserName = prestadorUsuario.pres_email,
                            Email = prestadorUsuario.pres_email
                        };
                        //adicionar a role para este usuario
                        IdentityUserRole role = new IdentityUserRole();
                        role.RoleId = "2"; //role 2 e role prestador
                        role.UserId = user.Id;
                        user.Roles.Add(role);
                        //cria o usuario
                        var result = _userManager.Create(user, prestadorUsuario.Senha);

                        EnviaEmailConfirmacao(user);

                        
                        if (result.Succeeded)
                        {
                            //pega o usuario cadastrado e adiciona ele no objeto prestador
                            Usuario usuarioCadastrado = new Usuario();
                            usuarioCadastrado = _usuarioApp.ObterPorEmail(prestadorUsuario.pres_email);
                            prestador.Usuario = usuarioCadastrado;
                            prestador.pres_Nome = prestadorUsuario.pres_nome;
                            prestador.pres_Email = prestadorUsuario.pres_email;
                            prestador.pres_Cpf_Cnpj = prestadorUsuario.pres_cpf_cnpj;
                            prestador.pres_Endereco = prestadorUsuario.pres_Endereco;
                            prestador.pres_Telefone_Celular = prestadorUsuario.pres_telefone_celular;
                            prestador.pres_Telefone_Residencial = prestadorUsuario.pres_telefone_residencial;
                            prestador.status = EnumClass.EnumStatus.Ativo;
                            prestador.pres_Raio_Recebimento = prestadorUsuario.pres_Raio_Recebimento;
                            prestador.pres_latitude = prestadorUsuario.pres_Latitude;
                            prestador.pres_longitude = prestadorUsuario.pres_Longitude;


                            var endereco = prestador.pres_Endereco;
                            var partes = endereco.Split(',');
                            foreach (var parte in partes.Where(s => s.Contains("-")))
                            {

                                var separar = parte.Split('-');
                                var ufs = " AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA,PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO";
                                if (ufs.Contains(separar[1]))
                                {
                                    prestador.Estado =
                                        (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), separar[1]);
                                    prestador.Cidade = separar[0];
                                }
                                else
                                    continue;

                            }

                            _prestadorApp.SaveOrUpdate(prestador);
                            //redireciona o cara para continuar o processo de cadastro dos serviços
                            return RedirectToAction("ServicosCategorias", "Servico",
                                new
                                {
                                    cpf = prestador.pres_Cpf_Cnpj,
                                    nome = prestador.pres_Nome,
                                    email = prestador.pres_Email,
                                    celular = prestador.pres_Telefone_Celular,
                                    editarPerfil = false
                                });
                        }
                        else
                        {
                            return View(prestadorUsuario);
                        }

                    }
                }
                else
                {
                    return View(prestadorUsuario);
                }

            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Prestador";
                logVm.View = "Create";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult PrestadorCadastroSucesso()
        {
            return View();
        }

        public ActionResult MeuPerfil(string usuarioId)
        {
            var prestador = _prestadorApp.GetPorGuid(usuarioId);

            var prestadorVm = Mapper.Map<Prestador, PrestadorUsuarioViewModel>(prestador);
            ViewBag.Nome = prestador.pres_Nome;
            ViewBag.CaminhoFoto = prestador.caminho_foto;
            var servicosList = new List<Servico>();
            var categoriaList = new List<Categoria>();

            var servicoPrestador = _servicoPrestadorApp.GetServicoPorPrestadorId(prestadorVm.pres_Id);
            foreach (var servicoPres in servicoPrestador)
            {
                var servico = _servicoAppService.GetById(servicoPres.serv_Id);
                var categoria = _categoriaApp.GetById(servico.cat_Id);
                categoriaList.Add(categoria);
                servicosList.Add(servico);
            }

            var servicosVm = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(servicosList);
            var categoriasVm = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(categoriaList.Distinct());

            ViewBag.Servicos = servicosVm;
            ViewBag.Categorias = categoriasVm;

            //categoriaList.GroupBy(s => s.cat_Id);

            return View(prestadorVm);
        }
        
        public ActionResult Editar(int id)
        {
            try
            {
                var prestador = _prestadorApp.GetById(id);
                ViewBag.Nome = prestador.pres_Nome;
                ViewBag.CaminhoFoto = prestador.caminho_foto;
                var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
                return View(prestadorViewModel);
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Prestador";
                logVm.View = "Get Editar Perfil";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }


        public ActionResult EditarPerfil(string usuarioId)
        {
            try
            {
                var prestador = _prestadorApp.GetPorGuid(usuarioId);
                ViewBag.Nome = prestador.pres_Nome;
                ViewBag.CaminhoFoto = prestador.caminho_foto;
                var prestadorViewModel = Mapper.Map<Prestador, PrestadorUsuarioViewModel>(prestador);
                return View(prestadorViewModel);
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Prestador";
                logVm.View = "Get Editar Perfil";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditarPerfil(PrestadorUsuarioViewModel prestadorViewModel)
        {
            try
            {
                DateTime weekDay = DateTime.Now;
                string data = weekDay.ToString("dd-MM-yyyy-HH-mm-ss");

                var file = this.Request.Files[0];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    string savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/ImagemPerfil");
                    savedFileName = Path.Combine(savedFileName, Path.GetFileName(data + "_" + file.FileName));
                    file.SaveAs(savedFileName);
                    prestadorViewModel.caminho_foto = Path.GetFileName(data + "_" + file.FileName);
                }
                else
                {
                    var prestadorOld = _prestadorApp.GetPorGuid(prestadorViewModel.pres_Id);
                    prestadorViewModel.caminho_foto = prestadorOld.caminho_foto;
                }
                ModelState["Senha"].Errors.Clear();
                ModelState["ConfirmaSenha"].Errors.Clear();

                if (ModelState.IsValid)
                {

                    var prestador = Mapper.Map<PrestadorUsuarioViewModel, Prestador>(prestadorViewModel);



                    var endereco = prestador.pres_Endereco;
                    var partes = endereco.Split(',');
                    foreach (var parte in partes.Where(s => s.Contains("-")))
                    {

                        var separar = parte.Split('-');
                        var ufs = " AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA,PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO";
                        if (ufs.Contains(separar[1]))
                        {
                            prestador.Estado =
                                (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), separar[1]);
                            prestador.Cidade = separar[0];
                        }
                        else
                            continue;

                    }

                    _prestadorApp.Update(prestador);
                    //redireciona o cara para continuar o processo de cadastro dos serviços
                    return RedirectToAction("ServicosCategorias", "Servico",
                        new
                        {
                            cpf = prestador.pres_Cpf_Cnpj,
                            nome = prestador.pres_Nome,
                            email = prestador.pres_Email,
                            celular = prestador.pres_Telefone_Celular,
                            editarPerfil = true
                        });

                    //return RedirectToAction("MeuPerfil", new { usuarioId = prestador.pres_Id });
                }
                else
                {
                    return View(prestadorViewModel);
                }
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Prestador";
                logVm.View = "Post Editar Perfil";

                var log = Mapper.Map<LogViewModel, Log>(logVm);

                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        //
        // POST: /\Prestador/Edit/5

        [HttpPost]
        public ActionResult Editar(PrestadorViewModel prestador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var prestadordomain = Mapper.Map<PrestadorViewModel, Prestador>(prestador);
                    _prestadorApp.Update(prestadordomain);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    var logVm = new LogViewModel();
                    logVm.Mensagem = e.Message;
                    logVm.Controller = "Prestador";
                    logVm.View = "Post Editar Perfil";

                    var log = Mapper.Map<LogViewModel, Log>(logVm);

                    _logAppService.SaveOrUpdate(log);
                    return RedirectToAction("ErroAoCadastrar");
                }
            }
            else
            {
                return View(prestador);
            }
        }


        public ActionResult Deletar(string id)
        {
            var prestador = _prestadorApp.GetPorGuid(id);
            var prestadorVm = Mapper.Map<Prestador, PrestadorUsuarioViewModel>(prestador);

            return View(prestadorVm);
        }

        //
        // POST: /Prestador/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(string id)
        {
            var prestador = _prestadorApp.GetPorGuid(id);
            prestador.status = EnumClass.EnumStatus.Inativo;
            _prestadorApp.Update(prestador);

            _msgRetorno = "Prestador excluído com sucesso.";
            return RedirectToAction("Index");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}
