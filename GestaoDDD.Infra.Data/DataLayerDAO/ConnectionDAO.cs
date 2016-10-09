using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

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


    //public void salvarCidades()
    //{
    //  var text = new StreamReader(@"C:\Repositorio\UnitTest\bin\Debug\cidades.txt", Encoding.GetEncoding(1252));

    //  var line = "";

    //  while ((line = text.ReadLine()) != null)
    //  {
    //    var cStr = @"Data Source=plumeria.arvixe.com;Initial Catalog=GestaoDados;Persist Security Info=True;User ID=user_gestao;Password=sa.@2";
    //    var cn = new SqlConnection(cStr);
    //    cn.Open();

    //    var sql = @"
    //                insert into cidade (codigo, data_inclusao, data_alteracao, situacao, nome_cidade, uf)
    //                values (0, getdate(), getdate(), 1, '" + line + "', 20)";

    //    var cmd = new SqlCommand(sql, cn);
    //    var reader = cmd.ExecuteReader();
    //    cn.Close();
    //  }
    //}

    public bool PegarQuantidadeOrcamentosPorPrestador(int orcamentoId)
    {
      var cStr = @"Data Source=plumeria.arvixe.com;Initial Catalog=GestaoDados;Persist Security Info=True;User ID=user_gestao;Password=sa.@2";
      var cn = new SqlConnection(cStr);
      cn.Open();

      var sql = @"SELECT COUNT(*) Qtd FROM ORCAMENTOPRESTADOR WHERE ORCAMENTO_ID_FK = " + orcamentoId;

      var cmd = new SqlCommand(sql, cn);
      var reader = cmd.ExecuteReader();
      int qtd = 0;
      while (reader.Read())
        qtd = (int)reader["Qtd"];

      if (qtd >= 4)
        return false;
      else
        return true;
    }
  }
}
