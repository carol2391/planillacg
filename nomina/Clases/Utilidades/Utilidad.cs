using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace nomina.Clases.Utilidades
{
    public static class Utilidad
    {

        #region valida si el campo no esta vacio y controla el error
        public static void isEmpyErrorPro(Object objecto, string mensaje, System.ComponentModel.CancelEventArgs e, ErrorProvider errorProvider1)
        {
            TextBox campo;
            if (objecto is TextBox) {
                campo = (TextBox)objecto;
                if (string.IsNullOrEmpty(campo.Text.Trim()))
                {
                    e.Cancel = true;
                    campo.Select(0, campo.Text.Length);
                    errorProvider1.SetError(campo, "Debe introducir " + mensaje);
                }
               
            }
        }
        #endregion

        public static void selectAllNumeric(NumericUpDown numeric)
        {
            numeric.Select(0, numeric.Value.ToString().Length);
        }
        public static void configuarForm(Form f, string text)
        {

            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            f.MaximizeBox = false;
            f.Text = text;
            //f.Icon = nomina.Properties.Resources.Retenciones;
            f.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            f.BackColor =
              System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            
        }
        public static void configurarDataGrid(DataGridView data)
        {
            data.MultiSelect = false;
            data.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            data.DefaultCellStyle.Font = new System.Drawing.Font(data.Font.Name, data.Font.Size, FontStyle.Regular);
            data.ReadOnly = true;
            data.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new System.Drawing.Font(data.Font.Name, data.Font.Size, FontStyle.Bold)
            };
            data.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
        }
        public static void esLetra(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    if (char.IsSeparator(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Handled = true;
                    }
                }
            }
        }
        public static void cambiarControlEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        public static void aceptaSoloNumerosDecimales(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }



         public static void aceptaSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
               
                e.Handled = true;
            }
        }
        public static void esDouble(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }//fin funcion es double

        public static bool esLetraMasket(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                return true;
            }
            return false;
        }

        public static bool esEntero(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
                return true;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    return false;
                }
                else
                {
                    e.Handled = true;
                    return true;
                }
            }
        }//fin funcion es entero

        public static void configurarNumericUp(NumericUpDown numeric)
        {
            numeric.Controls.RemoveAt(0);
            numeric.InterceptArrowKeys = false;
        }
    }
}
