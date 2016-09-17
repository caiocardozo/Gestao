using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using System;

namespace GestaoDDD.Application.Interface
{
    public interface IOrcamentoAppService : IAppServiceBase<Orcamento>
    {
        IEnumerable<Orcamento> RetornaOrcamentosPagos(int servico, string cidade, EnumEstados estado, string usuarioId);
        IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumEstados estado);
        IEnumerable<Orcamento> RetornaOrcamentosAbertos();
        IEnumerable<Orcamento> RetornarOrcamentosComDistanciaCalculada(string prestador_latitude, string prestador_longitude, string raio, string usuarioId);
        IEnumerable<Orcamento> GetOrcamentoPagosPeloPrestador(string usuarioId);
        Orcamento RetornaOrcamentoPorId(int id);
        IEnumerable<Guid> EnviaEmailParaPrestadoresQueOferecemOServico(int servicoId);
        KeyValuePair<bool, string> EnviaEmailNotificacao(Prestador prestador, Orcamento orcamento);
        IEnumerable<Orcamento> VerificaSeOrcamentoPertenceAoUsuario(IEnumerable<Orcamento> orcamentos, string raio, string pLatitude, string pLongitude);
    }
}
