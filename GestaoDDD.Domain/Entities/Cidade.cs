namespace GestaoDDD.Domain.Entities
{
   public class Cidade : Entidade
    {
       public int Id { get; set; }
       public int Codigo { get; set; }
       public bool Situacao { get; set; }
       public string CodigoCidade { get; set; }
       public string NomeCidade { get; set; }
       public string CodigoUf { get; set; }
       public int UF { get; set; }
       public string CodigoPais { get; set; }
       public string Pais { get; set; }
    }

    
}

