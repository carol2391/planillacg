using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace nomina.Clases.TipoAumento
{
    public class TipoAumentoConexion
    {
        Conexion conexion;
        public TipoAumentoConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region 
        public List<TipoAumentoData> obtenerTipoAumentos()
        {
            List<TipoAumentoData> lista = new List<TipoAumentoData>();
            try
            {

                string readCommand = "SELECT tipo_aumento_id,descripcion FROM tipo_aumento";
                MySqlCommand cmd = new MySqlCommand(readCommand, conexion.getConexion());
                cmd.CommandType = CommandType.Text;
                this.conexion.getConexion().Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    lista.Add(new TipoAumentoData(dataReader.GetInt32(0), dataReader.GetString(1)));

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
