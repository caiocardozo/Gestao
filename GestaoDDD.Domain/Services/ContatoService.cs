using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class ContatoService : ServiceBase<Contato>, IContatoService
    {
        private readonly IContatoRepository _cttRepository;
        public ContatoService(IContatoRepository cttRepository)
            : base(cttRepository)
        {
            _cttRepository = cttRepository;
        }
    }
}
