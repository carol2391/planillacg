using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Ausencia
{

    public class AusenciaData
    {
        public AusenciaData() {

        }


        public AusenciaData(int id,string codigoEmpleado, string tipoAusencia,
               DateTime fechaInicio, DateTime fechaFinal,
               int diasDeAusencia, decimal monto, string codigoNomina, string nombreEmpleado)
        {
            this.Id = id;
            this.CodigoEmpleado = codigoEmpleado;
            this.tipoAusencia = tipoAusencia;
            this.fechaInicio = fechaInicio;
            this.fechaFinal = fechaFinal;
            this.diasDeAusencia = diasDeAusencia;
            this.monto = monto;
            this.codigoNomina = codigoNomina;
            this.nombreEmpleado = nombreEmpleado;
        }

        public int Id { set; get; }
        public string CodigoEmpleado { set; get; }
        public string tipoAusencia { set; get; }
        public DateTime fechaInicio { set; get; }
        public DateTime fechaFinal { set; get; }
        public int diasDeAusencia { set; get; }
        public decimal monto { set; get; }
       
        public string codigoNomina { set; get; }

        public string nombreEmpleado{set;get;}
        public int IdEmpleado { set; get; }
        public string Septimo { set; get; }
        public int IdTipoAusencia { set; get; }
    }
}
