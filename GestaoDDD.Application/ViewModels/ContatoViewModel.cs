using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Application.ViewModels
{
    public class ContatoViewModel
    {

        [Key]
        public int contato_Id { get; set; }

        [Required(ErrorMessage = "É necessário informar o campo nome.")]
        [MaxLength(150, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        public string ctt_nome { get; set; }

        [Required(ErrorMessage = "É necessário preencher o seu E-mail.")]
        [DataType(DataType.EmailAddress)]
        public string ctt_email { get; set; }
        
        [Required(ErrorMessage = "Preencha  o campo com o número do seu telefone.")]
        public string ctt_telefone { get; set; }
        
        [Required(ErrorMessage="Envia sua breve mensagem de contato.")]
        [MaxLength(500, ErrorMessage="Tamanho máximo de {0} caracteres.")]
        public string ctt_msg { get; set; }

    }
}
