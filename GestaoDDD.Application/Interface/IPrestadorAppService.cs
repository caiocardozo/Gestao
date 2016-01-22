using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Application.Interface
{
    public interface IPrestadorAppService : IAppServiceBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
    }
}
