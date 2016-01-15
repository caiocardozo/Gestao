using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;
using System.Collections.Generic;

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
    }
}
