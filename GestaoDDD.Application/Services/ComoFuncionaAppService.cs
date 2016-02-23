using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class ComoFuncionaAppService : AppServiceBase<ComoFunciona>, IComoFuncionaAppService
    {
         private readonly IComoFuncionaService _comoFuncionaService;

         public ComoFuncionaAppService(IComoFuncionaService comoFuncionaService)
             : base(comoFuncionaService)
        {
            _comoFuncionaService = comoFuncionaService;
        }
    }
}
