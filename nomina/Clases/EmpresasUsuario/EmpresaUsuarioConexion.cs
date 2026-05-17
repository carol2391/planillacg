using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace nomina.Clases.EmpresasUsuario
{
    public class EmpresaUsuarioConexion
    {
        Conexion conexion;

        static string connectionString = "datasource=127.0.0.1;" +
                        "port=3306;username=root;" +
                         "password=C0ntrolG3rencial2019;database=empresas;Convert Zero Datetime=True;";

        //static string connectionString = "datasource=127.0.0.1;" +
        //          "port=3306;username=root;" +
        //           "password=;database=empresas;Convert Zero Datetime=True;";

        public MySqlConnection databaseConnection;

        public EmpresaUsuarioConexion(
            //Conexion con
            )
        {
            databaseConnection = new MySqlConnection(connectionString);
            //this.conexion = con;
        }

        public List<EmpresaUsuarioData> obtenerEmpresasUsuarios(int codigoUsuario)
        {
            List<EmpresaUsuarioData> empresas = new List<EmpresaUsuarioData>();
            MySqlCommand comando = new MySqlCommand("obtener_empresas_usuario", databaseConnection);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_USUARIO_ID", codigoUsuario);

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
