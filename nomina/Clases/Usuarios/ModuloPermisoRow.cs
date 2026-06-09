using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Usuarios
{
    public class ModuloPermisoRow
    {
        public int IdModulo { get; set; }
        public string Modulo { get; set; }
        public bool Ver { get; set; }
        public bool Nuevo { get; set; }
        public bool Modificar { get; set; }
        public bool Eliminar { get; set; }
        public bool VerAntecedentes { get; set; }
        public bool VerHistorialAumento { get; set; }
    }
}
