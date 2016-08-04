using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GestaoDDD.Application.Interface
{
    public interface IServicoPrestadorAppService : IAppServiceBase<ServicoPrestador>
    {
        void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador);

        IEnumerable<ServicoPrestador> GetServicoPorPrestadorId(string prestadorId);

        IEnumerable<ServicoPrestador> GetByServicoId(int servicoId);

        IEnumerable<Guid> PrestadoresOferecemServico(int servicoId);

    }
}
