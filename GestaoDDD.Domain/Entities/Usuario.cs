using System;
namespace GestaoDDD.Domain.Entities
{
    public class Usuario : Entidade
    {
        public Usuario()
        {
            Id = Guid.NewGuid().ToString();
        }

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

    //public class Usuario : Entidade
    //{
    //    public int usu_Id { get; set; }
    //    public string usu_endereco { get; set; }
    //    public string usu_email { get; set; }
    //}

 
}

 
