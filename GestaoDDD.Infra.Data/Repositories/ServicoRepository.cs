using System.Collections.Generic;
using System.Linq;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class ServicoRepository : RepositoryBase<Servico>, IServicoRepository
    {
        private readonly GestaoContext _db;
        public ServicoRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
            _db = new GestaoContext();
        }

        //retorna todos os serviços a partir de uma categoria
        public List<ServicoIdNome> RetornaServicoPelaCategoria(int categoria)
        {
            var resultado = (from ser in _db.Servico.Where(s => s.cat_Id == categoria)
            select new ServicoIdNome()
                            {
                                serv_Id = ser.serv_Id,
                                serv_Nome = ser.serv_Nome
                            }).ToList();
            return resultado;
        }
    }
}
