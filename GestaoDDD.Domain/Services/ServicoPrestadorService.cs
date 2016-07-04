using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace GestaoDDD.Domain.Services
{
    public class ServicoPrestadorService : ServiceBase<ServicoPrestador>, IServicoPrestadorService
    {
        private readonly IServicoPrestadorRepository _servPrestRepo;

        public ServicoPrestadorService(IServicoPrestadorRepository servPrestRepo)
            : base(servPrestRepo)
        {
            _servPrestRepo = servPrestRepo;
        }

        public void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador)
        {
            _servPrestRepo.SalvarServicosPrestador(checkboxes, prestador);
        }


        public IEnumerable<ServicoPrestador> GetServicoPorPrestadorId(string prestadorId)
        {
            return _servPrestRepo.GetServicoPorPrestadorId(prestadorId);
        }

        public IEnumerable<ServicoPrestador> GetByServicoId(int servicoId)
        {
            return _servPrestRepo.GetByServicoId(servicoId);
        }
    }
}
    