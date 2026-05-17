using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using nomina.Clases.Opciones;
using nomina.Clases.Utilidades;
using nomina.Estructuras;

namespace nomina.Clases.Bitacora
{
    public class BitacoraConexion
    {
        private Conexion conexion;
        public BitacoraConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener bitacora
        public List<BitacoraData> obtenerBitacora(string usuario, DateTime fechaInicial, DateTime fechaFinal,
          string modulo, string accion)
        {
            List<BitacoraData> list = new List<BitacoraData>();
            string campoNuevo = "";
            string campoAntiguo = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("obtener_bitacora", this.conexion.getConexion());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_usuario", usuario);
                cmd.Parameters.AddWithValue("@p_fecha_inicial", fechaInicial);
                cmd.Parameters.AddWithValue("@p_fecha_final", fechaFinal);
                cmd.Parameters.AddWithValue("@p_accion", accion);
                cmd.Parameters.AddWithValue("@p_modulo", modulo);

                this.conexion.getConexion().Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(7))
                        campoNuevo = dataReader.GetString(7);
                    else
                        campoNuevo = " ";
                    if (!dataReader.IsDBNull(6))
                        campoAntiguo = dataReader.GetString(6);
                    else
                        campoAntiguo = " ";

                    list.Add(new BitacoraData(dataReader.GetDateTime(0), dataReader.GetString(1),
                        dataReader.GetString(2), dataReader.GetString(3), dataReader.GetInt32(4),
                        dataReader.GetString(5), campoAntiguo, campoNuevo));
                }
                this.conexion.getConexion().Close();
                return list;
            }
            catch (Exception e)
            {
                this.conexion.getConexion().Close();
                MessageBox.Show(e.Message);
                return list;
            }


        }
        #endregion

        #region obtener bitacora
        public List<BitacoraData> obtenerBitacoraEmpresa(string usuario, DateTime fechaInicial, DateTime fechaFinal,
          string modulo, string accion)
        {
            Registro registro = new Registro();
            ServidorData servidorData = registro.leerRegistro();
            string connectionString = string.Concat(new string[]
            {
                "datasource = ",
                servidorData.server,
                ";port = ",
                servidorData.port,
                ";username = ",
                servidorData.user,
                ";password = ",
                servidorData.password,
                ";database= empresas;Convert Zero Datetime = True; "
            });
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            List<BitacoraData> list = new List<BitacoraData>();
            string campoNuevo = "";
            string campoAntiguo = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("obtener_bitacora", databaseConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_usuario", usuario);
                cmd.Parameters.AddWithValue("@p_fecha_inicial", fechaInicial);
                cmd.Parameters.AddWithValue("@p_fecha_final", fechaFinal);
                cmd.Parameters.AddWithValue("@p_accion", accion);
                cmd.Parameters.AddWithValue("@p_modulo", modulo);

                databaseConnection.Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(7))
                        campoNuevo = dataReader.GetString(7);
                    else
                        campoNuevo = " ";
                    if (!dataReader.IsDBNull(6))
                        campoAntiguo = dataReader.GetString(6);
                    else
                        campoAntiguo = " ";

                    list.Add(new BitacoraData(dataReader.GetDateTime(0), dataReader.GetString(1),
                        dataReader.GetString(2), dataReader.GetString(3), dataReader.GetInt32(4),
                        dataReader.GetString(5), campoAntiguo, campoNuevo));
                }
                databaseConnection.Close();
                return list;
            }
            catch (Exception e)
            {
                databaseConnection.Close();
                MessageBox.Show(e.Message);
                return list;
            }


        }
        #endregion
        public List<LOpciones> obtenerModulos()
        {
            string readCommand = "SELECT ID_MODULO, MODULO FROM MODULO";
            List<LOpciones> modulos = new List<LOpciones>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(readCommand, this.conexion.getConexion());
                cmd.CommandType = CommandType.Text;
                this.conexion.getConexion().Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    modulos.Add(new LOpciones(reader.GetInt32(0).ToString(), reader.GetString(1)));
                }

                this.conexion.getConexion().Close();
                return modulos;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error base de datos: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexion.getConexion().Close();
            }

            return modulos;
        }

    }
}
