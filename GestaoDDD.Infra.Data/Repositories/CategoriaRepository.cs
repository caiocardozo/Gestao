using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.IRepositories;
using GestaoDDD.Infra.Data.Context;
using GestaoDDD.Infra.Data.Repository;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {

        public CategoriaRepository(IGestaoContexto gestaoContexto)
            : base(gestaoContexto)
        {
        }
    }
}
