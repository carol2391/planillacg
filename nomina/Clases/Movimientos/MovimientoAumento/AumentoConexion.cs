using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;
using nomina.Clases.Seguridad;

namespace nomina.Clases.Movimientos.MovimientoAumento
{
   public class AumentoConexion
    {
        Conexion conexion;
        public AumentoConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener un aumento para editar en el formulario
        public List<AumentoData> obtenerAumentos()
        {
            AumentoData aumento = new AumentoData();
            List<AumentoData> lAumentos = new List<AumentoData>();
            MySqlCommand comando = new MySqlCommand("obtener_maumentos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

          while (lector.Read())
            {

                aumento.Id = lector.GetInt32(0);
                aumento.IdEmpleado = lector.GetInt32(1);
                aumento.Fecha = lector.GetDateTime(2);
                aumento.IdCategoria = lector.GetInt32(3);
                aumento.CategoriaDescripcion = lector.GetString(4);
                aumento.SueldoAnterior = lector.GetDecimal(5);
                aumento.SueldoActual = lector.GetDecimal(6);
                aumento.IdTipoAumento = lector.GetInt32(7);
                aumento.DescripcionTipoAumento = lector.GetString(8);
                aumento.Porcentaje = lector.GetDecimal(9);
                aumento.Monto = lector.GetDecimal(10);
                aumento.TotalMonto = lector.GetDecimal(11);
                aumento.Descripcion = lector.GetString(12);
                aumento.NombreEmpleado = lector.GetString(13);
                aumento.CodigoEmpleado = lector.GetString(14);
                lAumentos.Add(aumento);
                aumento = new AumentoData();
            }
            this.conexion.getConexion().Close();
            return lAumentos;
        }
        #endregion

        #region obtener un aumento para editar en el formulario
        public AumentoData obtenerAumento(int idAumento)
        {
            AumentoData aumento = new AumentoData();
            MySqlCommand comando = new MySqlCommand("obtener_maumento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_AUMENTO_ID", idAumento);
         

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {

                aumento.Id = lector.GetInt32(0);
                aumento.IdEmpleado = lector.GetInt32(1);
                aumento.Fecha = lector.GetDateTime(2);
                aumento.IdCategoria = lector.GetInt32(3);
                aumento.CategoriaDescripcion = lector.GetString(4);
                aumento.SueldoAnterior = lector.GetDecimal(5);
                aumento.SueldoActual = lector.GetDecimal(6);
                aumento.IdTipoAumento = lector.GetInt32(7);
                aumento.DescripcionTipoAumento = lector.GetString(8);
                aumento.Porcentaje =  lector.GetDecimal(9);
                aumento.Monto = lector.GetDecimal(10);
                aumento.TotalMonto = lector.GetDecimal(11);
                aumento.Descripcion = lector.GetString(12);
                aumento.NombreEmpleado = lector.GetString(13);
                aumento.CodigoEmpleado = lector.GetString(14);             
            }
            this.conexion.getConexion().Close();
            return aumento;
        }
        #endregion

        #region retorna una lista de aumentos, busca el aumento segun el codigo del empleado y un rango de fecha para llenar el datagrid
        public List<AumentoData> buscarAumentos(int idEmpleado, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<AumentoData> LAumentos = new List<AumentoData>();
            AumentoData aumento = new AumentoData();
            MySqlCommand comando = new MySqlCommand("buscar_maumentos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            comando.Parameters.AddWithValue("@P_FECHA_INICIAL", fechaInicial);
            comando.Parameters.AddWithValue("@P_FECHA_FINAL", fechaFinal);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                aumento.Id = lector.GetInt32(0);
                aumento.IdEmpleado = lector.GetInt32(1);
                aumento.Fecha = lector.GetDateTime(2);
                aumento.IdCategoria = lector.GetInt32(3);
                aumento.CategoriaDescripcion = lector.GetString(4);
                aumento.SueldoAnterior = lector.GetDecimal(5);
                aumento.SueldoActual = lector.GetDecimal(6);
                aumento.IdTipoAumento = lector.GetInt32(7);
                aumento.DescripcionTipoAumento = lector.GetString(8);
                aumento.Porcentaje = lector.GetDecimal(9);
                aumento.Monto = lector.GetDecimal(10);
                aumento.TotalMonto = lector.GetDecimal(11);
                aumento.Descripcion = lector.GetString(12);
                aumento.NombreEmpleado = lector.GetString(13);
                aumento.CodigoEmpleado = lector.GetString(14);
                LAumentos.Add(aumento);
                aumento = new AumentoData();

            }
            this.conexion.getConexion().Close();
            return LAumentos;
        }
        #endregion

        #region inserta un movimiento de aumento
        public bool accionesAumentos(string accion,int idAumento, int idEmpleado, int idCategoria, DateTime fecha,
                            decimal sueldoAnterior,
                            decimal sueldoActual, int idTipoAumento, decimal porcentaje, decimal montoAumento,
                            decimal totalMonto, string descripcion, string usuario)
        {
            MySqlCommand cmd = new MySqlCommand("acciones_maumentos", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_ACCION",accion );
            cmd.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            cmd.Parameters.AddWithValue("@P_ID_AUMENTO", idAumento);
            cmd.Parameters.AddWithValue("@P_FECHA", fecha);
            cmd.Parameters.AddWithValue("@P_ID_CATEGORIA", idCategoria);
            cmd.Parameters.AddWithValue("@P_SUELDO_ANTERIOR",sueldoAnterior);
            cmd.Parameters.AddWithValue("@P_SUELDO_ACTUAL", sueldoActual);
            cmd.Parameters.AddWithValue("@P_TIPO_AUMENTO_ID", idTipoAumento);
            cmd.Parameters.AddWithValue("@P_PORCENTAJE", porcentaje);
            cmd.Parameters.AddWithValue("@P_MONTO", montoAumento);
            cmd.Parameters.AddWithValue("@P_TOTAL_MONTO", totalMonto);
            cmd.Parameters.AddWithValue("@P_DESCRIPCION", descripcion);
            cmd.Parameters.AddWithValue("@P_USUARIO", usuario);
            cmd.Parameters.AddWithValue("@P_USUARIO", Session.Usuario);
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

        #region modifica un movimiento de aumento
        public bool modificarAumento(int idAumento, string codigoEmpleado, DateTime fecha,
                           string codigoCategoria, decimal sueldoAnterior,
                          decimal sueldoActual, string tipoAumento, decimal porcentaje, decimal montoAumento,
                          decimal monto, string descripcion,DateTime fechaAntigua)
        {
            {
                MySqlCommand cmd = new MySqlCommand("modificar_aumento", this.conexion.getConexion());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_AUMENTOS_ID", idAumento);
                cmd.Parameters.AddWithValue("@P_COD_TRB", codigoEmpleado);
                cmd.Parameters.AddWithValue("@P_FEC_AUM", fecha);
                cmd.Parameters.AddWithValue("@P_COD_CAT", codigoCategoria);
                cmd.Parameters.AddWithValue("@P_SUELDO_ANTERIOR", sueldoAnterior);
                cmd.Parameters.AddWithValue("@P_SUELDO_ACTUAL", sueldoActual);
                cmd.Parameters.AddWithValue("@P_TIPO_AUM", tipoAumento);
                cmd.Parameters.AddWithValue("@P_PORCENTAJE", porcentaje);
                cmd.Parameters.AddWithValue("@P_MONTO", montoAumento);
                cmd.Parameters.AddWithValue("@P_U_MONTO", monto);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", descripcion);
                cmd.Parameters.AddWithValue("@P_FECHA_ANTIGUA", fechaAntigua);
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
        public bool eliminarAumento(string codigoEmpleado, string codigoCategoria, DateTime fecha)
        {
            MySqlCommand comando = new MySqlCommand("eliminar_aumento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_TRB", codigoEmpleado);
            comando.Parameters.AddWithValue("@P_COD_CAT", codigoCategoria);
            comando.Parameters.AddWithValue("@P_FECHA", fecha);

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
        #endregion
    }
}
