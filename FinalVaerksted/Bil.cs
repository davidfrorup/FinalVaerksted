using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalVaerksted
{
    internal class Bil
    {

        public string Nummerplade { get; set; }

        public string Mærke { get; set; }

        public string Model { get; set; }

        public string Motorstørrelse { get; set; }

        public DateTime Registreringsdato { get; set; }

        public int Årgang { get; set; }

        public DateTime SidsteSyn { get; set; }

        public Person Ejer { get; set; }

        public string Fabriksfejl { get; set; }

    }
}
