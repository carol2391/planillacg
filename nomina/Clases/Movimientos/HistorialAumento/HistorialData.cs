using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Movimientos.HistorialAumento
{
   public class HistorialData
    {
        public HistorialData(string codigoEmpleado, string nombre,
            DateTime fecha, decimal sueldoAnterior, decimal monto, decimal sueldoActual,
            string categoria) {

            this.CodigoEmpleado = codigoEmpleado;
            this.Nombre = nombre;
            this.Fecha = fecha;
            this.SueldoAnterior = sueldoAnterior;
            this.Monto = monto;
            this.SueldoActual = sueldoActual;
            this.Categoria = categoria;
        }

        public string CodigoEmpleado { set; get; }
        public string Nombre { set; get; }
        public DateTime Fecha{ set; get; }
        public decimal SueldoAnterior { set; get; }
        public decimal Monto { set; get; }

        public decimal SueldoActual { set; get; }

        public string Categoria { set; get; }
    }
}
