using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;

namespace nomina.Clases.GenerarPlanilla
{
    public class PlanillaConexion
    {
        public event EventHandler ExportProgres;

      
        Conexion conexion;
        public PlanillaConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region GENERAR PLANILLA
        public string generarPlanilla(string codigoPlanilla,DateTime fecha)
        {
             MySqlCommand comando = new MySqlCommand("generar_planilla", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_PLANILLA", codigoPlanilla);
            comando.Parameters.AddWithValue("@P_FECHA", fecha.Date);
            comando.Parameters.Add("@P_SALIDA", MySqlDbType.Int32).Direction = ParameterDirection.Output;
            //try
            //{
            this.conexion.getConexion().Open();

            string salida = Convert.ToString(comando.Parameters["@P_SALIDA"].Value);
            this.conexion.getConexion().Close();
            return salida;

        //}
            //catch (MySqlException)
            //{
            //    this.conexion.getConexion().Close();
            //    return false;
            //}
}
        #endregion

      
        #region EXISTE PLANILLA
        public bool existePlanilla(string codigoPlanilla, DateTime fecha)
        {
            MySqlCommand comando = new MySqlCommand("existe_planilla", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_PLANILLA", codigoPlanilla);
            comando.Parameters.AddWithValue("@P_FECHA", fecha.Date);

            comando.Parameters.Add("@P_SALIDA", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();
                comando.ExecuteNonQuery();

                int salida = Convert.ToInt32(comando.Parameters["@P_SALIDA"].Value);
                /*si es igual a uno no existe*/
                if (salida >= 1)
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
