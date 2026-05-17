using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;
using nomina.Clases.Empleado;
using nomina.Clases.Descuentos;
using nomina.Clases.TipoPago;

namespace nomina.Clases.MovimiendoDescuentos
{
    public class MDescuentoConexion
    {
        Conexion conexion;
        public MDescuentoConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region obtener mdescuento para editar en el formulario
        public MDescuentoData obtenerMDescuento(int id
            
            //string codigoEmpleado, string codigoDescuento, DateTime fecha
            )
        {
            MDescuentoData mdescuento = new MDescuentoData();
            MySqlCommand comando = new MySqlCommand("obtener_mdescuento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID", id);
            //comando.Parameters.AddWithValue("@_COD_DEC", codigoDescuento);
            //comando.Parameters.AddWithValue("@_FEC_DEC", fecha);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                mdescuento.objEmpleado = new EmpleadoData();
                mdescuento.objDescuento = new DescuentoData();
                mdescuento.objTipoPago = new TipoPagoData();
                mdescuento.objEmpleado.Id = lector.GetInt32(0);
                mdescuento.objEmpleado.Codigo = lector.GetString(1);
                mdescuento.objEmpleado.Nombre = lector.GetString(2);
                mdescuento.objDescuento.Id = lector.GetInt32(3);
                mdescuento.objDescuento.Nombre = lector.GetString(4);
                mdescuento.CantidadDescuento = lector.GetDecimal(5);
                mdescuento.FechaDescuento = lector.GetDateTime(6);
                mdescuento.MontoDescuento = lector.GetDecimal(7);
                mdescuento.IdCuenta = lector.GetInt32(8);
                mdescuento.objTipoPago.id = lector.GetInt32(9);
                mdescuento.objTipoPago.descripcion = lector.GetString(10);
                mdescuento.idMDescuento = lector.GetInt32(11);
                mdescuento.objEmpleado.objDepto = new Departamento.DepartamentoData();
                mdescuento.objEmpleado.objDepto.NombreDepartamento= lector.GetString(12);
            }
            this.conexion.getConexion().Close();
            return mdescuento;
        }
        #endregion


        #region retorna una lista de mDescuentos, busca lel descuento segun el codigo del empleado y un rango de fecha para llenar el datagrid
        /*buscar los movimientos descuentos, todas las labores que tiene un empleado en un rango de fecha*/
        public List<MDescuentoData> buscarMDescuentos(string codigoEmpleado, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<MDescuentoData> mdescuentos = new List<MDescuentoData>();
            MDescuentoData mdescuento = new MDescuentoData();
           
            MySqlCommand comando = new MySqlCommand("buscar_mdescuentos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_EMPLEADO", codigoEmpleado);
            comando.Parameters.AddWithValue("@P_FEC_DEC_INICIAL", fechaInicial);
            comando.Parameters.AddWithValue("@P_FEC_DEC_FINAL", fechaFinal);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            
       
            while (lector.Read())
            {
                mdescuento.objEmpleado = new EmpleadoData();
                mdescuento.objDescuento = new DescuentoData();
                mdescuento.objTipoPago = new TipoPagoData();
                mdescuento.objEmpleado.Id = lector.GetInt32(0);
                mdescuento.objEmpleado.Codigo = lector.GetString(1);
                mdescuento.objEmpleado.Nombre = lector.GetString(2);
                mdescuento.objDescuento.Id = lector.GetInt32(3);
                mdescuento.objDescuento.Nombre = lector.GetString(4);
                mdescuento.CantidadDescuento = lector.GetDecimal(5);
                mdescuento.FechaDescuento = lector.GetDateTime(6);
                mdescuento.MontoDescuento = lector.GetDecimal(7);
                mdescuento.IdCuenta = lector.GetInt32(8);
                mdescuento.objTipoPago.id = lector.GetInt32(9);
                mdescuento.objTipoPago.descripcion = lector.GetString(10);
                mdescuentos.Add(mdescuento);
                mdescuento = new MDescuentoData();

            }
            this.conexion.getConexion().Close();
            return mdescuentos;
        }
        #endregion

        #region valida si un descuento esta asignado a  un movimiento  de descuento
        /*
            en el formulario de descuentos no se va poder eliminar 
            el descuento, si ya la tiene asignada un movimiento de descuento
         */
        public bool existeDescuentoEnMDescuentos(string codigoDescuento)
        {
            MySqlCommand comando = new MySqlCommand("existe_descuento_en_mdescuento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_COD_DEC", codigoDescuento);


            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                int existe = lector.GetInt32(0);
                if (existe > 0)
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
            return false;
        }
        #endregion

        #region valida si un empleado tiene un movimiento de descuento asignado
        /*
            en el formulario de empeadoos no se va poder eliminar el empleado
            si ya tiene asignado un movimiento de descuento
         */
        public bool existeEmpleadoEnMDesuentos(string codigoEmpleado)
        {
            MySqlCommand comando = new MySqlCommand("existe_empleado_en_mdescuento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_COD_TRB", codigoEmpleado);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                int existe = lector.GetInt32(0);
                if (existe > 0)
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
            return false;
        }
        #endregion

        #region valida si existe el movimiento de descuento 
        /*
          
         */
        public bool existeMDescuento(string codigoEmpleado, string codigoDescuento, DateTime fecha)
        {
            MySqlCommand comando = new MySqlCommand("existe_mdescuento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_COD_TRB", codigoEmpleado);
            comando.Parameters.AddWithValue("@_COD_DEC", codigoDescuento);
            comando.Parameters.AddWithValue("@_FEC_DEC", fecha);

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                int existe = lector.GetInt32(0);
                if (existe > 0)
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
            return false;
        }
        #endregion

        #region acciones un movimiento movimiento
        public bool accionesDescuento(string accion,int idMDescuentos, int idEmpleado, int idDescuento,
               string descripcionDescuento, int idTipoPago,
               decimal cantidadDescuento, DateTime fechaDescuento, decimal montoDescuento,
               int idCuenta
               //int idNomina,
             )
        {
            MySqlCommand cmd = new MySqlCommand("acciones_mdescuentos", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.AddWithValue("@P_ID_EMPLEADO", idEmpleado);//12
            cmd.Parameters.AddWithValue("@P_ID_DESCUENTO", idDescuento);//13
            cmd.Parameters.AddWithValue("@P_DESCRIPCION_DESCUENTO", descripcionDescuento);
            cmd.Parameters.AddWithValue("@P_ID_TIPO_PAGO", idTipoPago);//1
            cmd.Parameters.AddWithValue("@P_CANT_DESCUENTO ", cantidadDescuento);
            cmd.Parameters.AddWithValue("@P_ID_MDESCUENTOS", idMDescuentos);
            cmd.Parameters.AddWithValue("@P_FECHA_DESCUENTO", fechaDescuento);
            cmd.Parameters.AddWithValue("@P_MON_DESCUENTO", montoDescuento);
            cmd.Parameters.AddWithValue("@P_ID_CUENTA", idCuenta);
            
            // cmd.Parameters.AddWithValue("@P_ID_NOMINA",idNomina );

            cmd.Parameters.Add("@p_salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;

            //try
            //{
            //Se abre la conexión
            conexion.getConexion().Open();

            // int filasAfectadas = 
            cmd.ExecuteNonQuery();
            int salida = Convert.ToInt32(cmd.Parameters["@p_salida"].Value);

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

        #region retorna una lista de mDescuentos, busca lel descuento segun el codigo del empleado y un rango de fecha para llenar el datagrid
        /*buscar los movimientos descuentos, todas las labores que tiene un empleado en un rango de fecha*/
        public List<MDescuentoData> obtenerMDescuentos()
        {
            List<MDescuentoData> mdescuentos = new List<MDescuentoData>();
            MDescuentoData mdescuento = new MDescuentoData();

            MySqlCommand comando = new MySqlCommand("obtener_mdescuentos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
        

            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();



            while (lector.Read())
            {
                mdescuento.objEmpleado = new EmpleadoData();
                mdescuento.objDescuento = new DescuentoData();
                mdescuento.objTipoPago = new TipoPagoData();

                mdescuento.objEmpleado.Id = lector.GetInt32(0);
                mdescuento.objEmpleado.Codigo = lector.GetString(1);
                mdescuento.objEmpleado.Nombre = lector.GetString(2);
                mdescuento.objDescuento.Id = lector.GetInt32(3);
                mdescuento.objDescuento.Nombre = lector.GetString(4);
                mdescuento.CantidadDescuento = lector.GetDecimal(5);
                mdescuento.FechaDescuento = lector.GetDateTime(6);
                mdescuento.MontoDescuento = lector.GetDecimal(7);
                mdescuento.IdCuenta = lector.GetInt32(8);
                mdescuento.objTipoPago.id = lector.GetInt32(9);
                mdescuento.objTipoPago.descripcion = lector.GetString(10);
                mdescuento.idMDescuento = lector.GetInt32(11);
                mdescuento.objEmpleado.objDepto = new Departamento.DepartamentoData();
                mdescuento.objEmpleado.objDepto.NombreDepartamento = lector.GetString(12);
                mdescuentos.Add(mdescuento);
                mdescuento = new MDescuentoData();

            }
            this.conexion.getConexion().Close();
            return mdescuentos;
        }
        #endregion
    }
}
