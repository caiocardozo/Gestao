using GestaoDDD.Infra.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;

namespace GestaoDDD.Infra.Identity.Configuration
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {

            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configuração de Lockout
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 3;

            // Providers de Two Factor Autentication
            RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Definindo a classe de serviço de e-mail
            EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            SmsService = new SmsService();


            //var dataProtectionProvider = Startup.DataProtectionProvider;
            //UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));

            //var provider = new DpapiDataProtectionProvider("ASP.NET Identity");
            //var dataProtector = provider.Create("ASP.NET Identity");

            //UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector)
            //{
            //    TokenLifespan = TimeSpan.FromMinutes(5)
            //};

            var provider = new DpapiDataProtectionProvider("Identity");
            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"))
            {
                TokenLifespan = TimeSpan.FromHours(24),
            };

        }
    }
}
