using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;
using nomina.Clases.Descuentos;
using nomina.Clases.TipoPago;
using nomina.Clases.Opciones;

namespace nomina.Clases.Descuentos
{
    public class DescuentoConexion
    {
        Conexion conexion;
        public DescuentoConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener lista de descuentos
        public List<DescuentoData> obtenerDescuentos()
        {

            List<DescuentoData> descuentos = new List<DescuentoData>();
            MySqlCommand comando = new MySqlCommand("obtener_descuentos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                LOpciones tipoJornada = new LOpciones(lector.GetInt32(5), lector.GetString(6));
                TipoPagoData tipoPago = new TipoPagoData(lector.GetInt32(7), lector.GetString(8));

                descuentos.Add(new DescuentoData(
                              lector.GetInt32(0), lector.GetString(1),
                              lector.GetString(2), lector.GetDecimal(3),
                              lector.GetDecimal(4), tipoPago, tipoJornada
                           ));
            }
            this.conexion.getConexion().Close();
            return descuentos;
        }
        #endregion


        #region obtener un descuento
        public DescuentoData obtenerDescuento(int id)
        {
            DescuentoData descuento = new DescuentoData();
            MySqlCommand comando = new MySqlCommand("obtener_descuento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID_DEC", id);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                LOpciones tipoJornada = new LOpciones(lector.GetInt32(5), lector.GetString(6));
                TipoPagoData tipoPago = new TipoPagoData(lector.GetInt32(7), lector.GetString(8));
                

                descuento = new DescuentoData(
                              lector.GetInt32(0), lector.GetString(1),
                              lector.GetString(2), lector.GetDecimal(3),
                              lector.GetDecimal(4), tipoPago,tipoJornada
                           );
            }
            this.conexion.getConexion().Close();
            return descuento;
        }
        #endregion

        #region agregar un descuento
        public Boolean accionesDescuento(string accion, int idDescuento,string codigo, string nombre,
              decimal monto, decimal factor, int idTipoJornada,
               int idTipoPago, int idCodCuenta
            )
        {
            MySqlCommand cmd = new MySqlCommand("acciones_descuento", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_COD_DEC", codigo);
            cmd.Parameters.AddWithValue("@P_NOM_DEC", nombre);
            cmd.Parameters.AddWithValue("@P_VAL_DEC", monto);
            cmd.Parameters.AddWithValue("@P_FAC_DEC", factor);
            cmd.Parameters.AddWithValue("@P_ID_TIPO_JORNADA", idTipoJornada);
            cmd.Parameters.AddWithValue("@P_ID_TIPO_PAGO", idTipoPago);
            cmd.Parameters.AddWithValue("@P_ID_COD_CUE", idCodCuenta);
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.AddWithValue("@P_ID_DEC", idDescuento);
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

        #region elimina un descuento
        public bool eliminarDescuento(int id)
        {
            MySqlCommand cmd = new MySqlCommand("eliminar_descuento", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_ID_DEC ", id);
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

        #region modifica un descuento
        public Boolean modificarDescuento(int id, string codigo, string nombre,
              double monto, double factor, string TipoJornada,
               string tipoDescuento, string CodigoCuenta
            )
        {
            MySqlCommand cmd = new MySqlCommand("modificar_descuento", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_DEC", codigo);
            cmd.Parameters.AddWithValue("@_NOM_DEC", nombre);
            cmd.Parameters.AddWithValue("@_VAL_DEC", monto);
            cmd.Parameters.AddWithValue("@_FAC_DEC", factor);
            cmd.Parameters.AddWithValue("@_T_JOR", TipoJornada);
            cmd.Parameters.AddWithValue("@_TIPO_DEC", tipoDescuento);
            cmd.Parameters.AddWithValue("@_COD_CUE", CodigoCuenta);
            cmd.Parameters.AddWithValue("@_ID_DEC", id);
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

        #region buscar descuento por codigo y retorna una lista que se va mostrar en el datagrid
        public List<DescuentoData> buscarDescuento(string valor,string campo)
        {
            List<DescuentoData> descuentos = new List<DescuentoData>();
            MySqlCommand comando = new MySqlCommand("buscar_descuento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_VALOR", valor);
            comando.Parameters.AddWithValue("@P_CAMPO", campo);


            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();


            while (lector.Read())
            {
                TipoPagoData tipoPago = new TipoPagoData(lector.GetInt32(5), lector.GetString(6));
                LOpciones tipoJornada = new LOpciones(lector.GetInt32(7), lector.GetString(8));

                descuentos.Add(new DescuentoData(
                              lector.GetInt32(0), lector.GetString(1),
                              lector.GetString(2), lector.GetDecimal(3),
                              lector.GetDecimal(4), tipoPago, tipoJornada
                           
                           )
                           );
                           
            }
            this.conexion.getConexion().Close();
            return descuentos;
        }
        #endregion

       // #region buscar descuento por nombre retorna una lista de empleados que se muestra en el datagrid
        //public List<DescuentoData> buscarNombreDescuento(string nombre)
        //{
        //    //List<DescuentoData> descuentos = new List<DescuentoData>();
        //    //MySqlCommand comando = new MySqlCommand("buscar_nombre_descuento", this.conexion.getConexion());
        //    //comando.CommandType = System.Data.CommandType.StoredProcedure;
        //    //comando.Parameters.AddWithValue("@_NOM_DEC", nombre);
        //    //this.conexion.getConexion().Open();

        //    //MySqlDataReader lector = comando.ExecuteReader();

        //    //while (lector.Read())
        //    //{
        //    //    descuentos.Add(new DescuentoData(
        //    //                   lector.GetInt32(0), lector.GetString(1),
        //    //                   lector.GetString(2), lector.GetDouble(3),
        //    //                   lector.GetDouble(4), lector.GetString(5),
        //    //                   lector.GetString(6), lector.GetString(7)
        //    //                ));
        //    //}
        //    //this.conexion.getConexion().Close();
        //    //return descuentos;
        //}
        //#endregion

     
    }
}
