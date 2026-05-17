using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.Empleado;

//
namespace nomina.Clases.Departamento
{   /// <summary>
    /// estructura del departamento
    /// </summary>
    public class DepartamentoData
    {
        public DepartamentoData(int id ,string CodigoDepartamento, string nombreDepartamento, string nombreEncargado, int codigoCuenta)
        {
            this.CodigoDepartamento = CodigoDepartamento;
            this.NombreDepartamento = nombreDepartamento;
            this.NombreEncargado = nombreEncargado;
            this.CodigoCuenta = codigoCuenta;
            this.Id = id;
        }
        public DepartamentoData() {
            this.empleados = new List<EmpleadoData>();
        }
        public int Id { set; get; }
        public string CodigoDepartamento { set; get; }
        public string NombreDepartamento { set; get; }
        public string NombreEncargado { set; get; }
        public int CodigoCuenta { set; get; }

        public List<EmpleadoData> empleados { set; get; }
    }
}
