using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IPrestadorService : IServiceBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
        Prestador GetPorGuid(string guid);
    }
}
