using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;

namespace nomina.Clases.Movimientos.MovimientoPrestamo
{
    public class PrestamoConexion
    {
        Conexion conexion;
        public PrestamoConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener un prestamo para editar en el formulario
        public PrestamoData obtenerPrestamo(int id)
        {
            PrestamoData prestamo = new PrestamoData();
            MySqlCommand comando = new MySqlCommand("obtener_mprestamo", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID_PRESTAMO", id);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                prestamo.Id = lector.GetInt32(0);
                prestamo.CodigoEmpleado = lector.GetString(1);
                prestamo.CodigoPrestamo = lector.GetString(2);
                prestamo.Fecha = lector.GetDateTime(3);
                prestamo.Descripcion = lector.GetString(4);
                prestamo.Monto = lector.GetDecimal(5);
                prestamo.CuotaMes = lector.GetDecimal(6);
                prestamo.Tiempo = lector.GetInt32(7);
                prestamo.Estado = lector.GetString(8);
                prestamo.IdTipoPago = lector.GetInt32(9);
                prestamo.DescripcionTipoPago = lector.GetString(10);
                prestamo.MontoActual = lector.GetDecimal(11);
                prestamo.NombreEmpleado = lector.GetString(12);

            }
            this.conexion.getConexion().Close();
            return prestamo;
        }
        #endregion

        #region obtener un prestamo para editar en el formulario
        public List<PrestamoData> obtenerPrestamos()
        {
            PrestamoData prestamo = new PrestamoData();
            List<PrestamoData> lprestamos = new List<PrestamoData>();
            MySqlCommand comando = new MySqlCommand("obtener_mprestamos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
        
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                prestamo.Id = lector.GetInt32(0);
                prestamo.CodigoEmpleado = lector.GetString(1);
                prestamo.CodigoPrestamo = lector.GetString(2);
                prestamo.Fecha = lector.GetDateTime(3);
                prestamo.Descripcion = lector.GetString(4);
                prestamo.Monto = lector.GetDecimal(5);
                prestamo.CuotaMes = lector.GetDecimal(6);
                prestamo.Tiempo = lector.GetInt32(7);
                prestamo.Estado = lector.GetString(8);
                prestamo.IdTipoPago = lector.GetInt32(9);
                prestamo.DescripcionTipoPago = lector.GetString(10);
                prestamo.MontoActual = lector.GetDecimal(11);
                prestamo.NombreEmpleado = lector.GetString(12);
                lprestamos.Add(prestamo);
                prestamo = new PrestamoData();
            }
            this.conexion.getConexion().Close();
            return lprestamos;
        }
        #endregion

        #region retorna una lista de prestamos, busca el prestamo segun el codigo del empleado y un rango de fecha para llenar el datagrid
        public List<PrestamoData> buscarPrestamos(int idEmpleado, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<PrestamoData> lprestamos = new List<PrestamoData>();
            PrestamoData prestamo = new PrestamoData();
            MySqlCommand comando = new MySqlCommand("buscar_mprestamos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            comando.Parameters.AddWithValue("@P_FECHA_INICIAL", fechaInicial);
            comando.Parameters.AddWithValue("@P_FECHA_FINAL", fechaFinal);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                prestamo.Id = lector.GetInt32(0);
                prestamo.CodigoEmpleado = lector.GetString(1);
                prestamo.CodigoPrestamo = lector.GetString(2);
                prestamo.Fecha = lector.GetDateTime(3);
                prestamo.Descripcion = lector.GetString(4);
                prestamo.Monto = lector.GetDecimal(5);
                prestamo.CuotaMes = lector.GetDecimal(6);
                prestamo.Tiempo = lector.GetInt32(7);
                prestamo.Estado = lector.GetString(8);
                prestamo.IdTipoPago = lector.GetInt32(9);
                prestamo.DescripcionTipoPago = lector.GetString(10);
                prestamo.MontoActual = lector.GetDecimal(11);
                prestamo.NombreEmpleado = lector.GetString(12);
                lprestamos.Add(prestamo);          
                prestamo = new PrestamoData();

            }
            this.conexion.getConexion().Close();
            return lprestamos;
        }
        #endregion

        #region insertar prestamo
        public bool accionesPrestamos(string accion, int idPrestamo,int idEmpleado,
           string codigoPrestamo, DateTime fecha, string descripcion,
           int idTipoPago, decimal monto, decimal cuotaMes, int tiempo,string estado
           )
        {
            MySqlCommand cmd = new MySqlCommand("acciones_mprestamo", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.AddWithValue("@P_ID_PRESTAMO", idPrestamo);
            cmd.Parameters.AddWithValue("@P_CODIGO", codigoPrestamo);
            cmd.Parameters.AddWithValue("@P_FECHA", fecha);
            cmd.Parameters.AddWithValue("@P_DESCRIPCION", descripcion);
            cmd.Parameters.AddWithValue("@P_MONTO", monto);
            cmd.Parameters.AddWithValue("@P_ID_EMPLEADO",idEmpleado);
            cmd.Parameters.AddWithValue("@P_CUOTA_MES", cuotaMes);
            cmd.Parameters.AddWithValue("@P_ID_TIPO_PAGO", idTipoPago);
            cmd.Parameters.AddWithValue("@P_TIEMPO", tiempo);
            cmd.Parameters.AddWithValue("@P_ESTADO", estado);
            cmd.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            //try
            //{
            //    //Se abre la conexión
            conexion.getConexion().Open();

            cmd.ExecuteNonQuery();
            int salida = Convert.ToInt32(cmd.Parameters["@P_SALIDA"].Value);
            /*si es igual a uno no existe*/
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

        #region modifica un movimiento de prestamo
        public bool modificarPrestamo(int id, string codigoEmpleado,
                       string codigoPrestamo, DateTime fecha, string descripcion,
                       string tipoPago, string estado, decimal monto, decimal cuotaMes, int tiempo
                       
           )
        {
            {
                MySqlCommand cmd = new MySqlCommand("modificar_prestamo", this.conexion.getConexion());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_PRESTAMOS_ID", id);
                cmd.Parameters.AddWithValue("@P_COD_TRB", codigoEmpleado);
                cmd.Parameters.AddWithValue("@P_COD_PRT", codigoPrestamo);
                cmd.Parameters.AddWithValue("@P_FEC_PRT", fecha);
                cmd.Parameters.AddWithValue("@P_DESCRIP", descripcion);
                cmd.Parameters.AddWithValue("@P_MONTO", monto);
                cmd.Parameters.AddWithValue("@P_CUOTA_MES", cuotaMes);
                cmd.Parameters.AddWithValue("@P_T_PAG", tipoPago);
                cmd.Parameters.AddWithValue("@P_TIEMPO", tiempo);
                cmd.Parameters.AddWithValue("@P_ESTADO", estado);
                cmd.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;


                try
                {
                    //Se abre la conexión
                    conexion.getConexion().Open();

                    // int filasAfectadas = 
                    cmd.ExecuteNonQuery();
                    /*si es igual a 1 lo modifico*/
                    int salida = Convert.ToInt32(cmd.Parameters["@P_SALIDA"].Value);
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

                }
                catch (MySqlException)
                {
                    this.conexion.getConexion().Close();
                    return false;
                }
            }
        }
        #endregion

        #region elimina un  movimiento de aumento
        public bool eliminarPrestamo(int id)
        {
            MySqlCommand comando = new MySqlCommand("eliminar_prestamo", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_PRESTAMOS_ID", id);

             try
            {
                conexion.getConexion().Open();


                int filasAfectadas = comando.ExecuteNonQuery();

                // int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);

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

    } 
}
