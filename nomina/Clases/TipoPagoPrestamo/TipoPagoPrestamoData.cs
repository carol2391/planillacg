using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.TipoPagoPrestamo
{
   public class TipoPagoPrestamoData
    {
        public int id { set; get; }
        public string descripcion { set; get; }

        public TipoPagoPrestamoData() { }

        public TipoPagoPrestamoData(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }
    }
}
