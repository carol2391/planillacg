using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.EmpresasUsuario
{
   public class EmpresaUsuarioData
    {
        public int PermisoId { set; get; }
        public string Descripcion { set; get; }
        public int Permiso { set; get; }

        public EmpresaUsuarioData(int empresaId, string nombre, int activo)
        {
            this.PermisoId = empresaId;
            this.Descripcion = nombre;
            this.Permiso = activo;
        }
    }
}
