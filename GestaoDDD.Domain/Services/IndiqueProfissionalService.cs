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
    public class IndiqueProfissionalService : ServiceBase<IndiqueProfissional>, IIndiqueProfissionalService
    {
        private readonly IIndiqueProfissionalRepository _IindRepositorio;
        public IndiqueProfissionalService(IIndiqueProfissionalRepository IindRepositorio)
            : base(IindRepositorio)
        {
            _IindRepositorio = IindRepositorio;
        }
    }
}
