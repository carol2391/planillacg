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
using nomina.Clases.Departamento;
using nomina.Clases.Utilidades;
using nomina.Forms.Empleado;
using nomina.Forms.Main;
using nomina.Clases.Empleado;

namespace nomina.Forms.Departamento
{
    public partial class frmAddDepartamento : Form
    {

        Conexion conexion;
        DepartamentoData depto;
        DepartamentoConexion bd;
        frmMain frmMain;
        string codAntiguo;
        EmpleadoData empleado;
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
                    if (validarVacio()) {
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

        #region valida si el formulario esta vacio
        private bool validarVacio()
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text.Trim()))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtCodigo.Text.Trim()))
            {
                return false;
            }

           

            return true;
        }
        #endregion
        public frmAddDepartamento(frmMain frm, Conexion conexion)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Nuevo Departamento");
            this.conexion = conexion;
            this.frmMain = frm;
            this.txtCodigo.Select();
            bd = new DepartamentoConexion(conexion);
            this.lblTitulo.BackColor = Color.SkyBlue;
            this.btnGuardar.BackColor = Color.SkyBlue;
            this.btCancelar.BackColor = Color.Snow;
           
        }
        //constructor para modificar
        public frmAddDepartamento(Conexion conexion, DepartamentoData depto)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Modificar Departamento");
            this.conexion = conexion;
            bd = new DepartamentoConexion(conexion);
            this.depto = depto;
            this.txtCodigo.Text = depto.CodigoDepartamento;
            this.txtNombre.Text = depto.NombreDepartamento;
            //this.txtEncargado.Text = depto.NombreEncargado;
           // this.txtCodCuenta.Text = depto.CodigoCuenta;
            codAntiguo = this.txtCodigo.Text;
            this.txtCodigo.Select();
            this.lblTitulo.BackColor = Color.SkyBlue;
            this.btnGuardar.BackColor = Color.SkyBlue;
            this.btCancelar.BackColor = Color.Snow;
           
        }

        public void bloquearControles() {
            this.txtCodigo.Enabled = false;
            this.txtNombre.Enabled = false;
           
            this.txtCodCuenta.Enabled = false;
            this.btnGuardar.Visible = false;
            this.btCancelar.Visible = false;
            this.AutoSize = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();   
        }

        private void guardar() {
           
               
                if (this.Tag == "agregar")
                {
                    nuevo();
                }
                else 
                    if (this.Tag == "modificar") {
                        modificar();  
                    
                  }
           
        }

        private void nuevo() {
            bool agrego=false;

            if (empleado==null)
            {
                agrego = bd.accionDepartamento("N", -1, txtCodigo.Text, txtNombre.Text, -1, 1);
            }
            else
                agrego = bd.accionDepartamento("N", -1, txtCodigo.Text, txtNombre.Text, empleado.Id, 1);
            if (agrego)
            {
                 limpiarFormulario();
                 MessageBox.Show("Departamento agregado exitosamente", "Agregar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
                MessageBox.Show("Error ya existe un Departamento con ese código o nombre ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void modificar() {
            DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Departamento?", "Modificar Departamento", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                bool agrego = false;

                if (empleado == null)
                {
                    agrego = bd.accionDepartamento("M", depto.Id, txtCodigo.Text, txtNombre.Text, -1, 0);
                }
                else
                    agrego = bd.accionDepartamento("M", depto.Id, txtCodigo.Text, txtNombre.Text, empleado.Id, 0);
                if (agrego)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Text = "Modificar Departamento";
                    MessageBox.Show("Departamento modificado exitosamente", "Modificar departamento", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("Ya existe un departamento con ese código o nombre", "Agregar departamento", MessageBoxButtons.OK);
            }
        }

        private void frmAddDepartamento_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();
            if (this.Tag == "agregar")
            {
                lblTitulo.Text = "Crear Departamento";
                this.Text = "Nuevo Departamento";
            }
            else
                  if (this.Tag == "modificar") {
                      lblTitulo.Text = "Modificar Departamento";
                      this.Text = "Modificar Departamento";
                    }
                    else
                        if (this.Tag == "ver")
                        {
                           lblTitulo.Text = "Ver Departamento";
                           this.Text = "Ver Departamento";
                         }
        }

        public void agregarDepartamento(){

        }

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            this.cambiarControlEnter(e);
        }

        private void cambiarControlEnter(KeyPressEventArgs e){
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.cambiarControlEnter(e);
        }

        private void TxtEncargado_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.esLetra(e);
            this.cambiarControlEnter(e);
        }

        private void TxtCodCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.cambiarControlEnter(e);
        }

        private void btnCancelar(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }

        }


        #region funcion donde solo se permiten letras
        private void esLetra(KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }
        #endregion

        private void limpiarFormulario()
        {
            this.txtCodigo.Text = this.txtNombre.Text
            = this.txtCodCuenta.Text;// = this.txtEncargado.Text = "";
            
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }


        #region valida si el campo no esta vacio y controla el error
        private void isEmpy(TextBox campo, string mensaje, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(campo.Text.Trim()))
            {
                e.Cancel = true;
                campo.Select(0, txtCodigo.Text.Length);
                errorProvider1.SetError(campo, "Debe introducir " + mensaje);
            }


        }
        #endregion

        private void txtCodigo_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtCodigo, "");
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            isEmpy(txtCodigo," El código ",e);
        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNombre, "");
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            isEmpy(txtNombre, " El nombre ", e);
        }

        private void txtEncargado_Validated(object sender, EventArgs e)
        {
            //errorProvider1.SetError(txtCodigo, "");
        }

        private void txtEncargado_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtCodCuenta_Validated(object sender, EventArgs e)
        {
            //errorProvider1.SetError(txtCodigo, "");
        }

        private void txtCodCuenta_Validating(object sender, CancelEventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmEmpleado frm = new frmEmpleado(conexion,frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();
            if (frm.DialogResult.Equals(DialogResult.OK)) {
                empleado = frm.empleado;
                this.lblEmpleadoEncargado.Text = frm.empleado.Nombre;
                txtCodCuenta.Focus();
            }
        }

        private void btnBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtCodCuenta.Focus();
            }
        }
    }
}
