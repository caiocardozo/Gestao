using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Interface
{
    public interface IPessoaAppService : IServiceBase<Pessoa>
    {

        //retorna a pessoa atraves do id
        Pessoa RPessoaPorId(string id);
    }
}
