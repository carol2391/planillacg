using Microsoft.Win32;
using MySql.Data;
using MySql.Data.MySqlClient;
using nomina.Clases.ConexionManager;
using nomina.Estructuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.EmpresasUsuario
{
    public class EmpresaUsuarioConexion
    {
        Conexion conexion;

        ServidorData serverData = new ServidorData();
        Registro registro = new Registro();

        public MySqlConnection databaseConnection;

        public EmpresaUsuarioConexion()
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

        public List<EmpresaUsuarioData> obtenerEmpresasUsuarios(int codigoUsuario)
        {
            List<EmpresaUsuarioData> empresas = new List<EmpresaUsuarioData>();
            MySqlCommand comando = new MySqlCommand("obtener_empresas_usuario", databaseConnection);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_USUARIO_ID", codigoUsuario);
            comando.Parameters.AddWithValue("@P_MODULO", "NOMINA");
            databaseConnection.Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                empresas.Add(new EmpresaUsuarioData(
                               lector.GetInt32(0),
                               lector.GetString(1),
                               lector.GetInt32(2)

                            ));

            }
            this.databaseConnection.Close();
            return empresas;
        }

        public void insertarEmpresasUSuario(int usuario, int permiso, int activo)
        {

            MySqlCommand cmd = new MySqlCommand("insertar_empresas_usuario", databaseConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_USUARIO_ID", usuario);
            cmd.Parameters.AddWithValue("@P_EMPRESA_ID", permiso);
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

        }
     }//fin clase
    }
