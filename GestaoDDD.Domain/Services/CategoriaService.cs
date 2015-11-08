using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.IRepositories;
using GestaoDDD.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace GestaoDDD.Domain.Services
{
    public class CategoriaService : ServiceBase<Categoria>, ICategoriaService
    {

        private readonly ICategoriaRepository _categoriarepository;

        public CategoriaService(ICategoriaRepository categoriarepository)
        :base (categoriarepository)
        {
            _categoriarepository = categoriarepository;  
        }

        public IEnumerable<Categoria> ObterCategoriasEspeciais() 
        {
            //extender o metodo ao repositorio
            IEnumerable<Categoria> cat;
            return cat;
        }
    }
}
