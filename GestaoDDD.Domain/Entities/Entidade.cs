using System;

namespace GestaoDDD.Domain.Entities
{
    public class Entidade
    {
        public DateTime Data_Inclusao { get; set; }
        public DateTime Data_Alteracao { get; set; }

        public Entidade()
        {
            Data_Alteracao = DateTime.Now;
            Data_Inclusao = DateTime.Now;
        }
    }
}
