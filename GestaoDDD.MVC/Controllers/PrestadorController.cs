using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using GeoCoordinatePortable;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EnumStatus = GestaoDDD.Domain.Entities.EnumStatus;
using System.Web;


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
            var prestadorViewModel =
                Mapper.Map<IEnumerable<Prestador>, IEnumerable<PrestadorViewModel>>(_prestadorApp.GetAll());
            return View(prestadorViewModel);
        }


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


        private async Task EnviaEmailConfirmacao(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
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

                        ////envia o email de confirmação para o usuario
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        //await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");

                        var code = _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                         _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
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
                            prestador.status = EnumStatus.Orcamento_bloqueado;
                            prestador.pres_Raio_Recebimento = prestadorUsuario.pres_Raio_Recebimento;
                            prestador.pres_latitude = prestadorUsuario.pres_Latitude;
                            prestador.pres_longitude = prestadorUsuario.pres_Longitude;

                            _prestadorApp.SaveOrUpdate(prestador);
                            //redireciona o cara para continuar o processo de cadastro dos serviços
                            return RedirectToAction("ServicosCategorias", "Servico",
                                new
                                {
                                    cpf = prestador.pres_Cpf_Cnpj,
                                    nome = prestador.pres_Nome,
                                    email = prestador.pres_Email,
                                    celular = prestador.pres_Telefone_Celular
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

        public ActionResult ExibirOrcamentos()
        {
            //Essa view ta aqui so por questao de teste... Ela nao existe no sistema, era so pra chamar ela e debugar esse processo aqui.
            var list = Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(_orcamentoApp.GetAll());
            var lstCoord = new List<double>();

            foreach (var l in list)
            {

                //Coordenada de cada orçamento
                var coordenada_orcamento = new GeoCoordinate();
                coordenada_orcamento.Latitude = double.Parse(l.orc_latitude.Replace(",", "."), CultureInfo.InvariantCulture);
                coordenada_orcamento.Longitude = double.Parse(l.orc_longitude.Replace(",", "."), CultureInfo.InvariantCulture);

                //Coordenada fixa de cada prestador... Deve-se passar o id do prestador logado.
                string lat = "-16.2744242";
                string longt = "-48.95429619999999";

                var coordenada_prestador = new GeoCoordinate();
                coordenada_prestador.Latitude = double.Parse(lat.Replace(",", "."), CultureInfo.InvariantCulture);
                coordenada_prestador.Longitude = double.Parse(longt.Replace(",", "."), CultureInfo.InvariantCulture);
                // Coordenada fixa do parque dos pirineus.


                var distancia = coordenada_prestador.GetDistanceTo(coordenada_orcamento);
            }

            ViewBag.Coordenadas = lstCoord;

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
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);
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
                string savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/ImagemPerfil");
                savedFileName = Path.Combine(savedFileName, Path.GetFileName(data + "_" + file.FileName));
                file.SaveAs(savedFileName);
                prestadorViewModel.caminho_foto = Path.GetFileName(data + "_" + file.FileName);


                ModelState["Senha"].Errors.Clear();
                ModelState["ConfirmaSenha"].Errors.Clear();
                
                if (ModelState.IsValid)
                {
                    var prestador = Mapper.Map<PrestadorUsuarioViewModel, Prestador>(prestadorViewModel);

                    var endereco = prestador.pres_Endereco;
                    var x = endereco.Split(',');
                    var y = x[1].Split('-');
                    prestador.Cidade = y[0];
                    
                    prestador.Estado = (String)EnumEstados.Parse(typeof()) y[1];

                    _prestadorApp.Update(prestador);
                    RedirectToAction("MeuPerfil");
                }
                else
                {
                    return View (prestadorViewModel);
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
            

            return View();
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
                    //inserir pagina de erro
                    return RedirectToAction("ErroAoCadastrar");
                }
            }
            else
            {
                return View(prestador);
            }
        }


        public ActionResult Deletar(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);

        }

        //
        // POST: /Prestador/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var adm_grupo = _prestadorApp.GetById(id);
            _prestadorApp.Remove(adm_grupo);

            return RedirectToAction("Index");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}
