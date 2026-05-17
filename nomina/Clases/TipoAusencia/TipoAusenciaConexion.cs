using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace nomina.Clases.TipoAusencia
{
  public  class TipoAusenciaConexion
    {
        Conexion conexion;
        public TipoAusenciaConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region 
        public List<TipoAusenciaData> obtenerTipoAusencia()
        {
            List<TipoAusenciaData> lista = new List<TipoAusenciaData>();
            try
            {

                string readCommand = "SELECT id_tipo_ausencia,descripcion_larga FROM tipo_ausencia";
                MySqlCommand cmd = new MySqlCommand(readCommand, conexion.getConexion());
                cmd.CommandType = CommandType.Text;
                this.conexion.getConexion().Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    lista.Add(new TipoAusenciaData(dataReader.GetInt32(0), dataReader.GetString(1)));

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
