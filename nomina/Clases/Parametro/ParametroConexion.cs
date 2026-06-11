using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;

namespace nomina.Clases.Parametro
{
    public class ParametroConexion
    {
        Conexion conexion;
        public ParametroConexion(Conexion conexion) {
            this.conexion = conexion;
        }
        #region obtener un parametro para editar en el formulario
        public ParametroData obtenerParametro(int id)
        {
            ParametroData parametro = new ParametroData();
            MySqlCommand comando = new MySqlCommand("obtener_parametro", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_PARAMETRO_ID", id);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                parametro = new ParametroData(
                               lector.GetInt32(0),
                               lector.GetInt32(1),
                               lector.GetDecimal(2),
                               lector.GetDecimal(3),
                                lector.GetDecimal(4),
                                lector.GetDecimal(5),
                                lector.GetDecimal(6),
                                lector.GetDecimal(7),
                                lector.GetDecimal(8),
                               lector.GetDecimal(9),
                                lector.GetDecimal(10), 
                                lector.GetDecimal(11),
                                lector.GetDecimal(12),
                                lector.GetDecimal(13)

                            );

            }
            this.conexion.getConexion().Close();
            return parametro;
        }
        #endregion

        #region retorna una lista de prestamos, busca el prestamo segun el codigo del empleado y un rango de fecha para llenar el datagrid
        public List<ParametroData> buscarParametros(int periodo)
        {
            List<ParametroData> lParametros = new List<ParametroData>();
            MySqlCommand comando = new MySqlCommand("buscar_parametro", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_PERIODO", periodo);
           

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lParametros.Add(new ParametroData(
                               lector.GetInt32(0),
                               lector.GetInt32(1),
                               lector.GetDecimal(2),
                               lector.GetDecimal(3),
                                lector.GetDecimal(4),
                                lector.GetDecimal(5),
                                lector.GetDecimal(6),
                                lector.GetDecimal(7),
                                lector.GetDecimal(8),
                               lector.GetDecimal(9),
                                lector.GetDecimal(10),
                                lector.GetDecimal(11),
                                 lector.GetDecimal(12),
                                 lector.GetDecimal(13)
                            ));
            }
            this.conexion.getConexion().Close();
            return lParametros;
        }
        #endregion

        #region insertar paramero
        public bool agregarParametro( int Periodo, decimal Excento,
           decimal RangoInicial15,
            decimal RangoFinal15, decimal RangoInicial20, decimal RangoFinal20,
            decimal RangoInicial25, decimal RangoFinal25,decimal sueldoPromedio,
            decimal reservaLaboralRAP, decimal valorPisoRAP, decimal salarioMinimoPromedio,
            decimal valorTechoIhss

           )
        {
            MySqlCommand cmd = new MySqlCommand("insertar_parametro", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_PERIODO", Periodo);
            cmd.Parameters.AddWithValue("@P_EXCENTO", Excento);
            cmd.Parameters.AddWithValue("@P_RANGO_INICIAL15", RangoInicial15);
            cmd.Parameters.AddWithValue("@P_RANGO_FINAL15", RangoFinal15);
            cmd.Parameters.AddWithValue("@P_RANGO_INICIAL20", RangoInicial20);
            cmd.Parameters.AddWithValue("@P_RANGO_FINAL20", RangoFinal20);
            cmd.Parameters.AddWithValue("@P_RANGO_INICIAL25", RangoInicial25);
            cmd.Parameters.AddWithValue("@P_RANGO_FINAL25", RangoFinal25);
            cmd.Parameters.AddWithValue("@P_SUELDO_PROMEDIO", sueldoPromedio);
            cmd.Parameters.AddWithValue("@P_RESERVA_LABORAL_RAP", reservaLaboralRAP);
            cmd.Parameters.AddWithValue("@P_VALOR_PISO_RAP", valorPisoRAP);
            cmd.Parameters.AddWithValue("@P_SALARIO_MINIMO_PROMEDIO", salarioMinimoPromedio);
            cmd.Parameters.AddWithValue("@P_VALOR_TECHO_IHSS", valorTechoIhss);
            // cmd.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            try
            {
                //    //Se abre la conexión
                conexion.getConexion().Open();

            int filasAfectadas = cmd.ExecuteNonQuery();
            //int salida = Convert.ToInt32(cmd.Parameters["@P_SALIDA"].Value);
            /*si es igual a uno no existe*/
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

        #region modificar parametro
        public bool modificarParametro(int id, int Periodo, decimal Excento, decimal RangoInicial15,
            decimal RangoFinal15, decimal RangoInicial20, decimal RangoFinal20,
            decimal RangoInicial25, decimal RangoFinal25, decimal sueldoPromedio,
            decimal reservaLaboralRAP, decimal valorPisoRAP, decimal salarioMinimoPromedio,
            decimal valorTechoIhss

           )
        { 
            MySqlCommand cmd = new MySqlCommand("modificar_parametro", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("@P_PARAMETRO_ID", id);
            cmd.Parameters.AddWithValue("@P_PERIODO", Periodo);
            cmd.Parameters.AddWithValue("@P_EXCENTO", Excento);
            cmd.Parameters.AddWithValue("@P_RANGO_INICIAL15", RangoInicial15);
            cmd.Parameters.AddWithValue("@P_RANGO_FINAL15", RangoFinal15);
            cmd.Parameters.AddWithValue("@P_RANGO_INICIAL20", RangoInicial20);
            cmd.Parameters.AddWithValue("@P_RANGO_FINAL20", RangoFinal20);
            cmd.Parameters.AddWithValue("@P_RANGO_INICIAL25", RangoInicial25);
            cmd.Parameters.AddWithValue("@P_RANGO_FINAL25", RangoFinal25);
            cmd.Parameters.AddWithValue("@P_SUELDO_PROMEDIO",sueldoPromedio);
            cmd.Parameters.AddWithValue("@P_SUELDO_PROMEDIO", sueldoPromedio);
            cmd.Parameters.AddWithValue("@P_RESERVA_LABORAL_RAP", reservaLaboralRAP);
            cmd.Parameters.AddWithValue("@P_VALOR_PISO_RAP", valorPisoRAP);
            cmd.Parameters.AddWithValue("@P_SALARIO_MINIMO_PROMEDIO", salarioMinimoPromedio);
            cmd.Parameters.AddWithValue("@P_VALOR_TECHO_IHSS", valorTechoIhss);
            // cmd.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            try
            {
                //    //Se abre la conexión
                conexion.getConexion().Open();

            int filasAfectadas = cmd.ExecuteNonQuery();
            //int salida = Convert.ToInt32(cmd.Parameters["@P_SALIDA"].Value);
            /*si es igual a uno no existe*/
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

        #region elimina un parametro
        public bool eliminarParametro(int id)
        {
            MySqlCommand comando = new MySqlCommand("eliminar_parametro", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_PARAMETRO_ID", id);

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



