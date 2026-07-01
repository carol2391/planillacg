using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.Empresas;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using System.Windows.Forms;
using nomina.Estructuras;
using nomina.Clases.ConexionManager;
using nomina.Clases.Seguridad;
using System.Drawing;
using nomina.Clases.Opciones;

namespace nomina.Clases.Empresas
{
    public class EmpresaConexion
    {
        
            private static string connectionString;
            public MySqlConnection databaseConnection;
            private Registro registro = new Registro();
            private ServidorData serverData;
            Encryptacion seguridad = new Encryptacion();
            public EmpresaConexion()
            {
                this.serverData = this.registro.leerRegistro();
                EmpresaConexion.connectionString = string.Concat(new string[]
            {
                "datasource = ",
                this.serverData.server,
                ";port = ",
                this.serverData.port,
                ";username = ",
                this.serverData.user,
                ";password = ",
                this.serverData.password,
                ";database= empresas;Convert Zero Datetime = True;"
            });
                this.databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
            }


            #region existe el usuario en date
            public bool existeUsuarioDate(string codigoEmpresa, string user)
            {

                try
                {
                    int existe;
                    EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", "cr" + codigoEmpresa);
                    EmpresaConexion.connectionString += "Allow User Variables=True;";
                    this.databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                    string readCommand = "SELECT COUNT(USER) FROM DATES  WHERE USER=@p_user";
                    MySqlCommand cmd = new MySqlCommand(readCommand, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@p_user", user);
                    cmd.CommandType = CommandType.Text;
                    this.databaseConnection.Open();
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        existe = dataReader.GetInt32(0);
                        if (existe == 1)
                        {
                            this.databaseConnection.Close();
                            return true;
                        }
                        else
                        {
                            this.databaseConnection.Close();
                            return false;
                        }
                    }
                    else
                    {
                        this.databaseConnection.Close();
                        return false;
                    }

                }
                catch (Exception e)
                {
                    this.databaseConnection.Close();
                    MessageBox.Show(e.Message);
                    return false;
                }
            }

            #endregion

            #region existe el usuario en date
            public int[] obtenerMesYAnioDate(string codigoEmpresa, string user)
            {
                int[] arreglo = new int[2];
               

                try
                {

                    EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", "cr" + codigoEmpresa);
                    EmpresaConexion.connectionString += "Allow User Variables=True;";
                    this.databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                    string readCommand = "SELECT mon,yea FROM DATES  WHERE USER=@p_user";
                    MySqlCommand cmd = new MySqlCommand(readCommand, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@p_user", user);
                    cmd.CommandType = CommandType.Text;
                    this.databaseConnection.Open();
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        arreglo[0] = dataReader.GetInt32(0);
                        arreglo[1] = dataReader.GetInt32(1);
                        return arreglo;
                    }

                    return null;
                }
                catch (Exception e)
                {
                    this.databaseConnection.Close();
                    MessageBox.Show(e.Message);
                    return null;
                }
            }

            #endregion

            #region actualizar el mes y el año del usuario esta usando en el formulario principal
            public bool actualizarMesyAnio(string codigoEmpresa, int mes, int year, string user)
            {


                try
                {
                    if (existeUsuarioDate(codigoEmpresa, user))
                    {
                        EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", "cr" + codigoEmpresa);
                        EmpresaConexion.connectionString += "Allow User Variables=True;";
                        this.databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                        string readCommand = "UPDATE DATES SET mon=@p_mes,YEA=@p_anio" +
                             " WHERE USER=@p_user";
                        MySqlCommand cmd = new MySqlCommand(readCommand, this.databaseConnection);
                        cmd.Parameters.AddWithValue("@p_anio", year);
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_user", user);
                        cmd.CommandType = CommandType.Text;
                        this.databaseConnection.Open();
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            this.databaseConnection.Close();
                            return true;
                        }
                        else
                        {
                            this.databaseConnection.Close();
                            return false;
                        }
                    }
                    else
                        return false;

                }
                catch (Exception e)
                {
                    this.databaseConnection.Close();
                    MessageBox.Show(e.Message);
                    return false;
                }

            }

            #endregion

            #region setea el campo de retenciones en 1 cuando activa el modulo de retenciones en modificar
            public bool activarRetenciones(string codigoEmpresa)
            {

             

                try
                {
                    EmpresaConexion.connectionString += "Allow User Variables=True;";
                    this.databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                    string readCommand = "UPDATE cgpc00 SET retenciones=@p_retencion WHERE COD_EM=@p_codigo";
                    MySqlCommand cmd = new MySqlCommand(readCommand, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@p_retencion", 1);
                    cmd.Parameters.AddWithValue("@p_codigo", codigoEmpresa);
                    cmd.CommandType = CommandType.Text;
                    this.databaseConnection.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        this.databaseConnection.Close();
                        return true;
                    }
                    else
                    {
                        this.databaseConnection.Close();
                        return false;
                    }

                }
                catch (Exception e)
                {
                    this.databaseConnection.Close();
                    MessageBox.Show(e.Message);
                    return false;
                }

            }

            #endregion

            #region inserta el mes y el año del usuario
            public bool insertarMesyAnio(string codigoEmpresa, int mes, int year, string user)
            {
                int flag = 0;
           

                try
                {
                    if (!existeUsuarioDate(codigoEmpresa, user))
                    {
                        EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", "cr" + codigoEmpresa);
                        EmpresaConexion.connectionString += "Allow User Variables=True;";
                        this.databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                        string readCommand = "INSERT INTO DATES(mon,YEA,USER) VALUES (@p_mes,@p_anio,@p_user)";
                        MySqlCommand cmd = new MySqlCommand(readCommand, this.databaseConnection);
                        cmd.Parameters.AddWithValue("@p_anio", year);
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_user", user);
                        cmd.CommandType = CommandType.Text;
                        this.databaseConnection.Open();
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            this.databaseConnection.Close();
                            return true;
                        }
                        else
                        {
                            this.databaseConnection.Close();
                            return false;
                        }
                    }
                    else
                        return false;

                }
                catch (Exception e)
                {
                    this.databaseConnection.Close();
                    MessageBox.Show(e.Message);
                    return false;
                }

            }

            #endregion

            #region retorna un 1 si existe retenciones
            public bool existeNomina(int idEmpresa)
            {
                int flag = 0;
                EmpresaConexion.connectionString = string.Concat(new string[]
               {
                "datasource = ",
                this.serverData.server,
                ";port = ",
                this.serverData.port,
                ";username = ",
                this.serverData.user,
                ";password = ",
                this.serverData.password,
                ";database= empresas;Convert Zero Datetime = True;"
              });

                string readCommand = "SELECT nom FROM cgpc00 WHERE EMPRESA_ID=@param";
                MySqlCommand cmd = new MySqlCommand(readCommand, this.databaseConnection);
                cmd.Parameters.AddWithValue("@param", idEmpresa);
                cmd.CommandType = CommandType.Text;
                this.databaseConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                        flag = 0;
                    else
                        flag = reader.GetInt32(0);
                }

                if (flag == 0)
                {
                    return false;
                }
                else
                    return true;
            }

            #endregion

            #region retorna un 1 si existe retenciones cuando crea una nueva
            public bool existenEmpresas(string codigo)
            {
                int flag = 0;
          
                this.databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                string readCommand = "SELECT COUNT(nom) FROM cgpc00 where cod_em="+codigo;
                MySqlCommand cmd = new MySqlCommand(readCommand, this.databaseConnection);
                cmd.Parameters.AddWithValue("@param", codigo);
                cmd.CommandType = CommandType.Text;
                this.databaseConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                        flag = 0;
                    else
                        flag = reader.GetInt32(0);
                }

                if (flag == 0)
                {
                    this.databaseConnection.Close();
                    return false;
                }
                else
                {
                    this.databaseConnection.Close();
                    return true;
                }
            }

            #endregion

            public Image byteArrayToImage(byte[] byteAvatar)
            {
                MemoryStream ms = new MemoryStream(byteAvatar);
                Image devolverImagen = Image.FromStream(ms);
                return devolverImagen;
            }

            public static byte[] convertirAvatarAByte(string filePath)
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);

                byte[] avatar = reader.ReadBytes((int)stream.Length);

                reader.Close();
                stream.Close();

                return avatar;
            }

            public static byte[] convertirImageAByte(Image imageIn)
            {
                using (var ms = new MemoryStream())
                {
                    imageIn.Save(ms, imageIn.RawFormat);
                    return ms.ToArray();
                }
            }
            #region obtener empresas
            public List<EmpresaData> obtenerEmpresas(int usuarioId)
            {
                List<EmpresaData> list = new List<EmpresaData>();

                try
                {
                    MySqlCommand mySqlCommand = new MySqlCommand("obtener_empresas", this.databaseConnection);
                    mySqlCommand.Parameters.AddWithValue("@P_USUARIO_ID", usuarioId);
                    mySqlCommand.Parameters.AddWithValue("@P_MODULO", "NOMINA");
                    mySqlCommand.CommandType = CommandType.StoredProcedure;
                    this.databaseConnection.Open();
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        EmpresaData empresaData = new EmpresaData();
                        if (!reader.IsDBNull(3))
                        {
                            empresaData.Fecha = reader.GetDateTime(3);
                        }
                        if (!reader.IsDBNull(4))
                        {
                            empresaData.Direccion = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(5))
                        {
                            empresaData.RTN = reader.GetString(5);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            empresaData.Telefono = reader.GetString(6);
                        }
                        if (!reader.IsDBNull(7))
                        {
                            empresaData.DirConeccion = reader.GetString(7);
                        }
                        if (!reader.IsDBNull(8))
                        {
                            empresaData.Correo = reader.GetString(8);
                        }
                        else
                        {
                            empresaData.Correo = "";
                        }

                        if (!reader.IsDBNull(9))
                        {
                            empresaData.Image = byteArrayToImage((byte[])reader[9]);
                        }
                        if (!reader.IsDBNull(10))
                        {
                            empresaData.NominaFlag = reader.GetInt32(10);
                        }
                        else
                        {
                            empresaData.NominaFlag = 0;
                        }

                        list.Add(new EmpresaData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), empresaData.Fecha, empresaData.Direccion,
                            empresaData.RTN, empresaData.Telefono, empresaData.DirConeccion,
                            empresaData.Correo, empresaData.Image, empresaData.NominaFlag));

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

            #region obtener empresas
            public List<EmpresaData> obtenerEmpresasAdmin()
            {
                List<EmpresaData> list = new List<EmpresaData>();

                try
                {
                    MySqlCommand mySqlCommand = new MySqlCommand("obtener_empresas_admin", this.databaseConnection);

                    mySqlCommand.Parameters.AddWithValue("@P_MODULO", "NOMINA");
                    mySqlCommand.CommandType = CommandType.StoredProcedure;
                    this.databaseConnection.Open();
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        EmpresaData empresaData = new EmpresaData();
                        if (!reader.IsDBNull(3))
                        {
                            empresaData.Fecha = reader.GetDateTime(3);
                        }
                        if (!reader.IsDBNull(4))
                        {
                            empresaData.Direccion = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(5))
                        {
                            empresaData.RTN = reader.GetString(5);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            empresaData.Telefono = reader.GetString(6);
                        }
                        if (!reader.IsDBNull(7))
                        {
                            empresaData.DirConeccion = reader.GetString(7);
                        }
                        if (!reader.IsDBNull(8))
                        {
                            empresaData.Correo = reader.GetString(8);
                        }
                        else
                        {
                            empresaData.Correo = "";
                        }

                        if (!reader.IsDBNull(9))
                        {
                            empresaData.Image = byteArrayToImage((byte[])reader[9]);
                        }
                        if (!reader.IsDBNull(10))
                        {
                            empresaData.NominaFlag = reader.GetInt32(10);
                        }
                        else
                        {
                            empresaData.NominaFlag = 0;
                        }

                        list.Add(new EmpresaData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), empresaData.Fecha, empresaData.Direccion,
                            empresaData.RTN, empresaData.Telefono, empresaData.DirConeccion,
                            empresaData.Correo, empresaData.Image, empresaData.NominaFlag));

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


            #region agregar empresa
            public bool agregarEmpresa(string codigo, string nombreBaseDeDatos, DateTime fecha,
                string direccion, string RTN, string telefono, string correo,
                string filePathImagen, string usuarioCreador)
            {
                new ServidorData();
                Registro registro = new Registro();
                registro.leerRegistro();
                try
                {
                    if (!existenEmpresas(codigo))
                    {
                        string scriptEstructura = Properties.Resources.estructura_empresas;
                        string scriptProcedimientos = Properties.Resources.procedimientos__almacenados_empresas;
                        string bdname = "n" + codigo;
                        scriptEstructura = scriptEstructura.Replace("sistema_nomina", $"`{bdname}`");
                         scriptProcedimientos = scriptProcedimientos.Replace("sistema_nomina", $"`{bdname}`");

                        crearTablasProcedimientoEnEmpresas(scriptEstructura);
                        crearTablasProcedimientoEnEmpresas(scriptProcedimientos);
                }

                 


                    this.databaseConnection = new MySqlConnection(connectionString);


                    byte[] avatar = null;
                    if (filePathImagen != null)
                        avatar = convertirAvatarAByte(filePathImagen);
                    MySqlCommand cmd = new MySqlCommand("insertar_empresa", this.databaseConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_CODIGO", codigo);
                    cmd.Parameters.AddWithValue("@P_NOMBRE", nombreBaseDeDatos);
                    cmd.Parameters.AddWithValue("@P_FECHA", fecha);
                    cmd.Parameters.AddWithValue("@P_DIRECCION", direccion);
                    cmd.Parameters.AddWithValue("@P_RTN", RTN);
                    cmd.Parameters.AddWithValue("@P_TELEFONO", telefono);
                    cmd.Parameters.AddWithValue("@P_CORREO", correo);
                    cmd.Parameters.AddWithValue("@P_MODULO", "NOMINA");
                    cmd.Parameters.AddWithValue("@P_USUARIO", usuarioCreador);
                    cmd.Parameters.AddWithValue("@P_IMAGEN", avatar);


                    cmd.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
                    this.databaseConnection.Open();
                    cmd.ExecuteNonQuery();
                    int num = Convert.ToInt32(cmd.Parameters["@P_SALIDA"].Value);
                    bool result;
                    if (num == 1)
                    {
                        this.databaseConnection.Close();
                        result = true;
                        //encryptar("estructura_1.sql")   

                        

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

        #region crear empresa estructura
        public void crearTablasProcedimientoEnEmpresas(string estructura)
            {

                try
                {
              
                    EmpresaConexion.connectionString += "Allow User Variables=True;";
                    string text = estructura;
                if (!serverData.server.Equals("127.0.0.1"))
                    text = text.Replace("localhost", serverData.server);
                else {
                    text = text.Replace("root", serverData.user);
                }
                    


                    this.databaseConnection = new MySqlConnection(connectionString);
                    MySqlCommand mySqlCommand = new MySqlCommand(text, databaseConnection);
                    mySqlCommand.CommandType = CommandType.Text;
                    databaseConnection.Open();
                    mySqlCommand.ExecuteNonQuery();
                    databaseConnection.Close();
                }
                catch (Exception e)
                {
                    databaseConnection.Close();
                    MessageBox.Show(e.Message);
                }

            }

            #endregion

            #region crear empresa
            public void crearEmpresaBD(string nombreEmpresa, string estructura)
            {

                try
                {

                    EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", "");
                    EmpresaConexion.connectionString += "Allow User Variables=True;";

                    string text = estructura.Replace("cr90", nombreEmpresa);
                    this.databaseConnection = new MySqlConnection(connectionString);
                    MySqlCommand mySqlCommand = new MySqlCommand(text, databaseConnection);
                    mySqlCommand.CommandType = CommandType.Text;
                    databaseConnection.Open();
                    mySqlCommand.ExecuteNonQuery();
                    databaseConnection.Close();

                }
                catch (Exception e)
                {
                    databaseConnection.Close();
                    MessageBox.Show(e.Message);
                }

            }

            #endregion

          
            #region crear los triguers
            private void crearTriguers(string codigoEmpresa, string triguer, string tabla, bool documento)
            {

          
            
                try
                {
                    //string text = File.ReadAllText(path);
                    string text = triguer.Replace("cr90", codigoEmpresa);
                    if (!serverData.server.Equals("127.0.0.1"))
                        text = text.Replace("localhost", serverData.server);

                    text = text.Replace("root", serverData.user);

                    if (tabla != null && documento)
                    {
                        text = text.Replace("docretenene", tabla);
                    }
                    else
                        if (tabla != null)
                    {
                        text = text.Replace("provene", tabla);
                    }

                    EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", codigoEmpresa);
                    databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                    MySqlCommand mySqlCommand = new MySqlCommand(text, databaseConnection);
                    mySqlCommand.CommandType = CommandType.Text;
                    databaseConnection.Open();
                    mySqlCommand.ExecuteNonQuery();
                    databaseConnection.Close();
                }
                catch (Exception e)
                {
                    this.databaseConnection.Close();
                    MessageBox.Show("Error Base de Datos:" + e.Message);
                }
            }


            #endregion

            #region crear procedimientos almacenados
            public void crearProcedimientosAlmacenados(string nombreEmpresa, string procedimiento)
            {
               
                try
                {
                    //string text = File.ReadAllText(path);
                    string text = procedimiento.Replace("sistema_nomina", nombreEmpresa);
                    if (!serverData.server.Equals("127.0.0.1"))
                        text = text.Replace("localhost", serverData.server);
                        text = text.Replace("root", serverData.user);

                    EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", nombreEmpresa);
                    databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                    MySqlCommand mySqlCommand = new MySqlCommand(text, databaseConnection);
                    mySqlCommand.CommandType = CommandType.Text;
                    databaseConnection.Open();
                    mySqlCommand.ExecuteNonQuery();
                    databaseConnection.Close();
                }
                catch (Exception e)
                {
                    this.databaseConnection.Close();
                    MessageBox.Show(e.Message);
                }


            }
            #endregion

            #region modificar empresa y crea la bd retenciones, cuando ya hay una empresa creada
            public bool modificarEmpresaNomina(int id, string codigo, string nombre, DateTime fecha, string direccion,
                string RTN, string telefono, string correo, string codigoAntiguo, Image imagen, string usuarioCreador)
            {
                bool result;
                try
                {
                    if (!existenEmpresas(codigo))
                    {
                       crearTablasProcedimientoEnEmpresas(Properties.Resources.estructura_empresas);
                        crearTablasProcedimientoEnEmpresas(Properties.Resources.procedimientos__almacenados_empresas);
                        //crearTablasProcedimientoEnEmpresas(Properties.Resources.triguers_empresas);
                    }
                    byte[] avatar = null;
                    if (imagen != null)
                        avatar = convertirImageAByte(imagen);
                    MySqlCommand cmd = new MySqlCommand("modificar_empresa", this.databaseConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_CODIGO", codigo);
                    cmd.Parameters.AddWithValue("@P_NOMBRE", nombre);
                    cmd.Parameters.AddWithValue("@P_FECHA", fecha);
                    cmd.Parameters.AddWithValue("@P_DIRECCION", direccion);
                    cmd.Parameters.AddWithValue("@P_RTN", RTN);
                    cmd.Parameters.AddWithValue("@P_TELEFONO", telefono);
                    cmd.Parameters.AddWithValue("@P_CORREO", correo);
                    cmd.Parameters.AddWithValue("@P_EMPRESA_ID", id);
                    cmd.Parameters.AddWithValue("@P_IMAGEN", avatar);
                    cmd.Parameters.AddWithValue("@P_USUARIO", usuarioCreador);
                    cmd.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
                    this.databaseConnection.Open();
                    cmd.ExecuteNonQuery();
                    int num = Convert.ToInt32(cmd.Parameters["@P_SALIDA"].Value);

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


                    if (!existeNomina(id))
                    {
                        //crearBaseDeDatosYProcedimientos(codigo);
                        activarRetenciones(codigo);
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

            #region modificar empresa
            public bool modificarEmpresa(int id, string codigo, string nombre, DateTime fecha, string direccion,
                string RTN, string telefono, string correo, string codigoAntiguo, Image imagen, string usuarioCreador)
            {
                bool result;
                try
                {

                    byte[] avatar = null;
                    if (imagen != null)
                        avatar = convertirImageAByte(imagen);
                    MySqlCommand cmd = new MySqlCommand("modificar_empresa", this.databaseConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_CODIGO", codigo);
                    cmd.Parameters.AddWithValue("@P_NOMBRE", nombre);
                    cmd.Parameters.AddWithValue("@P_FECHA", fecha);
                    cmd.Parameters.AddWithValue("@P_DIRECCION", direccion);
                    cmd.Parameters.AddWithValue("@P_RTN", RTN);
                    cmd.Parameters.AddWithValue("@P_TELEFONO", telefono);
                    cmd.Parameters.AddWithValue("@P_CORREO", correo);
                    cmd.Parameters.AddWithValue("@P_EMPRESA_ID", id);
                    cmd.Parameters.AddWithValue("@P_IMAGEN", avatar);
                    cmd.Parameters.AddWithValue("@P_USUARIO", usuarioCreador);

                    cmd.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
                    this.databaseConnection.Open();
                    cmd.ExecuteNonQuery();
                    int num = Convert.ToInt32(cmd.Parameters["@P_SALIDA"].Value);

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

            #region eliminar empresa
            public bool eliminarEmpresa(string codigo, string usuarioCreador)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("eliminar_empresa", this.databaseConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_ID", codigo);
                    cmd.Parameters.AddWithValue("@P_USUARIO", usuarioCreador);
                    this.databaseConnection.Open();
                    int num = cmd.ExecuteNonQuery();
                    bool result;
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

            #region obtener empresa por codigo
            public EmpresaData obtenerEmpresaId(string codigo)
            {
                EmpresaData result = new EmpresaData();
                try
                {
                    MySqlCommand mySqlCommand = new MySqlCommand("obtener_empresa_id", this.databaseConnection);
                    mySqlCommand.CommandType = CommandType.StoredProcedure;
                    mySqlCommand.Parameters.AddWithValue("@P_CODIGO", codigo);
                    this.databaseConnection.Open();
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();
                    EmpresaData empresaData = new EmpresaData();
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(3))
                        {
                            empresaData.Fecha = reader.GetDateTime(3);
                        }
                        if (!reader.IsDBNull(4))
                        {
                            empresaData.Direccion = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(5))
                        {
                            empresaData.RTN = reader.GetString(5);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            empresaData.Telefono = reader.GetString(6);
                        }
                        if (!reader.IsDBNull(7))
                        {
                            empresaData.DirConeccion = reader.GetString(7);
                        }
                        if (!reader.IsDBNull(8))
                        {
                            empresaData.Correo = reader.GetString(8);
                        }
                        else
                        {
                            empresaData.Correo = "";
                        }


                        if (!reader.IsDBNull(9))
                        {
                            empresaData.Image = byteArrayToImage((byte[])reader[9]);
                        }

                        if (!reader.IsDBNull(10))
                        {
                            empresaData.NominaFlag = reader.GetInt32(10);
                        }
                        else
                        {
                            empresaData.NominaFlag = 0;
                        }
                        result = new EmpresaData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), empresaData.Fecha, empresaData.Direccion,
                            empresaData.RTN, empresaData.Telefono, empresaData.DirConeccion,
                            empresaData.Correo, empresaData.Image, empresaData.NominaFlag);
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

            #region buscar empresa por nombre
            public List<EmpresaData> buscarEmpresaNombre(string nombre)
            {
                List<EmpresaData> list = new List<EmpresaData>();
                try
                {
                    MySqlCommand mySqlCommand = new MySqlCommand("buscar_empresa_nombre", this.databaseConnection);
                    mySqlCommand.CommandType = CommandType.StoredProcedure;
                    mySqlCommand.Parameters.AddWithValue("@P_NOMBRE", nombre);
                    this.databaseConnection.Open();
                    EmpresaData empresaData = new EmpresaData();
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(3))
                        {
                            empresaData.Fecha = reader.GetDateTime(3);
                        }
                        if (!reader.IsDBNull(4))
                        {
                            empresaData.Direccion = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(5))
                        {
                            empresaData.RTN = reader.GetString(5);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            empresaData.Telefono = reader.GetString(6);
                        }
                        if (!reader.IsDBNull(7))
                        {
                            empresaData.DirConeccion = reader.GetString(7);
                        }
                        if (!reader.IsDBNull(8))
                        {
                            empresaData.Correo = reader.GetString(8);
                        }
                        else
                        {
                            empresaData.Correo = "";
                        }

                        if (!reader.IsDBNull(9))
                        {
                            empresaData.Image = byteArrayToImage((byte[])reader[9]);
                        }

                        if (!reader.IsDBNull(10))
                        {
                            empresaData.NominaFlag = reader.GetInt32(10);
                        }
                        else
                        {
                            empresaData.NominaFlag = 0;
                        }

                        list.Add(new EmpresaData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), empresaData.Fecha, empresaData.Direccion,
                            empresaData.RTN, empresaData.Telefono, empresaData.DirConeccion,
                            empresaData.Correo, empresaData.Image, empresaData.NominaFlag));
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


            #region buscar empresa por codigo
            public List<EmpresaData> buscarEmpresaCodigo(string codigo)
            {
                List<EmpresaData> list = new List<EmpresaData>();

                try
                {
                    MySqlCommand mySqlCommand = new MySqlCommand("buscar_empresa_codigo", this.databaseConnection);
                    mySqlCommand.CommandType = CommandType.StoredProcedure;
                    mySqlCommand.Parameters.AddWithValue("@P_CODIGO", codigo);
                    this.databaseConnection.Open();
                    EmpresaData empresaData = new EmpresaData();
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(3))
                        {
                            empresaData.Fecha = reader.GetDateTime(3);
                        }
                        if (!reader.IsDBNull(4))
                        {
                            empresaData.Direccion = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(5))
                        {
                            empresaData.RTN = reader.GetString(5);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            empresaData.Telefono = reader.GetString(6);
                        }
                        if (!reader.IsDBNull(7))
                        {
                            empresaData.DirConeccion = reader.GetString(7);
                        }
                        if (!reader.IsDBNull(8))
                        {
                            empresaData.Correo = reader.GetString(8);
                        }
                        else
                        {
                            empresaData.Correo = "";
                        }

                        if (!reader.IsDBNull(9))
                        {
                            empresaData.Image = byteArrayToImage((byte[])reader[9]);
                        }

                        if (!reader.IsDBNull(10))
                        {
                            empresaData.NominaFlag = reader.GetInt32(10);
                        }
                        else
                        {
                            empresaData.NominaFlag = 0;
                        }

                        list.Add(new EmpresaData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), empresaData.Fecha, empresaData.Direccion,
                            empresaData.RTN, empresaData.Telefono, empresaData.DirConeccion,
                            empresaData.Correo, empresaData.Image, empresaData.NominaFlag));
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

            #region actualizar los procedimientos de la base de datos
            public bool actualizarProcedimientoBD(string codigoEmpresa, string archivo)
            {
       
                string path = archivo;

             
                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path);
                    text = text.Replace("cr90", "cr" + codigoEmpresa);
                    text = text.Replace("root", serverData.user);
                    if (!serverData.server.Equals("127.0.0.1"))
                        text = text.Replace("localhost", serverData.server);

                    EmpresaConexion.connectionString = EmpresaConexion.connectionString.Replace("empresas", "cr" + codigoEmpresa);
                    try
                    {
                        databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                        MySqlCommand mySqlCommand = new MySqlCommand(text, databaseConnection);
                        mySqlCommand.CommandType = CommandType.Text;
                        databaseConnection.Open();

                        int n = mySqlCommand.ExecuteNonQuery();
                        databaseConnection.Close();
                        if (n == 0)
                        {
                            return true;
                        }
                        else
                            return false;


                    }
                    catch (Exception e)
                    {
                        this.databaseConnection.Close();
                        MessageBox.Show(e.Message);
                        return false;
                    }

                }
                return false;
            }
            #endregion

            #region actualizar los procedimientos de la base de datos
            public bool actualizarBDEmpresas(string archivo)
            {
         
                string path = archivo;

                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path);
                    text = text.Replace("root", serverData.user);
                    if (!serverData.server.Equals("127.0.0.1"))
                        text = text.Replace("localhost", serverData.server);
                    try
                    {
                        databaseConnection = new MySqlConnection(EmpresaConexion.connectionString);
                        MySqlCommand mySqlCommand = new MySqlCommand(text, databaseConnection);
                        mySqlCommand.CommandType = CommandType.Text;
                        databaseConnection.Open();

                        int n = mySqlCommand.ExecuteNonQuery();
                        databaseConnection.Close();
                        if (n == 0)
                        {
                            return true;
                        }
                        else
                            return false;


                    }
                    catch (Exception e)
                    {
                        this.databaseConnection.Close();
                        MessageBox.Show("Error Base de Datos" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return false;
            }
        #endregion


        public DataTable ObtenerBitacoraEmpresa(string user, string modo)
        {
            DataTable dt = new DataTable();

            using (MySqlCommand comando = new MySqlCommand("sp_consultar_bitacora", this.databaseConnection))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@P_NOMBRE_USUARIO", user);
                comando.Parameters.AddWithValue("@P_MODO", modo);

                this.databaseConnection.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                this.databaseConnection.Close();
            }

            return dt;
        }
    }
    }
