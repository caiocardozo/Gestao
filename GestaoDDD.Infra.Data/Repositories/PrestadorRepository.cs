using System.Collections;
using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System.Linq;
using System;
using GestaoDDD.Infra.Data.DataLayerDAO;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class PrestadorRepository : RepositoryBase<Prestador>, IPrestadorRepository
    {
        private readonly GestaoContext _db;
        private ConnectionDAO _con;

        public PrestadorRepository(GestaoContext dbContext)
            : base(dbContext)
        {
            _db = dbContext;
            
        }

        public Prestador GetPorCpf(string cpf)
        {
            return _db.Prestador.FirstOrDefault(s => s.pres_Cpf_Cnpj.Equals(cpf));
        }

        public Prestador GetPorGuid(Guid guid)
        {
            return _db.Prestador.FirstOrDefault(s => s.pres_Id.Equals(guid.ToString()));
        }

        //retorna o prestador atraves do email
        public Prestador GetPorEmail(string email)
        {
            return _db.Prestador.FirstOrDefault(s => s.pres_Email.Equals(email));
        }

        //retorna os pretadores que nao estao ligados ao orçamento selecionado
        public IEnumerable<Prestador> GetPrestadores(int orcamentoId)
        {
            Orcamento orc2 = _db.Orcamento.Where(p => p.orc_Id == orcamentoId).SingleOrDefault();
            return (from pres in _db.Prestador
                    from orc in pres.OrcamentoFk
                    where !pres.OrcamentoFk.Contains(orc2)
                    select pres);
        }

        public IEnumerable<Prestador> GetPrestadoresComServicos()
        {
            return _db.Prestador.Include("ServicoPrestador");
        }

        public IEnumerable<Prestador> GetAdministradores()
        {
            _con = new ConnectionDAO();
            return _con.GetPrestadorAdmin();
        }


        //retorna todos os prestadores ativos
        public IEnumerable<Prestador> RetornaPrestadoresAtivos()
        {
            
            return _db.Prestador.Where(p => p.status == (EnumStatus)0);
            //return _db.Prestador;
        }

        //public Prestador VeriricaPrestadorExiste(string email)
        //{
        //    return _db.Prestador.FirstOrDefault(p => p.pres_Email == email);
        //}
    }
}
