using System.Collections.Generic;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IOrcamentoService : IServiceBase<Orcamento>
    {
        //retorna os orcamentos 
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade);
    }
}
