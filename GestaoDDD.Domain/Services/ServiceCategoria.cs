using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.IRepositories;
using GestaoDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Services
{
    public class ServiceCategoria : ServiceBase<Categoria>, IServiceCategoria
    {
        private readonly ICategoriaRepositorio _categoriaRpp;

        public ServiceCategoria(ICategoriaRepositorio categoriaRpp)
        :base (categoriaRpp)
        {
            _categoriaRpp = categoriaRpp;  
        }

        public IEnumerable<Categoria> ObterCategoriasEspeciais(IEnumerable<Categoria> categoria) 
        {
            return categoria;
        }
    }
}
