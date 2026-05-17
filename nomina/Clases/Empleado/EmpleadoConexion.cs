using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.Opciones;

namespace nomina.Clases.Empleado
{
    public class EmpleadoConexion
    {
        Conexion conexion;
        public EmpleadoConexion(Conexion con)
        {
            this.conexion = con;
        }

        #region 
        public List<LOpciones> obtenerTipoEmpleado()
        {
            List<LOpciones> lista = new List<LOpciones>();
            try
            {

                string readCommand = "SELECT * FROM TIPO_EMPLEADO";
                MySqlCommand cmd = new MySqlCommand(readCommand, conexion.getConexion());
                cmd.CommandType = CommandType.Text;
                this.conexion.getConexion().Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    lista.Add(new LOpciones(dataReader.GetInt32(0), dataReader.GetString(1)));

                }
                this.conexion.getConexion().Close();
                return lista;
            }
            catch (Exception e)
            {
                this.conexion.getConexion().Close();
                return lista;
            }
        }

        #endregion

        #region 
        public List<LOpciones> obtenerTipoPagoEmpleado()
        {
            List<LOpciones> lista = new List<LOpciones>();
            try
            {

                string readCommand = "SELECT * FROM TIPO_PAGO_EMPLEADO";
                MySqlCommand cmd = new MySqlCommand(readCommand, conexion.getConexion());
                cmd.CommandType = CommandType.Text;
                this.conexion.getConexion().Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    lista.Add(new LOpciones(dataReader.GetInt32(0), dataReader.GetString(1)));

                }
                this.conexion.getConexion().Close();
                return lista;
            }
            catch (Exception e)
            {
                this.conexion.getConexion().Close();
                return lista;
            }
        }

        #endregion

        #region obtener lista de  empleados
        public List<EmpleadoData> obtenerEmpleados()
        {

            List<EmpleadoData> empleados = new List<EmpleadoData>();
            MySqlCommand comando = new MySqlCommand("obtener_empleados", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                EmpleadoData empleado = new EmpleadoData();
                empleado.Id = lector.GetInt32(0);
                empleado.Codigo  = lector.GetString(1);
                empleado.Nombre = lector.GetString(2);
                empleado.FechaNacimiento = lector.GetDateTime(3);
                empleado.Identidad  = lector.GetString(4);
                empleado.EstadoCivil = lector.GetString(5);
                empleado.Pasaporte = lector.GetString(6);
                empleado.RTN  = lector.GetString(7);
                empleado.Antecedentes = lector.GetString(8);
                empleado.IHS = lector.GetString(9);
                empleado.Direccion = lector.GetString(10);
                empleado.Telefono = lector.GetString(11);
                empleado.FechaIngreso = lector.GetDateTime(12);
                empleado.Sexo = lector.GetString(13);

                LOpciones tipoEmpleado = new LOpciones(lector.GetInt32(14),lector.GetString(15));



                //e.TipoEmpleado = lector.GetInt32(14);
                empleado.PuestoAsignado = lector.GetString(16);
                empleado.Sueldo = lector.GetDecimal(17);
            
                empleado.A_IHS = lector.GetString(18);
                empleado.A_FSV = lector.GetString(19);
                empleado.A_SIN = lector.GetString(20);
                empleado.A_ISR = lector.GetString(21);
                //e.TipoPago = lector.GetString(21);
                empleado.Bancos = lector.GetString(23);
                empleado.NCuenta = lector.GetString(24);
                empleado.objDepto = new Departamento.DepartamentoData(lector.GetInt32(25),"" ,lector.GetString(26),"",-1);
                empleado.objCategoria = new Categoria.CategoriaData(lector.GetInt32(27), "", lector.GetString(28), 0, 0);
               
                empleado.Celular = lector.GetString(29);
                empleado.Residencia = lector.GetString(30);
                empleado.Licencia = lector.GetString(31);
                //empleado.FechaInicio = lector.GetDateTime(32);
                empleados.Add(empleado);
            }
            this.conexion.getConexion().Close();
            return empleados;
        }
        #endregion


        #region obtener un empleado por id
        public EmpleadoData obtenerEmpleado(int id)
        {

            EmpleadoData empleado = new EmpleadoData();
            MySqlCommand comando = new MySqlCommand("obtener_empleado", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_Id", id);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
              
                empleado.Id = lector.GetInt32(0);
                empleado.Codigo = lector.GetString(1);
                empleado.Nombre = lector.GetString(2);
                empleado.FechaNacimiento = lector.GetDateTime(3);
                empleado.Identidad = lector.GetString(4);
                empleado.EstadoCivil = lector.GetString(5);
                empleado.Pasaporte = lector.GetString(6);
                empleado.RTN = lector.GetString(7);
                empleado.Antecedentes = lector.GetString(8);
                empleado.IHS = lector.GetString(9);
                empleado.Direccion = lector.GetString(10);
                empleado.Telefono = lector.GetString(11);
                empleado.FechaIngreso = lector.GetDateTime(12);
                empleado.Sexo = lector.GetString(13);

                LOpciones tipoEmpleado = new LOpciones(lector.GetInt32(14), lector.GetString(15));
                empleado.TipoEmpleado = tipoEmpleado;


                //e.TipoEmpleado = lector.GetInt32(14);
                empleado.PuestoAsignado = lector.GetString(16);
                empleado.Sueldo = lector.GetDecimal(17);

                empleado.A_IHS = lector.GetString(18);
                empleado.A_FSV = lector.GetString(19);
                empleado.A_SIN = lector.GetString(20);
                empleado.A_ISR = lector.GetString(21);
                //e.TipoPago = lector.GetString(21);
                empleado.Bancos = lector.GetString(23);
                empleado.NCuenta = lector.GetString(24);
                empleado.objDepto = new Departamento.DepartamentoData(lector.GetInt32(25), "", lector.GetString(26), "", -1);
                empleado.objCategoria = new Categoria.CategoriaData(lector.GetInt32(27), "", lector.GetString(28), 0, 0);

                empleado.Celular = lector.GetString(29);
                empleado.Residencia = lector.GetString(30);
                empleado.Licencia = lector.GetString(31);
              
                empleado.FechaInicio = lector.GetDateTime(36);
                empleado.TipoEmpleadoNacionalidad = lector.GetString(37);
                empleado.CuentaSueldo = lector.GetDecimal(38);
                empleado.CuentaSeguroSocial = lector.GetDecimal(39);
                empleado.CuentaRegimenEspecial = lector.GetDecimal(40);
                empleado.CuentaISR = lector.GetDecimal(41);
                empleado.OtraCuent1 = lector.GetDecimal(42);
                empleado.OtraCuenta2 = lector.GetDecimal(43);
                empleado.TipoPago = new LOpciones(lector.GetInt32(22), lector.GetString(32));
            }
            this.conexion.getConexion().Close();
            return empleado;
        }
        #endregion

        #region obtener un empleado por codigo
        public EmpleadoData obtenerEmpleadoCodigo(string codigo)
        {

            EmpleadoData empleado = new EmpleadoData();
            MySqlCommand comando = new MySqlCommand("obtener_empleado_codigo", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_CODIGO", codigo);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {

                empleado.Id = lector.GetInt32(0);
                empleado.Codigo = lector.GetString(1);
                empleado.Nombre = lector.GetString(2);
                empleado.FechaNacimiento = lector.GetDateTime(3);
                empleado.Identidad = lector.GetString(4);
                empleado.EstadoCivil = lector.GetString(5);
                empleado.Pasaporte = lector.GetString(6);
                empleado.RTN = lector.GetString(7);
                empleado.Antecedentes = lector.GetString(8);
                empleado.IHS = lector.GetString(9);
                empleado.Direccion = lector.GetString(10);
                empleado.Telefono = lector.GetString(11);
                empleado.FechaIngreso = lector.GetDateTime(12);
                empleado.Sexo = lector.GetString(13);

                LOpciones tipoEmpleado = new LOpciones(lector.GetInt32(14), lector.GetString(15));



                //e.TipoEmpleado = lector.GetInt32(14);
                empleado.PuestoAsignado = lector.GetString(16);
                empleado.Sueldo = lector.GetDecimal(17);

                empleado.A_IHS = lector.GetString(18);
                empleado.A_FSV = lector.GetString(19);
                empleado.A_SIN = lector.GetString(20);
                empleado.A_ISR = lector.GetString(21);
                //e.TipoPago = lector.GetString(21);
                empleado.Bancos = lector.GetString(23);
                empleado.NCuenta = lector.GetString(24);
                empleado.objDepto = new Departamento.DepartamentoData(lector.GetInt32(25), "", lector.GetString(26), "", -1);
                empleado.objCategoria = new Categoria.CategoriaData(lector.GetInt32(27), "", 
                    lector.GetString(28), lector.GetDecimal(38), lector.GetDecimal(39));

                empleado.Celular = lector.GetString(29);
                empleado.Residencia = lector.GetString(30);
                empleado.Licencia = lector.GetString(31);
                empleado.TipoEmpleado = tipoEmpleado;
                empleado.FechaInicio = lector.GetDateTime(36);
                empleado.TipoPago = new LOpciones(lector.GetInt32(22), lector.GetString(32));
            }
            this.conexion.getConexion().Close();
            return empleado;
        }
        #endregion

        #region obtener un empleado por codigo
        public EmpleadoData obtenerEmpleadoID(int id)
        {

            EmpleadoData empleado = new EmpleadoData();
            MySqlCommand comando = new MySqlCommand("obtener_empleado", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_ID", id);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {

                empleado.Id = lector.GetInt32(0);
                empleado.Codigo = lector.GetString(1);
                empleado.Nombre = lector.GetString(2);
                empleado.FechaNacimiento = lector.GetDateTime(3);
                empleado.Identidad = lector.GetString(4);
                empleado.EstadoCivil = lector.GetString(5);
                empleado.Pasaporte = lector.GetString(6);
                empleado.RTN = lector.GetString(7);
                empleado.Antecedentes = lector.GetString(8);
                empleado.IHS = lector.GetString(9);
                empleado.Direccion = lector.GetString(10);
                empleado.Telefono = lector.GetString(11);
                empleado.FechaIngreso = lector.GetDateTime(12);
                empleado.Sexo = lector.GetString(13);

                LOpciones tipoEmpleado = new LOpciones(lector.GetInt32(14), lector.GetString(15));



                //e.TipoEmpleado = lector.GetInt32(14);
                empleado.PuestoAsignado = lector.GetString(16);
                empleado.Sueldo = lector.GetDecimal(17);

                empleado.A_IHS = lector.GetString(18);
                empleado.A_FSV = lector.GetString(19);
                empleado.A_SIN = lector.GetString(20);
                empleado.A_ISR = lector.GetString(21);
                //e.TipoPago = lector.GetString(21);
                empleado.Bancos = lector.GetString(23);
                empleado.NCuenta = lector.GetString(24);
                empleado.objDepto = new Departamento.DepartamentoData(lector.GetInt32(25), "", lector.GetString(26), "", -1);
                empleado.objCategoria = new Categoria.CategoriaData(lector.GetInt32(27), "", lector.GetString(28), 0, 0);

                empleado.Celular = lector.GetString(29);
                empleado.Residencia = lector.GetString(30);
                empleado.Licencia = lector.GetString(31);
                empleado.TipoEmpleado = tipoEmpleado;
                empleado.FechaInicio = lector.GetDateTime(36);
                empleado.TipoPago = new LOpciones(lector.GetInt32(22), lector.GetString(32));
            }
            this.conexion.getConexion().Close();
            return empleado;
        }
        #endregion


        #region agregar empleado
        /*
         Método que agrega un empleado a la base de datos
         retorna verdadero si se agregó existosamente
         falso para lo contrario
         */

        public bool accionesEmpleado(string accion, int id, string Codigo, string nombre,
                           DateTime FechaNacimiento,
                           string Identidad, string EstadoCivil, string Pasaporte, string RTN,
                           string Antecedentes,
                           string IHS, string direccion, string Telefono, DateTime FechaIngreso,
                           string Sexo, int TipoEmpleado, int idDepto, int idCategoria,
                           string PuestoAsignado,
                           double Sueldo, string A_IHS, string A_FSV, string A_SIN, string A_ISR,
                           int TipoPago, string Bancos, string NCuenta, string celular, string residencia,
                           string licencia,DateTime fechaInicio,
                           string tipoEmpleadoNacionalidad,string numeroCuenta,string cuentaSeguroSocial,
                           string cuentaRegimenEspecial,string cuentaISR, string cuenta1,string cuenta2)
        {
            MySqlCommand cmd = new MySqlCommand("acciones_empleado", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_ACCION", accion);
            cmd.Parameters.AddWithValue("@P_ID", id);
            cmd.Parameters.AddWithValue("@P_COD_TRB", Codigo);
            cmd.Parameters.AddWithValue("@P_NOM_TRB", nombre);
            cmd.Parameters.AddWithValue("@P_FEC_NAC", FechaNacimiento.Date);
            cmd.Parameters.AddWithValue("@P_IDEN_TRB", Identidad);
            cmd.Parameters.AddWithValue("@P_EST_TRB", EstadoCivil);
            cmd.Parameters.AddWithValue("@P_PAST_TRB", Pasaporte);
            cmd.Parameters.AddWithValue("@P_RTN_TRB", RTN);
            cmd.Parameters.AddWithValue("@P_ANT_TRB", Antecedentes);
            cmd.Parameters.AddWithValue("@P_IHS_TRB", IHS);
            cmd.Parameters.AddWithValue("@P_DIR_TRB", direccion);
            cmd.Parameters.AddWithValue("@P_TEL_TRB", Telefono);
            cmd.Parameters.AddWithValue("@P_FEC_DEF", FechaIngreso.Date);
            cmd.Parameters.AddWithValue("@P_SEX_TRB", Sexo);
            cmd.Parameters.AddWithValue("@P_TIPO_TRB", TipoEmpleado);
            cmd.Parameters.AddWithValue("@P_ID_DEP", idDepto);
            cmd.Parameters.AddWithValue("@P_ID_CAT", idCategoria);
            cmd.Parameters.AddWithValue("@P_PUEST_TRB", PuestoAsignado);
            cmd.Parameters.AddWithValue("@P_SUELDO", Sueldo);
            cmd.Parameters.AddWithValue("@P_A_IHS", A_IHS);
            cmd.Parameters.AddWithValue("@P_A_FSV", A_FSV);
            cmd.Parameters.AddWithValue("@P_A_SIN", A_SIN);
            cmd.Parameters.AddWithValue("@P_A_ISR", A_ISR);
            cmd.Parameters.AddWithValue("@P_ID_FORMA_PAGO", TipoPago);
            cmd.Parameters.AddWithValue("@P_BANCOS", Bancos);
            cmd.Parameters.AddWithValue("@P_NCUENTA", NCuenta);
            cmd.Parameters.AddWithValue("@P_CELULAR_TRB", celular);
            cmd.Parameters.AddWithValue("@P_RESIDENCIA_TRB",residencia);
            cmd.Parameters.AddWithValue("@P_LICENCIA_TRB", licencia);
            cmd.Parameters.AddWithValue("@P_FECHA_INICIO", fechaInicio);
            cmd.Parameters.AddWithValue("@P_TIPO_EMPLEADO", tipoEmpleadoNacionalidad);

            cmd.Parameters.AddWithValue("@P_CUENTA_SUELDO", numeroCuenta);
            cmd.Parameters.AddWithValue("@P_CUENTA_SEGURO_SOCIAL", cuentaSeguroSocial);
            cmd.Parameters.AddWithValue("@P_CUENTA_REGIMEN_ESPECIAL", cuentaRegimenEspecial);
            cmd.Parameters.AddWithValue("@P_CUENTA_ISR", cuentaISR);
            cmd.Parameters.AddWithValue("@P_OTRA_CUENTA_1", cuenta1);
            cmd.Parameters.AddWithValue("@P_OTRA_CUENTA_2", cuenta2);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;


            //try
            //{
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

        //}
        //     catch (MySqlException)
        //     {
        //         this.conexion.getConexion().Close();
        //         return false;
        //     }
}
        #endregion

        #region eliminar empleado si no tiene objetos asociados
        public bool eliminarEmpleado(string codEmpleado)
        {
            MySqlCommand cmd = new MySqlCommand("eliminar_empleado", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_TRB", codEmpleado);
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

        #region modificar empleado
        /*
         Método que modifica un empleado a la base de datos
         retorna verdadero si se agregó existosamente
         falso para lo contrario
         */

        public bool modificarEmpleado(string Codigo, string nombre,
                           DateTime FechaNacimiento,
                           string Identidad, string EstadoCivil, string Pasaporte, string RTN,
                           string Antecedentes,
                           string IHS, string direccion, string Telefono, DateTime FechaIngreso,
                           string Sexo, string TipoEmpleado, int idDepto, int idCategoria,
                           string PuestoAsignado,
                           double Sueldo, string A_IHS, string A_FSV, string A_SIN, string A_ISR,
                           string TipoPago, string Bancos, string NCuenta, int id,
                           string celular, string residencia,string licencia)
        {
            MySqlCommand cmd = new MySqlCommand("modificar_empleado", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_TRB", Codigo);
            cmd.Parameters.AddWithValue("@_NOM_TRB", nombre);
            cmd.Parameters.AddWithValue("@_FEC_NAC", FechaNacimiento.Date);
            cmd.Parameters.AddWithValue("@_IDEN_TRB", Identidad);
            cmd.Parameters.AddWithValue("@_EST_TRB", EstadoCivil);
            cmd.Parameters.AddWithValue("@_PAST_TRB", Pasaporte);
            cmd.Parameters.AddWithValue("@_RTN_TRB", RTN);
            cmd.Parameters.AddWithValue("@_ANT_TRB", Antecedentes);
            cmd.Parameters.AddWithValue("@_IHS_TRB", IHS);
            cmd.Parameters.AddWithValue("@_DIR_TRB", direccion);
            cmd.Parameters.AddWithValue("@_TEL_TRB", Telefono);
            cmd.Parameters.AddWithValue("@_FEC_DEF", FechaIngreso.Date);
            cmd.Parameters.AddWithValue("@_SEX_TRB", Sexo);
            cmd.Parameters.AddWithValue("@_TIPO_TRB", TipoEmpleado);
            cmd.Parameters.AddWithValue("@_ID_DEP", idDepto);
            cmd.Parameters.AddWithValue("@_ID_CAT", idCategoria);
            cmd.Parameters.AddWithValue("@_PUEST_TRB", PuestoAsignado);
            cmd.Parameters.AddWithValue("@_SUELDO", Sueldo);
            cmd.Parameters.AddWithValue("@_A_IHS", A_IHS);
            cmd.Parameters.AddWithValue("@_A_FSV", A_FSV);
            cmd.Parameters.AddWithValue("@_A_SIN", A_SIN);
            cmd.Parameters.AddWithValue("@_A_ISR", A_ISR);
            cmd.Parameters.AddWithValue("@_TIPO_PAG", TipoPago);
            cmd.Parameters.AddWithValue("@_BANCOS", Bancos);
            cmd.Parameters.AddWithValue("@_NCUENTA", NCuenta);
            cmd.Parameters.AddWithValue("@_CELULAR_TRB", celular);
            cmd.Parameters.AddWithValue("@_RESIDENCIA_TRB",residencia);
            cmd.Parameters.AddWithValue("@_LICENCIA_TRB", licencia);
            cmd.Parameters.AddWithValue("@ID",id);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32).Direction = ParameterDirection.Output;
            //try
            //{
                //Se abre la conexión
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
            // }
            //catch (MySqlException)
            //{
            //    this.conexion.getConexion().Close();
            //    return false;
            //}
        }
        #endregion

        #region buscar empleado por codigo y retorna una lista que se va mostrar en el datagrid
        public List<EmpleadoData> buscarEmpleado(string valor,string campo)
        {
            List<EmpleadoData> empleados = new List<EmpleadoData>();
            EmpleadoData empleado = new EmpleadoData();
            MySqlCommand comando = new MySqlCommand("buscar_empleado", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_CAMPO", campo);
            comando.Parameters.AddWithValue("@P_VALOR", valor);


            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                empleado = new EmpleadoData();
                empleado.Id = lector.GetInt32(0);
                empleado.Codigo = lector.GetString(1);
                empleado.Nombre = lector.GetString(2);
                empleado.FechaNacimiento = lector.GetDateTime(3);
                empleado.Identidad = lector.GetString(4);
                empleado.EstadoCivil = lector.GetString(5);
                empleado.Pasaporte = lector.GetString(6);
                empleado.RTN = lector.GetString(7);
                empleado.Antecedentes = lector.GetString(8);
                empleado.IHS = lector.GetString(9);
                empleado.Direccion = lector.GetString(10);
                empleado.Telefono = lector.GetString(11);
                empleado.FechaIngreso = lector.GetDateTime(12);
                empleado.Sexo = lector.GetString(13);
                LOpciones tipoEmpleado = new LOpciones(lector.GetInt32(14), lector.GetString(15));

                //e.TipoEmpleado = lector.GetInt32(14);
                empleado.PuestoAsignado = lector.GetString(16);
                empleado.Sueldo = lector.GetDecimal(17);

                empleado.A_IHS = lector.GetString(18);
                empleado.A_FSV = lector.GetString(19);
                empleado.A_SIN = lector.GetString(20);
                empleado.A_ISR = lector.GetString(21);
                //e.TipoPago = lector.GetString(21);
                empleado.Bancos = lector.GetString(23);
                empleado.NCuenta = lector.GetString(24);
                empleado.objDepto = new Departamento.DepartamentoData(lector.GetInt32(25), "", lector.GetString(26),"", -1);
                empleado.objCategoria = new Categoria.CategoriaData(lector.GetInt32(27), "", lector.GetString(28), 0, 0);

                empleado.Celular = lector.GetString(29);
                empleado.Residencia = lector.GetString(30);
                empleado.Licencia = lector.GetString(31);
                empleado.TipoEmpleado = tipoEmpleado;
                empleado.TipoPago = new LOpciones(lector.GetInt32(22), lector.GetString(32));
                empleados.Add(empleado);
            }
            this.conexion.getConexion().Close();
            return empleados;
        }
        #endregion

        #region buscar empleado por nombre retorna una lista de empleados que se muestra en el datagrid
        public List<EmpleadoData> buscarNombreEmpleado(string nombre)
        {
            List<EmpleadoData> empleados = new List<EmpleadoData>();
            MySqlCommand comando = new MySqlCommand("buscar_nombre_empleado", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_NOM_TRB", nombre);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                //empleados.Add(new EmpleadoData(
                //            lector.GetInt32(0), lector.GetString(1),
                //            lector.GetString(2), lector.GetDateTime(3),
                //            lector.GetString(4), lector.GetString(5),
                //            lector.GetString(6), lector.GetString(7),
                //            lector.GetString(8), lector.GetString(9),
                //            lector.GetString(10), lector.GetString(11),
                //            lector.GetDateTime(12), lector.GetString(13),
                //            lector.GetString(14), lector.GetString(15),
                //            lector.GetDouble(16), lector.GetString(17),
                //            lector.GetString(18), lector.GetString(19),
                //            lector.GetString(20), lector.GetString(21),
                //            lector.GetString(22), lector.GetString(23),
                //            lector.GetInt32(24), lector.GetString(25),
                //             lector.GetInt32(26), lector.GetString(27),
                //              lector.GetString(28), lector.GetString(29),
                //               lector.GetString(30), lector.GetString(31),
                //              lector.GetDecimal(32), lector.GetDecimal(33)
                //         ));
            }
            this.conexion.getConexion().Close();
            return empleados;
        }
        #endregion
    }
}
