using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Empresas;
using nomina.Forms.Main;
using nomina.Clases.ConexionManager;
using nomina.Clases.Utilidades;
using nomina.BarraProgreso;

namespace nomina.Forms.Empresas
{
    public partial class frmAddEmpresa : Form
    {
        #region propiedades
        private Conexion conexion;
        public frmMain frmMain { get; set; }
        public EmpresaConexion bdEmpresa { get; set; }
        public EmpresaData empData { get; set; }
        public string codigoAntiguo { get; set; }
        public string PathImagen { get; set; }
        #endregion

        #region constructores
        //para una nueva empresa
        public frmAddEmpresa(Conexion con, frmMain main)
        {
            InitializeComponent();
            this.frmMain = main;
            Utilidad.configuarForm(this, "Agregar Nueva Empresa");
            this.bdEmpresa = new EmpresaConexion();
            this.txtCodigo.Select();
            this.conexion = con;
            this.lblTitulo.BackColor = Color.SkyBlue;
            //this.btnCargar.BackColor = Color.SkyBlue;
            this.btnCancelar.BackColor = Color.Snow;
            btnGuardar.BackColor = Color.SkyBlue;

        }

        //constructor para editar una empresa
        public frmAddEmpresa(Conexion con, frmMain frmMain, EmpresaData empData)
        {
            InitializeComponent();
            this.frmMain = frmMain;
            Utilidad.configuarForm(this, "Modificar Empresa");
            this.bdEmpresa = new EmpresaConexion();
            this.txtCodigo.Select();
            this.frmMain = frmMain;
            this.conexion = con;
            this.empData = this.bdEmpresa.obtenerEmpresaId(empData.Codigo);
            this.codigoAntiguo = empData.Codigo;
            this.cargarInformacion();
            this.lblTitulo.BackColor = Color.SkyBlue;
            //this.btnCargar.BackColor = Color.SkyBlue;
            this.btnCancelar.BackColor = Color.Snow;
            btnGuardar.BackColor = Color.SkyBlue;
        }
        #endregion

        #region evento cuando se le da enter a un control
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

            Utilidad.cambiarControlEnter(e);
        }

        private void dptFecha_KeyPress(object sender, KeyPressEventArgs e)
        {

            Utilidad.cambiarControlEnter(e);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {

            Utilidad.cambiarControlEnter(e);
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.esEntero(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {

            Utilidad.cambiarControlEnter(e);
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {

            Utilidad.cambiarControlEnter(e);
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
        #endregion

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
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                        guardar();

                }//fin scape
                result = true;
            }
            return result;
        }
        #endregion
        #region menu
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardar()
        {
            if (validar())
            {
                if (this.Tag == "agregar")
                    nuevo();
                else
                    modificar();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }
        #endregion

        #region evento load
        private void frmAddEmpresas_Load(object sender, EventArgs e)
        {
            if (Tag.Equals("agregar"))
            {
                this.lblTitulo.Text = "Nueva Empresa";
                this.Text = "Nueva Empresa";
            }
            else
            {
                if (Tag.Equals("modificar"))
                {
                    this.lblTitulo.Text = "Modificar Empresa";
                    this.Text = "Modificar Empresa";
                }
            }
        }
        #endregion

        #region validar
        public bool validar()
        {

            if (string.IsNullOrEmpty(this.txtCodigo.Text))
            {
                MessageBox.Show("Ingrese el código de la empresa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(this.txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la empresa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (string.IsNullOrEmpty(this.txtDireccion.Text))
            {
                MessageBox.Show("Ingrese la dirección de la empresa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (string.IsNullOrEmpty(this.txtRTN.Text))
            {
                MessageBox.Show("Ingrese el RTN de la empresa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            else
                if (txtRTN.Text.Length != 14)
            {
                MessageBox.Show("El RTN tiene que tener 14 digitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (string.IsNullOrEmpty(this.txtTelefono.Text))
            {
                MessageBox.Show("Ingrese el Telefono de la empresa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(this.txtCorreo.Text))
            {
                MessageBox.Show("Ingrese el Correo de la empresa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }


            return true;
        }

        #endregion

        #region metodo donde se crea una nueva empresa
        public void nuevo()
        {
            //bool creo = bdEmpresa.agregarEmpresa();
            frmBarraProgreso frmBarrraProgreso = new frmBarraProgreso(this, Tipo.NuevaEmpresa);
            frmBarrraProgreso.ShowDialog();
            if (frmBarrraProgreso.resultado.Equals(DialogoResultado.Si))
            {
                this.limpiarControles();
                MessageBox.Show("Empresa creada exitosamente", "Nueva empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Ya existe una empresa con ese nombre o código", "Nueva empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        #endregion

        #region metodo donde se modifica una empresa
        public void modificar()
        {
            DialogResult resultado = MessageBox.Show("¿Desea activar Nomina?", "Activar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                frmBarraProgreso frmBarrraProgreso = new frmBarraProgreso(this, Tipo.ModificarEmpresa);
                frmBarrraProgreso.ShowDialog();
                if (frmBarrraProgreso.resultado.Equals(DialogoResultado.Si))
                {
                    MessageBox.Show("Empresa modificada exitosamente", "Nueva empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Ya existe una empresa con ese nombre o código", "Nueva empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                if (this.bdEmpresa.modificarEmpresa(this.empData.Id, this.txtCodigo.Text, this.txtNombre.Text, this.dptFecha.Value.Date, this.txtDireccion.Text, this.txtRTN.Text, this.txtTelefono.Text, this.txtCorreo.Text, this.codigoAntiguo,
                frmMain.usuarioName))
                {
                    this.limpiarControles();
                    MessageBox.Show("Empresa modificada exitosamente", "Nueva empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Ya existe una empresa con ese nombre o código", "Nueva empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }
        #endregion

        #region limpiar controles
        public void limpiarControles()
        {
            foreach (Control control in this.gbEmpresa.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    control.Text = "";
                }
            }

            //pbImagen.Image = null;
            PathImagen = null;
        }
        #endregion

        #region cargar informacion cuando se va editar una empresa
        public void cargarInformacion()
        {
            this.txtNombre.Text = this.empData.Nombre;
            this.txtCodigo.Text = this.empData.Codigo;
            this.txtDireccion.Text = this.empData.Direccion;
            this.txtRTN.Text = this.empData.RTN;
            this.txtTelefono.Text = this.empData.Telefono;
            this.txtCorreo.Text = this.empData.Correo;
           // this.pbImagen.Image = empData.Image;
            if (this.empData.Fecha.Date.ToString().Substring(0, 10) != "01/01/0001")
            {
                this.dptFecha.Value = this.empData.Fecha;
            }

        }
        #endregion

        #region valida si el formulario esta vacio
        private bool validarVacio()
        {
            foreach (Control control in this.gbEmpresa.Controls)
            {
                if (control is TextBox)
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

        private void btnCargar_Click(object sender, EventArgs e)
        {

        }

        private void btnCargar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

    }
}
