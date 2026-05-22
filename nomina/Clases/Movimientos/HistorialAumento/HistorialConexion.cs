using MySql.Data.MySqlClient;
using nomina.Clases.ConexionManager;
using System;
using System.Collections.Generic;

namespace nomina.Clases.Movimientos.HistorialAumento
{
    class HistorialConexion
    {
        Conexion conexion;
        public HistorialConexion(Conexion con)
        {
            this.conexion = con;
        }
        #region
        public List<HistorialData> buscarAumentosHistorial(int idEmpelado, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<HistorialData> lHistorial = new List<HistorialData>();
            MySqlCommand comando = new MySqlCommand("buscar_aumentos_en_historial", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpelado);
            comando.Parameters.AddWithValue("@P_FECI", fechaInicial);
            comando.Parameters.AddWithValue("@P_FECF", fechaFinal);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lHistorial.Add(new HistorialData(
                               lector.GetString(0), lector.GetString(1), lector.GetDateTime(2),
                               lector.GetDecimal(3), lector.GetDecimal(4),
                               lector.GetDecimal(5), lector.GetString(6)
                            ));

            }
            this.conexion.getConexion().Close();
            return lHistorial;
        }
       #endregion
    }
}
