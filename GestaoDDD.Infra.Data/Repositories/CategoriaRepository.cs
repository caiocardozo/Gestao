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

        //retorna todas as cetegorias especias
        //public string retorna(string id, int numero)
        //{
        //    return consulta
        //}
    }
}
