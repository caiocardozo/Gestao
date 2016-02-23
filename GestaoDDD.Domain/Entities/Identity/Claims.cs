using System;

namespace GestaoDDD.Domain.Entities.Identity
{
    public class Claims
    {
        public Claims() 
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
