using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class CategoriaService : ServiceBase<Categoria>, ICategoriaService
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriarepository)
        :base (categoriarepository)
        {
            _categoriaRepository = categoriarepository;  
        }

        //public IEnumerable<Categoria> ObterCategoriasEspeciais() 
        //{
        //    //extender o metodo ao repositorio
        //    IEnumerable<Categoria> cat;
        //    return cat;
        //}
    }
}
