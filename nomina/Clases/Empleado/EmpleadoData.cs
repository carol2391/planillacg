using nomina.Clases.Categoria;
using nomina.Clases.Departamento;
using System;
using nomina.Clases.Opciones;
namespace nomina.Clases.Empleado
{
    public class EmpleadoData
    {
        public EmpleadoData() {
            this.objCategoria= new CategoriaData();
            this.objDepto = new DepartamentoData();
        }

        public EmpleadoData(String codigoEmpleado, string nombreEmpleado, decimal sueldo, 
                          string nombreDepto, string codigoCategora, string nombreCategoria,
                            decimal salarioInicial, decimal salarioFinal)
        {
            this.objCategoria = new CategoriaData();
            this.objDepto = new DepartamentoData();

            this.Codigo = codigoEmpleado;
            this.Nombre = nombreEmpleado;
            this.Sueldo = sueldo;
            this.objCategoria.CodigoCategoria = codigoCategora;
            this.objCategoria.NombreCategoria = nombreCategoria;

            this.objCategoria.SalarioInicial = salarioInicial;
            this.objCategoria.SalarioFinal = salarioFinal;
          
        }

        public EmpleadoData(int id, string Codigo, string nombre,
                   DateTime FechaNacimiento,
                   string Identidad, string EstadoCivil, string Pasaporte, string RTN,
                   string Antecedentes,
                   string IHS, string direccion, string Telefono, DateTime FechaIngreso,
                   string Sexo, LOpciones TipoEmpleado, string PuestoAsignado,
                   decimal Sueldo, string A_IHS, string A_FSV, string A_SIN, string A_ISR,
                   string TipoPago, string Bancos, string NCuenta,
                   int idDepto, string depto, int idCategoria, string categoria,
                   string celular,string residencia, string licencia,string codigoCategoria,
                   decimal salarioInicial, decimal salarioFinal

        )
        {
            this.objCategoria = new CategoriaData();
            this.objDepto = new DepartamentoData();
            this.Id = id;
            this.Codigo = Codigo;
            this.Nombre = nombre;
            this.FechaNacimiento = FechaNacimiento;
            this.Identidad = Identidad;
            this.EstadoCivil = EstadoCivil;
            this.Pasaporte = Pasaporte;
            this.RTN = RTN;
            this.Antecedentes = Antecedentes;
            this.IHS = IHS;
            this.Direccion = direccion;
            this.Telefono = Telefono;
            this.FechaIngreso = FechaIngreso;
            this.Sexo = Sexo;
            this.TipoEmpleado = TipoEmpleado;
            this.PuestoAsignado = PuestoAsignado;
            this.Sueldo = Sueldo;
            this.A_IHS = A_IHS;
            this.A_FSV = A_FSV;
            this.A_SIN = A_SIN;
            this.A_ISR = A_ISR;
            
            this.Bancos = Bancos;
            this.NCuenta = NCuenta;
            this.objDepto.Id = idDepto;
            this.objDepto.NombreDepartamento = depto;

            this.objCategoria.Id = idCategoria;
            this.objCategoria.CodigoCategoria = codigoCategoria;
            this.objCategoria.NombreCategoria = categoria;
            this.objCategoria.SalarioInicial = salarioInicial;
            this.objCategoria.SalarioFinal = salarioFinal;

             this.Celular = celular;
            this.Residencia = residencia;
            this.Licencia = licencia;
        }
        public int Id { set; get; }
        public string Codigo { set; get; }
        public string Nombre { set; get; }
        public DateTime FechaNacimiento { set; get; }
        public string Identidad { set; get; }
        public string EstadoCivil { set; get; }
        public string Pasaporte { set; get; }
        public string RTN { set; get; }
        public string Antecedentes { set; get; }
        public string IHS { set; get; }
        public string Direccion { set; get; }
        public string Telefono { set; get; }
        public DateTime FechaIngreso { set; get; }
       public string Sexo { set; get; }
       //public string TipoEmpleado { set; get; }
       public CategoriaData objCategoria { set; get; }
        public DepartamentoData objDepto { set; get; }
        public string PuestoAsignado { set; get; }
        public decimal Sueldo { set; get; }
        public string A_IHS { set; get; }
        public string A_FSV { set; get; }
        public string A_SIN { set; get; }
        public string A_ISR{ set; get; }
        
        public string Bancos { set; get; }
        public string NCuenta { set; get; }
        public string nombreCategoria => objCategoria.NombreCategoria;
        public string nombreDepto => objDepto.NombreDepartamento;

        public string Residencia { set; get; }

        public string Licencia { set; get; }

        public string Celular { set; get; }
        public LOpciones TipoEmpleado { set; get; }
        public LOpciones TipoPago { set; get; }
        public DateTime FechaInicio { set; get; }
        public string TipoEmpleadoNacionalidad { set; get; }

        public decimal CuentaSueldo{ set; get; }
        public decimal CuentaSeguroSocial { set; get; }
        public decimal CuentaRegimenEspecial { set; get; }
        public decimal CuentaISR { set; get; }
        public decimal OtraCuent1 { set; get; }
        public decimal OtraCuenta2 { set; get; }
        public decimal NumeroCuenta { set; get; }
        
    }
}
