using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Windows.Forms;

namespace nomina.Clases.ConexionManager
{
    public class EncriptacionConexion
    {
        public string encryptConnection(string strAEncriptar)
        {

            string resultado = null;

            try
            {
                byte[] bytesAEncriptar = Encoding.UTF8.GetBytes(strAEncriptar);
                RijndaelManaged algoritmo = new RijndaelManaged(); byte[] bytesEncriptados;

                // inicializar algoritmo
                algoritmo.Key = new Rfc2898DeriveBytes("0&.mQ8@\"¡n*g]7°<=nr%]ap40c/uCJV_236", Encoding.UTF8.GetBytes("p&N30=cB*}.ñ")).GetBytes(32);
                algoritmo.GenerateIV();

                // Encriptarbytes

                bytesEncriptados = algoritmo.CreateEncryptor().TransformFinalBlock(bytesAEncriptar, 0, bytesAEncriptar.Length);
                Array.Resize(ref bytesEncriptados, bytesEncriptados.Length + algoritmo.IV.Length);
                Array.Copy(algoritmo.IV, 0, bytesEncriptados, bytesEncriptados.Length - algoritmo.IV.Length, algoritmo.IV.Length);

                // Convertir bytesEncriptados en un string de resultado

                resultado = Convert.ToBase64String(bytesEncriptados);
            }
            catch (CryptographicException e)
            {
                // Console.WriteLine("A Cryptographic error occurred: {0}", }
            }

            catch (UnauthorizedAccessException e)
            {
                // Console.WriteLine("A file error occurred: {0}", }
            }


            return resultado;
        }

        public string decryptConnection(string strADesencriptar)
        {
            string resultado = null;
            if (string.IsNullOrEmpty(strADesencriptar))
                return resultado;

            try
            {
                RijndaelManaged algoritmo = new RijndaelManaged();
                byte[] IVAux = new byte[algoritmo.IV.Length - 1 + 1];
                byte[] bytesADesencriptarConIV = Convert.FromBase64String(strADesencriptar);
                byte[] bytesADesencriptar = new byte[bytesADesencriptarConIV.Length - algoritmo.IV.Length - 1 + 1];
                Array.Copy(bytesADesencriptarConIV, 0, bytesADesencriptar, 0, bytesADesencriptarConIV.Length - algoritmo.IV.Length);

                byte[] bytesDesencriptados;

                // inicializar algoritmo
                algoritmo.Key = new Rfc2898DeriveBytes("0&.mQ8@\"¡n*g]7°<=nr%]ap40c/uCJV_236", Encoding.UTF8.GetBytes("p&N30=cB*}.ñ")).GetBytes(32);
                Array.Copy(bytesADesencriptarConIV, bytesADesencriptarConIV.Length - algoritmo.IV.Length, IVAux, 0, algoritmo.IV.Length);
                algoritmo.IV = IVAux;

                // Encriptar
                bytesDesencriptados = algoritmo.CreateDecryptor().TransformFinalBlock(bytesADesencriptar, 0, bytesADesencriptar.Length);

                // Convertir bytesEncriptados en un string de resultado
                resultado = Encoding.UTF8.GetString(bytesDesencriptados);
            }
            /// Cambiar mensaje por el messenger
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error al desencriptar cadena de caracteres.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return resultado;
        }
    }
}
