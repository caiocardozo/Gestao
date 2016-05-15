using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class IndiqueProfissionalAppService : AppServiceBase<IndiqueProfissional>, IIndiqueProfissionalAppService
    {
        private readonly IIndiqueProfissionalService _indProfService;
        public IndiqueProfissionalAppService(IIndiqueProfissionalService indProfService)
            : base(indProfService)
        {
            indProfService = _indProfService;
        }
    }
}
