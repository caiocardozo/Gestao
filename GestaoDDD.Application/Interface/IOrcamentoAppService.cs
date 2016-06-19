using System.Collections.Generic;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Application.Interface
{
    public interface IOrcamentoAppService : IAppServiceBase<Orcamento>
    {
        //retorna os orcamentos 
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade);
    }
}
