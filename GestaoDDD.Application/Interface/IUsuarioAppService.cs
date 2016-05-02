using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Application.Interface
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {
        void DesativarLock(string id);

        //obtem o usuario atraves do email
        Usuario ObterPorEmail(string email);
    }
}
