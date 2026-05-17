using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.TipoAusencia
{
    public class TipoAusenciaData
    {
        public int id { set; get; }
        public string descripcion { set; get; }
        public TipoAusenciaData(int id, string descripcion) {
            this.id = id;
            this.descripcion = descripcion;
        }
    }
}
