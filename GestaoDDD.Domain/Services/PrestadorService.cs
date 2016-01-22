using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class PrestadorService : ServiceBase<Prestador>, IPrestadorService
    {
        private readonly IPrestadorRepository _prestadorRepository;

        public PrestadorService(IPrestadorRepository prestadorRepositorio)
            : base(prestadorRepositorio)
        {
            _prestadorRepository = prestadorRepositorio;
        }

        public Prestador GetPorCpf(string cpf) 
        {
            return _prestadorRepository.GetPorCpf(cpf);
        }
    }
}
