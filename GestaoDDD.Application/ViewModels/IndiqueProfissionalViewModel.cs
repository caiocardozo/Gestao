using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Application.ViewModels
{
    public class IndiqueProfissionalViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]
        [MaxLength(150, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        public string Nome_Profissional { get; set; }

        [DisplayName("Telefone de contato")]
        [Required(ErrorMessage = "Preencha o campo telefone de contato")]
        [MaxLength(20, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [DisplayName("E-mail da empresa")]
        [DataType(DataType.EmailAddress)]
        public string Email_Empresa { get; set; }

        [DisplayName("UF")]
        public EnumEstados Estado { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Preencha o campo de cidade")]
        [MaxLength(128, ErrorMessage = "Tamanho máximo de {0} caracteres")]

        public string Cidade { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Preencha o campo nome")]
        [MaxLength(256, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail do solicitante")]
        [Required(ErrorMessage = "Preencha o campo e-mail do solicitante")]
        public string Email_Solicitante { get; set; }

        public Servico Servico { get; set; }
    }
    [DataContract]
    public enum EnumEstados
    {
        AC,
        AL,
        AP,
        AM,
        BA,
        CE,
        DF,
        ES,
        GO,
        MA,
        MT,
        MS,
        MG,
        PA,
        PB,
        PR,
        PE,
        PI,
        RJ,
        RN,
        RS,
        RO,
        RR,
        SC,
        SP,
        SE,
        TO
    };
}
