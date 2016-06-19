using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Services;
using GestaoDDD.Application.Interface;

namespace GestaoDDD.Application.Services
{
    public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
    {
        private readonly ICidadeService _cidadeService;
        public CidadeAppService(ICidadeService cidadeService)
            : base(cidadeService)
        {
            _cidadeService = cidadeService;
        }

         //retorna todos as cidades a partir de um estado
        public List<CidadeIdNome> RetornaCidadePeloEstado(int estado)
        {
            return _cidadeService.RetornaCidadePeloEstado(estado);
        }
    }
}
