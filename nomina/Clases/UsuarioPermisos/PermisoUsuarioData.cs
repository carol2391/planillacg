using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.PermisosUsuario
{
   public class PermisoUsuarioData
    {

        public PermisoUsuarioData() { 
        }
        public int IdModulo { set; get; }
        public int IdAccion { set; get; }
        public string NombreAccion { set; get; }
        public string NombreModulo { set; get; }
       public bool TienePermiso { set; get; }
        public PermisoUsuarioData(int idModulo, string modulo, int  IdAccion,  string accion,bool tienePermiso)
        {
            this.IdModulo = idModulo;
            this.IdAccion = IdAccion;
            this.NombreAccion = accion;
            this.NombreModulo = modulo;
            this.TienePermiso = tienePermiso;
        }

    }

}
