using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Application.Interface;
using GestaoDDD.Infra.Data.Repositories;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Context;
using GestaoDDD.Infra.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using GestaoDDD.Application.Services;
using GestaoDDD.Domain.Interfaces.Services;
using GestaoDDD.Domain.Services;
using GestaoDDD.Infra.Data.Contexto;


namespace GestaoDDD.Infra.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.RegisterPerWebRequest<ApplicationDbContext>();
            container.RegisterPerWebRequest<GestaoContext>();

            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>());
            container.RegisterPerWebRequest<ApplicationRoleManager>();
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();

            #region Categoria
            container.RegisterPerWebRequest<ICategoriaRepository, CategoriaRepository>();
            container.RegisterPerWebRequest<ICategoriaAppService, CategoriaAppService>();
            container.RegisterPerWebRequest<ICategoriaService, CategoriaService>();
            #endregion

            #region Servico
            container.RegisterPerWebRequest<IServicoRepository, ServicoRepository>();
            container.RegisterPerWebRequest<IServicoAppService, ServicoAppService>();
            container.RegisterPerWebRequest<IServicoService, ServicoService>();
            #endregion

            #region Prestador
            container.RegisterPerWebRequest<IPrestadorRepository, PrestadorRepository>();
            container.RegisterPerWebRequest<IPrestadorAppService, PrestadorAppService>();
            container.RegisterPerWebRequest<IPrestadorService, PrestadorService>();
            #endregion

            #region Orcamento
            container.RegisterPerWebRequest<IOrcamentoRepository, OrcamentoRepository>();
            container.RegisterPerWebRequest<IOrcamentoAppService, OrcamentoAppService>();
            container.RegisterPerWebRequest<IOrcamentoService, OrcamentoService>();
            #endregion

            #region Usuario
            container.RegisterPerWebRequest<IUsuarioRepository, UsuarioRepository>();
            container.RegisterPerWebRequest<IUsuarioAppService, UsuarioAppService>();
            container.RegisterPerWebRequest<IUsuarioService, UsuarioService>();
            #endregion

            #region Servico Prestador
            container.RegisterPerWebRequest<IServicoPrestadorRepository, ServicoPrestadorRepository>();
            container.RegisterPerWebRequest<IServicoPrestadorAppService, ServicoPrestadorAppService>();
            container.RegisterPerWebRequest<IServicoPrestadorService, ServicoPrestadorService>();
            #endregion

            #region ComoFunciona
            container.RegisterPerWebRequest<IComoFuncionaRepository, ComoFuncionaRepository>();
            container.RegisterPerWebRequest<IComoFuncionaAppService, ComoFuncionaAppService>();
            container.RegisterPerWebRequest<IComoFuncionaService, ComoFuncionaService>();
            #endregion

            #region IndiqueProfissional
            container.RegisterPerWebRequest<IIndiqueProfissionalRepository, IndiqueProfissionalRepository>();
            container.RegisterPerWebRequest<IIndiqueProfissionalAppService, IndiqueProfissionalAppService>();
            container.RegisterPerWebRequest<IIndiqueProfissionalService, IndiqueProfissionalService>();
            #endregion

            #region Pessoa
            container.RegisterPerWebRequest<IPessoaRepository, PessoaRepository>();
            container.RegisterPerWebRequest<IPessoaAppService, PessoaAppService>();
            container.RegisterPerWebRequest<IPessoaService, PessoaService>();
            #endregion

            #region Contato
            //container.RegisterPerWebRequest<IContatoRepository, ContatoRepository>();
            container.RegisterPerWebRequest<IContatoAppService, ContatoAppService>();
            container.RegisterPerWebRequest<IContatoService, ContatoService>();

            #endregion


        } 
    }
}
