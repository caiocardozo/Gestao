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


        //retorna os orcamentos 
        public IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumClasses.EnumEstados estado)
        {
            return _db.Orcamento.Where(o => o.serv_Id == servico && o.orc_estado == estado && o.orc_cidade.Equals(cidade) && o.Status == EnumClasses.EnumStatusOrcamento.Aberto);
        } 
    }
}
