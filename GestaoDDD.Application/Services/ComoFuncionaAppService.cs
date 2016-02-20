using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
