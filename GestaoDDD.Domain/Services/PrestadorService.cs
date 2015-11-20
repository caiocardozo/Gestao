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
    public class PrestadorService : ServiceBase<Prestador>, IPrestadorService
    {
        private readonly IPrestadorRepository _prestadorRepository;

        public PrestadorService(IPrestadorRepository prestadorRepositorio)
            : base(prestadorRepositorio)
        {
            _prestadorRepository = prestadorRepositorio;
        }
    }
}
