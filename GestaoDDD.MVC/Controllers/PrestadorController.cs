using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Model;
using GestaoDDD.MVC.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private EnviaEmail _enviarEmail;
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
        public ActionResult Detalhes(string id)
        {
            var prestador = _prestadorApp.GetPorGuid(Guid.Parse(id));
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


        private void EnviaEmailConfirmacao (ApplicationUser user)
        {

            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id }, protocol: Request.Url.Scheme);
            _enviarEmail = new EnviaEmail();
            var corpo = "Por favor confirme sua conta clicando neste link:  <a href=" + '\u0022' + callbackUrl +
                        '\u0022' + ">Clique aqui</a>";
            var assunto = "Confirme seu email";

            var send = _enviarEmail.EnviaEmailConfirmacao(user.Email, corpo, assunto);

            if (!send.Key)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = send.Value;
                logVm.Controller = "Prestador";
                logVm.View = "EnviaEmailConfirmacao";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
            }
        }

        [HttpPost]
        public ActionResult Create(PrestadorUsuarioViewModel prestadorUsuario, string cpf, string cnpj)
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
                            prestadorUsuario.pres_cpf_cnpj = cpf.Replace("-", "").Replace("/", "").Replace(".", "");
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
            var prestador = _prestadorApp.GetPorGuid(Guid.Parse(usuarioId));

            var prestadorVm = Mapper.Map<Prestador, PrestadorUsuarioViewModel>(prestador);
            ViewBag.Nome = prestador.pres_Nome;
            ViewBag.CaminhoFoto = prestador.caminho_foto;
            var servicosList = new List<Servico>();
            var categoriaList = new List<Categoria>();
            var servico = new Servico();
            var servicoPrestador = _servicoPrestadorApp.GetServicoPorPrestadorId(prestadorVm.pres_Id);
            foreach (var servicoPres in servicoPrestador)
            {
                servico = _servicoAppService.GetById(servicoPres.serv_Id);
                categoriaList.Add(_categoriaApp.GetById(servico.cat_Id));
                servicosList.Add(servico);
            }
            
            var servicosVm = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(servicosList);
            var categoriasVm = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(categoriaList.Distinct());

            ViewBag.Servicos = servicosVm;
            ViewBag.Categorias = categoriasVm;

            //categoriaList.GroupBy(s => s.cat_Id);

            return View(prestadorVm);
        }
        
        public ActionResult EditarPerfil(string id)
        {
            try
            {
                var prestador = _prestadorApp.GetPorGuid(Guid.Parse(id));
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
                    var prestadorOld = _prestadorApp.GetPorGuid(Guid.Parse(prestadorViewModel.pres_Id));
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


        public ActionResult Editar(string id)
        {
            try
            {
                var prestador = _prestadorApp.GetPorGuid(Guid.Parse(id));
                ViewBag.Nome = prestador.pres_Nome;
                ViewBag.CaminhoFoto = prestador.caminho_foto;
                var prestadorViewModel = Mapper.Map<Prestador, PrestadorEditarViewModel>(prestador);
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
        public ActionResult Editar(PrestadorEditarViewModel prestador)
        {
            if (ModelState.IsValid)
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
                        prestador.caminho_foto = Path.GetFileName(data + "_" + file.FileName);
                    }
                    else
                    {
                        var prestadorOld = _prestadorApp.GetPorGuid(Guid.Parse(prestador.pres_Id));
                        prestador.caminho_foto = prestadorOld.caminho_foto;
                    }
                    //inicia a distribuição das propriedades do prestador
                    var prestadordomain = _prestadorApp.GetPorGuid(Guid.Parse(prestador.pres_Id));
                    prestadordomain.pres_Nome = prestador.pres_nome;
                    prestadordomain.pres_Email = prestador.pres_email;
                    prestadordomain.nome_Empresa = prestador.nome_Empresa;
                    prestadordomain.apresentacao_Empresa = prestador.apresentacao_Empresa;
                    prestadordomain.apresentacao_Pesssoal = prestador.apresentacao_Pesssoal;
                    prestadordomain.pres_Cpf_Cnpj = prestador.pres_cpf_cnpj;
                    prestadordomain.pres_Telefone_Celular = prestador.pres_telefone_celular;
                    prestadordomain.pres_Telefone_Residencial = prestador.pres_telefone_residencial;
                    //atualiza o email do usuario no aspnetuser tambem
                    prestadordomain.Usuario.Email = prestador.pres_email;
                    prestadordomain.Usuario.PasswordHash = prestador.pres_email;
                    //grava os dados
                    _prestadorApp.Update(prestadordomain);
                    //redireciona o cara para continuar o processo de cadastro dos serviços
                    return RedirectToAction("ServicosCategorias", "Servico",
                        new
                        {
                            cpf = prestador.pres_cpf_cnpj,
                            nome = prestador.pres_nome,
                            email = prestador.pres_email,
                            celular = prestador.pres_telefone_celular,
                            editarPerfil = true
                        });
                    //return RedirectToAction("MeuPerfil", new { usuarioId = prestador.pres_Id });
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
            var prestador = _prestadorApp.GetPorGuid(Guid.Parse(id));
            var prestadorVm = Mapper.Map<Prestador, PrestadorUsuarioViewModel>(prestador);

            return View(prestadorVm);
        }

        //
        // POST: /Prestador/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(string id)
        {
            var prestador = _prestadorApp.GetPorGuid(Guid.Parse(id));
            prestador.status = EnumClass.EnumStatus.Inativo;
            _prestadorApp.Update(prestador);
            var usuario = _usuarioApp.ObterPorId(id);
            usuario.LockoutEnabled = false;
            _usuarioApp.Update(usuario);
            _msgRetorno = "Prestador inativo.";
            return RedirectToAction("Index");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }

        public ActionResult ListaPrestador(string id)
        {
            ViewBag.Orcamento = id;
            var prestadorViewModel =
            Mapper.Map<IEnumerable<Prestador>,
            IEnumerable<PrestadorViewModel>>(_prestadorApp.RetornaPrestadoresAtivos());
            //var prestadorViewModel =
            //     Mapper.Map<IEnumerable<Prestador>, IEnumerable<PrestadorViewModel>>(_prestadorApp.GetPrestadores(Convert.ToInt32(id)));
            return PartialView(prestadorViewModel);
        }
    }
}
