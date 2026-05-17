using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace nomina.Clases.Seguridad
{
    public class Encryptacion
    {
        Rfc2898DeriveBytes key;
        RijndaelManaged algoritmo = new RijndaelManaged();
        public Encryptacion()
        {
            algoritmo.IV = ASCIIEncoding.ASCII.GetBytes("HR$2pIjHR$2pIj12");
        }



        public string EncryptStringToBytes(string plainText)
        {
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.

            try
            {
                key = new Rfc2898DeriveBytes("1&.mL8@\"pm/g]8°<=nr%]pc40c/uCJV_521", Encoding.UTF8.GetBytes("n.;34.6]p/&"));

                // Create a RijndaelManaged object

                algoritmo.Key = key.GetBytes(32);


                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = algoritmo.CreateEncryptor(algoritmo.Key, algoritmo.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                            swEncrypt.Close();
                        }
                        encrypted = msEncrypt.ToArray();
                        csEncrypt.Close();
                    }
                    msEncrypt.Close();

                }

                return Convert.ToBase64String(encrypted);
                // Return the encrypted bytes from the memory stream.

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (algoritmo != null)
                    algoritmo.Clear();
            }

        }

        public string DecryptStringFromBytes(string strEncryptar)
        {

            byte[] bytes = Convert.FromBase64String(strEncryptar);
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Rijndael object
            // with the specified key and IV.

            try
            {
                key = new Rfc2898DeriveBytes("1&.mL8@\"pm/g]8°<=nr%]pc40c/uCJV_521", Encoding.UTF8.GetBytes("n.;34.6]p/&"));

                // Create a RijndaelManaged object

                algoritmo.Key = key.GetBytes(32);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = algoritmo.CreateDecryptor(algoritmo.Key, algoritmo.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = reader.ReadToEnd();
                            reader.Close();
                        }
                        csDecrypt.Close();
                    }
                    msDecrypt.Close();
                }

                return plaintext;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return null;
            }

            finally
            {
                if (algoritmo != null)
                    algoritmo.Clear();
            }

        }
        }
    }
