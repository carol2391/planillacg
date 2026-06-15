using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using nomina.Clases.Empleado;
using nomina.Clases.Seguridad;

namespace nomina.Clases.MovimientoLabores
{
   public   class MLaboresConexion
    {
        Conexion conexion;
        public MLaboresConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener mlavor para editar en el formulario
        public MLaboresData obtenerMLabor(int idEmpleado, int idLabor, DateTime fecha)
        {
            MLaboresData mLabor = new MLaboresData();
            MySqlCommand comando = new MySqlCommand("obtener_mLabor", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            comando.Parameters.AddWithValue("@P_ID_LABOR", idLabor);
            comando.Parameters.AddWithValue("@P_FEC_LAB", fecha);

            this.conexion.getConexion().Open();
            int idNomina = 0;
            MySqlDataReader lector = comando.ExecuteReader();
            EmpleadoData empleado = new EmpleadoData();
            if (lector.Read())
            {
                if (!lector.IsDBNull(7))
                {
                    idNomina = lector.GetInt32(7);
                }
                empleado.Nombre = lector.GetString(8);
                empleado.Codigo = lector.GetString(9);

                mLabor = new MLaboresData(
                               lector.GetInt32(0), lector.GetInt32(1),
                               lector.GetString(2), 
                               //lector.GetString(3),
                               lector.GetDecimal(3), lector.GetDateTime(4),
                               lector.GetDecimal(5), lector.GetInt32(6),
                               idNomina, empleado,
                               lector.GetString(10),
                               lector.GetInt32(11)
                            );

            }
            this.conexion.getConexion().Close();
            return mLabor;
        }
        #endregion

        #region obtener lista de labores
        public List<MLaboresData> obtenerLabores()
        {

            List<MLaboresData> mlabores = new List<MLaboresData>() ;

            //try
            //{
                /*procedimiento que me obtiene un movimiento de labor*/
                MySqlCommand comando = new MySqlCommand("obtener_mLabores", this.conexion.getConexion());
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                this.conexion.getConexion().Open();

                MySqlDataReader lector = comando.ExecuteReader();
                int idNomina = 0;
               EmpleadoData empleado = new EmpleadoData();
               MLaboresData mLabor;
               while (lector.Read())
                {
                  if (!lector.IsDBNull(8)) {
                    idNomina = lector.GetInt32(8);
                }

                empleado.Id = lector.GetInt32(0);
                empleado.Codigo= lector.GetString(10);
                empleado.Nombre = lector.GetString(11);

              mlabores.Add( new MLaboresData(
                               lector.GetInt32(0), lector.GetInt32(1),
                               lector.GetString(2),
                               lector.GetString(3),
                               lector.GetDecimal(4), lector.GetDateTime(5),
                               lector.GetDecimal(6), lector.GetInt32(7),
                               idNomina, empleado, lector.GetInt32(12)
                            ));
                empleado = new EmpleadoData();
            }
                this.conexion.getConexion().Close();
                return mlabores;
            //}
            //catch(Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //    this.conexion.getConexion().Close();
            //    return mlabores;
            //}
            
        }
        #endregion

        #region buscar retorna una lista de mlavores, busca la labor segun el codigo del empleado y un rango de fecha para llenar el datagrid
        public List<MLaboresData> buscarMLabor(string codigoEmpleado, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<MLaboresData> mLabores = new List<MLaboresData>();
            MySqlCommand comando = new MySqlCommand("buscar_mlabores", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_EMPLEADO", codigoEmpleado);
            comando.Parameters.AddWithValue("@P_FEC_LAB_INICIAL", fechaInicial);
            comando.Parameters.AddWithValue("@P_FEC_LAB_FINAL", fechaFinal);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();
            int idNomina = 0;
            EmpleadoData empleado = new EmpleadoData();
            MLaboresData mLabor;
            //try
            //{
                while (lector.Read())
                {
                    if (!lector.IsDBNull(8))
                    {
                        idNomina = lector.GetInt32(8);
                    }
                    empleado.Nombre = lector.GetString(10);
                    empleado.Codigo = lector.GetString(11);

                   mLabores.Add( new MLaboresData(
                                   lector.GetInt32(0), lector.GetInt32(1),
                                   lector.GetString(2), lector.GetString(3),
                                   lector.GetDecimal(4), lector.GetDateTime(5),
                                   lector.GetDecimal(6), lector.GetInt32(7),
                                   idNomina, empleado, lector.GetInt32(12)
                                ));

                    empleado = new EmpleadoData();

                }
                this.conexion.getConexion().Close();
                return mLabores;

            //}
            //catch(Exception e)
            //{
            //    this.conexion.getConexion().Close();
            //    return mLabores;
               
            //}
            
        }
        #endregion

        #region valida si un empleado tiene la labor asignada
        /*
            en el formulario de labores no se va poder eliminar 
            la labor si ya la tiene asignada un movimiento de labor
         */
        public bool existelaborEnMLabores( string codigoLabor)
        {
            MySqlCommand comando = new MySqlCommand("existe_labores_en_mlabores", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_COD_LAB", codigoLabor);
            

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                int existe = lector.GetInt32(0);
                if (existe > 0)
                {
                    this.conexion.getConexion().Close();
                    return true;
                }
                else
                {
                    this.conexion.getConexion().Close();
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region valida si un empleado tiene la labor asignada
        /*
            en el formulario de empeadoos no se va poder eliminar el empleado
            si ya tiene asignado un movimiento de labor
         */
        public bool existeEmpleadoEnMlabores(string codigoEmpleado)
        {
            MySqlCommand comando = new MySqlCommand("existe_empleado_en_mlabores", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_COD_TRB", codigoEmpleado);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                int existe = lector.GetInt32(0);
                if (existe > 0)
                {
                    this.conexion.getConexion().Close();
                    return true;
                }
                else
                {
                    this.conexion.getConexion().Close();
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region valida si existe la labor asignada al empleado con la fecha
        public bool existeMLabor(string codigoEmpleado,string codigoLabor, DateTime fecha)
        {
            MySqlCommand comando = new MySqlCommand("existe_mlabor", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_COD_TRB", codigoEmpleado);
            comando.Parameters.AddWithValue("@_COD_LAB", codigoLabor);
            comando.Parameters.AddWithValue("@_FEC_LAB", fecha);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                int existe = lector.GetInt32(0);
                if (existe > 0)
                {
                    this.conexion.getConexion().Close();
                    return true;
                }
                else
                   {
                      this.conexion.getConexion().Close();
                    return false;
                   }
            }
            return false;
        }
        #endregion
 
        #region acciones un movimiento labores
        public bool accionesLabores(string accion,int idEmpleado, int id_labor,
               string DescripcionLabor, int idTipoPago,
               decimal CantidaLabor, DateTime FechaLabor, decimal MontoLabor,
               int idCuenta, 
               //int idNomina,
               
               string isr, int idMlabores)
        {
            MySqlCommand cmd = new MySqlCommand("acciones_mlabores", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.AddWithValue("@P_ID_M_LABORES", idMlabores);//0
            cmd.Parameters.AddWithValue("@P_ID_EMPLEADO" , idEmpleado);//12
            cmd.Parameters.AddWithValue("@P_ID_LABOR", id_labor);//13
            cmd.Parameters.AddWithValue("@P_DESC_LAB", DescripcionLabor);
            cmd.Parameters.AddWithValue("@P_TIPO_LAB", idTipoPago);//1
            cmd.Parameters.AddWithValue("@P_CANT_LAB ", CantidaLabor);
            cmd.Parameters.AddWithValue("@P_FEC_LAB", FechaLabor);
            cmd.Parameters.AddWithValue("@P_MON_LAB", MontoLabor);
            cmd.Parameters.AddWithValue("@P_ID_CUENTA", idCuenta);
           // cmd.Parameters.AddWithValue("@P_ID_NOMINA",idNomina );
            cmd.Parameters.AddWithValue("@P_ISR", isr);
            cmd.Parameters.AddWithValue("@P_USUARIO", Session.Usuario);
            cmd.Parameters.Add("@p_salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            //try
            //{
                //Se abre la conexión
                conexion.getConexion().Open();

            // int filasAfectadas = 
             cmd.ExecuteNonQuery();
             int salida = Convert.ToInt32(cmd.Parameters["@p_salida"].Value);

            if (salida == 1)
            {
                this.conexion.getConexion().Close();
                return true;
            }
            else
            {
                this.conexion.getConexion().Close();
                return false;
            }

            //}
            //catch (MySqlException)
            //{
            //    this.conexion.getConexion().Close();
            //    return false;
            //}
        }
        #endregion

        #region modifica un movimiento labores
        public bool modificarMLabores(string codigoEmpleado, string CodigoLabor,
               string DescripcionLabor,
               double CantidaLabor, DateTime FechaLabor, double MontoLabor,
               string CodigoCuenta, string nombreCuenta,DateTime fechaAntigua,string isr)
        {
            MySqlCommand cmd = new MySqlCommand("modificar_mlabor", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_TRB", codigoEmpleado);
            cmd.Parameters.AddWithValue("@_COD_LAB", CodigoLabor);
            cmd.Parameters.AddWithValue("@_DESC_LAB", DescripcionLabor);
            cmd.Parameters.AddWithValue("@_CANT_LAB ", CantidaLabor);
            cmd.Parameters.AddWithValue("@_FEC_LAB", FechaLabor);
            cmd.Parameters.AddWithValue("@_MON_LAB", MontoLabor);
            cmd.Parameters.AddWithValue("@_COD_CUE", CodigoCuenta);
            cmd.Parameters.AddWithValue("@_COD_NOM", nombreCuenta);
            cmd.Parameters.AddWithValue("@_FEC_ANTIGUA", fechaAntigua);
            cmd.Parameters.AddWithValue("@P_ISR", isr);

            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();

                // int filasAfectadas = 
                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    this.conexion.getConexion().Close();
                    return true;
                }
                else
                {
                    this.conexion.getConexion().Close();
                    return false;
                }

            }
            catch (MySqlException)
            {
                this.conexion.getConexion().Close();
                return false;
            }
        }
        #endregion

        #region elimina un  movimiento de descuento
        public bool eliminarMLabor(int idEmpleado, int idLabor, DateTime fecha)
        {
            MLaboresData mdescuento = new MLaboresData();
            try
            {
                MySqlCommand comando = new MySqlCommand("eliminar_mlabores", this.conexion.getConexion());
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@P_ID_MLABORES", idEmpleado);
                comando.Parameters.AddWithValue("@P_ID_LABOR", idLabor);
                comando.Parameters.AddWithValue("@P_FEC_LAB", fecha);

                conexion.getConexion().Open();

                // int filasAfectadas = 
                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    this.conexion.getConexion().Close();
                    return true;
                }
                else
                {
                    this.conexion.getConexion().Close();
                    return false;
                }
            }
            catch (Exception e) {
                return false;
            }
           
        }
        #endregion
    }
}
