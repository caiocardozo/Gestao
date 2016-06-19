using System.Collections.Generic;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IOrcamentoRepository : IRepositoryBase<Orcamento>
    {

        //retorna os orcamentos 
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade);
    }
}
