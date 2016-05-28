using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System.Collections.Generic;
using System.Linq;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class ServicoPrestadorRepository : RepositoryBase<ServicoPrestador>, IServicoPrestadorRepository
    {
        private readonly GestaoContext _db;

        public ServicoPrestadorRepository(GestaoContext gestaoContext)
            :base(gestaoContext)
        {
            _db = new GestaoContext();
        }

        public void SalvarServicosPrestador(IEnumerable<Servico> checkboxes, Prestador prestador) 
        {
            ServicoPrestador Objeto = new ServicoPrestador();
            //var servicosPrestador = _db.ServicoPrestador.FirstOrDefault(s => s.serv_Pres_Id.Equals(prestador.pres_Id));

            foreach (var check in checkboxes) 
            {
                Objeto.pres_Id = prestador.pres_Id;
                Objeto.serv_Id = check.serv_Id;
                SaveOrUpdate(Objeto);
            }
        }

        private void VerificarSeExiste() { }
    }
}
