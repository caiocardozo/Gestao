using System;
using System.Collections.Generic;
using System.Linq;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class OrcamentoRepository : RepositoryBase<Orcamento>, IOrcamentoRepository
    {
        private readonly GestaoContext _dbContext;
        public OrcamentoRepository(GestaoContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Orcamento> RetornaOrcamentosPagos(int servico, string cidade, EnumEstados estado, string usuarioId)
        {
            return _db.Orcamento.Where(o => o.serv_Id == servico &&
                o.orc_estado == estado &&
                o.orc_cidade.Equals(cidade) &&
                o.Status == EnumStatusOrcamento.Aberto &&
                o.PrestadorFk.Any(p => p.pres_Id == usuarioId));
        }

        //retorna os orcamentos 
        public IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumEstados estado)
        {
            return _db.Orcamento.Where(o => o.serv_Id == servico &&
                o.orc_estado == estado &&
                o.orc_cidade.Equals(cidade) && 
                o.Status == EnumStatusOrcamento.Aberto);
        }


        public IEnumerable<Orcamento> RetornaOrcamentosAbertos()
        {
            return _db.Orcamento.Where(o => o.Status == EnumStatusOrcamento.Aberto);
        }

        public IEnumerable<Orcamento> GetOrcamentoPagosPeloPrestador(string usuarioId)
        {
            return _db.Orcamento.Where(o => o.PrestadorFk.Any(p => p.pres_Id == usuarioId));
        }
    }
}
