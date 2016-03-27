using System;

namespace GestaoDDD.Domain.Entities.Identity
{
    public class Claims : Entidade 
    {
        public Claims() 
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
