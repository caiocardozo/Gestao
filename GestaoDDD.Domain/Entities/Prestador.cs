 using System.Collections.Generic;
 using EnumClass = GestaoDDD.Domain.Entities.NoSql;
using System.Runtime.Serialization;
 using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Domain.Entities
{
    public class Prestador : Entidade
    {
        public string pres_Id { get; set; }

        public string pres_Nome { get; set; }

        public string pres_Cpf_Cnpj { get; set; }

        public string pres_Endereco { get; set; }

        public string pres_Raio_Recebimento { get; set; }

        public string pres_Email { get; set; }

        public string pres_Telefone_Residencial { get; set; }

        public string pres_Telefone_Celular { get; set; }

        public EnumClass.EnumStatus status { get; set; }

        public string nome_Empresa { get; set; }

        public string caminho_foto { get; set; }

        public string apresentacao_Pesssoal { get; set; }
        public string apresentacao_Empresa { get; set; }

        public string pres_latitude { get; set; }
        public string pres_longitude { get; set; }
        public virtual Usuario Usuario { get; set; }
        public ICollection<ServicoPrestador> ServicoPrestador { get; set; }
        public string Cidade { get; set; }
        public EnumEstados Estado { get; set; }
        public virtual ICollection<Orcamento> OrcamentoFk { get; set; }
    }
}
