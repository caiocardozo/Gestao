using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class PessoaAppService : AppServiceBase<Pessoa>, IPessoaAppService
    {
        private readonly IPessoaService _pessoaService;
        public PessoaAppService(IPessoaService pessoaService)
            :base(pessoaService)
        {
            _pessoaService = pessoaService;
        }
    }
}
