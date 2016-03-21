using System.Collections.Generic;
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class ServicoAppService : AppServiceBase<Servico>, IServicoAppService
    {
        private readonly IServicoService _servicoService;

        public ServicoAppService(IServicoService servicoService)
            : base(servicoService)
        {
            _servicoService = servicoService;
        }

         //retorna todos os serviços a partir de uma categoria
        public List<ServicoIdNome> RetornaServicoPelaCategoria(int categoria)
        {
            return _servicoService.RetornaServicoPelaCategoria(categoria);
        }
    }
}
