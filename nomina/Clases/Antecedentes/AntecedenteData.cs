using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Antecedentes
{
    public class AntecedenteData
    {
        public int Id{ set; get; }
        public int IdEmpleado { set; get; }
        public int NumeroAntecedente { set; get; }
        public DateTime FechaEmision { set; get; }
        public DateTime Vigencia { set; get; }
        public DateTime FechaVencimiento { set; get; }
        public string LugarOrigen { set; get; }
    }
}
