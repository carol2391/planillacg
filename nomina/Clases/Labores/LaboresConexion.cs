using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.TipoPago;
using nomina.Clases.Opciones;
using nomina.Clases.Seguridad;

namespace nomina.Clases.Labores
{
    public class LaboresConexion
    {
        Conexion conexion;
        public LaboresConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener lista de labores
        public List<LaboresData> obtenerLabores()
        {

            LaboresData labor = new LaboresData();
            List<LaboresData> labores = new List<LaboresData>();
            MySqlCommand comando = new MySqlCommand("obtener_labores", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                TipoPagoData tipoPago = new TipoPagoData(lector.GetInt32(5), lector.GetString(6));
                LOpciones tipoJornada = new LOpciones(lector.GetInt32(7), lector.GetString(8));
                labores.Add(new LaboresData(
                              lector.GetInt32(0), lector.GetString(1),
                              lector.GetString(2), lector.GetDecimal(3),
                              lector.GetDecimal(4), 
                              tipoPago, tipoJornada, lector.GetInt32(9)
                           ));
            }
            this.conexion.getConexion().Close();
            return labores;
        }
        #endregion


        #region obtener una labor
        public LaboresData obtenerLabor(int id)
        {

            LaboresData labor = new LaboresData();
            MySqlCommand comando = new MySqlCommand("obtener_labor", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID_LAB", id);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {


                TipoPagoData tipoPago = new TipoPagoData(lector.GetInt32(5), lector.GetString(6));
                LOpciones tipoJornada = new LOpciones(lector.GetInt32(7), lector.GetString(8));
                labor = new LaboresData(
                              lector.GetInt32(0), lector.GetString(1),
                              lector.GetString(2), lector.GetDecimal(3),
                              lector.GetDecimal(4),
                              tipoPago, tipoJornada, lector.GetInt32(9)
                           );
            }
            this.conexion.getConexion().Close();
            return labor;
        }
        #endregion

        #region obtener una labor por el codigo para el formulario de movimientos labores
        public LaboresData obtenerLabores(string valor,string campo)
        {

            LaboresData labor = new LaboresData();
            MySqlCommand comando = new MySqlCommand("buscar_labor", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_VALOR", valor);
            comando.Parameters.AddWithValue("@P_CAMPO", campo);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                TipoPagoData tipoPago = new TipoPagoData(lector.GetInt32(6), lector.GetString(7));
                LOpciones tipoJornada = new LOpciones(lector.GetInt32(8), lector.GetString(9));
                labor = new LaboresData(
                              lector.GetInt32(0), lector.GetString(1),
                              lector.GetString(2), lector.GetDecimal(3),
                              lector.GetDecimal(4),
                              tipoPago, tipoJornada, lector.GetInt32(9)
                           );
            }
            this.conexion.getConexion().Close();
            return labor;
        }
        #endregion

        #region agregar una labor
        /*
         Método que agrega una labor a la base de datos
         retorna verdadero si se agregó existosamente
         falso para lo contrario
         */

        public Boolean accionesLabor(string accion, int idLabor, string CodigoLabor, string nombreLabor,
               int TipoJornada, decimal MontoLabor, decimal FactorLabor,
               int tipoPago, string CodigoCuenta
            )
        {
            MySqlCommand cmd = new MySqlCommand("acciones_labor", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.AddWithValue("@P_ID_LAB", idLabor);
            cmd.Parameters.AddWithValue("@P_COD_LAB", CodigoLabor);
            cmd.Parameters.AddWithValue("@P_NOM_LAB", nombreLabor);
            cmd.Parameters.AddWithValue("@P_TIPO_JORNADA", TipoJornada);
            cmd.Parameters.AddWithValue("@P_VAL_LAB", MontoLabor);
            cmd.Parameters.AddWithValue("@P_FAC_LAB", FactorLabor);
            cmd.Parameters.AddWithValue("@P_ID_TIPO_PAGO", tipoPago);
            cmd.Parameters.AddWithValue("@P_ID_CUENTA", CodigoCuenta);
            cmd.Parameters.AddWithValue("@P_USUARIO", Session.Usuario);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;


            /* try
             {*/
            //Se abre la conexión
            conexion.getConexion().Open();

            // int filasAfectadas = 
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

            /* }
             catch (MySqlException)
             {
                 this.conexion.getConexion().Close();
                 return false;
             }*/
        }
        #endregion

        #region elimina una labor si no tiene objetos asociados
        public bool eliminarLabor(int id)
        {
            MySqlCommand cmd = new MySqlCommand("eliminar_labores", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_ID_LAB ", id);
            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();

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

        #region modificar labor
        /*
         Método que modifica un empleado a la base de datos
         retorna verdadero si se agregó existosamente
         falso para lo contrario
         */

        public bool modificarLabor(int id, string CodigoLabor, string nombreLabor,
               string TipoJornada, double MontoLabor, double FactorLabor,
               string TipoLabor, string CodigoCuenta)
        {
            MySqlCommand cmd = new MySqlCommand("modificar_labores", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_LAB", CodigoLabor);
            cmd.Parameters.AddWithValue("@_NOM_LAB", nombreLabor);
            cmd.Parameters.AddWithValue("@_TIPO_DE_JORNADA", TipoJornada);
            cmd.Parameters.AddWithValue("@_VAL_LAB", MontoLabor);
            cmd.Parameters.AddWithValue("@_FAC_LAB", FactorLabor);
            cmd.Parameters.AddWithValue("@_TIPO_LAB", TipoLabor);
            cmd.Parameters.AddWithValue("@_COD_CUE", CodigoCuenta);
            cmd.Parameters.AddWithValue("@_ID_LAB", id);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();

                int filasAfectadas = cmd.ExecuteNonQuery();

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
        #region buscar labor por codigo y retorna una lista que se va mostrar en el datagrid
        public List<LaboresData> buscarLabor(string valor,string campo)
        {
            List<LaboresData> labores = new List<LaboresData>();
            MySqlCommand comando = new MySqlCommand("buscar_labor", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_VALOR", valor);
            comando.Parameters.AddWithValue("@P_CAMPO", campo);


            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();


            while (lector.Read())
            {
                TipoPagoData tipoPago = new TipoPagoData(lector.GetInt32(5), lector.GetString(6));
                LOpciones tipoJornada = new LOpciones(lector.GetInt32(7), lector.GetString(8));
                labores.Add(new LaboresData(
                              lector.GetInt32(0), lector.GetString(1),
                              lector.GetString(2), lector.GetDecimal(3),
                              lector.GetDecimal(4),
                              tipoPago, tipoJornada, lector.GetInt32(9)
                           ));
            }
            this.conexion.getConexion().Close();
            return labores;
        }
        #endregion

       
    }
}
