using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.EstadoCivil
{
    public class CEstadoCivil
    {   public string id { set; get; }
        public string nombre { set; get; }

        public CEstadoCivil(string id, string nombre) {
            this.id = id;
            this.nombre = nombre;
        }
    }
}
