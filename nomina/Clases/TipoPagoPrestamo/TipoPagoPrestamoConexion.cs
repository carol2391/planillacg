using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace nomina.Clases.TipoPagoPrestamo
{
   public class TipoPagoPrestamoConexion
    {
        Conexion conexion;
        public TipoPagoPrestamoConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region 
        public List<TipoPagoPrestamoData> obtenerTipoPagos()
        {
            List<TipoPagoPrestamoData> lista = new List<TipoPagoPrestamoData>();
            try
            {

                string readCommand = "SELECT * FROM tipo_pago_prestamo";
                MySqlCommand cmd = new MySqlCommand(readCommand, conexion.getConexion());
                cmd.CommandType = CommandType.Text;
                this.conexion.getConexion().Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    lista.Add(new TipoPagoPrestamoData(dataReader.GetInt32(0), dataReader.GetString(1)));

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
