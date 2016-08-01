using System.Collections.Generic;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IPrestadorService : IServiceBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
        Prestador GetPorGuid(string guid);

        //retorna o prestador atraves do email
        Prestador GetPorEmail(string email);

        //retorna os pretadores que nao estao ligados ao orçamento selecionado
        IEnumerable<Prestador> GetPrestadores(int orcamentoId);
    }
}
