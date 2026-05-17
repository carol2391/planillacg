using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;

namespace nomina.Clases.Antecedentes
{
  public  class AntedecenteConexion
    {
        Conexion conexion;
        public AntedecenteConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region accione antecedente
        public bool accionesAntecedentes(string accion,int idAntecente, int idEmpleado, int numeroAntecedente,
                                        DateTime fechaEmision, DateTime fechaVencimiento,
                                           DateTime vigencia, string lugarOrigen,string tipoAntecedente)
        {
            MySqlCommand cmd = new MySqlCommand("acciones_antecedente", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_NUMERO_ANTECEDENTE", numeroAntecedente);
            cmd.Parameters.AddWithValue("@P_FECHA_EMISION", fechaEmision);
            cmd.Parameters.AddWithValue("@P_FECHA_VENCIMIENTO", fechaVencimiento);
            cmd.Parameters.AddWithValue("@P_VIGENCIA", vigencia);
            cmd.Parameters.AddWithValue("@P_LUGAR_ORIGEN", lugarOrigen);
            cmd.Parameters.AddWithValue("@P_ID_EMPLEADO",idEmpleado);
            cmd.Parameters.AddWithValue("@P_ID_ANTECEDENTE", idAntecente);
            cmd.Parameters.AddWithValue("@P_TIPO_ANTECEDENTE", tipoAntecedente);
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            try
            {
                conexion.getConexion().Open();
                cmd.ExecuteNonQuery();
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

        #region obtener lista de categorias
        public List<AntecedenteData> obtenerAntecedentes(string tipoAntecedente,int idEmpleado)
        {

            List<AntecedenteData> antecedentes = new List<AntecedenteData>();
            MySqlCommand comando = new MySqlCommand("obtener_antecedentes", this.conexion.getConexion());
            comando.Parameters.AddWithValue("@P_TIPO_ANTECEDENTE", tipoAntecedente);
            comando.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();
            AntecedenteData antece = new AntecedenteData();
            while (lector.Read())
            {
                
                antece.Id = lector.GetInt32(0);
                antece.NumeroAntecedente = lector.GetInt32(1);
                antece.FechaEmision = lector.GetDateTime(2);
                antece.FechaVencimiento= lector.GetDateTime(3);
                antece.Vigencia = lector.GetDateTime(4);
                antece.LugarOrigen = lector.GetString(5);
                antece.IdEmpleado = lector.GetInt32(6);
                antecedentes.Add(antece);
                antece = new AntecedenteData();
            }

            this.conexion.getConexion().Close();
            return antecedentes;
        }
        #endregion

        #region obtener antecedente
        public AntecedenteData obtenerAntecedente(int id, int idEmpleado,string tipoAntecedente)
        {

             MySqlCommand comando = new MySqlCommand("obtener_antecedente", this.conexion.getConexion());
            comando.Parameters.AddWithValue("@P_ID", id);
            comando.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);
            comando.Parameters.AddWithValue("@P_TIPO_ANTECEDENTE", tipoAntecedente);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();
            AntecedenteData antecedente = new AntecedenteData();
            if (lector.Read())
            {

                antecedente.Id = lector.GetInt32(0);
                antecedente.NumeroAntecedente = lector.GetInt32(1);
                antecedente.FechaEmision = lector.GetDateTime(2);
                antecedente.FechaVencimiento = lector.GetDateTime(3);
                antecedente.Vigencia = lector.GetDateTime(4);
                antecedente.LugarOrigen = lector.GetString(5);
                antecedente.IdEmpleado = lector.GetInt32(6);

            }
            this.conexion.getConexion().Close();
            return antecedente;
        }
        #endregion

        #region buscar antecedente por codigo y retorna una lista que se va mostrar en el datagrid
        public List<AntecedenteData> buscarAntecedente(DateTime fechaInicial, DateTime fechaFinal, string tipoAntecedente)
        {
            List<AntecedenteData> antecedentes = new List<AntecedenteData>();
            AntecedenteData antecedente = new AntecedenteData();
            MySqlCommand comando = new MySqlCommand("buscar_antecedentes", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_FECHA_VENCIMIENTO_INICIAL", fechaInicial);
            comando.Parameters.AddWithValue("@P_FECHA_VENCIMIENTO_FINAL", fechaFinal);
            comando.Parameters.AddWithValue("@P_TIPO_ANTECEDENTE", tipoAntecedente);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                antecedente = new AntecedenteData();
                antecedente.Id = lector.GetInt32(0);
                antecedente.NumeroAntecedente = lector.GetInt32(1);
                antecedente.FechaEmision = lector.GetDateTime(2);
                antecedente.FechaVencimiento = lector.GetDateTime(3);
                antecedente.Vigencia = lector.GetDateTime(4);
                antecedente.LugarOrigen = lector.GetString(5);
                antecedente.IdEmpleado = lector.GetInt32(6);
                antecedentes.Add(antecedente);
            }
            this.conexion.getConexion().Close();
            return antecedentes;
        }
        #endregion
    }
}
