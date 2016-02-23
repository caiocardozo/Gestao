using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IPrestadorRepository : IRepositoryBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
    }
}
