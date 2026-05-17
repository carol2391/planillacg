using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.Empleado;
namespace nomina.Clases.Categoria
{
    public class CategoriaData
    {
        public CategoriaData() {
            empleados = new List<EmpleadoData>();
        }
        public CategoriaData(int Id, string CodigoCategoria, string NombreCategoria,
                               decimal SalarioInicial, decimal SalarioFinal)
        {
            this.Id = Id;
            this.CodigoCategoria = CodigoCategoria;
            this.NombreCategoria = NombreCategoria;
            this.SalarioInicial = SalarioInicial;
            this.SalarioFinal = SalarioFinal;
            empleados = new List<EmpleadoData>();
        }
       public int Id { set;get; }
       public string CodigoCategoria { set; get; }
       public string NombreCategoria { set; get; }
       public decimal SalarioInicial { set; get; }
       public decimal SalarioFinal { set; get; }
       public List<EmpleadoData> empleados { set; get; }
        
    }
}
