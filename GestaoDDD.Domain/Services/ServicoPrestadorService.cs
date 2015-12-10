using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class ServicoPrestadorService : ServiceBase<ServicoPrestador>, IServicoPrestadorService
    {
        private readonly IServicoPrestadorRepository _servPrestRepo;
        public ServicoPrestadorService(IServicoPrestadorRepository servPrestRepo)
            :base(servPrestRepo)
        {
            _servPrestRepo = servPrestRepo;
        }
    }
}
