using GestaoDDD.Domain.Entities;
using System.Collections.Generic;

namespace GestaoDDD.Application.Interface
{
    public interface IServicoPrestadorAppService : IAppServiceBase<ServicoPrestador>
    {
        void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador);

        //void CalcularDistanciaOrcamentos();
    }
}
