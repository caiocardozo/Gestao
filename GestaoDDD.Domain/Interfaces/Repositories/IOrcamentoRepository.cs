using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IOrcamentoRepository : IRepositoryBase<Orcamento>
    {
        IEnumerable<Orcamento> RetornaOrcamentosPagos(int servico, string cidade, EnumEstados estado, string usuarioId);
        //retorna os orcamentos 
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumEstados estado);
        IEnumerable<Orcamento> RetornaOrcamentosAbertos();
        IEnumerable<Orcamento> GetOrcamentoPagosPeloPrestador(string usuarioId);
    }
}
