using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class IndiqueProfissionalService : ServiceBase<IndiqueProfissional>, IIndiqueProfissionalService
    {
        private readonly IIndiqueProfissionalRepository _IindRepositorio;
        public IndiqueProfissionalService(IIndiqueProfissionalRepository IindRepositorio)
            : base(IindRepositorio)
        {
            _IindRepositorio = IindRepositorio;
        }
    }
}
