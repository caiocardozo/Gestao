using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class CategoriaAppService : AppServiceBase<Categoria>, ICategoriaAppService
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaAppService(ICategoriaService categoriaService)
            : base(categoriaService)
        {
            _categoriaService = categoriaService;
        }

        //public IEnumerable<Categoria> ObterCategoriasEspeciais()
        //{
        //    return Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_categoriaService.ObterCategoriasEspeciais());
        //}
    }
}
