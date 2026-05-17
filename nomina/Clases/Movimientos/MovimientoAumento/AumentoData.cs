using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Movimientos.MovimientoAumento
{
    public class AumentoData
    {
        public AumentoData() {

        }

        public AumentoData(int id, string codigoEmpleado, DateTime fecha,
             string codigoCategoria, string categoria , decimal sueldoAnterior,
             decimal sueldoActual, string tipoAumento, decimal porcentaje, decimal montoAumento, 
             decimal monto, string descripcion, string nombreEmpleado
         )
        {
            this.CodigoCategoria = codigoCategoria;
            this.Id = id;
            this.CodigoEmpleado = codigoEmpleado;
            this.Fecha = fecha;
            this.CategoriaDescripcion = categoria;
            this.SueldoAnterior = sueldoAnterior;
            this.SueldoActual = sueldoActual;
            this.TipoAumento = tipoAumento;
            this.Porcentaje = porcentaje;
            this.MontoAumento = montoAumento;
            this.Monto = monto;
            this.Descripcion = descripcion;
            this.NombreEmpleado = nombreEmpleado;
        }

        public int Id { set; get; }

        public string CodigoEmpleado { set; get; }

        public DateTime Fecha { set; get; }

        public string CodigoCategoria { set; get; }

        public string CategoriaDescripcion { set; get; }

        public decimal SueldoAnterior { set; get; }

        public decimal SueldoActual { set; get; }

        public string TipoAumento { set; get; }

        public decimal Porcentaje { set; get; }

        public decimal MontoAumento { set; get; }

        public decimal Monto { set; get; }

        public string Descripcion { set; get; }

        public string NombreEmpleado { set; get; }

        public int IdEmpleado { set; get; }

        
        public int IdCategoria { set; get; }

        public int IdTipoAumento { set; get; }
        public string DescripcionTipoAumento { set; get; }

        public decimal TotalMonto { set; get; }

    }
}
