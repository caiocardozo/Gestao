using GestaoDDD.Domain.Entities;
using System.Collections.Generic;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IServicoPrestadorService : IServiceBase<ServicoPrestador>
    {
        void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador);
        IEnumerable<ServicoPrestador> GetServicoPorPrestadorId(string prestadorId);
    }

}
