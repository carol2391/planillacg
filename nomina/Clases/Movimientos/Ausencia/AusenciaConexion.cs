using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;

namespace nomina.Clases.Ausencia
{
    public class AusenciaConexion
    {
        Conexion conexion;
        public AusenciaConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener una ausencia para editar en el formulario
        public List<AusenciaData> obtenerAusencias()
        {
            AusenciaData mausencia = new AusenciaData();
            List<AusenciaData> lausencias = new List<AusenciaData>();
            MySqlCommand comando = new MySqlCommand("obtener_mausencias", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
           // comando.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            //comando.Parameters.AddWithValue("@P_COD_NOM", codigoNomina);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {

                mausencia.Id = lector.GetInt32(0);
                mausencia.CodigoEmpleado = lector.GetString(1);
                mausencia.nombreEmpleado = lector.GetString(2);
                mausencia.IdEmpleado = lector.GetInt32(3);
                mausencia.fechaInicio = lector.GetDateTime(4);
                mausencia.fechaFinal = lector.GetDateTime(5);
                mausencia.diasDeAusencia = lector.GetInt32(6);
                mausencia.monto = lector.GetDecimal(7);
                mausencia.tipoAusencia = lector.GetString(8);
                mausencia.Septimo = lector.GetString(9);
                mausencia.IdTipoAusencia = lector.GetInt32(10);
                lausencias.Add(mausencia);
                mausencia = new AusenciaData();


            }
            this.conexion.getConexion().Close();
            return lausencias;
        }
        #endregion
        #region obtener una ausencia para editar en el formulario
        public AusenciaData obtenerAusencia(string codigoEmpleado, DateTime fechaInicial,string codigoNomina)
        {
            AusenciaData mausencia = new AusenciaData();
            MySqlCommand comando = new MySqlCommand("obtener_mausencia", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_TRB", codigoEmpleado);
            comando.Parameters.AddWithValue("@P_FECHA_INICIAL", fechaInicial);
            //comando.Parameters.AddWithValue("@P_COD_NOM", codigoNomina);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {

                mausencia.Id = lector.GetInt32(0);
                mausencia.CodigoEmpleado = lector.GetString(1);
                mausencia.nombreEmpleado = lector.GetString(2);
                mausencia.IdEmpleado = lector.GetInt32(3);
                mausencia.fechaInicio = lector.GetDateTime(4);
                mausencia.fechaFinal = lector.GetDateTime(5);
                mausencia.diasDeAusencia = lector.GetInt32(6);
                mausencia.monto = lector.GetDecimal(7);
                mausencia.tipoAusencia = lector.GetString(8);
                mausencia.Septimo = lector.GetString(9);
                mausencia.IdTipoAusencia = lector.GetInt32(10);
                
                            

            }
            this.conexion.getConexion().Close();
            return mausencia;
        }
        #endregion
        /*falta*/
        #region retorna una lista de ausencias, busca la ausnecia segun el codigo del empleado y un rango de fecha para llenar el datagrid
        public List<AusenciaData> buscarAusencias(string codigoEmpleado, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<AusenciaData> lAusencias = new List<AusenciaData>();
            MySqlCommand comando = new MySqlCommand("buscar_mausencias", this.conexion.getConexion());
            AusenciaData mausencia = new AusenciaData();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_TRB", codigoEmpleado);
            comando.Parameters.AddWithValue("@P_FECHA_INICIAL", fechaInicial);
            comando.Parameters.AddWithValue("@P_FECHA_FINAL", fechaFinal);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                mausencia.Id = lector.GetInt32(0);
                mausencia.CodigoEmpleado = lector.GetString(1);
                mausencia.nombreEmpleado = lector.GetString(2);
                mausencia.IdEmpleado = lector.GetInt32(3);
                mausencia.fechaInicio = lector.GetDateTime(4);
                mausencia.fechaFinal = lector.GetDateTime(5);
                mausencia.diasDeAusencia = lector.GetInt32(6);
                mausencia.monto = lector.GetDecimal(7);
                mausencia.tipoAusencia = lector.GetString(8);
                mausencia.Septimo = lector.GetString(9);
                mausencia.IdTipoAusencia = lector.GetInt32(10);
                lAusencias.Add(mausencia);
                mausencia = new AusenciaData();

            }
            this.conexion.getConexion().Close();
            return lAusencias;
        }
        #endregion

        #region inserta un movimiento de ausencia
        public bool accionesMAusencia(string accion, int idEmpleado, int idAusencia, int idTipoAusencia,
               DateTime fechaInicial, DateTime fechaFinal,
              int idNomina,string septimo,decimal monto)
        {
            MySqlCommand cmd = new MySqlCommand("acciones_mausencias", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            cmd.Parameters.AddWithValue("@P_ID_AUSENCIA", idAusencia);
            cmd.Parameters.AddWithValue("@P_ID_TIPO_AUSENCIA", idTipoAusencia);
            cmd.Parameters.AddWithValue("@P_FEC_INICIAL_AU", fechaInicial);
            cmd.Parameters.AddWithValue("@P_FEC_FINAL_AU", fechaFinal);
            cmd.Parameters.AddWithValue("@P_ID_NOMINA", idNomina);
            cmd.Parameters.AddWithValue("@P_SEPTIMO", septimo);
            cmd.Parameters.AddWithValue("@P_MONTO", monto);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            //try
            //{
                //Se abre la conexión
                conexion.getConexion().Open();

               
               cmd.ExecuteNonQuery();
                int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);
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
        //    catch (MySqlException)
        //    {
        //        this.conexion.getConexion().Close();
        //        return false;
        //    }
}
        #endregion

        #region modifica un movimiento de ausencia
        public bool modificarAusencia(int id, string codigoEmpleado, string tipoAusencia,
               DateTime fechaInicial, DateTime fechaFinal,
               string codigoNomina,DateTime fechaAntigua,string septimo)
        {
            MySqlCommand cmd = new MySqlCommand("modificar_ausencia", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_AUSENCIAS_ID", id);
            cmd.Parameters.AddWithValue("@_COD_TRB", codigoEmpleado);
            cmd.Parameters.AddWithValue("@_T_AU", tipoAusencia);
            cmd.Parameters.AddWithValue("@_FECI_AU", fechaInicial);
            cmd.Parameters.AddWithValue("@_FECF_AU", fechaFinal);
            cmd.Parameters.AddWithValue("@_COD_NOM ", codigoNomina); ;
            cmd.Parameters.AddWithValue("@FECHA_ANTIGUA ", fechaAntigua);
            cmd.Parameters.AddWithValue("@P_SEPTIMO", septimo);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;


            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();

                // int filasAfectadas = 
                cmd.ExecuteNonQuery();
                /*si es igual a 1 lo modifico*/
                int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);
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
        #endregion


        #region elimina un  movimiento de ausencia
        public bool eliminarAusencia(string codigoEmpleado, string codigoNomina, DateTime fecha)
        {
            MySqlCommand comando = new MySqlCommand("eliminar_ausencia", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_TRB", codigoEmpleado);
            comando.Parameters.AddWithValue("@P_COD_NOM", codigoNomina);
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
