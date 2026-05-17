using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.TipoJornada;
using nomina.Clases.TipoPago;
using nomina.Clases.Opciones;
namespace nomina.Clases.Labores
{
    public class LaboresData
    {
        public LaboresData() { }
        public LaboresData(int Id,  string CodigoLabor, string nombreLabor,
                decimal valor, decimal FactorLabor,
                TipoPagoData tipoPago, LOpciones tipoJornada,int idCuenta
            )
        {
            this.Id = Id;
            this.Codigo = CodigoLabor;
            this.Nombre = nombreLabor;
            this.TipoJornada = TipoJornada;
           //this.Monto = MontoLabor;
            this.Valor = valor;
            this.Factor = FactorLabor;
            this.TipoPago = tipoPago;
            this.TipoJornada = tipoJornada;
            this.idCodigoCuenta = idCuenta;
        }
        public decimal Valor { set; get; }
        public int Id { set; get; }
        public string Codigo { set; get; }
        public string Nombre { set; get; }
        public LOpciones TipoJornada { set; get; }
        public decimal Monto { set; get; }
        public decimal Factor { set; get; }

        public TipoPagoData TipoPago { set; get; }
        public int idCodigoCuenta { set; get; }
        public string TipoPagoD => TipoPago.descripcion;
        public string Jornada => TipoJornada.descripcion;
             
    }
}
