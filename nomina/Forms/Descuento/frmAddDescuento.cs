using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Descuentos;
using nomina.Clases.ConexionManager;
using nomina.Forms.Labores;
using nomina.Clases.Utilidades;
using nomina.Forms.Empleado;
using nomina.Clases.Opciones;
using nomina.Clases.TipoJornada;
using nomina.Clases.TipoPago;

namespace nomina.Forms.Descuento
{
    public partial class frmAddDescuento : Form
    {
        Conexion conexion;
        DescuentoData descuento;
        DescuentoConexion bdDescuento;
        TipoJornadaConexion bdTiJornada;
        TipoPagoConexion bdTiPago;
        #region constructores
        /*formulario para agregar un nuevo descuento*/
        public frmAddDescuento(Conexion conexion)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Nuevo Descuento");
            bdTiJornada = new TipoJornadaConexion(conexion);
            bdTiPago = new TipoPagoConexion(conexion);
            colorForm();
            this.conexion = conexion;
            bdDescuento = new DescuentoConexion(conexion);
            configurarComboboxs();
           
            txtCodigo.Select();
            activarLaborOFactor();
        }

        /*formulario para modificar un descuento*/
        public frmAddDescuento(Conexion conexion, DescuentoData descuento)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Modificar Descuento");
            bdTiJornada = new TipoJornadaConexion(conexion);
            bdTiPago = new TipoPagoConexion(conexion);
            colorForm();
            this.conexion = conexion;
            this.descuento = descuento;
            bdDescuento = new DescuentoConexion(conexion);
            configurarComboboxs();
            this.descuento = bdDescuento.obtenerDescuento(descuento.Id);
            cargarDatosEditar();
            txtCodigo.Select();
        }

        #endregion

      
        #region color
        private void colorForm()
        {
            this.lblTitulo.BackColor = Color.SkyBlue;
            this.btnGuardar.BackColor = Color.SkyBlue;
            this.btnCancelar.BackColor = Color.Snow;
        }
        #endregion
        #region eventos
        private void frmAddDescuento_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();
            if (this.Tag == "agregar")
            {
                Text = "Nuevo Descuento";
                lblTitulo.Text = "Nuevo Descuento";
            }
            else
                  if (this.Tag == "modificar")
                 {
                    Text = "Modificar Descuento";
                    lblTitulo.Text = "Modificar Descuento";
                  }
                    else
                         if (this.Tag == "ver")
                        {
                            Text = "Ver Descuento";
                            lblTitulo.Text = "Ver Descuento";
                        }
        }

        private void cbTipoDescuento_SelectedValueChanged(object sender, EventArgs e)
        {
            activarValorOFactor();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
            //Utilidad.esLetra(e);
        }

        private void cbTipoJornada_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void cbTipoDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
            Utilidad.esDouble(e);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (this.Tag == "agregar")
                    nuevo();
                else
                    if (this.Tag == "modificar")
                    {
                        DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Descuento?", "Modificar Descuento", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            modificar();
                        }
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

        //#region validar
        //public bool validar()
        //{
        //    if (String.IsNullOrWhiteSpace(txtCodigo.Text))
        //    {
        //        MessageBox.Show("Ingrese el código del descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    if (String.IsNullOrWhiteSpace(txtNombre.Text))
        //    {
        //        MessageBox.Show("Ingrese el nombre del descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    if ((int)cbTipoJornada.SelectedIndex == 0)
        //    {
        //        MessageBox.Show("Seleccione un tipo de jornada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    if (String.IsNullOrWhiteSpace(txtCuenta.Text))
        //    {
        //        MessageBox.Show("Ingrese el número de cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    if ((int)cbTipoDescuento.SelectedIndex == 0)
        //    {
        //        MessageBox.Show("Seleccione un tipo de labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    string tipoDescuento = cbTipoDescuento.SelectedValue.ToString();
        //    if (tipoDescuento.Equals("V") || tipoDescuento.Equals("F"))
        //    {
        //        if (nudMonto.Value==(decimal)0.00)
        //        {
        //            MessageBox.Show("Escriba el monto del tipo de descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    return true;
        //}//fin validar 
        //#endregion

        private bool esDouble(string salario)
        {
            try
            {
                Double.Parse(salario);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region guardar
        private void nuevo()
        {
            int tipoJornada = Convert.ToInt32(cbTipoJornada.SelectedValue.ToString());
            int tipoDescuento = Convert.ToInt32(cbTipoDescuento.SelectedValue.ToString());
            bool agrego = false;
            
            double monto;
            //||
            if (!tipoDescuento.Equals("VALOR") && !tipoDescuento.Equals("FACTOR"))
            {
                agrego = bdDescuento.accionesDescuento("N",0, txtCodigo.Text, txtNombre.Text, 0,
                        0,tipoJornada,tipoDescuento,0);
            }
            else
                 if (tipoDescuento.Equals("VALOR"))
            {
                monto = Convert.ToDouble(nudMonto.Value);
                agrego = bdDescuento.accionesDescuento("N", 0, txtCodigo.Text, txtNombre.Text, nudMonto.Value,
                        0, tipoJornada, tipoDescuento, 0);
            }
            else
                 if (tipoDescuento.Equals("FACTOR"))
            {
                monto = Convert.ToDouble(nudMonto.Value);
                agrego = bdDescuento.accionesDescuento("N", 0, txtCodigo.Text, txtNombre.Text, 0,
                       nudMonto.Value, tipoJornada, tipoDescuento, 0);
            }

            if (agrego)
            {
                //cargar en el formulario principal los datos
                // this.DialogResult = DialogResult.OK;
                limpiarControles();
                MessageBox.Show("Descuento agregado exitosamente", "Agregar descuento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error ya existe un descuento con ese código o nombre ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
        #region limpiar controles
        private void limpiarControles()
        {
            foreach (Control c in pnDescuento.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    txt.Text = "";

                }

                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.SelectedIndex = 0;
                }

                if (c is NumericUpDown)
                {
                    NumericUpDown nud = (NumericUpDown)c;
                    nud.Value = (decimal)0.00;
                }

            }

            }
        
        #endregion

        #region modificar
        private void modificar()
        {
            int tipoJornada = Convert.ToInt32(cbTipoJornada.SelectedValue.ToString());
            int tipoDescuento = Convert.ToInt32(cbTipoDescuento.SelectedValue.ToString());
            bool agrego = false;
            double monto;
            //||
            if (!tipoDescuento.Equals("VALOR") && !tipoDescuento.Equals("FACTOR"))
            {
                agrego = bdDescuento.accionesDescuento("M",descuento.Id,txtCodigo.Text, txtNombre.Text, 0,
                        0, tipoJornada, tipoDescuento, 0);
            }
            else
                 if (tipoDescuento.Equals("VALOR"))
            {
                
                agrego = bdDescuento.accionesDescuento("M", this.descuento.Id, txtCodigo.Text, txtNombre.Text, nudMonto.Value,
                        0, tipoJornada, tipoDescuento, 0);
            }
            else
                 if (tipoDescuento.Equals("FACTOR"))
            {
               
                agrego =bdDescuento.accionesDescuento("M", this.descuento.Id,txtCodigo.Text, txtNombre.Text, 0,
                        nudMonto.Value, tipoJornada, tipoDescuento, 0);
            }

            if (agrego)
            {
                //cargar en el formulario principal los datos
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Descuento agregado exitosamente", "Agregar descuento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error ya existe un descuento con ese código o nombre ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        #endregion

        #region configurar combobox
        private void configurarComboboxs()
        {
            List<LOpciones> tipoJornada = new List<LOpciones>();
                
            tipoJornada = bdTiJornada.obtenerTipoJornada();
            tipoJornada.Insert(0, new LOpciones("0", "Seleccione una opción..."));
            
            this.cbTipoJornada.DataSource = tipoJornada;
            configurarPropiedadesCombo(cbTipoJornada);
            this.cbTipoJornada.SelectedIndex = 0;

            List<TipoPagoData> tipoPago = new List<TipoPagoData>();
            
            tipoPago = bdTiPago.obtenerTipoPagos();
            tipoPago.Insert(0, new TipoPagoData(0, "Seleccione una opción..."));
            
            this.cbTipoDescuento.DataSource = tipoPago;
            configurarPropiedadesComboTipoPago(this.cbTipoDescuento);
            this.cbTipoDescuento.SelectedIndex = 0;
        }
        #endregion

        #region configurar propiedades del combobox
        private void configurarPropiedadesCombo(ComboBox cb)
        {
            cb.DisplayMember = "descripcion";
            cb.ValueMember = "idEntero";
        }
        #endregion

        #region configurar propiedades del combobox
        private void configurarPropiedadesComboTipoPago(ComboBox cb)
        {
            cb.DisplayMember = "descripcion";
            cb.ValueMember = "id";
        }
        #endregion

        #region desactiva el monto de la labor y el facotr
        private void activarLaborOFactor()
        {
            if (this.cbTipoDescuento.SelectedValue != null)
            {
                string tipoLabor = this.cbTipoDescuento.SelectedValue.ToString();
                if (tipoLabor.Equals("V"))
                {
                    this.lblMonto.Visible = true;
                    this.lblMonto.Text = "Monto de descuento:";
                    this.nudMonto.Visible = true;
                    //this.pnLabor.AutoSize = true;
                    nudMonto.Value = (decimal)0.00;
                }
                else if (tipoLabor.Equals("F"))
                {
                    this.lblMonto.Visible = true;
                    this.lblMonto.Text = "Factor del Descuento:";
                    this.nudMonto.Visible = true;
                    //this.pnLabor.AutoSize = true;
                    nudMonto.Value = (decimal)0.00;
                }
                else
                {
                    this.lblMonto.Visible = false;
                    this.nudMonto.Visible = false;
                    //this.pnLabor.AutoSize = true;
                }
            }
        }

        #endregion

        #region cargar datos de la labor para editar
        private void cargarDatosEditar()
        {
            this.txtCodigo.Text = this.descuento.Codigo;
            this.txtNombre.Text = this.descuento.Nombre;
            this.cbTipoJornada.SelectedValue = this.descuento.TipoJornada.idEntero;
            this.txtCuenta.Text = this.descuento.CodigoCuenta.ToString();
            this.cbTipoDescuento.SelectedValue = this.descuento.TipoPago.id;

            TipoPagoData tipoDescuento = (TipoPagoData)cbTipoDescuento.SelectedItem;
            if (tipoDescuento.descripcion.Substring(0,1).Equals("V"))
            {
                nudMonto.DecimalPlaces = 7;
                nudMonto.Maximum = (decimal)9999999999.0000000;
                this.nudMonto.Value = (decimal)this.descuento.Factor;
            }
            else
                 if (tipoDescuento.descripcion.Substring(0, 1).Equals("F"))
            {
                nudMonto.DecimalPlaces = 2;
                nudMonto.Maximum = (decimal)999999999999999.00;
                this.nudMonto.Value = (decimal)this.descuento.Monto;
            }

        }
        #endregion

        #region desactivar controles cuando el usuario solo tiene permiso de ver
        public void bloquearControles()
        {
            this.pnDescuento.Enabled = false;
            this.btnGuardar.Visible = false;
            this.btnCancelar.Visible = false;
            this.AutoSize = true;
        }
        #endregion

        #region desactiva o activa el monto de la labor y el factor del descuento
        /*Si el tipo de descuento es de valor activa al igual si es de factor
         de lo contrario los desactiva*/
        private void activarValorOFactor()
        {
            if (this.cbTipoDescuento.SelectedValue != null)
            {
                TipoPagoData tipoDescuento = (TipoPagoData)cbTipoDescuento.SelectedItem;
                //3 VALOR
                if (tipoDescuento.descripcion.Substring(0, 1).Equals("V"))
                {
                    this.lblMonto.Visible = true;
                    this.lblMonto.Text = "Monto del Descuento:";
                    this.nudMonto.Visible = true;
                    //this.pnLabor.AutoSize = true;
                    nudMonto.Value = (decimal)0.00;
                    nudMonto.DecimalPlaces = 2;
                    nudMonto.Maximum = (decimal)999999999999999.00;
                }
                //FACTOR
                else if (tipoDescuento.descripcion.Substring(0, 1).Equals("F"))
                {
                    this.lblMonto.Visible = true;
                    this.lblMonto.Text = "Factor de la Labor:";
                    this.nudMonto.Visible = true;
                    //this.pnLabor.AutoSize = true;
                    nudMonto.Value= (decimal)0.00;
                    nudMonto.DecimalPlaces = 7;
                    nudMonto.Maximum = (decimal)9999999999.0000000;
                }
                else
                {
                    this.lblMonto.Visible = false;
                    this.nudMonto.Visible = false;
                    //this.pnLabor.AutoSize = true;
                }
            }
        }

        #endregion

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtCodigo_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtCodigo, "");
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            Utilidad.isEmpyErrorPro(txtCodigo, " el código del descuento", e, errorProvider1);
        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNombre, "");
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            Utilidad.isEmpyErrorPro(txtNombre, " el nombre del descuento", e, errorProvider1);
        }

        private void cbTipoJornada_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(cbTipoJornada, "");
        }

        private void cbTipoJornada_Validating(object sender, CancelEventArgs e)
        {
            if (cbTipoJornada.SelectedIndex == 0) {
                e.Cancel = true;
                cbTipoJornada.Select(0, cbTipoJornada.Text.Length);
                errorProvider1.SetError(cbTipoJornada, "Seleccione un tipo de jornada");
            }
        }

        private void cbTipoDescuento_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(cbTipoDescuento, "");
        }

        private void cbTipoDescuento_Validating(object sender, CancelEventArgs e)
        {
            if (cbTipoDescuento.SelectedIndex == 0)
            {
                e.Cancel = true;
                cbTipoDescuento.Select(0, cbTipoDescuento.Text.Length);
                errorProvider1.SetError(cbTipoDescuento, "Seleccione un tipo de descuento");
            }

           
        }

        private void nudMonto_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(nudMonto, "");
        }

        private void nudMonto_Validating(object sender, CancelEventArgs e)
        {
            TipoPagoData tipoDescuento = (TipoPagoData)cbTipoDescuento.SelectedItem;
            if (tipoDescuento.descripcion.Substring(0, 1).Equals("V") || tipoDescuento.descripcion.Substring(0, 1).Equals("F"))
            {
                if (nudMonto.Value == (decimal)0.00)
                {
                    e.Cancel = true;
                    cbTipoDescuento.Select(0, cbTipoDescuento.Text.Length);
                    errorProvider1.SetError(cbTipoDescuento, "Escriba el monto del tipo de descuento");

                }
            }
        }
    }
}
