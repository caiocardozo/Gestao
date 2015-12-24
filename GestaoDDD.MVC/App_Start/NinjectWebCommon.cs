using GestaoDDD.Application.Interface;
using GestaoDDD.Application.Services;
using GestaoDDD.Domain.Interfaces.IRepositories;
using GestaoDDD.Domain.Interfaces.Services;
using GestaoDDD.Domain.Services;
using GestaoDDD.Infra.Data.Contexto;
using GestaoDDD.Infra.Data.Repositories;
using GestaoDDD.MVC.App_Start;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace GestaoDDD.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using GestaoDDD.Domain.Interfaces.Repositories;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //inRequeste nao deixa instanciar varias vezes o contexto
            kernel.Bind<IGestaoContext>().To<GestaoContext>().InRequestScope();

            #region Injeção de dependencia.
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));

            #region Categoria
            kernel.Bind<ICategoriaAppService>().To<CategoriaAppService>();
            kernel.Bind<ICategoriaService>().To<CategoriaService>();
            kernel.Bind<ICategoriaRepository>().To<CategoriaRepository>();
            #endregion

            #region Servico
            kernel.Bind<IServicoAppService>().To<ServicoAppService>();
            kernel.Bind<IServicoService>().To<ServicoService>();
            kernel.Bind<IServicoRepository>().To<ServicoRepository>();
            #endregion

            #region Prestador
            kernel.Bind<IPrestadorAppService>().To<PrestadorAppService>();
            kernel.Bind<IPrestadorService>().To<PrestadorService>();
            kernel.Bind<IPrestadorRepository>().To<PrestadorRepository>();
            #endregion

            #region Orcamento
            kernel.Bind<IOrcamentoAppService>().To<OrcamentoAppService>();
            kernel.Bind<IOrcamentoService>().To<OrcamentoService>();
            kernel.Bind<IOrcamentoRepository>().To<OrcamentoRepository>();
            #endregion

            #region Usuario
            kernel.Bind<IUsuarioAppService>().To<UsuarioAppService>();
            kernel.Bind<IUsuarioService>().To<UsuarioService>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();

            #endregion

            #endregion
        }
    }
}
