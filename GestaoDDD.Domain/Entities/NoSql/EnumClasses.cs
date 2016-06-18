using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Entities.NoSql
{
    public class EnumClasses
    {
        public enum EnumStatus
        {
            Ativo,
            Inativo
        }
        public enum EnumEstados
        {
            AC,
            AL,
            AP,
            AM,
            BA,
            CE,
            DF,
            ES,
            GO,
            MA,
            MT,
            MS,
            MG,
            PA,
            PB,
            PR,
            PE,
            PI,
            RJ,
            RN,
            RS,
            RO,
            RR,
            SC,
            SP,
            SE,
            TO
        }

        public enum EnumStatusOrcamento
        {
            Aberto, 
            Fechado, 
            Aceito
        }
    }
}
