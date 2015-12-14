using System;

namespace GestaoDDD.Domain.Entities
{
    public class Entidade
    {
        public DateTime data_Inclusao { get; set; }
        public DateTime data_Alteracao { get; set; }

        public Entidade()
        {
            data_Alteracao = DateTime.Now;
            data_Inclusao = DateTime.Now;
        }
    }
}
