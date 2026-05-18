using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
namespace nomina.Clases.Departamento
{
    public class DepartamentoConexion
    {
        Conexion conexion;
        public DepartamentoConexion(Conexion con) {
            this.conexion = con;
        }

        /*
         Método que agrega un departamento a la base de datos
         retorna verdadero si se agregó existosamente
         falso para lo contrario
         */

        public bool accionDepartamento(string accion, int idDepartamento, string codDepto,string nombreDepto, 
                                           int id_encargado,int idCuenta)
       {

           
          MySqlCommand cmd = new MySqlCommand("acciones_departamento", this.conexion.getConexion());
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@P_ACCION", accion);
          cmd.Parameters.AddWithValue("@P_ID_DEP", idDepartamento);
          cmd.Parameters.AddWithValue("@P_COD_DEP",codDepto);
          cmd.Parameters.AddWithValue("@P_NOM_DEP", nombreDepto);
         
                cmd.Parameters.AddWithValue("@P_ID_EMPLEADO", id_encargado);
          cmd.Parameters.AddWithValue("@P_ID_CUENTA", idCuenta);
          cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
            //try
            //{
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
            //}
            //catch (MySqlException)
            //{
            //    this.conexion.getConexion().Close();
            //    return false;
            //}
        }

        /*
        Método que modifica un departamento a la base de datos
        retorna verdadero si se agregó existosamente
        falso para lo contrario
        */

        public Boolean modificarDepartamento(string codDepto, string nombreDepto,
                                          string encargadoDepto, string codCuenta, int id)
        {
            MySqlCommand cmd = new MySqlCommand("modificar_departamento", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_DEP", codDepto);
            cmd.Parameters.AddWithValue("@_NOM_DEP", nombreDepto);
            cmd.Parameters.AddWithValue("@_NOM_ENC", encargadoDepto);
            cmd.Parameters.AddWithValue("@_COD_CUE", codCuenta);
            cmd.Parameters.AddWithValue("@_ID_DEP ", id);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            try
            {
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

        public List<DepartamentoData> obtenerDepartamentos() {

            List<DepartamentoData> departamentos = new List<DepartamentoData>();
            MySqlCommand comando = new MySqlCommand("obtener_departamentos", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();
            int idDepto = 0;
           int codCuenta = 0;
            string jefe=" ";
            while (lector.Read())
            {
               if (!lector.IsDBNull(3))
                  idDepto = lector.GetInt32(3);
                if (!lector.IsDBNull(4))
                    codCuenta = lector.GetInt32(4);
                if (!lector.IsDBNull(5))
                    jefe = lector.GetString(5);
               departamentos.Add(new DepartamentoData(lector.GetInt32(0),
                                    lector.GetString(1),
                                    lector.GetString(2),
                                    jefe,
                                    codCuenta                                    
                                   ));
                idDepto = 0;
                codCuenta = 0;
                jefe = "";
            }
            this.conexion.getConexion().Close();
            return departamentos;
        }
        public bool existeNombreDepto(string nombreDepto,int id) {
            MySqlCommand cmd = new MySqlCommand("existe_nombre_depto", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_NOM_DEP", nombreDepto);
            cmd.Parameters.AddWithValue("@_ID_DEP", id);
            cmd.Parameters.Add("@salida", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();
                MySqlDataReader lector = cmd.ExecuteReader();
                bool filas = lector.Read();
                if (filas)
                {
                      conexion.getConexion().Close();
                      return true;
                }
                else
                {   conexion.getConexion().Close();
                    return false;
                }
    
            }
            catch (MySqlException)
            {
                this.conexion.getConexion().Close();
                return false;
            }
        }

        public int existeDepartamentoEditar(int idDepto, string codigoDepto, string nombreDepto)
        {
            MySqlCommand cmd = new MySqlCommand("existe_departamento_editar", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_ID_DEP", idDepto);
            cmd.Parameters.AddWithValue("@_COD_DEP", codigoDepto);
            cmd.Parameters.AddWithValue("@_NOM_DEP", nombreDepto);
            

            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();
                MySqlDataReader lector = cmd.ExecuteReader();
                bool filas = lector.Read();
                if (filas)
                {
                    
                    int id = lector.GetInt32(0);
                    conexion.getConexion().Close();
                    return id;
                }
                else
                {  //si retorna -1 no existe el departamento
                    conexion.getConexion().Close();
                    return -1;
                }

            }
            catch (MySqlException)
            {
                this.conexion.getConexion().Close();
                return 0;
            }
        }

        public bool existeNombreModificar(string nombreDepto)
        {
            MySqlCommand cmd = new MySqlCommand("existe_nombre_depto_modificar", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_NOM_DEP", nombreDepto);

            try
            {
                //Se abre la conexión
                conexion.getConexion().Open();
                MySqlDataReader lector = cmd.ExecuteReader();
                bool filas = lector.Read();
                if (filas)
                {    
                     conexion.getConexion().Close();
                     return true;  
                }
                else
                {
                    conexion.getConexion().Close();
                    return false;
                }

            }
            catch (MySqlException)
            {
                this.conexion.getConexion().Close();
                return false;
            }
        }
        public bool existeCodigoDepto(string codDepto)
        {
            MySqlCommand cmd = new MySqlCommand("existe_codigo_depto", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_DEP", codDepto);

            try
            {
                conexion.getConexion().Open();
                MySqlDataReader lector = cmd.ExecuteReader();
                bool filas = lector.Read();
                if (filas)
                {
                     conexion.getConexion().Close();
                     return true;
                }
                else
                {
                    conexion.getConexion().Close();
                    return false;
                }
            }
            catch (MySqlException)
            {
                this.conexion.getConexion().Close();
                return false;
            }
           
        }

        public bool eliminarDepartamento(string codDepto,int id) {
            MySqlCommand cmd = new MySqlCommand("eliminar_departamento", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@_COD_DEP", codDepto);
            cmd.Parameters.AddWithValue("@_ID_DEP", id);
            //cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
            try
            {
                conexion.getConexion().Open();

                int filasAfectadas = cmd.ExecuteNonQuery();

                //int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);

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
        
        public List<DepartamentoData> buscarNombreDepartamentos(string nombre)
        {

            List<DepartamentoData> departamentos = new List<DepartamentoData>();
            MySqlCommand comando = new MySqlCommand("buscar_nombre_departamento", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@_NOM_DEP", nombre);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();
            string jefe = " ";
            while (lector.Read())
            {
                if (!lector.IsDBNull(5))
                    jefe = lector.GetString(5);
                departamentos.Add(new DepartamentoData(lector.GetInt32(0),
                                     lector.GetString(1),
                                     lector.GetString(2),
                                     jefe,
                                     lector.GetInt32(4)
                                    ));
            }
            this.conexion.getConexion().Close();
            return departamentos;
        }

        public List<DepartamentoData> buscarDepartamento(string valor, string campo)
        {

            try
            {
                List<DepartamentoData> departamentos = new List<DepartamentoData>();
                MySqlCommand comando = new MySqlCommand("buscar_departamento", this.conexion.getConexion());
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@P_VALOR", valor);
                comando.Parameters.AddWithValue("@P_CAMPO", campo);
                this.conexion.getConexion().Open();

                MySqlDataReader lector = comando.ExecuteReader();
                int idDepto = 0;
                int codCuenta = 0;
                string jefe = " ";
                while (lector.Read())
                {
                    if (!lector.IsDBNull(5))
                        jefe = lector.GetString(5);
                        departamentos.Add(new DepartamentoData(lector.GetInt32(0),
                                             lector.GetString(1),
                                             lector.GetString(2),
                                             jefe,
                                             lector.IsDBNull(4) ? 0 : lector.GetInt32(4)
                                            ));
                }

                this.conexion.getConexion().Close();
                return departamentos;
        }
            catch (Exception ex)
            {
                return null;
            }

}
    }
}
