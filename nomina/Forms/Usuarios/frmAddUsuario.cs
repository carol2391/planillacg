using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using nomina.Clases.ConexionManager;
using nomina.Clases.Usuarios;
using nomina.Clases.Utilidades;
using nomina.Forms.Main;

namespace nomina.Forms.Usuarios
{
    public partial class frmAddUsuario : Form
    {

        #region propiedades
        Conexion conexion;
        UsuarioData usuarioData;
        UsuarioConexion bd;
        frmMain frmMain;
        #endregion
        // constructor par nuevo usuario
        public frmAddUsuario(Conexion con, frmMain frmMain)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Agregar Usuario");
            this.frmMain = frmMain;
            this.conexion = con;
            this.bd = new UsuarioConexion();
            this.cbActivo.Visible = false;
            this.lblTitulo.BackColor = btnGuardar.BackColor = Color.SkyBlue;
            this.btnCancelar.BackColor = Color.Snow;

        }

        //constructor par modificar usuario
        public frmAddUsuario(Conexion con, UsuarioData usuario, frmMain frmMain)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Modificar Usuario");
            this.frmMain = frmMain;
            this.conexion = con;
            this.bd = new UsuarioConexion();
            this.usuarioData = usuario;
            this.usuarioData = this.bd.obtenerUsuario(this.usuarioData.UsuarioId);
            this.cargarInformacion();
            this.lblTitulo.BackColor = btnGuardar.BackColor = Color.SkyBlue;
            this.btnCancelar.BackColor = Color.Snow;
        }

        #region eventos scape, f2,f4
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool result;
            if (keyData != Keys.Escape)
            {
                result = base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {

                if (keyData == Keys.Escape)
                {
                    if (validarVacio())
                        DialogResult = DialogResult.OK;
                    else
                        guardar();

                }//fin scape
                result = true;
            }
            return result;
        }
        #endregion
        #region evento para cambiar de control con la tecla enter
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtContrasenia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtRepetirContrasenia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void cbActivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);

        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
        #endregion

        #region menu
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardar()
        {
            if (this.validar())
            {
                if (Tag == "nuevo")
                {
                    this.nuevo();
                }
                else
                {
                    if (Tag == "modificar")
                    {
                        this.modificar();
                    }
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                base.Dispose();
            }
        }
        #endregion

        #region evento load
        private void frmAddUsuarios_Load(object sender, EventArgs e)
        {
            this.txtNombre.Focus();
            if (Tag == "nuevo")
            {
                this.lblTitulo.Text = "Nuevo Usuario";
                this.Text = "Nuevo Usuario";
            }
            else
            {
                if (Tag == "modificar")
                {
                    this.lblTitulo.Text = "Modificar Usuario";
                    this.Text = "Modificar Usuario";
                }
                else
                {
                    if (Tag == "ver")
                    {
                        this.lblTitulo.Text = "Ver  Usuario";
                        this.Text = "Ver Usuario";
                    }
                }
            }
        }
        #endregion

        #region mostrar contraseña
        private void cbMostrarContrasenia_CheckedChanged(object sender, EventArgs e)
        {
            this.txtContrasenia.PasswordChar = (this.cbMostrarContrasenia.Checked ? '\0' : '*');
        }
        #endregion

        #region validar
        public bool validar()
        {
            bool result;
            if (string.IsNullOrEmpty(this.txtUsuario.Text))
            {
                MessageBox.Show("Ingrese el nombre de usuario", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                result = false;
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtNombre.Text))
                {
                    MessageBox.Show("Ingrese su nombre", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    result = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtCorreo.Text))
                    {
                        MessageBox.Show("Ingrese su correo", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        result = false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.txtContrasenia.Text))
                        {
                            MessageBox.Show("Ingrese la contraseña ", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            result = false;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(this.txtRepetirContrasenia.Text))
                            {
                                MessageBox.Show("Vuelva a ingresar la contraseña", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                result = false;
                            }
                            else
                            {
                                if (!txtContrasenia.Text.Trim().Equals(txtRepetirContrasenia.Text.Trim()))
                                {
                                    MessageBox.Show("Contraseñas diferentes", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    result = false;
                                }
                                else
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region nuevo usuario
        public void nuevo()
        {
            if (this.bd.agregarUsuario(this.txtNombre.Text, this.txtUsuario.Text, this.txtCorreo.Text, this.txtContrasenia.Text,
                this.dtpFecha.Value.Date, frmMain.usuarioName))
            {
                MessageBox.Show("Usuario agregado exitosamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.limpiarControles();
            }
            else
            {
                MessageBox.Show("Ya existe un usuario con ese nombre de usuario o contraseña", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        #endregion

        #region modificar usuario
        public void modificar()
        {
            int activo = 0;
            if (this.cbActivo.Checked)
            {
                activo = 1;
            }
            if (this.bd.modificarUsuario(this.usuarioData.UsuarioId, this.txtNombre.Text,
                this.txtUsuario.Text, this.txtCorreo.Text, this.txtContrasenia.Text,
                this.dtpFecha.Value.Date, activo, frmMain.usuarioName))
            {
                MessageBox.Show("Usuario modificado exitosamente", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.limpiarControles();
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Ya existe un usuario con ese nombre de usuario o contraseña", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        #endregion

        #region cargar datos en los controles
        public void cargarInformacion()
        {
            this.txtNombre.Text = this.usuarioData.Nombre;
            this.txtUsuario.Text = this.usuarioData.Usuario;
            this.txtContrasenia.Text = this.usuarioData.Contrasenia;
            this.txtRepetirContrasenia.Text = this.usuarioData.Contrasenia;
            this.txtCorreo.Text = this.usuarioData.Correo;
            this.dtpFecha.Value = this.usuarioData.FechaIngreso;
            if (this.usuarioData.Activo == 1)
            {
                this.cbActivo.Checked = true;
            }
            else
            {
                this.cbActivo.Checked = false;
            }
        }
        #endregion

        #region limpiar controles
        private void limpiarControles()
        {
            foreach (Control control in this.gbDatos.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)control;
                    textBox.Text = "";
                }
            }
        }
        #endregion

        #region valida si el formulario esta vacio
        private bool validarVacio()
        {
            foreach (Control control in this.gbDatos.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    if (!String.IsNullOrEmpty(textBox.Text.Trim()))
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        #endregion
    }
}
