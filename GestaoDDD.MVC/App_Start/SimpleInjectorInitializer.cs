using GestaoDDD.Application.Interface;
using GestaoDDD.Application.Services;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;
using GestaoDDD.Domain.Services;
using GestaoDDD.Infra.Data.Contexto;
using GestaoDDD.Infra.Data.Repositories;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Context;
using GestaoDDD.Infra.Identity.Model;
using GestaoDDD.Infra.IoC;
using GestaoDDD.MVC.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebActivatorEx;


[assembly: PostApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]

namespace GestaoDDD.MVC.App_Start
{
    public class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Chamada dos módulos do Simple Injector
           /// InitializeContainer(container);
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register(() => new GestaoContext(), Lifestyle.Scoped);

            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>());
            container.RegisterPerWebRequest<ApplicationRoleManager>();
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();

            #region Categoria
            container.Register<ICategoriaRepository, CategoriaRepository>(Lifestyle.Scoped);
            container.Register<ICategoriaAppService, CategoriaAppService>(Lifestyle.Scoped);
            container.Register<ICategoriaService, CategoriaService>(Lifestyle.Scoped);
            #endregion

            #region Servico
            container.Register<IServicoRepository, ServicoRepository>(Lifestyle.Scoped);
            container.Register<IServicoAppService, ServicoAppService>(Lifestyle.Scoped);
            container.Register<IServicoService, ServicoService>(Lifestyle.Scoped);
            #endregion

            #region Prestador
            container.Register<IPrestadorRepository, PrestadorRepository>(Lifestyle.Scoped);
            container.Register<IPrestadorAppService, PrestadorAppService>(Lifestyle.Scoped);
            container.Register<IPrestadorService, PrestadorService>(Lifestyle.Scoped);
            #endregion

            #region Orcamento
            container.Register<IOrcamentoRepository, OrcamentoRepository>(Lifestyle.Scoped);
            container.Register<IOrcamentoAppService, OrcamentoAppService>(Lifestyle.Scoped);
            container.Register<IOrcamentoService, OrcamentoService>(Lifestyle.Scoped);
            #endregion

            #region Usuario
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Scoped);
            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);
            #endregion

            #region Servico Prestador
            container.Register<IServicoPrestadorRepository, ServicoPrestadorRepository>(Lifestyle.Scoped);
            container.Register<IServicoPrestadorAppService, ServicoPrestadorAppService>(Lifestyle.Scoped);
            container.Register<IServicoPrestadorService, ServicoPrestadorService>(Lifestyle.Scoped);
            #endregion

            #region ComoFunciona
            container.Register<IComoFuncionaRepository, ComoFuncionaRepository>(Lifestyle.Scoped);
            container.Register<IComoFuncionaAppService, ComoFuncionaAppService>(Lifestyle.Scoped);
            container.Register<IComoFuncionaService, ComoFuncionaService>(Lifestyle.Scoped);
            #endregion

            #region IndiqueProfissional
            container.Register<IIndiqueProfissionalRepository, IndiqueProfissionalRepository>(Lifestyle.Scoped);
            container.Register<IIndiqueProfissionalAppService, IndiqueProfissionalAppService>(Lifestyle.Scoped);
            container.Register<IIndiqueProfissionalService, IndiqueProfissionalService>(Lifestyle.Scoped);
            #endregion

            #region Pessoa
            container.Register<IPessoaRepository, PessoaRepository>(Lifestyle.Scoped);
            container.Register<IPessoaAppService, PessoaAppService>(Lifestyle.Scoped);
            container.Register<IPessoaService, PessoaService>(Lifestyle.Scoped);
            #endregion

            #region Contato
            container.Register<IContatoRepository, ContatoRepository>(Lifestyle.Scoped);
            container.Register<IContatoAppService, ContatoAppService>(Lifestyle.Scoped);
            container.Register<IContatoService, ContatoService>(Lifestyle.Scoped);

            #endregion

            #region Cidade
            container.Register<ICidadeRepository, CidadeRepository>(Lifestyle.Scoped);
            container.Register<ICidadeAppService, CidadeAppService>(Lifestyle.Scoped);
            container.Register<ICidadeService, CidadeService>(Lifestyle.Scoped);
            #endregion

            #region Log
            container.Register<ILogRepository, LogRepository>(Lifestyle.Scoped);
            container.Register<ILogAppService, LogAppService>(Lifestyle.Scoped);
            container.Register<ILogService, LogService>(Lifestyle.Scoped);
            #endregion 
            // Necessário para registrar o ambiente do Owin que é dependência do Identity
            // Feito fora da camada de IoC para não levar o System.Web para fora
            container.RegisterPerWebRequest(() =>
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && container.IsVerifying)
                {
                    return new OwinContext().Authentication;
                }
                return HttpContext.Current.GetOwinContext().Authentication;

            });

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            BootStrapper.RegisterServices(container);
        }
    }
}