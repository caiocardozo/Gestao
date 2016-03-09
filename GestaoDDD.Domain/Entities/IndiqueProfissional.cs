using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Entities
{
    public class IndiqueProfissional
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public string Email_Empresa { get; set; }
        public EnumEstados Estado { get; set; }
        public string Cidade { get; set; }
        public string  Nome { get; set; }
        public string Email_Solicitante { get; set; }

    }

    [DataContract]
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
    };
}


