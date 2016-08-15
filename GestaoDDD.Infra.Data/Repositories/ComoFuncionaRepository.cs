using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class ComoFuncionaRepository : RepositoryBase<ComoFunciona>, IComoFuncionaRepository
    {
        private readonly GestaoContext _db;
        public ComoFuncionaRepository(GestaoContext dbContext)
            : base(dbContext)
        {
            _db = dbContext;

        }
    }
}
