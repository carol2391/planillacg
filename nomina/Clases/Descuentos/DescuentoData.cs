using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.TipoPago;
using nomina.Clases.TipoJornada;
using nomina.Clases.Opciones;

namespace nomina.Clases.Descuentos
{
   public class DescuentoData
    {
        public DescuentoData() { }
        public DescuentoData(int Id, string codigo, string nombre,
              decimal monto, decimal factor,TipoPagoData tipoPago,
              LOpciones tipoJornada
                
            )
        {
            this.Id = Id;
            this.Codigo = codigo;
            this.Nombre = nombre;       
            this.Monto = monto;
            this.Factor = factor;
            this.TipoJornada = tipoJornada;
            TipoPago = tipoPago;
            this.CodigoCuenta = CodigoCuenta;
        }

        public int Id { set; get; }
        public string Codigo { set; get; }
        public string Nombre { set; get; }
       
        public decimal Monto { set; get; }
        public decimal Factor { set; get; }
       
        public int CodigoCuenta { set; get; }
        public LOpciones TipoJornada { set; get; }
        public TipoPagoData TipoPago { set; get; }

        public string descripcionTPago => TipoPago.descripcion;
        public string descripcionTJornada => TipoJornada.descripcion;

    }
}
