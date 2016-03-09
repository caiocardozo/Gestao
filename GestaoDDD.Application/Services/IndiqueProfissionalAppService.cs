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
