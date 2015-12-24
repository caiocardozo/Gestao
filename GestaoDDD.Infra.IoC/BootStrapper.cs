using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Repositories;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Context;
using GestaoDDD.Infra.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;


namespace GestaoDDD.Infra.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.RegisterPerWebRequest<ApplicationDbContext>();
            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>());
            container.RegisterPerWebRequest<ApplicationRoleManager>();
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();
            
            container.RegisterPerWebRequest<IUsuarioRepository, UsuarioRepository>();
        } 
    }
}
