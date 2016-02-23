using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class UsuarioViewModel 
    {
        //[Key]
        //public int usu_Id { get; set; }

        //[Required(ErrorMessage = "Preencha o campo de endereço")]
        //[MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        //[MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        //[DisplayName("Endereço")]
        //public string usu_endereco { get; set; }

        //[DataType(DataType.EmailAddress, ErrorMessage="Utilize um e-mail válido.")]
        //[Required(ErrorMessage="Preencha o campo de e-mail.")] 
        //public string usu_email { get; set; }

        public UsuarioViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual string UserName { get; set; }
    }
}
