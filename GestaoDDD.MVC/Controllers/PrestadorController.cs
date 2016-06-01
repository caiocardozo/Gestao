using AutoMapper;
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


namespace GestaoDDD.MVC.Controllers
{
    public class PrestadorController : Controller
    {
        private readonly IPrestadorAppService _prestadorApp;
        private readonly IUsuarioAppService _usuarioApp;
        private readonly IOrcamentoAppService _orcamentoApp;
        private readonly IServicoPrestadorAppService _servicoPrestadorApp;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public PrestadorController(IPrestadorAppService prestadorApp, IOrcamentoAppService orcamentoApp,
            IUsuarioAppService usuarioApp, IServicoPrestadorAppService servicoPrestadorApp, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _prestadorApp = prestadorApp;
            _orcamentoApp = orcamentoApp;
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioApp = usuarioApp;
            _servicoPrestadorApp = servicoPrestadorApp;
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

        [HttpPost]
        public async Task<ActionResult> Create(PrestadorUsuarioViewModel prestadorUsuario)
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
                    Prestador prestador = new Prestador();

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
                        //envia o email de confirmação para o usuario
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
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
                        foreach (var erro in result.Errors)
                        {
                            var erros = "";
                            erros += erro;
                        }
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

        // POST: /Prestador/Cadastrar
        [HttpPost]
        public ActionResult Cadastrar(Prestador prestador)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
                    return View(prestador);
                }

            }
            catch (Exception)
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult MeuPerfil(string usuarioId)
        {
            var prestador = _prestadorApp.GetPorGuid(usuarioId);

            var prestadorVm = Mapper.Map<Prestador, PrestadorUsuarioViewModel>(prestador);
            ViewBag.Nome = prestador.pres_Nome;
            var servicoPrestador = _servicoPrestadorApp.GetServicoPorPrestadorId(prestadorVm.pres_Id.ToString());
            //var servicoPrestadorVm = Mapper.Map<IEnumerable<ServicoPrestador>, IEnumerable<ServicoPrestadorViewModel>>(servicoPrestador);
            return View(prestadorVm);
        }

        public ActionResult Editar(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);
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
                catch (Exception)
                {
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
