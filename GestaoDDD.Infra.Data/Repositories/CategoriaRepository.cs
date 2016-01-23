using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        private readonly GestaoContext _db;
        public CategoriaRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
            _db = new GestaoContext();
        }

        //retorna todas as cetegorias especias
        //public string retorna(string id, int numero)
        //{
        //    return consulta
        //}
    }
}
