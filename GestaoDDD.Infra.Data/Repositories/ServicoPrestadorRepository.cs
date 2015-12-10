using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class ServicoPrestadorRepository : RepositoryBase<ServicoPrestador>, IServicoPrestadorRepository
    {
        public ServicoPrestadorRepository(IGestaoContext gestaoContext)
            :base(gestaoContext)
        {

        }
    }
}
