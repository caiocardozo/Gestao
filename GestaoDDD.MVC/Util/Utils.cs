using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GestaoDDD.MVC.Util
{
    public class Utils
    {
        public Utils()
        {

        }
        public string PrimeiraLetraMaiuscula(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public string DefineSaudacao()
        {
            var saudacao = "";
            var date = DateTime.Now;
            if (date.Hour > 12 && date.Hour < 18)
            {
                saudacao = "boa tarde";
            }
            else if (date.Hour > 0 && date.Hour < 12)
            {
                saudacao = "bom dia";
            }
            else
            {
                saudacao = "boa noite";
            }
            return saudacao;
        }
    }
}