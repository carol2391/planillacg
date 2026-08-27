using MySql.Data.MySqlClient;
using nomina.Clases.ConexionManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nomina.Clases.Reportes
{
    public class ReportesConexion
    {
        Conexion conexion;
        public ReportesConexion(Conexion conexion)
        {
            this.conexion = conexion;
        }
        public DataTable ObtenerReportesNomina(string nombreReporte, int mes, int anio, string tipo)
        {
            DataTable dt = new DataTable();

            MySqlConnection conn = this.conexion.getConexion();

            using (MySqlCommand comando = new MySqlCommand(nombreReporte, conn))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@P_MES", mes);
                comando.Parameters.AddWithValue("@P_ANIO", anio);
                comando.Parameters.AddWithValue("@P_TIPO", tipo);

                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                conn.Close();
            }

            return dt;
        
        }

        public DataTable ObtenerReportesAsalariados(string nombreReporte, int mes, int anio)
        {
            DataTable dt = new DataTable();

            MySqlConnection conn = this.conexion.getConexion();

            using (MySqlCommand comando = new MySqlCommand(nombreReporte, conn))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@P_MES", mes);
                comando.Parameters.AddWithValue("@P_ANIO", anio);

                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                conn.Close();
            }

            return dt;

        }

        public DataTable ObtenerReportes(string nombreReporte, int? filtro)
        {
            DataTable dt = new DataTable();

            MySqlConnection conn = this.conexion.getConexion();

            using (MySqlCommand comando = new MySqlCommand(nombreReporte, conn))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@P_FILTRO",
                    (object)filtro ?? DBNull.Value);

                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                 da.Fill(dt);
                 conn.Close();
            }

            return dt;
        }
        public DataTable ObtenerReporteLiquidacion(int idEmpleado, DateTime fecha)
        {
            DataTable dt = new DataTable();

            MySqlConnection conn = this.conexion.getConexion();

            using (MySqlCommand comando = new MySqlCommand("sp_reporte_liquidacion_detallado", conn))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@p_id_empleado",idEmpleado );
                comando.Parameters.AddWithValue("@p_fecha_fin", fecha);

                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                conn.Close();
            }

            return dt;
        }

        public DataTable ObtenerFichaEmpleado(int idEmpleado)
        {
            DataTable dt = new DataTable();

            MySqlConnection conn = this.conexion.getConexion();

            using (MySqlCommand comando = new MySqlCommand("sp_ficha_empleado", conn))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@p_id_empleado", idEmpleado);

                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                conn.Close();
            }

            return dt;
        }

        public DataTable ObtenerBitacora(string user, string modo)
        {
            DataTable dt = new DataTable();

            MySqlConnection conn = this.conexion.getConexion();

            using (MySqlCommand comando = new MySqlCommand("sp_consultar_bitacora", conn))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@P_NOMBRE_USUARIO", user);
                comando.Parameters.AddWithValue("@P_MODO", modo);

                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                conn.Close();
            }

            return dt;
        }

    }
}
