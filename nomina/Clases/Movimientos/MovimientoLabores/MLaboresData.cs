using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.TipoPago;
using nomina.Clases.Empleado;
using System.Security.Authentication.ExtendedProtection;

namespace nomina.Clases.MovimientoLabores
{
    public class MLaboresData
    {
        public MLaboresData() { }

        public MLaboresData( int idEmpleado, int idLabor,
               string DescripcionLabor, 
               decimal CantidaLabor, DateTime FechaLabor, decimal MontoLabor,
               int idCuenta,int idTipoPago,EmpleadoData empleado, string tipoLabor, int id)
        {
            this.Empleado = empleado;
            this.IdLabor = idLabor;
            this.DescripcionLabor = DescripcionLabor;
            this.TipoLabor = TipoLabor;
            this.CantidaLabor = CantidaLabor;
            this.FechaLabor = FechaLabor;
            this.MontoLabor = MontoLabor;
            this.IdCuenta = idCuenta;
            this.IdEmpleado = idEmpleado;
            //this.NombreCuenta = nombreCuenta;
            this.TipoLabor = tipoLabor;
            TipoPago = new TipoPagoData(idTipoPago,TipoLabor);
            this.MontoTotal = CantidaLabor * MontoLabor;
            this.Id = Id;
        }

        public MLaboresData(int idEmpleado, int idLabor,
               string DescripcionLabor,
               string TipoLabor,
               decimal CantidaLabor, DateTime FechaLabor, decimal MontoLabor,
               int idCuenta, int idTipoPago, EmpleadoData empleado, int id)
        {
            this.Empleado = empleado;
            this.IdLabor = idLabor;
            this.DescripcionLabor = DescripcionLabor;
            this.TipoLabor = TipoLabor;
            this.CantidaLabor = CantidaLabor;
            this.FechaLabor = FechaLabor;
            this.MontoLabor = MontoLabor;
            this.IdCuenta = idCuenta;
            this.IdEmpleado = idEmpleado;
            this.Id = id;
            this.MontoTotal = CantidaLabor * MontoLabor;
            //this.NombreCuenta = nombreCuenta;

            TipoPago = new TipoPagoData(idTipoPago, TipoLabor);
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

        public int Id { set; get; }
        public int idNomina { set; get; }

        public int IdEmpleado { set; get; }
        public int IdLabor { set; get; }
        public string DescripcionLabor { set; get; }
        public string TipoLabor { set; get; }
        public decimal CantidaLabor { set; get; }
        public DateTime FechaLabor { set; get; }
        public decimal MontoLabor { set; get; }
        public int IdCuenta { set; get; }
        public string NombreCuenta { set; get; }
        public string NombreEmpleado => Empleado.Nombre;
        public string CodigoEmpleado => Empleado.Codigo;
        public decimal MontoTotal { set; get; }
        public TipoPagoData TipoPago { set; get; }
        public EmpleadoData Empleado { set; get; }
        public string TipoPagoD { set; get; }
    }
}
