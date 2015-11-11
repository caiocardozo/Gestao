using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.IRepositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {

        public CategoriaRepository(IGestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
        }
    }
}
