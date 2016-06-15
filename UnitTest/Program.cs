using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var endereco = "Parque dos Pirineus, Anápolis - GO, Brasil";
            var x = endereco.Split(',');
            var y = x[1].Split('-');
            var cidade = y[0];
            var estado = y[1];



        }
    }
}
