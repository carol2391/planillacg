using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;

using nomina.Estructuras;
using nomina.Clases.Seguridad;
using nomina.Clases.Opciones;
using System.Windows.Forms;
using nomina.Clases.Bitacora;

namespace nomina.Clases.Usuarios
{
    public class UsuarioConexion
    {
        Encryptacion encryp;
        public MySqlConnection databaseConnection;
        public UsuarioConexion()
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
            this.databaseConnection = new MySqlConnection(connectionString);
        }

        #region obtener proveedores
        public List<BitacoraData> obtenerBitacora(string usuario, DateTime fechaInicial, DateTime fechaFinal,
          string modulo, string accion)
        {
            List<BitacoraData> list = new List<BitacoraData>();
            string campoNuevo = "";
            string campoAntiguo = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("obtener_bitacora", this.databaseConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_usuario", usuario);
                cmd.Parameters.AddWithValue("@p_fecha_inicial", fechaInicial);
                cmd.Parameters.AddWithValue("@p_fecha_final", fechaFinal);
                cmd.Parameters.AddWithValue("@p_accion", accion);
                cmd.Parameters.AddWithValue("@p_modulo", modulo);

                this.databaseConnection.Open();
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
                this.databaseConnection.Close();
                return list;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return list;
            }


        }
        #endregion

        #region login
        public int login(string usuario, string pass)
        {
            encryp = new Encryptacion();
            int result = 0;
            string passEncryp = encryp.EncryptStringToBytes(pass);
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("login", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("@P_USUARIO", usuario);

                mySqlCommand.Parameters.AddWithValue("@P_PASS", passEncryp);

                //mySqlCommand.Parameters.AddWithValue("@P_PASS", pass);

                mySqlCommand.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
                this.databaseConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                result = Convert.ToInt32(mySqlCommand.Parameters["@P_SALIDA"].Value);
                this.databaseConnection.Close();
                return result;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return result;
            }

        }
        #endregion

        #region obtener usuarios
        public List<UsuarioData> obtenerUsuarios()
        {
            List<UsuarioData> list = new List<UsuarioData>();
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("obtener_usuarios", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                this.databaseConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    mySqlDataReader.GetInt32(6);
                    list.Add(new UsuarioData(mySqlDataReader.GetInt32(0), mySqlDataReader.GetString(1), mySqlDataReader.GetString(2), mySqlDataReader.GetString(3), mySqlDataReader.GetString(4), mySqlDataReader.GetDateTime(5), mySqlDataReader.GetInt32(6)));
                }
                this.databaseConnection.Close();
                return list;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return list;
            }

        }
        #endregion

        #region obtener usuario
        public UsuarioData obtenerUsuario(int id)
        {
            encryp = new Encryptacion();
            UsuarioData result = new UsuarioData();
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("obtener_usuario_id", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("@P_ID", id);
                string pass;
                this.databaseConnection.Open();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    pass = encryp.DecryptStringFromBytes(reader.GetString(4));
                    result = new UsuarioData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), pass, reader.GetDateTime(5), reader.GetInt32(6));
                }
                this.databaseConnection.Close();
                return result;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return result;
            }

        }
        #endregion

        #region buscar usuario por nombre
        public List<UsuarioData> buscarUsuarioNombre(string nombre)
        {
            encryp = new Encryptacion();
            string pass;
            List<UsuarioData> list = new List<UsuarioData>();
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("buscar_usuarios_nombre", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("@P_NOMBRE", nombre);
                this.databaseConnection.Open();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    pass = encryp.DecryptStringFromBytes(reader.GetString(4));

                    list.Add(new UsuarioData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), pass, reader.GetDateTime(5), reader.GetInt32(6)));
                }
                this.databaseConnection.Close();
                return list;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return list;
            }

        }
        #endregion

        #region buscar usuarios
        public List<UsuarioData> buscarUsuarios(string usuario)
        {
            string pass;
            encryp = new Encryptacion();
            List<UsuarioData> list = new List<UsuarioData>();
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("buscar_usuario", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("@P_USUARIO", usuario);
                this.databaseConnection.Open();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    pass = encryp.DecryptStringFromBytes(reader.GetString(4));
                    list.Add(new UsuarioData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), pass, reader.GetDateTime(5), reader.GetInt32(6)));
                }
                this.databaseConnection.Close();
                return list;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return list;
            }

        }
        #endregion

        #region agregar usuario
        public bool agregarUsuario(string nombre, string usuario, string correo, string contrasenia,
            DateTime fecha, String usuarioCreador)
        {
            encryp = new Encryptacion();
            string passEncryptado = encryp.EncryptStringToBytes(contrasenia);
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("insertar_usuario", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("@P_NOMBRE", nombre);
                mySqlCommand.Parameters.AddWithValue("@P_USUARIO", usuario);
                mySqlCommand.Parameters.AddWithValue("@P_USUARIO_CREADOR", usuarioCreador);
                mySqlCommand.Parameters.AddWithValue("@P_CORREO", correo);
                mySqlCommand.Parameters.AddWithValue("@P_CONTRASENIA", passEncryptado);
                mySqlCommand.Parameters.AddWithValue("@P_FECHA_INGRESO", fecha.Date);
                bool result;

                this.databaseConnection.Open();
                int num = mySqlCommand.ExecuteNonQuery();
                if (num == 1)
                {
                    this.databaseConnection.Close();
                    result = true;
                }
                else
                {
                    this.databaseConnection.Close();
                    result = false;
                }
                return result;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return false;
            }


        }
        #endregion

        #region modificar usuario
        public bool modificarUsuario(int id, string nombre, string usuario, string correo,
            string contrasenia, DateTime fecha, int activo, string usuarioCreador)
        {
            encryp = new Encryptacion();
            string passEncryp = encryp.EncryptStringToBytes(contrasenia);
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("modificar_usuario", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("@P_ID", id);
                mySqlCommand.Parameters.AddWithValue("@P_NOMBRE", nombre);
                mySqlCommand.Parameters.AddWithValue("@P_USUARIO", usuario);
                mySqlCommand.Parameters.AddWithValue("@P_CORREO", correo);
                mySqlCommand.Parameters.AddWithValue("@P_CONTRASENIA", passEncryp);
                mySqlCommand.Parameters.AddWithValue("@P_FECHA_INGRESO", fecha.Date);
                mySqlCommand.Parameters.AddWithValue("@P_ACTIVO", activo);
                mySqlCommand.Parameters.AddWithValue("@P_USUARIO_CREADOR", usuarioCreador);
                bool result;
                try
                {
                    this.databaseConnection.Open();
                    int num = mySqlCommand.ExecuteNonQuery();
                    if (num == 1)
                    {
                        this.databaseConnection.Close();
                        result = true;
                    }
                    else
                    {
                        this.databaseConnection.Close();
                        result = false;
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show("Error de la base de datos: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.databaseConnection.Close();
                    result = false;
                }
                return result;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return false;
            }

        }
        #endregion

        #region eliminar usuario
        public bool eliminarUsuario(int id, string usuarioCreador)
        {

            bool result;
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand("eliminar_usuario", this.databaseConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("@P_ID", id);
                mySqlCommand.Parameters.AddWithValue("@P_USUARIO_CREADOR", usuarioCreador);

                this.databaseConnection.Open();
                int num = mySqlCommand.ExecuteNonQuery();
                if (num == 1)
                {
                    this.databaseConnection.Close();
                    result = true;
                }
                else
                {
                    this.databaseConnection.Close();
                    result = false;
                }
                return result;
            }
            catch (Exception e)
            {
                this.databaseConnection.Close();
                MessageBox.Show(e.Message);
                return false;
            }


        }
        #endregion
    }
}
