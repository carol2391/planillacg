using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using nomina.Estructuras;

namespace nomina.Clases.ConexionManager
{
    public class Conexion
    {
        static string query;
        // Prepara la conexión
        public static MySqlConnection databaseConnection;
        //MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

        public Conexion(string baseDeDatos)
        {
            ServidorData serverData = new ServidorData();
            Registro registro = new Registro();
            serverData = (ServidorData)registro.leerRegistro();

            string connectioUrl = "datasource = " + serverData.server + ";" +
                                  "port = " + serverData.port + ";" +
                                  "username = " + serverData.user + ";" +
                                   "Password = " + serverData.password + ";" +
                                   "database= " + baseDeDatos + ";" +
                                   " Persist Security Info = True; " +
                                    "Convert Zero Datetime = True; ";

            //return connectioUrl;
            databaseConnection = new MySqlConnection(connectioUrl);
        }

        public MySqlConnection getConexion()
        {
            return databaseConnection;
        }
    }
}
