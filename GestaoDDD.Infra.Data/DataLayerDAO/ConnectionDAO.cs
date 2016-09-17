using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestaoDDD.Infra.Data.DataLayerDAO
{
    public class ConnectionDAO
    {
        public ConnectionDAO()
        {

        }
        public IEnumerable<Prestador> GetPrestadorAdmin()
        {
            var prestadores = new List<Prestador>();

            var cStr = @"Data Source=plumeria.arvixe.com;Initial Catalog=GestaoDados;Persist Security Info=True;User ID=user_gestao;Password=sa.@2";
            var cn = new SqlConnection(cStr);
            cn.Open();

            var sql = @"
                        select p.nome, p.email from prestador p
                        inner join aspnetuserroles ar on p.Id = ar.userid
                        where ar.roleid = 1";

            var cmd = new SqlCommand(sql, cn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var prestador = new Prestador
                {
                    pres_Nome = (string)reader["nome"],
                    pres_Email = (string)reader["email"]
                };

                prestadores.Add(prestador);
            }
            return prestadores;
        }
    }
}
