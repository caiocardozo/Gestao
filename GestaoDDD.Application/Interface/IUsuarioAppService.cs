using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Application.Interface
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {
        void DesativarLock(string id);
    }
}
