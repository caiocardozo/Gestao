using GestaoDDD.Domain.Entities;
using System.Collections.Generic;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IServicoPrestadorRepository : IRepositoryBase<ServicoPrestador>
    {
        void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador);
    }
}
