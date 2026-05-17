using MySql.Data.MySqlClient;
using nomina.Clases.ConexionManager;
using System;
using System.Collections.Generic;
using System.Data;

namespace nomina.Clases.PermisosUsuario
{
    class PermisoUsuarioConexion
    {
        Conexion conexion;

        static string connectionString = "datasource=127.0.0.1;" +
                        "port=3306;username=root;" +
                         "password=;database=empresas;Convert Zero Datetime=True;";

        //static string connectionString = "datasource=127.0.0.1;" +
        //          "port=3306;username=root;" +
        //           "password=;database=empresas;Convert Zero Datetime=True;";

        public MySqlConnection databaseConnection;

        public PermisoUsuarioConexion(
            //Conexion con
            )
        {
            databaseConnection = new MySqlConnection(connectionString);
            //this.conexion = con;
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
                               lector.GetInt32(2)
                               
                            ));

            }
            this.databaseConnection.Close();
            return permisos;
        }

        public bool existePermiso(int codigoUsuario, int codigoPermiso)
        {
            MySqlCommand cmd = new MySqlCommand("verificar_acceso_permiso", databaseConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_USUARIOS_ID", codigoUsuario);
            cmd.Parameters.AddWithValue("@P_PERMISOS_ID", codigoPermiso);
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

        public void insertarPermisos(int usuario,int permiso, int activo) {

            MySqlCommand cmd = new MySqlCommand("insertar_permisos", databaseConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_USUARIO_ID", usuario);
            cmd.Parameters.AddWithValue("@P_PERMISO_ID", permiso);
            cmd.Parameters.AddWithValue("@P_ACTIVO", activo);
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
