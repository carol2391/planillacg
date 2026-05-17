using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace nomina.Clases.Empresas
{
    public class EmpresaData
    {

        public string Codigo { set; get; }
        public string Nombre { set; get; }
        public DateTime Fecha { set; get; }
        public string Direccion { set; get; }
        public string RTN { set; get; }
        public string Telefono { set; get; }
        public string DirConeccion { set; get; }
        public int Id { set; get; }
        public string Correo { set; get; }
        public Image Image { set; get; }
        public int NominaFlag { set; get; }
        public EmpresaData() { }

        public EmpresaData(int id, string Codigo, string Nombre,
            DateTime Fecha, string Direccion, string RTN, string Telefono, string DirConeccion, string correo, Image imagen, int retencionFlag)
        {
            this.Codigo = Codigo;
            this.Nombre = Nombre;
            this.Fecha = Fecha;
            this.Direccion = Direccion;
            this.RTN = RTN;
            this.Telefono = Telefono;
            this.DirConeccion = DirConeccion;
            this.Id = id;
            this.Correo = correo;
            this.Image = imagen;
            this.NominaFlag = retencionFlag;
        }
    }
}
