using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Application.Services
{
    public class ServicoPrestadorAppService : AppServiceBase<ServicoPrestador>, IServicoPrestadorAppService
    {
        private readonly IServicoPrestadorService _servicoPresService;
        public ServicoPrestadorAppService(IServicoPrestadorService servicoPrestService)
            : base(servicoPrestService)
        {
            _servicoPresService = servicoPrestService;
        }

        public void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador) 
        {
            _servicoPresService.SalvarServicosPrestador(checkboxes, prestador);
        }
    }
}
