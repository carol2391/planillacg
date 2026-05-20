using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.TipoPago;
using nomina.Clases.Empleado;
using nomina.Clases.Descuentos;

namespace nomina.Clases.MovimiendoDescuentos
{
   public  class MDescuentoData
    {
        public MDescuentoData() { }

        //public MDescuentoData(
        //       DescuentoData descuento,
        //       decimal CantidadDescuento, DateTime FechaDescuento, decimal MontoDescuento,
        //       int idCuenta, int idTipoPago, EmpleadoData empleado)
        //{
           
        //    this.TipoDescuento = TipoDescuento;
        //    this.CantidadDescuento = CantidadDescuento;
        //    this.FechaDescuento = FechaDescuento;
        //    this.MontoDescuento = MontoDescuento;
        //    this.CodigoCuenta = CodigoCuenta;
        //    this.objDescuento = descuento;
        //    this.objEmpleado = empleado;
        //}

       

        public string CodigoEmpleado=>this.objEmpleado.Codigo;
        public string NombreEmpleado => this.objEmpleado.Nombre;
        public string TipoDescuento => this.objTipoPago.descripcion;
        public decimal CantidadDescuento{ set; get; }
        public DateTime FechaDescuento { set; get; }
        public decimal MontoDescuento { set; get; }
        public string CodigoCuenta { set; get; }
        public string NombreCuenta { set; get; }
        public int IdCuenta { set; get; }
        public decimal Total { set; get; }
        public TipoPagoData objTipoPago { set; get; }
        public EmpleadoData objEmpleado { set; get; }
        public DescuentoData objDescuento { set; get; }
        public string TipoPagoD { set; get; }
        public string CodigoDescuento => objDescuento.Codigo;
        public string DescripcionDescuento => objDescuento.Nombre;
        public int idDescuento => objDescuento.Id;
        public int idEmpleado => objEmpleado.Id;
        public int idMDescuento { set; get; }
    }
}
