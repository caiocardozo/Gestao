using GestaoDDD.Domain.Entities;
using System.Collections.Generic;

namespace GestaoDDD.Application.Interface
{
    public interface ICategoriaAppService : IAppServiceBase<Categoria>
    {
        IEnumerable<Categoria> ObterCategoriasEspeciais();
    }
}
