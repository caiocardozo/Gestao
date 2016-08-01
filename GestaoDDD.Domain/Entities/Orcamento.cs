using System.Collections.Generic;
using EnumClass = GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Domain.Entities
{
    public class Orcamento : Entidade
    {
        public int orc_Id { get; set; }

        public string orc_descricao { get; set; }
        public string orc_endereco { get; set; }
        public string orc_prazo { get; set; }
        //public virtual Categoria categoria_id { get; set; }
        
        public string orc_nome_solicitante { get; set; }
        public string orc_email_solicitante { get; set; }
        public string orc_telefone_solicitante { get; set; }
        public string orc_endereco_solicitante { get; set; }
        public string orc_latitude { get; set; }
        public int serv_Id { get; set; }
        public string orc_longitude { get; set; }
        public string orc_cidade { get; set; }
        public EnumClass.EnumEstados orc_estado { get; set; }
        //public virtual Servico Servico { get; set; }
        public EnumClass.EnumStatusOrcamento Status { get; set; }
        public string Distancia { get; set; }
        public virtual ICollection<Prestador> PrestadorFk  { get; set; } 
       
        // public Orcamento()
        //{
        //    PrestadorFk = new HashSet<Prestador>();
        //   // Usuario = new HashSet<ADM_USUARIO>();
        //}
    }

}
