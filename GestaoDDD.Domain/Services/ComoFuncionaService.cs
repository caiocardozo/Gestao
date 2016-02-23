using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class ComoFuncionaService : ServiceBase<ComoFunciona>, IComoFuncionaService
    {
        private readonly IComoFuncionaRepository _comoFuncionaRepository;
        
        ComoFuncionaService(IComoFuncionaRepository comoFuncionaRepository)
            : base(comoFuncionaRepository)
        {
            _comoFuncionaRepository = comoFuncionaRepository;
        }
    }
}
