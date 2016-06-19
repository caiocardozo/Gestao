using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IOrcamentoRepository : IRepositoryBase<Orcamento>
    {

        //retorna os orcamentos 
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade,EnumClasses.EnumEstados estado);
    }
}
