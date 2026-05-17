using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.TipoPago;
using nomina.Clases.Empleado;

namespace nomina.Clases.MovimientoLabores
{
    public class MLaboresData
    {
        public MLaboresData() { }

        public MLaboresData(int idEmpleaddo, int idLabor,
               string DescripcionLabor, string TipoLabor,
               decimal CantidaLabor, DateTime FechaLabor, decimal MontoLabor,
               int idCuenta,int idTipoPago,EmpleadoData empleado)
        {
            this.empleado = empleado;
            this.idLabor = idLabor;
            this.DescripcionLabor = DescripcionLabor;
            this.TipoLabor = TipoLabor;
            this.CantidaLabor = CantidaLabor;
            this.FechaLabor = FechaLabor;
            this.MontoLabor = MontoLabor;
            this.IdCuenta = idCuenta;
            //this.NombreCuenta = nombreCuenta;
          
            TipoPago = new TipoPagoData(idTipoPago,TipoLabor);
        }

        //public MLaboresData(int CodigoEmpleado, int CodigoLabor,
        //    string DescripcionLabor, string TipoLabor,
        //    decimal CantidaLabor, DateTime FechaLabor, decimal MontoLabor,
        //    int CodigoCuenta, int idNomina,int idTipoPago,EmpleadoData empleado)
        //{
        //    this.empleado = empleado;
        //    this.idEmpleado = CodigoEmpleado;
        //    this.idLabor = CodigoLabor;
        //    this.DescripcionLabor = DescripcionLabor;
        //    this.TipoLabor = TipoLabor;
        //    this.CantidaLabor = CantidaLabor;
        //    this.FechaLabor = FechaLabor;
        //    this.MontoLabor = MontoLabor;
        //    this.IdCuenta = CodigoCuenta;
        //    this.idNomina= idNomina;
        //    TipoPago = new TipoPagoData(idTipoPago, TipoLabor);

        //}

        public int idNomina { set; get; }

        public int idEmpleado { set; get; }
        public int idLabor { set; get; }
        public string DescripcionLabor { set; get; }
        public string TipoLabor { set; get; }
        public decimal CantidaLabor { set; get; }
        public DateTime FechaLabor { set; get; }
        public decimal MontoLabor { set; get; }
        public int IdCuenta { set; get; }
        public string NombreCuenta { set; get; }
        public string NombreEmpleado => empleado.Nombre;
        public string CodidgoEmpleado => empleado.Codigo;
        public decimal Total { set; get; }
        public TipoPagoData TipoPago { set; get; }
        public EmpleadoData empleado { set; get; }
        public string TipoPagoD => TipoPago.descripcion;
    }
}
