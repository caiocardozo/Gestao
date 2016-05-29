using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IPrestadorRepository : IRepositoryBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
        Prestador GetPorGuid(string guid);

        //retorna o prestador atraves do email
        Prestador GetPorEmail(string email);
    }
}
