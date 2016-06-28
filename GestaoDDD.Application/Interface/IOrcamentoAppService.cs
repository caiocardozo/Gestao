using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Application.Interface
{
    public interface IOrcamentoAppService : IAppServiceBase<Orcamento>
    {
        IEnumerable<Orcamento> RetornaOrcamentosPagos(int servico, string cidade, EnumEstados estado, string usuarioId);
        //retorna os orcamentos 
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumEstados estado);
        IEnumerable<Orcamento> RetornaOrcamentosAbertos();
        IEnumerable<Orcamento> RetornarOrcamentosComDistanciaCalculada(string prestador_latitude, string prestador_longitude, string raio, string usuarioId);
        IEnumerable<Orcamento> GetOrcamentoPagosPeloPrestador(string usuarioId);
    }
}
