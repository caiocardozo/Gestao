using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IServicoPrestadorRepository : IRepositoryBase<ServicoPrestador>
    {
        void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador);
        IEnumerable<ServicoPrestador> GetServicoPorPrestadorId(string prestadorId);
        IEnumerable<ServicoPrestador> GetByServicoId(int servicoId);
        IEnumerable<Guid> PrestadoresOferecemServico(int servicoId);
    }
}
