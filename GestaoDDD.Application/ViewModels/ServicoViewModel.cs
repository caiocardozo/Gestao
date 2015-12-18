using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Application.ViewModels
{
    public class ServicoViewModel
    {
        [ScaffoldColumn(false)]
        public int serv_Id { get; set; }

        [DisplayName("Nome do Serviço")]
        [Required(ErrorMessage = "Preencha o nome do serviço")]
        [MinLength(4, ErrorMessage = "Nome do serviço deve ter ao menos {1} caracteres")]
        [MaxLength(100, ErrorMessage = "Nome do Serviço deve conter no maximo {0} caracteres")]
        public string serv_Nome { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Categoria é obrigatória")]
        public int cat_Id { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
