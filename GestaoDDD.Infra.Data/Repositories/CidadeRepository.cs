using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class CidadeRepository : RepositoryBase<Cidade>, ICidadeRepository
    {
        private readonly GestaoContext _dbContext;
        public CidadeRepository(GestaoContext dbContext)
            :base(dbContext)
        {
            _dbContext = dbContext;
        }

        //retorna todos as cidades a partir de um estado
        public List<CidadeIdNome> RetornaCidadePeloEstado(int estado)
        {
            var resultado = (from cid in _db.Cidade.Where(s => s.UF == estado)
                             select new CidadeIdNome()
                             {
                                 Id = cid.Id,
                                 NomeCidade = cid.NomeCidade
                             }).ToList();
            return resultado;
        }
    }
}
