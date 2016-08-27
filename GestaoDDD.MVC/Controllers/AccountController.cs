using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using AutoMapper;
using GestaoDDD.MVC.Util;

namespace GestaoDDD.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ILogAppService _logAppService;
        private readonly IPrestadorAppService _prestadorAppService;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ILogAppService logAppService, IPrestadorAppService prestadorAppService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logAppService = logAppService;
            _prestadorAppService = prestadorAppService;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
                var prestador = _prestadorAppService.GetPorEmail(model.Email);
                var userId = prestador.pres_Id;
                var rolesPrestador = _userManager.GetRoles(userId);
                var role = "";

                role = rolesPrestador != null ? rolesPrestador[0] : null;



                switch (result)
                {
                    case SignInStatus.Success:
                        //joga o usuario para a tela inicial 
                        //@Html.ActionLink("[ Disponíveis ]", "BuscaTrabalhos", "Orcamento", new { usuarioId = @Model.UsuarioId }, null)
                        if (role == "Prestador")
                            return RedirectToAction("BuscaTrabalhos", "Orcamento", new { usuarioId = userId });
                        else
                            return RedirectToAction("Index", "Prestador", new { usuarioId = userId });
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Login ou Senha incorretos.");
                        return View(model);
                }
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel {Mensagem = e.Message, Controller = "Account", View = "Login"};
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }
        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            try
            {
                // Require that the user has already logged in via username/password or external login
                if (!await _signInManager.HasBeenVerifiedAsync())
                {
                    return View("Error");
                }
                var user = await _userManager.FindByIdAsync(await _signInManager.GetVerifiedUserIdAsync());
                if (user != null)
                {
                    ViewBag.Status = "DEMO: Caso o código não chegue via " + provider + " o código é: ";
                    ViewBag.CodigoAcesso = await _userManager.GenerateTwoFactorTokenAsync(user.Id, provider);
                }
                return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
            }
            catch (Exception e)
            {

                var logVm = new LogViewModel {Mensagem = e.Message, Controller = "Account", View = "VerifyCode"};
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToLocal(model.ReturnUrl);
                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Código Inválido.");
                            return View(model);
                    }
                }
                else
                    return View(model);

            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "VerifyCode";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
                        ViewBag.Link = callbackUrl;
                        return View("DisplayEmail");
                    }
                    AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "Register";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string userId)
        {
            try
            {
                bool emailConfirmed = false;
                if (userId == null)
                {
                    return View("Error");
                }

                var user = _userManager.FindById(userId);
                if (user != null)
                {
                    user.EmailConfirmed = true;
                    _userManager.Update(user);
                    emailConfirmed = true;
                }

                
                //var result = await _userManager.ConfirmEmailAsync(userId, code);
                return View(emailConfirmed ? "EmailConfirmadoOk" : "Error");
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "Register";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult EmailConfirmadoOk()
        {
            return View();
        }


        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    try
        //    {
        //        if (userId == null || code == null)
        //        {
        //            return View("Error");
        //        }
        //        var result = await _userManager.ConfirmEmailAsync(userId, code);
        //        return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //    }
        //    catch (Exception e)
        //    {
        //        var logVm = new LogViewModel();
        //        logVm.Mensagem = e.Message;
        //        logVm.Controller = "Account";
        //        logVm.View = "Register";
        //        var log = Mapper.Map<LogViewModel, Log>(logVm);
        //        _logAppService.SaveOrUpdate(log);
        //        return RedirectToAction("ErroAoCadastrar");
        //    }

        //}

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  var user = _userManager.FindByName(model.Email);
                    if (user == null)
                    {
                        // Não revelar se o usuario nao existe ou nao esta confirmado
                        return View("ForgotPasswordConfirmation");
                    }

                    var code = _userManager.GeneratePasswordResetToken(user.Id);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await _userManager.SendEmailAsync(user.Id, "Esqueci minha senha", "Por favor altere sua senha clicando aqui: <a href='" + callbackUrl + "'></a>");
                    //ViewBag.Link = callbackUrl;
                    //ViewBag.Status = "DEMO: Caso o link não chegue: ";
                    //ViewBag.LinkAcesso = callbackUrl;
                    

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


                    var corpoNotificacao = "Olá, " + saudacao + "!" + " <br /><br /> Troque agora sua senha." +
                                "<br /> <a href=" + '\u0022' + callbackUrl + '\u0022' + "><strong>Clique aqui</strong></a> para alterar sua senha. " +
                                "<br /><br /> Att, <br />" +
                                " Equipe Agiliza.";

                    var assuntoNotificacao = "Solicitação de nova senha";
                   var _enviaEmail = new EnviaEmail();
                    var enviouNotificacao = _enviaEmail.EnviaEmailConfirmacao(model.Email, corpoNotificacao, assuntoNotificacao);
                    if (!enviouNotificacao.Key)
                    {
                        var logVm = new LogViewModel();
                        logVm.Mensagem = enviouNotificacao.Value;
                        logVm.Controller = "Enviar Email Nova Senha";
                        logVm.View = "Enviar email notificação de nova senha.";
                        var log = Mapper.Map<LogViewModel, Log>(logVm);
                        _logAppService.SaveOrUpdate(log);
                    }
                    return View("ForgotPasswordConfirmation");
                }

                // No caso de falha, reexibir a view. 
                return View(model);
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "ForgotPassword";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Não revelar se o usuario nao existe ou nao esta confirmado
                    return RedirectToAction("Login", "Account");
                }
                var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
                return View();
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "ResetPassword";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            try
            {
                var userId = await _signInManager.GetVerifiedUserIdAsync();
                if (userId == null)
                {
                    return View("Error");
                }
                var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
                var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
                // return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });

                return View("Error");

            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "SendCode";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            try
            {
                var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (loginInfo == null)
                {
                    return RedirectToAction("Login");
                }

                // Sign in the user with this external login provider if the user already has a login
                var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                    case SignInStatus.Failure:
                    default:
                        // Se ele nao tem uma conta solicite que crie uma
                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
                }
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "ExternalLoginCallback";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Manage");
                }

                if (ModelState.IsValid)
                {
                    // Pegar a informação do login externo.
                    var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                    if (info == null)
                    {
                        return View("ExternalLoginFailure");
                    }
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddLoginAsync(user.Id, info.Login);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    AddErrors(result);
                }

                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
            catch (Exception e)
            {
                var logVm = new LogViewModel();
                logVm.Mensagem = e.Message;
                logVm.Controller = "Account";
                logVm.View = "ExternalLoginConfirmation";
                var log = Mapper.Map<LogViewModel, Log>(logVm);
                _logAppService.SaveOrUpdate(log);
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "Prestador");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }



        #endregion
    }
}
