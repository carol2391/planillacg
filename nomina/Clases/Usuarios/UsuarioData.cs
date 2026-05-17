using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Usuarios
{
    public class UsuarioData
    {

       public int UsuarioId{set;get; }
       public string Usuario { set; get; }
       public string Correo { set; get; } 
       public string Contrasenia { set; get; }

        public string Nombre { set; get; }
        public int Activo { set; get; }
        public DateTime FechaIngreso { set; get; }

        public UsuarioData()
        {
        }
        public UsuarioData(int UsuarioId, string Usuario, string Nombre, string Correo,
            string Contrasenia, DateTime FechaIngreso,int activo)
        {

            this.UsuarioId = UsuarioId;
            this.Usuario = Usuario;
            this.Correo = Correo;
            this.Contrasenia = Contrasenia;
            this.FechaIngreso = FechaIngreso;
            this.Nombre = Nombre;
            this.Activo = activo;
        }
        
        }

    }

