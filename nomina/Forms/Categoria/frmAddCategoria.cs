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
using nomina.Clases.Categoria;
using nomina.Clases.Utilidades;

namespace nomina.Forms.Categoria
{
    public partial class frmAddCategoria : Form
    {
        Conexion conexion;
        CategoriaData categoria;
        string codigoAntiguo;
        //nueva categoria
        public frmAddCategoria(Conexion conexion) {
            InitializeComponent();
            Utilidad.configuarForm(this, "Nueva Categoria");
            colorForm();
            this.conexion = conexion;
            this.txtCodigo.Select();
        }

//modificar categoria
        public frmAddCategoria(Conexion conexion, CategoriaData categoria)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Modificar Categoria");
            colorForm();
            this.conexion = conexion;
            this.categoria = categoria;
            this.txtCodigo.Text = categoria.CodigoCategoria;
            this.txtNombre.Text = categoria.NombreCategoria;
            this.nudSalInicial.Text = categoria.SalarioInicial.ToString();
            this.nudSalFinal.Text = categoria.SalarioFinal.ToString();
            codigoAntiguo = txtCodigo.Text;
            this.txtCodigo.Select();

        }

        #region color
        private void colorForm() {
            this.lblTitulo.BackColor = Color.SkyBlue;
            this.btnGuardar.BackColor = Color.SkyBlue;
            this.btnCancelar.BackColor = Color.Snow;
        }
        #endregion

        #region eventos
        private void FrmAddCategoria_Load(object sender, EventArgs e)
        {

            this.txtCodigo.Focus();
            if (this.Tag == "agregar")
            {
                lblTitulo.Text = "Nueva Categoria";
                this.Text = "Nueva Categoria";
            }
            else
                  if (this.Tag == "modificar")
            {
                lblTitulo.Text = "Modificar Categoria";
                this.Text = "Modificar Categoria";
            }
            else
                 if (this.Tag == "ver")
            {
                lblTitulo.Text = "Ver  Categoria";
                this.Text = "Ver Categoria";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
      
                if (this.Tag == "agregar")
                    nuevo();
                else
                    if (this.Tag == "modificar")
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro de modificar La Categoria?", "Modificar Categoria", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                        modificar();
                    }
                }
     
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }

        private void TxtSalInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            aceptaSoloNumeros(e);
        }

        private void TxtSalFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            aceptaSoloNumeros(e);
        }
        #endregion

        #region nueva categoria
        private void nuevo() { 
            CategoriaConexion bd = new CategoriaConexion(conexion);
            bool agrego = bd.accionesCategoria("N",0,txtCodigo.Text, txtNombre.Text,nudSalInicial.Value, nudSalFinal.Value);
            if (agrego)
            {
                 MessageBox.Show("Categoria agregada exitosamente", "Agregar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarFormulario();
            }
            else
                MessageBox.Show("Error ya existe una categoria con ese código o nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region limpiar formulario
        private void limpiarFormulario() {
            this.txtCodigo.Text = this.txtNombre.Text = "";
            this.nudSalInicial.Value = this.nudSalFinal.Value = (decimal)0.00;
        }

        #endregion

        #region  guardar una categoria modificada
        public void modificar() {
            CategoriaConexion bd = new CategoriaConexion(conexion);
            if (bd.accionesCategoria("M",this.categoria.Id,txtCodigo.Text, txtNombre.Text, nudSalInicial.Value, nudSalFinal.Value))
            {
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Categoria modificada exitosamente", "Modificar categoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Ya existe una categoria con ese código o nombre", "Agregar categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        private bool  esDouble(string salario) {
            try {
                Double.Parse(salario);
                return true;
            } catch (Exception e )
              {
                return false;
              }
        }

        private void aceptaSoloNumeros(KeyPressEventArgs e) {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void bloquearControles()
        {
            this.txtCodigo.Enabled = false;
            this.txtNombre.Enabled = false;
            this.nudSalInicial.Enabled = false;
            this.nudSalFinal.Enabled = false;
            this.btnGuardar.Visible = false;
            this.btnCancelar.Visible = false;
            this.AutoSize = true;
        }

        private void cambiarControlEnter(KeyPressEventArgs e)
        {
                if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            cambiarControlEnter(e);
        }

        private void TxtSalInicial_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            esDouble(e);
            cambiarControlEnter(e);
        }

        private void TxtSalFinal_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            esDouble(e);
            cambiarControlEnter(e);
        }

        public void esDouble(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void nudSalInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void nudSalFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void FrmAddCategoria_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult res = 
            //    MessageBox.Show("Si cierra el formulario sin guardar los cambios,"+
            //    "se perderan", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            //if (res == DialogResult.No)
            //    e.Cancel = true;

        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        #region validacion error provider
        private void txtCodigo_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtCodigo, "");
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            Utilidad.isEmpyErrorPro(txtCodigo, " el código", e, errorProvider1);
        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNombre, "");
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            Utilidad.isEmpyErrorPro(txtNombre, " el nombre de la categoría", e, errorProvider1);
        }

        private void nudSalInicial_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(nudSalInicial, "");
        }

        private void nudSalInicial_Validating(object sender, CancelEventArgs e)
        {
            
        }
        private void nudSalFinal_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(nudSalFinal, "");
        }

        private void nudSalFinal_Validating(object sender, CancelEventArgs e)
        {
            if (nudSalFinal.Value <= nudSalInicial.Value)
            {
                e.Cancel = true;
                nudSalFinal.Select(0, nudSalFinal.Value.ToString().Length);
                errorProvider1.SetError(nudSalFinal, "El salario final debe de ser mayor al salario inicial " );
            }
        }
        #endregion

        
    }
}
