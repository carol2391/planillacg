using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Parametro
{
   public  class ParametroData
    {
        public ParametroData() { }

        public ParametroData(int Id, int Periodo, decimal Excento,
           decimal RangoInicial15,
            decimal RangoFinal15, decimal RangoInicial20, decimal RangoFinal20,
            decimal RangoInicial25, decimal RangoFinal25, decimal sueldoPromedio,
            decimal reservaLaboral, decimal valorPiso, decimal valorTecho, decimal
            salarioMinimoPromedio
            )
        {
                this.Id = Id;
                this.Periodo = Periodo;
                this.Excento = Excento;
                this.RangoInicial15 = RangoInicial15;
                this.RangoFinal15 = RangoFinal15;
                this.RangoInicial20 = RangoInicial20;
                this.RangoFinal20 = RangoFinal20;
                this.RangoInicial25 = RangoInicial25;
                this.RangoFinal25 = RangoFinal25;
                this.SueldoPromedio = sueldoPromedio;
                this.ReservaLaboralRAP = reservaLaboral;
                this.ValorPisoRap = valorPiso;
                this.ValorTechoIHSS = valorTecho;
                this.SalarioMinimoPromedio = salarioMinimoPromedio;

        }

        public int Id { set; get; }
        public int Periodo { set; get; }
        public decimal Excento { set; get; }
        public decimal RangoInicial10 { set; get; }
        public decimal RangoFinal10{ set; get; }
        public decimal RangoInicial15 { set; get; }
        public decimal RangoFinal15 { set; get; }
        public decimal RangoInicial20 { set; get; }
        public decimal RangoFinal20 { set; get; }
        public decimal RangoInicial25 { set; get; }
        public decimal RangoFinal25 { set; get; }

        public decimal SueldoPromedio { set; get; }

        public decimal ReservaLaboralRAP { set; get; }
        public decimal ValorPisoRap { set; get; }
        public decimal ValorTechoIHSS { set; get; }
        public decimal SalarioMinimoPromedio { set; get; }
    }
}
