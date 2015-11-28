using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class ServicoViewModel
    {
        public int serv_Id { get; set; }

        [Required(ErrorMessage="Preencha o campo nome do serviço.")]
        public string serv_nome { get; set; }
    }
}
