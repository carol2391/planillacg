using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using nomina.Clases.Opciones;
using nomina.Clases.ConexionManager;
using System.Data;
using System.Windows.Forms;
namespace nomina.Clases.TipoJornada
{
    public class TipoJornadaConexion
    {
        Conexion conexion;
        public TipoJornadaConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region existe el usuario en date
        public List<LOpciones> obtenerTipoJornada()
        {
            List<LOpciones> lista = new List<LOpciones>();
            try
            {
                
                string readCommand = "SELECT * FROM tipo_jornada";
                MySqlCommand cmd = new MySqlCommand(readCommand, conexion.getConexion());
                cmd.CommandType = CommandType.Text;
                this.conexion.getConexion().Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    
                    lista.Add(new LOpciones(dataReader.GetInt32(0),dataReader.GetString(1)));
                    ;
                }
                this.conexion.getConexion().Close();
                return lista;
            }
            catch (Exception e)
            {
                this.conexion.getConexion().Close();
                MessageBox.Show(e.Message);
                return lista;
            }
        }

        #endregion
    }
}
