using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.TipoPago
{
   public class TipoPagoData
    {
        public int id { set; get; }
        public string descripcion { set; get; }
        public string Codigo { set; get; }
        public TipoPagoData() { }

        public TipoPagoData(int id, string descripcion) {
            this.id = id;
            this.descripcion = descripcion;
            this.Codigo = descripcion;
        }
    }
}
