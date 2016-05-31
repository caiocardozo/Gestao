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

        public Prestador GetPorGuid(string guid)
        {
            return _prestadorRepository.GetPorGuid(guid);
        }

        //retorna o prestador atraves do email
        public Prestador GetPorEmail(string email)
        {
            return _prestadorRepository.GetPorEmail(email);
        }
    }
}
