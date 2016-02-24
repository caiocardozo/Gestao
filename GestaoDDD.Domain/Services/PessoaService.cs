using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class PessoaService : ServiceBase<Pessoa>, IPessoaService
    {

        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoarepository)
            : base(pessoarepository)
        {
            _pessoaRepository = pessoarepository;  
        }
    }
}
