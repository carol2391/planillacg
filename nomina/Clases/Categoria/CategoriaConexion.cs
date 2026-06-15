using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.Seguridad;


namespace nomina.Clases.Categoria
{
    public class CategoriaConexion
    {
        Conexion conexion;
        public CategoriaConexion(Conexion con)
        {
            this.conexion = con;
        }

       

        #region obtener lista de categorias
        public List<CategoriaData> obtenerCategorias()
        {

            List<CategoriaData> categorias = new List<CategoriaData>();
            MySqlCommand comando = new MySqlCommand("obtener_categorias", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {

                categorias.Add(new CategoriaData(lector.GetInt32(0), lector.GetString(1),
                                     lector.GetString(2),
                                     lector.GetDecimal(3),
                                     lector.GetDecimal(4)
                                    ));
            }
            this.conexion.getConexion().Close();
            return categorias;
        }
        #endregion

        #region agregar categoria
        /*
         Método que agrega una categoria a la base de datos
         retorna verdadero si se agregó existosamente
         falso para lo contrario
         */

        public bool accionesCategoria(string accion, int idCategoria, string codCategoria, string nombreCategoria,
                                           decimal saldoInicial, decimal saldoFinal)
        {
            MySqlCommand cmd = new MySqlCommand("acciones_categoria", this.conexion.getConexion());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@P_COD_CAT", codCategoria);
            cmd.Parameters.AddWithValue("@P_NOM_CAT", nombreCategoria);
            cmd.Parameters.AddWithValue("@P_SAL_INI", saldoInicial);
            cmd.Parameters.AddWithValue("@P_SAL_FIN", saldoFinal);
            cmd.Parameters.AddWithValue("@P_ID_CATEGORIA", idCategoria);
            cmd.Parameters.AddWithValue("@P_ACCION",accion);
            cmd.Parameters.AddWithValue("@P_USUARIO", Session.Usuario);
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

//        #region eliminar categoria si no tiene objetos asociados
//        public bool eliminarCategoria(string codCategoria, int id)
//        {
//            MySqlCommand cmd = new MySqlCommand("eliminar_categoria", this.conexion.getConexion());
//            cmd.CommandType = System.Data.CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@_COD_CAT", codCategoria);
//            cmd.Parameters.AddWithValue("@_ID_CAT", id);
//            //cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
//            try
//            {
//                conexion.getConexion().Open();

//            int filasAfectadas = cmd.ExecuteNonQuery();

//           // int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);

//            if (filasAfectadas == 1)
//            {
//                this.conexion.getConexion().Close();
//                return true;
//            }
//            else
//            {
//                this.conexion.getConexion().Close();
//                return false;
//            }
//        }
//              catch (MySqlException)
//              {
//                  this.conexion.getConexion().Close();
//                  return false;
//              }
//}
//        #endregion

//        #region modificar categoria
//        /*
//      Método que modifica un categoria la base de datos
//      retorna verdadero si se agregó existosamente
//      falso para lo contrario
//      */

//        public Boolean modificarCategoria(string codCategoria, string nombreCategoria,
//                                         double saldoInicial, double saldoFinal, int id)
//        {
//            MySqlCommand cmd = new MySqlCommand("modificar_categoria", this.conexion.getConexion());
//            cmd.CommandType = System.Data.CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@_COD_CAT", codCategoria);
//            cmd.Parameters.AddWithValue("@_NOM_CAT", nombreCategoria);
//            cmd.Parameters.AddWithValue("@_SAL_INI", saldoInicial);
//            cmd.Parameters.AddWithValue("@_SAL_FIN", saldoFinal);
//            cmd.Parameters.AddWithValue("@_ID_CAT", id);
//            cmd.Parameters.Add("@salida", MySqlDbType.Int32, 20).Direction = ParameterDirection.Output;
//            /*try
//            {*/
//            conexion.getConexion().Open();

//            // int filasAfectadas = 
//            cmd.ExecuteNonQuery();
//            int salida = Convert.ToInt32(cmd.Parameters["@salida"].Value);

//            if (salida == 1)
//            {
//                this.conexion.getConexion().Close();
//                return true;
//            }
//            else
//            {
//                this.conexion.getConexion().Close();
//                return false;
//                /* }
//                 catch (MySqlException)
//                 {
//                     this.conexion.getConexion().Close();
//                     return false;
//                 }*/
//            }
//        }
//        #endregion

        //#region valida si existe el nombre o la categoria para un registro nuevo
        //public bool existeCategoriaNuevo(string codigoCategoria,string nombreCategoria)
        //{
        //    MySqlCommand cmd = new MySqlCommand("existe_categoria_nuevo", this.conexion.getConexion());
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@_COD_CAT", codigoCategoria);
        //    cmd.Parameters.AddWithValue("@_NOM_CAT", nombreCategoria);
        //    try
        //    {
        //        //Se abre la conexión
        //        conexion.getConexion().Open();
        //        MySqlDataReader lector = cmd.ExecuteReader();
        //        bool filas = lector.Read();
        //        if (filas)
        //        {
        //            conexion.getConexion().Close();
        //            return true;
        //        }
        //        else
        //        {
        //            conexion.getConexion().Close();
        //            return false;
        //        }
        //    }
        //    catch (MySqlException)
        //    {
        //        this.conexion.getConexion().Close();
        //        return false;
        //    }
        //}
        //#endregion

        //#region valida si existe la categoria para un registro que se va editar
        //public int existeCategoriaModificar(int id, string codigoCategoria, string nombreCategoria)
        //{
        //    MySqlCommand cmd = new MySqlCommand("existe_categoria_editar", this.conexion.getConexion());
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@_ID_CAT", id);
        //    cmd.Parameters.AddWithValue("@_COD_CAT", codigoCategoria);
        //    cmd.Parameters.AddWithValue("@_NOM_CAT", nombreCategoria);

        //    try
        //    {
        //        //Se abre la conexión
        //        conexion.getConexion().Open();
        //        MySqlDataReader lector = cmd.ExecuteReader();
        //        bool filas = lector.Read();
        //        if (filas)
        //        {
        //            int existeId = lector.GetInt32(0);
        //            conexion.getConexion().Close();
        //            return existeId;
        //        }
        //        else
        //        {  //si retorna -1 no existe  la categoria
        //            conexion.getConexion().Close();
        //            return -1;
        //        }

        //    }
        //    catch (MySqlException)
        //    {
        //        this.conexion.getConexion().Close();
        //        return 0;
        //    }
        //}
        //#endregion

        //#region buscar codigo
        //public List<CategoriaData> buscarCodigoCategoria(string codigo)
        //{
        //    List<CategoriaData> categorias = new List<CategoriaData>();
        //    MySqlCommand comando = new MySqlCommand("buscar_codigo_categoria", this.conexion.getConexion());
        //    comando.CommandType = System.Data.CommandType.StoredProcedure;
        //    comando.Parameters.AddWithValue("@_COD_CAT", codigo);
        //    this.conexion.getConexion().Open();

        //    MySqlDataReader lector = comando.ExecuteReader();

        //    while (lector.Read())
        //    {

        //        categorias.Add(new CategoriaData(lector.GetInt32(0),lector.GetString(1),
        //                             lector.GetString(2),
        //                             lector.GetDouble(3),
        //                             lector.GetDouble(4)
        //                            ));
        //    }
        //    this.conexion.getConexion().Close();
        //    return categorias;
        //}
        //#endregion

        #region buscar nombre
        public List<CategoriaData> buscarCategoria(string valor,string campo)
        {
            List<CategoriaData> categorias = new List<CategoriaData>();
            MySqlCommand comando = new MySqlCommand("buscar_categoria", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_VALOR", valor);
            comando.Parameters.AddWithValue("@P_CAMPO", campo);
            this.conexion.getConexion().Open();

            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {


                categorias.Add(new CategoriaData(lector.GetInt32(0), lector.GetString(1),
                                     lector.GetString(2),
                                     lector.GetDecimal(3),
                                     lector.GetDecimal(4)
                                    ));
            }
            this.conexion.getConexion().Close();
            return categorias;
        }
        #endregion
    }
}
