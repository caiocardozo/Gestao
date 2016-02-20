using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
