using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Usuarios;
using nomina.Clases.Utilidades;
using nomina.Clases.PermisosUsuario;
using nomina.Estructuras;
using nomina.Forms.Servidor;
using nomina.Clases.ConexionManager;

namespace nomina.Forms.Login
{
    public partial class frmLogin : Form
    {
        #region propiedades
        private UsuarioConexion userConexion;
        private PermisoUsuarioConexion bdPermisos;
        public int usuarioId { set; get; }
        public string usuarioName { set; get; }
        ServidorData servidor;
        #endregion
        public frmLogin()
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Login");
            this.Opacity = .90;

            FormBorderStyle = FormBorderStyle.None;
            this.userConexion = new UsuarioConexion();
            this.bdPermisos = new PermisoUsuarioConexion();
            this.txtPassword.Text = "Contraseña";
            this.txtUser.Text = "Usuario";
            txtPassword.PasswordChar = '\0';
            this.txtUser.ForeColor = txtPassword.ForeColor = Color.Snow;
            pictureBox1.Select();
            txtUser.Text = "YURO";
            txtPassword.Text = "dragonrojo";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.usuario.ToUpper().Trim().Equals(txtUser.Text.ToUpper().Trim()) &&
              Properties.Settings.Default.password.ToUpper().Equals(txtPassword.Text.ToUpper()))
            {
                this.usuarioName = txtUser.Text.Trim();
                this.usuarioId = -1;
                DialogResult = DialogResult.OK;
            }
            else
            {

                this.usuarioId = this.userConexion.login(this.txtUser.Text.Trim(), this.txtPassword.Text.Trim());
                if (this.usuarioId > 0)
                {
                    this.usuarioName = txtUser.Text;
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrectos", "Login", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.txtUser.Focus();
                }

            }

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);

        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUser.Text.Trim()))
            {
                txtUser.Text = "Usuario";
                this.txtUser.ForeColor = Color.Snow;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                txtPassword.Text = "Contraseña";
                this.txtPassword.ForeColor = Color.Snow;
                txtPassword.PasswordChar = '\0';

            }
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {

            if (txtUser.Text == "Usuario")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Black;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Contraseña")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.PasswordChar = '*';
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            txtUser.Focus();
        }

        private void lklConnection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (Properties.Settings.Default.usuario.ToUpper().Trim().Equals(txtUser.Text.ToUpper().Trim()) &&
              Properties.Settings.Default.password.ToUpper().Equals(txtPassword.Text.ToUpper()))
            {
                frmServidor frmServidor = new frmServidor();
                frmServidor.ShowDialog();
                
            }
            
        }
    }
}
