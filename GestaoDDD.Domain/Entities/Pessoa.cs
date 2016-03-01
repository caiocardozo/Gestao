namespace GestaoDDD.Domain.Entities
{
   public class Pessoa
    {
       public string usu_id { get; set; }

       public string pes_nome { get; set; }

       public string pes_cpf { get; set; }

       public string pes_rg { get; set; }

       public string pes_endereco { get; set; }

       public int pes_numero { get; set; }

       public string pes_bairro { get; set; }

       public string pes_cidade { get; set; }

       public string pes_cep { get; set; }
       
       public virtual Usuario Usuario { get; set; }

    }
}
