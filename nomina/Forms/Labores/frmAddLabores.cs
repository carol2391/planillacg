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
using nomina.Clases.Labores;
using nomina.Forms.Empleado;
using nomina.Clases.Utilidades;
using nomina.Clases.Opciones;
using nomina.Clases.TipoPago;
using nomina.Clases.TipoJornada;

namespace nomina.Forms.Labores
{
    public partial class frmAddLabores : Form
    {
        Conexion conexion;
        LaboresData labor;
        TipoJornadaConexion bdTiJornada;
        TipoPagoConexion bdTiPago;
        /*formulario para agregar una nueva labor*/
        public frmAddLabores(Conexion conexion)
        {

            InitializeComponent();
            Utilidad.configuarForm(this, "Nueva Labor");
            colorForm();
            bdTiJornada = new TipoJornadaConexion(conexion);
            bdTiPago = new TipoPagoConexion(conexion);
            this.conexion = conexion;
            configurarComboboxs();
            txtCodigo.Select();
            activarValorOFactor();
        }

        /*formulario para editar una labor*/
        public frmAddLabores(Conexion conexion, LaboresData labor)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Modificar Labor");
            colorForm();
            bdTiJornada = new TipoJornadaConexion(conexion);
            bdTiPago = new TipoPagoConexion(conexion);
            this.conexion = conexion;
            this.labor = labor;
            configurarComboboxs();
            LaboresConexion bd = new LaboresConexion(conexion);
            this.labor = bd.obtenerLabor(labor.Id);
            cargarDatosEditar();
            txtCodigo.Select();
        }
        #region color
        private void colorForm()
        {
            this.lblTitulo.BackColor = Color.SkyBlue;
            this.btnGuardar.BackColor = Color.SkyBlue;
            this.btnCancelar.BackColor = Color.Snow;
        }
        #endregion
        #region eventos para validar y pasar a otro control cuando se de enter
        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Utilidad.esLetra(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void CbTipoJornada_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void TxtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
            //Utilidad.esDouble(e);
        }

        private void CbTipoLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);

        }

        private void TxtTipoLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
            Utilidad.esDouble(e);

        }
        #endregion

        private void FrmAddLabores_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();
            if (this.Tag == "agregar")
            {
                lblTitulo.Text = "Nueva Labor";
                Text = "Nueva Labor";
            }
            else
                  if (this.Tag == "modificar")
            {
                lblTitulo.Text = "Modificar Labor";
                Text = "Modificar Labor";
            }
            else
                 if (this.Tag == "ver")
            {
                lblTitulo.Text = "Ver Labor";
                Text = "Ver Labor";
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {

            if (validar())
            {
                if (this.Tag == "agregar")
                    nuevo();
                else
                    if (this.Tag == "modificar")
                    {
                            DialogResult resultado = MessageBox.Show("¿Está seguro de modificar la Labor?", "Modificar Labor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (resultado == System.Windows.Forms.DialogResult.Yes)
                            {
                                modificar();
                            }
                     }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }

        #region cuando ocurre este evento activa o desactiva el monto de la labor o el factor
        private void CbTipoLabor_SelectedValueChanged(object sender, EventArgs e)
        {
            activarValorOFactor();
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

            this.cbTipoLabor.DataSource = tipoPago;
            configurarPropiedadesComboTipoPago(this.cbTipoLabor);
            this.cbTipoLabor.SelectedIndex = 0;
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


        #region desactiva el monto de la labor y el factor de la labor
        /*Si el tipo de descuento es de valor activa al igual si es de factor
         de lo contrario los desactiva*/
        private void activarValorOFactor()
        {
            if (this.cbTipoLabor.SelectedValue != null)
            {
                TipoPagoData tipoPago = (TipoPagoData)cbTipoLabor.SelectedItem;
                if (tipoPago.descripcion.Substring(0,1).Equals("V"))
                {
                    this.lblMontoLabor.Visible = true;
                    this.lblMontoLabor.Text = "Monto de la Labor:";
                    this.nudMontoLabor.Visible = true;
                    //this.pnLabor.AutoSize = true;
                    nudMontoLabor.DecimalPlaces = 2;
                    nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    nudMontoLabor.Value = (decimal)0.00;
                }
                else if (tipoPago.descripcion.Substring(0, 1).Equals("F"))
                {
                    this.lblMontoLabor.Visible = true;
                    this.lblMontoLabor.Text = "Factor de la Labor:";
                    this.nudMontoLabor.Visible = true;
                    //this.pnLabor.AutoSize = true;
                    nudMontoLabor.DecimalPlaces = 7;
                    nudMontoLabor.Maximum = (decimal)9999999999.0000000;
                    nudMontoLabor.Value = (decimal)0.00;
                }
                else
                {
                    this.lblMontoLabor.Visible = false;
                    this.nudMontoLabor.Visible = false;
                    //this.pnLabor.AutoSize = true;
                }
            }
        }

        #endregion

        #region validar
        public bool validar() {
            if (String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Ingrese el código de la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((int)cbTipoJornada.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un tipo de jornada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtCodigoCuenta.Text))
            {
                MessageBox.Show("Ingrese el número de cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbTipoLabor.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un tipo de labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string tipoLabor = cbTipoLabor.SelectedValue.ToString();
            if (tipoLabor.Equals("V") || tipoLabor.Equals("F")) {
                if (this.nudMontoLabor.Value ==(decimal)0.00) {
                    MessageBox.Show("Escriba el monto del tipo de labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }//fin validar 
        #endregion
        #region valida si es un numero decimal
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
        #endregion
        #region nueva labor
        private void nuevo()
        {
            LaboresConexion bd = new LaboresConexion(conexion);
            LOpciones tipoJornada = (LOpciones)cbTipoJornada.SelectedItem;
            TipoPagoData tipoPago = (TipoPagoData)cbTipoLabor.SelectedItem;

            bool agrego = false;
            decimal monto;
            //||
            if (!tipoPago.descripcion.Substring(0, 1).Equals("V") && !tipoPago.descripcion.Substring(0, 1).Equals("F"))
            {
                agrego = bd.accionesLabor("N",0,txtCodigo.Text, txtNombre.Text, tipoJornada.idEntero,
                        0, 0,tipoPago.id, txtCodigoCuenta.Text);
            }
            else
                 if (tipoPago.descripcion.Substring(0, 1).Equals("V")) { 
           
                monto = nudMontoLabor.Value;
                agrego = bd.accionesLabor("N", 0, txtCodigo.Text, txtNombre.Text, tipoJornada.idEntero,
                              monto, 0, tipoPago.id, txtCodigoCuenta.Text);
            }
            else
                 if (tipoPago.descripcion.Substring(0, 1).Equals("F"))
            {
                monto = nudMontoLabor.Value;
                agrego = bd.accionesLabor("N", 0, txtCodigo.Text, txtNombre.Text, tipoJornada.idEntero,
                               0, monto, tipoPago.id, txtCodigoCuenta.Text);
            }

            if (agrego)
            {
                //cargar en el formulario principal los datos
                //this.DialogResult = DialogResult.OK;
                MessageBox.Show("Labor agregada exitosamente", "Agrega Labor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles();
            }
            else
                MessageBox.Show("Error ya existe una labor con ese código o nombre ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        #endregion

        #region modificar
        public void modificar()
        {
            LaboresConexion bd = new LaboresConexion(conexion);
            LOpciones tipoJornada = (LOpciones)cbTipoJornada.SelectedItem;
            TipoPagoData tipoPago = (TipoPagoData)cbTipoLabor.SelectedItem;
            bool agrego = false;
            decimal monto;
            //||
            if (!tipoPago.descripcion.Substring(0,1).Equals("V") && !tipoPago.descripcion.Substring(0, 1).Equals("F"))
            {
                agrego = bd.accionesLabor("M",labor.Id, txtCodigo.Text, txtNombre.Text, Convert.ToInt32(tipoJornada.idEntero),
                        0, 0, Convert.ToInt32(tipoPago.id), txtCodigoCuenta.Text);
            }
            else
                 if (tipoPago.descripcion.Substring(0, 1).Equals("V"))
                 {
                    monto = nudMontoLabor.Value;
                    agrego = bd.accionesLabor("M",labor.Id, txtCodigo.Text, txtNombre.Text, Convert.ToInt32(tipoJornada.idEntero),
                                    monto, 0, Convert.ToInt32(tipoPago.id), txtCodigoCuenta.Text);
                  }
                    else
                         if (tipoPago.descripcion.Substring(0, 1).Equals("F"))
                        {
                            monto = nudMontoLabor.Value;
                            agrego = bd.accionesLabor("M",labor.Id, txtCodigo.Text, txtNombre.Text, Convert.ToInt32(tipoJornada.idEntero),
                                            0, monto, Convert.ToInt32(tipoPago.id), txtCodigoCuenta.Text);
                        }

            if (agrego)
            {
                //cargar en el formulario principal los datos
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Labor modificada exitosamente", "Agrega Labor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error ya existe una labor con ese código o nombre ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        #endregion

        #region cargar datos de la labor para editar
        private void cargarDatosEditar() {
            this.txtCodigo.Text = labor.Codigo;
            this.txtNombre.Text = labor.Nombre;
            this.cbTipoJornada.SelectedValue = labor.TipoJornada.idEntero;
            //this.txtCodigoCuenta.Text = labor.CodigoCuenta;
            this.cbTipoLabor.SelectedValue = labor.TipoPago.id;

            TipoPagoData tipoPago = (TipoPagoData)cbTipoLabor.SelectedItem;
            if (tipoPago.descripcion.Substring(0, 1).Equals("F"))
            {
                nudMontoLabor.DecimalPlaces = 7;
                nudMontoLabor.Maximum = (decimal)9999999999.0000000;
                this.nudMontoLabor.Value = (decimal)labor.Factor;
                    
            }
            else
                 if (tipoPago.descripcion.Substring(0, 1).Equals("V")) {
                    nudMontoLabor.DecimalPlaces = 2;
                    this.nudMontoLabor.Value = (decimal)labor.Valor;
                    nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    
                  }
                 
        }
        #endregion

        #region desactivar controles cuando el usuario solo tiene permiso de ver
        public void bloquearControles() {
            this.pnLabor.Enabled = false;
            this.btnGuardar.Visible = false;
            this.btnCancelar.Visible = false;
            this.AutoSize = true;
        }
        #endregion


        #region limpiar controles
        private void limpiarControles()
        {
            foreach (Control c in pnLabor.Controls)
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

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);

        }
    }
}
