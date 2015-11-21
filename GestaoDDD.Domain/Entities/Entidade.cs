using System;

namespace GestaoDDD.Domain.Entities
{
    public class Entidade
    {
        public DateTime data_inclusao { get; set; }
        public DateTime data_alteracao { get; set; }

        public Entidade()
        {
            data_alteracao = DateTime.Now;
            data_inclusao = DateTime.Now;
        }
    }
}
