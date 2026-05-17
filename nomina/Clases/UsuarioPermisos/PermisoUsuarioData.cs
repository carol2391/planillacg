using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.PermisosUsuario
{
   public class PermisoUsuarioData
    {
        //public int UsuarioId { set; get; }
        public int PermisoId { set; get; }
        public string Descripcion { set; get; }
        public int Permiso { set; get; }
        //public PermisoUsuarioData(int usuario, int permisoId, int permiso) {
        // this.UsuariosId = usuario;
        //    this.PermisosId = permisoId;
        //    this.Permiso = permiso;
        //}

        //public PermisoUsuarioData(int usuarioId, int permisoId,string descripcion, int permiso)
        //{
        //    this.UsuarioId = usuarioId;
        //    this.PermisoId = permisoId;
        //    this.Descripcion = descripcion;
        //    this.Permiso = permiso;
        //}

        public PermisoUsuarioData(int permisoId, string descripcion, int permiso)
        {
            this.PermisoId = permisoId;
            this.Descripcion = descripcion;
            this.Permiso = permiso;
        }
    }

}
