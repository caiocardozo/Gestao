using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa>
    {

        //retorna a pessoa atraves do id
        Pessoa RPessoaPorId(string id);
    }
}
