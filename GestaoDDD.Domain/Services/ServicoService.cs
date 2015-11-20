using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class ServicoService : ServiceBase<Servico>, IServicoService
    {
        private readonly IServicoRepository _servicoRepositorio;

        public ServicoService(IServicoRepository servicoRepositorio)
            :base(servicoRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;
        }
    }
}
