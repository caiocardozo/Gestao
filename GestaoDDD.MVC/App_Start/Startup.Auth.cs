using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Web.Mvc;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using System.Threading.Tasks;

namespace GestaoDDD.MVC
{
	public partial class Startup
	{
        private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationSignInManager>());
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationRoleManager>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(0),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers

            app.UseMicrosoftAccountAuthentication(
                clientId: "MYCLIENTID",
                clientSecret: "MYCLIENTID");

            app.UseTwitterAuthentication(
               consumerKey: "MYCLIENTID",
               consumerSecret: "MYCLIENTID");

            app.UseFacebookAuthentication(
               appId: "MYCLIENTID",
               appSecret: "MYCLIENTID");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "103842386017689616225",
                ClientSecret = "fc88974c3ea2abc0df457963b7240f4e844e46bd"
            });

            var fao = new FacebookAuthenticationOptions
            {
                AppId = "1213099755371227",
                AppSecret = "67c91421c354cdd6272f3c96765d8d53"
            };

            fao.Scope.Add("email");
            fao.Scope.Add("publish_actions");
            fao.Scope.Add("basic_info");
            fao.Scope.Add("public_profile");

            fao.Provider = new FacebookAuthenticationProvider()
            {

                OnAuthenticated = (context) =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                    foreach (var x in context.User)
                    {
                        var claimType = string.Format("urn:facebook:{0}", x.Key);
                        string claimValue = x.Value.ToString();
                        if (!context.Identity.HasClaim(claimType, claimValue))
                            context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));

                    }
                    return Task.FromResult(0);
                }
            };

            fao.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseFacebookAuthentication(fao); 
        }


	}
}