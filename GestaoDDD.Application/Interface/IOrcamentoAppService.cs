using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Application.Interface
{
    public interface IOrcamentoAppService : IAppServiceBase<Orcamento>
    {
        //retorna os orcamentos 
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumClasses.EnumEstados estado);
    }
}
