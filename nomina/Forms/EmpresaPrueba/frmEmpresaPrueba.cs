using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // No olvidar este using.
using MySql.Data;
using System.Data.Entity.Migrations;
using System.Text;
using System.IO;
using System.Data;

namespace nomina.Forms.EmpresaPrueba
{
    public partial class frmEmpresaPrueba : Form 
    {
        public frmEmpresaPrueba()
        {
            InitializeComponent();
        }

        static string connectionString = "datasource=127.0.0.1;" +
                   "port=3306;username=root;" +
                  "password=;Convert Zero Datetime=True;  Allow User Variables=True";

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string path = @"C:\estructura_bd.sql";

            // This text is added only once to the file.
            if (File.Exists(path))
            {
                // Open the file to read from.
                string readText = File.ReadAllText(path);
                readText = readText.Replace("nomina_v1", "n1");
                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlCommand cmd = new MySqlCommand(readText,connection);
                cmd.CommandType = CommandType.Text;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("agrego");
            }

             path = @"C:\procedimientos_almacenados.sql";

            // This text is added only once to the file.
            if (File.Exists(path))
            {
                // Open the file to read from.
                string readText = File.ReadAllText(path);
                readText = readText.Replace("nomina_v1", "n1");
                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlCommand cmd = new MySqlCommand(readText, connection);
                cmd.CommandType = CommandType.Text;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("agrego procedimientos");
            }



        }

        public void archivo() {

            string path =@"C:\bdnomina.sql";

            // This text is added only once to the file.
            if (File.Exists(path))
            {
                // Open the file to read from.
                string readText = File.ReadAllText(path);
                readText = readText.Replace("nomina_v1","n1");
               MessageBox.Show(readText);
            }
        
           

          
        }
      
        private void crearBd()
        {

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS prueba_db;", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            string categoria = "CREATE TABLE IF NOT EXISTS `nomina_v1`.`categoria` (" +
              "`ID_CAT` INT(11) NOT NULL AUTO_INCREMENT," +
              "`COD_CAT` VARCHAR(3) NOT NULL," +
              "`NOM_CAT` VARCHAR(30) NOT NULL," +
              "`SAL_INI` DOUBLE(17, 2) NOT NULL COMMENT 'salario inicial'," +
              "`SAL_FIN` DOUBLE(17, 2) NOT NULL COMMENT 'salario final'," +
              "`ACCESO` VARCHAR(24) NULL DEFAULT NULL," +
              "PRIMARY KEY(`ID_CAT`))" +

            "DEFAULT CHARACTER SET = latin1;" +
            "CREATE UNIQUE INDEX `COD_CATEGORIA_UNIQUE` ON `nomina_v1`.`categoria` (`COD_CAT` ASC);" +
            "CREATE UNIQUE INDEX `NOM_CAT_UNIQUE` ON `nomina_v1`.`categoria` (`NOM_CAT` ASC);";

            categoria = categoria.Replace("nomina_v1", "prueba_db");
            cmd = new MySqlCommand("USE prueba_db;", connection);
            connection.Open();
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(categoria, connection);
            
            cmd.ExecuteNonQuery();
            connection.Close();

            string insertarCategoria =
            "DELIMITER ## " +
             "USE `nomina_v1` " + " ## " +
            " CREATE DEFINER =`root`@`localhost` PROCEDURE  `insertar_categoria`(" + 

                           " IN _COD_CAT VARCHAR(3)," +
                            "IN _NOM_CAT VARCHAR(30)," +
                            "IN _SAL_INI DOUBLE(17, 2)," +
                            "IN _SAL_FIN DOUBLE(17, 2)," +
                             "OUT salida INT" +
                           " )" + 
        "BEGIN "  +
            " set salida = 0;" + 
            "          if (existe_categoria_nuevo(_COD_CAT, _NOM_CAT) = 0) THEN " + 
                            "INSERT INTO categoria(COD_CAT, NOM_CAT, SAL_INI, SAL_FIN)" +
        "  VALUES(_COD_CAT, _NOM_CAT, _SAL_INI, _SAL_FIN);" + "\n" +
            " SET salida = 1; " + 
            "ELSE "  +
                "  SET salida = 0; " +
            "END IF; " +
            "END## " + 
            "DELIMITER; ";

            insertarCategoria = insertarCategoria.Replace("nomina_v1", "prueba_db");
            MessageBox.Show(insertarCategoria);
            //conexion = new MySqlConnection(connectionString);
            cmd = new MySqlCommand("USE prueba_db;", connection);
            cmd.CommandType = System.Data.CommandType.Text;
            connection.Open();
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(insertarCategoria, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
