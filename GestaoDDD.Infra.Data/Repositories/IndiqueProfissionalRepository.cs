using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class IndiqueProfissionalRepository : RepositoryBase<IndiqueProfissional>, IIndiqueProfissionalRepository
    {
        private readonly GestaoContext _dbContext;
        public IndiqueProfissionalRepository(GestaoContext dbContext)
            :base(dbContext)
        {
            _dbContext = dbContext;

        }
    }
}
