using nomina.Clases.ConexionManager;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.Seguridad;
using nomina.Forms.Main;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace nomina.Clases.UsuarioPermisos
{
    public static class Validator
    {
        private static readonly PermisoUsuarioConexion BdPermisos = new PermisoUsuarioConexion();



        public static bool ExistePermiso(int idUsuario, int idModulo, int idAccion, Button btn)
        {
            // Si es Súper Usuario, siempre tiene permiso
            if (Session.superUsuario)
            {
                btn.Visible = true;
                return true;
            }

            // Consultamos a la base de datos
            bool tienePermiso = BdPermisos.existePermiso(idUsuario, idModulo, idAccion);

            // Asignamos el estado visual
            btn.Visible = tienePermiso;

            // Retornamos el valor real para usarlo en el reacomodo de posiciones
            return tienePermiso;
        }

        public static void validarPermisos(int usuarioId , Button btnNuevo, Button btnModificar,  Button bntQuitar, Button btnSalir, Form frm, int moduloId)
        {
            // 1. Validamos los accesos y guardamos el resultado REAL en variables
            bool tieneNuevo = Validator.ExistePermiso(usuarioId, moduloId, 2, btnNuevo);
            bool tieneModificar = Validator.ExistePermiso(usuarioId, moduloId, 3, btnModificar);
            bool tieneQuitar = Validator.ExistePermiso(usuarioId, moduloId, 4, bntQuitar);
          
            bool tieneSalir = true;
            btnSalir.Visible = true;

            // 2. Creamos la lista amarrando el botón con su visibilidad real de la BD
            var listaBotones = new List<(Button Boton, bool Mostrar)>
            {
                (btnNuevo, tieneNuevo),
                (btnModificar, tieneModificar),
                (bntQuitar, tieneQuitar),
                (btnSalir, tieneSalir)
            };
            int yActual;
            if (moduloId >= 8 && moduloId <= 12)
            {
                yActual = 120;
            }
            else {
                yActual = 90;
            }
 
            int espacioEntreBotones = 5; // Separación en píxeles entre un botón y otro

            foreach (var item in listaBotones)
            {
                if (item.Mostrar)
                {
                    // Calculamos X restando el ancho del botón y un margen de 20px al ancho total del formulario
                    // Esto evita usar números fijos y hace que se pegue al borde derecho de forma limpia
                    int xDinamico = frm.ClientSize.Width - item.Boton.Width - 3;

                    // Asignamos la nueva posición calculada
                    item.Boton.Location = new Point(xDinamico, yActual);

                    // Acumulamos para el siguiente botón: posición actual + alto del botón + el espacio de separación
                    yActual += item.Boton.Height + espacioEntreBotones;
                }
            }
        }
    }
}
