using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IPrestadorRepository : IRepositoryBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
    }
}
