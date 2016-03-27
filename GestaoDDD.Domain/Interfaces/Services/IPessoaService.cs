using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IPessoaService : IServiceBase<Pessoa>
    {

        //retorna a pessoa atraves do id
        Pessoa RPessoaPorId(string id);
    }
}
