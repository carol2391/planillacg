using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Movimientos.MovimientoPrestamo
{
    public class PrestamoData
    {
        public PrestamoData() { }

        /*para cargar datos en el datagrid*/
        public PrestamoData(int id, string codigoEmpleado,
            string codigoPrestamo, DateTime fecha, string descripcion,
            string tipoPago, decimal monto, decimal cuotaMes, int tiempo,
            decimal montoActual, string nombreEmpleado, string nombreDepartamento,
            string nombreCategoria, string estado
            )
        {
            this.Id = id;
            this.CodigoEmpleado = codigoEmpleado;
            this.CodigoPrestamo = codigoEmpleado;
            this.Fecha = fecha;
            this.Descripcion = descripcion;
            this.TipoPago = tipoPago;
            this.Monto = monto;
            this.CuotaMes = cuotaMes;
            this.Tiempo = tiempo;
            this.MontoActual = montoActual;
            this.NombreEmpleado = nombreEmpleado;
            this.NombreDepartamento = nombreDepartamento;
            this.NombreCategoria = nombreCategoria;
            this.Estado = estado;
        }

        public PrestamoData(int id, string codigoEmpleado,
           string codigoPrestamo, DateTime fecha, string descripcion,
           string tipoPago, decimal monto, decimal cuotaMes, int tiempo,
           decimal montoActual,string estado
           )
        {
            this.Id = id;
            this.CodigoEmpleado = codigoEmpleado;
            this.CodigoPrestamo = codigoEmpleado;
            this.Fecha = fecha;
            this.Descripcion = descripcion;
            this.TipoPago = tipoPago;
            this.Monto = monto;
            this.CuotaMes = cuotaMes;
            this.Tiempo = tiempo;
            this.MontoActual = montoActual;
            this.Estado = estado;
           
        }

        public int Id { set; get; }
        public string CodigoEmpleado { set; get; }
        public string CodigoPrestamo { set; get; }

        public string Descripcion { set; get; }

        public DateTime Fecha { set; get; }

        public string TipoPago { set; get; }

        public decimal Monto { set; get; }

        public decimal CuotaMes { set; get; }

        public int Tiempo { set; get; }

        public decimal MontoActual { set; get; }

        public string NombreEmpleado { set; get; }
        public string NombreDepartamento { set; get; }

        public string NombreCategoria { set; get; }

        public string Estado { set; get; }

        public decimal Cuota { set; get; }
        public int IdTipoPago { set; get; }
        public string DescripcionTipoPago { set; get; }
    }
}
