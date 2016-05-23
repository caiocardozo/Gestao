using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

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
