using Microsoft.Win32;
using MySql.Data.MySqlClient;
using nomina.Clases.ConexionManager;
using nomina.Clases.Usuarios;
using nomina.Estructuras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace nomina.Clases.PermisosUsuario
{
    class PermisoUsuarioConexion
    {
        Conexion conexion;

        //static string connectionString = "datasource=127.0.0.1;" +
        //                "port=3306;username=root;" +
        //                 "password=;database=empresas;Convert Zero Datetime=True;";

        ServidorData serverData = new ServidorData();
        Registro registro = new Registro();
        
        public MySqlConnection databaseConnection;

        public PermisoUsuarioConexion( )
        {
            serverData = (ServidorData)registro.leerRegistro();
            string connectioUrl = "datasource = " + serverData.server + ";" +
                                 "port = " + serverData.port + ";" +
                                 "username = " + serverData.user + ";" +
                                  "Password = " + serverData.password + ";" +
                                  "database= " + "empresas" + ";" +
                                  " Persist Security Info = True; " +
                                   "Convert Zero Datetime = True; ";

            databaseConnection = new MySqlConnection(connectioUrl);

        }
        public List<PermisoUsuarioData> obtenerPermisos(int codigoUsuario)
        {
            List<PermisoUsuarioData> permisos = new List<PermisoUsuarioData>();
            MySqlCommand comando = new MySqlCommand("obtener_permisos_usuarios", databaseConnection);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_USUARIOS_ID", codigoUsuario);

            databaseConnection.Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                permisos.Add(new PermisoUsuarioData(
                               lector.GetInt32(0),
                               lector.GetString(1),
                               lector.GetInt32(2),
                               lector.GetString(3),
                                lector.GetInt32(4) == 1

                            ));

            }
            this.databaseConnection.Close();
            return permisos;
        }

        public DataGridView CargarMatrizPermisos(int idUsuario, DataGridView dgv)
        {
            // Usamos un diccionario para agrupar las acciones por cada Módulo en una sola línea
            Dictionary<int, ModuloPermisoRow> matrizPivoteada = new Dictionary<int, ModuloPermisoRow>();

            MySqlCommand comando = new MySqlCommand("obtener_permisos_usuarios", databaseConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("P_USUARIOS_ID", idUsuario);

            try
            {
                databaseConnection.Open();
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        int idModulo = lector.GetInt32("id_modulo");
                        string nombreModulo = lector.IsDBNull(1) ? "Sin Nombre" : lector.GetString("nombre_modulo");
                        string nombreAccion = lector.IsDBNull(3) ? "" : lector.GetString("nombre_accion").ToUpper().Trim();
                        bool tienePermiso = lector.GetInt32("tiene_permiso") == 1;

                        // Si es la primera vez que leemos este módulo, creamos su fila base
                        if (!matrizPivoteada.ContainsKey(idModulo))
                        {
                            matrizPivoteada[idModulo] = new ModuloPermisoRow
                            {
                                IdModulo = idModulo,
                                Modulo = nombreModulo
                            };
                        }

                        // Encendemos el CheckBox correspondiente mapeando el nombre de la acción
                        switch (nombreAccion)
                        {
                            case "VER":
                                matrizPivoteada[idModulo].Ver = tienePermiso;
                                break;
                            case "NUEVO":
                                matrizPivoteada[idModulo].Nuevo = tienePermiso;
                                break;
                            case "EDITAR":
                                matrizPivoteada[idModulo].Modificar = tienePermiso;
                                break;
                            case "ELIMINAR":
                                matrizPivoteada[idModulo].Eliminar = tienePermiso;
                                break;
                            case "VER ANTECEDENTES":
                                matrizPivoteada[idModulo].VerAntecedentes = tienePermiso;
                                break;
                        }
                    }
                }

             
                // Convertimos el diccionario en una lista plana para alimentar el DataGridView
                dgv.DataSource = new List<ModuloPermisoRow>(matrizPivoteada.Values);

                // 2. LA CLAVE: Ocultamos el CheckBox en las filas que NO sean "Empleados"
                foreach (DataGridViewRow fila in dgv.Rows)
                {
                    if (fila.DataBoundItem is ModuloPermisoRow filaPermiso)
                    {
                        // Comparamos el nombre del módulo
                        if (filaPermiso.Modulo.ToUpper().Trim() != "EMPLEADO")
                        {
                            // 1. Instanciamos la celda de texto totalmente limpia
                            DataGridViewTextBoxCell celdaTexto = new DataGridViewTextBoxCell();

                            // 2. La asignamos a la fila PRIMERO (así C# ya sabe a qué control pertenece)
                            fila.Cells["VerAntecedentes"] = celdaTexto;

                            // 3. AHORA SÍ, ya podemos configurar el valor y bloquearla de forma segura
                            celdaTexto.Value = string.Empty;
                            celdaTexto.ReadOnly = true;
                        }
                    }
                }
                return dgv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al estructurar matriz: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                databaseConnection.Close();
            }
        }
        public bool existePermiso(int idUsuario, int idModulo, int idAccion)
        {
            MySqlCommand cmd = new MySqlCommand("verificar_acceso_permiso", databaseConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_USUARIO_ID", idUsuario);
            cmd.Parameters.AddWithValue("@P_ACCION_ID", idAccion);
            cmd.Parameters.AddWithValue("@P_MODULO_ID", idModulo);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32).Direction = ParameterDirection.Output;
            //try
            //{
                //Se abre la conexión
                databaseConnection.Open();

                // int filasAfectadas = 
                //int filas = 
                cmd.ExecuteNonQuery();
                int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);
                if (salida >= 1)
                {
                    databaseConnection.Close();
                    return true;
                }
                else
                {
                    databaseConnection.Close();
                    return false;
                }
                

            //}
            //catch (MySqlException)
            //{
            //    databaseConnection.Close();
            //    return false;
            //}

        }//fin funcion

        public void insertarPermisos(int idUsuario, int idModulo, int idAccion, int tienePermiso) {

            MySqlCommand cmd = new MySqlCommand("insertar_permisos_nomina", databaseConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_USUARIO_ID", idUsuario);
            cmd.Parameters.AddWithValue("@P_ACCION_ID", idAccion);
            cmd.Parameters.AddWithValue("@P_MODULO_ID", idModulo);
            cmd.Parameters.AddWithValue("@P_ACTIVO",tienePermiso);
           //  cmd.Parameters.AddWithValue("@_TIPO_LAB", TipoLabor);
            // cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;


            //try
            //{
            //    //Se abre la conexión
              databaseConnection.Open();

                // int filasAfectadas = 
                int filas = cmd.ExecuteNonQuery();
                //  int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);

                if (filas == 1)
                {
                    databaseConnection.Close();
                    //return true;
                }
                else
                {
                    databaseConnection.Close();
                    //return false;
                }

            //}
            //catch (MySqlException)
            //{
            //    databaseConnection.Close();
            //    //return false;
            //}
        }
    }
}
