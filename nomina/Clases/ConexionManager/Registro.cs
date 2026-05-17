using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Estructuras;
using System.Windows.Forms;
using Microsoft.Win32;

namespace nomina.Clases.ConexionManager
{
    public class Registro
    {
        public Registro()
        {

        }

        EncriptacionConexion Encrpt = new EncriptacionConexion();

        /// <summary>
        /// Clase de crear el registro para la base de datos
        /// </summary>
        /// <param name="psServerData"></param>
        public void createRegister(ServidorData psServerData)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\GS\RR");

            try
            {
                key.SetValue("Servidor", psServerData.server);
                key.SetValue("Usuario", psServerData.user);
                key.SetValue("Contraseña", psServerData.password);
                //key.SetValue("BaseDatos", Encrpt.encryptConnection(psServerData.database));
                //key.SetValue("Limiteconexion", Encrpt.encryptConnection(psServerData.limitedConnection));
                key.SetValue("Puerto", psServerData.port);

                key.Close();

                MessageBox.Show("Se creo nueva conexión", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al crear conexión: " + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// Clase de leer el registro si existe
        /// </summary>
        /// <returns></returns>
        public ServidorData leerRegistro()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\GS\RR");
            ServidorData serverData = new ServidorData();


            if (key != null)
            {

                serverData.server = key.GetValue("servidor").ToString();
                serverData.user = key.GetValue("Usuario").ToString();
                serverData.password = key.GetValue("Contraseña").ToString();
                //serverData.database = Encrpt.decryptConnection(key.GetValue("BaseDatos").ToString());
                //serverData.limitedConnection = Encrpt.decryptConnection(key.GetValue("LimiteConexion").ToString());
                serverData.port = key.GetValue("Puerto").ToString();

                key.Close();

                return serverData;
            }
            else
            {

                //MessageBox.Show("Error al encontrar conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serverData = null;
                return serverData;
            }
        }


        public string getConnetionString()
        {

            ServidorData serverData = new ServidorData();
            serverData = (ServidorData)leerRegistro();

            string connectioUrl = "Data Source = " +
                                   serverData.server + "," +
                                   serverData.port + "; Initial Catalog = " +
                                   serverData.database + "; Persist Security Info = True; User ID = " +
                                   serverData.user + "; Password = " +
                                   serverData.password + "; Encrypt = False; Network Library = dbmssocn; MultipleActiveResultSets=True";

            return connectioUrl;
        }
    }
}
