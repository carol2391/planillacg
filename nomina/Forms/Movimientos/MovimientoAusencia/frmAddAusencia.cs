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
using nomina.Clases.Ausencia;
using nomina.Forms.Empleado;
using nomina.Clases.Empleado;
using nomina.Clases.Utilidades;
using nomina.Forms.Main;
using nomina.Clases.TipoAusencia;


namespace nomina.Forms.Movimientos.Ausencia
{
    public partial class frmAddAusencia : Form
    {
        Conexion conexion;
        EmpleadoData empleado;
        AusenciaData ausenciaData = new AusenciaData();
        AusenciaConexion bdAusencia;
        DateTime fechaAntigua;
        string septimo;
        frmMain frmMain;
        /*FORMULARIO PARA EDITAR*/
        public frmAddAusencia(Conexion conexion,AusenciaData ausenciaData,frmMain frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
            this.conexion = conexion;
            bdAusencia = new AusenciaConexion(conexion);
           
            this.ausenciaData = bdAusencia.obtenerAusencia(ausenciaData.CodigoEmpleado,
                                ausenciaData.fechaInicio,ausenciaData.codigoNomina);
            configurarComboboxs();
            this.cbTipoAusencia.SelectedIndex = this.ausenciaData.IdTipoAusencia;
           // cbTipoAusencia.Select();
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicial.Visible = false;
            this.fechaAntigua = ausenciaData.fechaInicio;
            cargarDatosEditar();
            cbTipoAusencia.Select();
        }
        /*formulario para nuevo*/
        public frmAddAusencia(Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.frmMain = frmMain;
            bdAusencia = new AusenciaConexion(conexion);
            desactivarInfoEmpleado();
            configurarComboboxs();
            cbTipoAusencia.Select();
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicial.Visible = false;
            btnBuscarEmpleado.Select();

        }
        public void cambiarControlEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
                calcularMonto();
            }
        }
        #region eventos tecla enter
        private void DtpFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
           cambiarControlEnter(e);
           calcularMonto();
        }

        private void DtpFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
           calcularMonto();
        }

        private void CbFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }


        private void NudMontoDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudMontoAusencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void CbTipoAusencia_KeyPress(object sender, KeyPressEventArgs e)
        {
             Utilidad.cambiarControlEnter(e);
        }

        private void CbFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void BtnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void BtnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        #endregion

        #region evento calue changed fechas 
        private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
        {
           // calcularMonto();
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {

           // calcularMonto();
        }
        #endregion

        #region configurar combobox
        private void configurarComboboxs()
        {
            List<Opciones> opciones = new List<Opciones>();
            configurarPropiedadesCombo(cbTipoAusencia);
            TipoAusenciaConexion bdTipoAusencia = new TipoAusenciaConexion(conexion);
            List<TipoAusenciaData> lTipoAusencia = bdTipoAusencia.obtenerTipoAusencia();
            lTipoAusencia.Insert(0, new TipoAusenciaData(0, "Seleccione una opción..."));
            //opciones.Add(new Opciones("I", "Incapacidad"));
            //opciones.Add(new Opciones("V", "Vacaciones"));
            //opciones.Add(new Opciones("PG", "Permisos sin goce de sueldo"));
            //opciones.Add(new Opciones("N", "No se presento"));
            //opciones.Add(new Opciones("P", "Permisos"));
            this.cbTipoAusencia.DataSource = lTipoAusencia;
            this.cbTipoAusencia.SelectedIndex = 0;
        }

        private void configurarPropiedadesCombo(ComboBox cb)
        {
            cb.DisplayMember = "descripcion";
            cb.ValueMember = "id";
        }
        #endregion

        #region buscar empleado
        private void BtnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleado frm = new frmEmpleado(conexion, frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.empleado = frm.empleado;
                mostrarInfoEmpleado();
                this.lblCodigoEmpleado.Text = empleado.Codigo;
                this.lblNombreEmpleado.Text = empleado.Nombre;
                this.lblSueldo.Text = empleado.Sueldo.ToString();
                this.lblDepartamento.Text = empleado.objDepto.NombreDepartamento;
                cbTipoAusencia.Select();
            }

        }
        #endregion

        #region evento checked 
        private void CbFechaInicial_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFechaInicial.Checked)
            {
                this.dtpFechaInicial.Visible = false;
                // this.dtpFechaInicial.Enabled = false;

            }
            else
            {
                this.dtpFechaInicial.Visible = true;
                // this.dtpFechaInicial.Enabled = true;
            }
        }

        private void CbFechaFinal_CheckedChanged(object sender, EventArgs e)
        {

            if (!cbFechaFinal.Checked)
            {
                // this.dtpFechaFinal.Enabled = false;
                this.dtpFechaFinal.Visible = false;
            }

            else
            {
                // this.dtpFechaFinal.Enabled = true;
                this.dtpFechaFinal.Visible = true;
            }
        }
        #endregion

        #region menu
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }

        }


        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (validar()) {

                DialogResult resul= MessageBox.Show("¿Quiere deducir el septimo?", "Deducir septimo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resul == System.Windows.Forms.DialogResult.Yes)
                {
                    this.septimo = "S";
                }
                else
                    this.septimo = "N";

                if (this.Tag == "agregar")
                    nuevo();
                else
                      if (this.Tag == "modificar")
                      {
                         DialogResult resultado = MessageBox.Show("¿Está seguro de modificar la Ausencia?", "Modificar Ausencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                         if (resultado == System.Windows.Forms.DialogResult.Yes)
                       {
                          modificar();
                       }
                }
            }
        }
            #endregion

            #region validar
            public bool validar() {

            if (String.IsNullOrWhiteSpace(lblCodigoEmpleado.Text))
            {
                MessageBox.Show("Seleccione un empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((int)cbTipoAusencia.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un Tipo de Ausencia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!this.cbFechaInicial.Checked)
            {
                MessageBox.Show("Seleccione la fecha de Inicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                if (this.dtpFechaInicial.Value.Date > this.dtpFechaFinal.Value.Date)
                {
                    MessageBox.Show("La fecha Inicial no puede ser mayor que la final", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            
            }
            if (String.IsNullOrWhiteSpace(txtCodigoNomina.Text)) {
                MessageBox.Show("Ingrese el código de nomina", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                return true;
        }
        #endregion

        #region guardar
        public void nuevo() {
            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                TipoAusenciaData tipoAusencia = (TipoAusenciaData)cbTipoAusencia.SelectedItem;
                DateTime fInicial = new DateTime();
                if (bdAusencia.accionesMAusencia("N",empleado.Id,0,
                    tipoAusencia.id, dtpFechaInicial.Value.Date,
                    dtpFechaFinal.Value.Date, 0,septimo,nudMontoAusencia.Value)) {
                    MessageBox.Show("Movimiento de Ausencia agregada exitosamente", "Agregar Movimiento Ausencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarControles();
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Error ya existe una ausencia en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
                 if (cbFechaInicial.Checked)
            {
                   DateTime fFinal = new DateTime();
                  TipoAusenciaData tipoAusencia = (TipoAusenciaData)cbTipoAusencia.SelectedItem;

                if (bdAusencia.accionesMAusencia("N",empleado.Id,0,
                    tipoAusencia.id, dtpFechaInicial.Value.Date,
                    dtpFechaFinal.Value.Date, 0, septimo, nudMontoAusencia.Value))
                 {
                    MessageBox.Show("Movimiento de Ausencia agregada exitosamente", "Agregar Movimiento Ausencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarControles();
                    this.DialogResult = DialogResult.OK;
                }
                 else
                    MessageBox.Show("Error ya existe una ausencia en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        #endregion

        #region calcular monto
        public void calcularMonto() {
            if (!cbFechaFinal.Checked)
            {
                this.nudMontoDias.Value = (decimal)1;
                decimal monto = Convert.ToDecimal(empleado.Sueldo / 30);
                this.nudMontoAusencia.Value = Decimal.Round(monto, 2);
            }
            else
                 if (cbFechaInicial.Checked && cbFechaFinal.Checked)
                {
                    TimeSpan tSpan = dtpFechaFinal.Value.Date - this.dtpFechaInicial.Value.Date;
                    int dias = tSpan.Days + 1;
                    try
                    {

                        this.nudMontoDias.Value = (decimal)dias;
                        decimal monto = Convert.ToDecimal(empleado.Sueldo / 30);
                        this.nudMontoAusencia.Value = Decimal.Round(monto, 2) * this.nudMontoDias.Value;
                    }
                    catch (Exception e) {
                        this.nudMontoAusencia.Value = (decimal)0;
                        this.nudMontoDias.Value = (decimal)0;
                        MessageBox.Show("La fecha inicial tiene que ser menor que la final", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

            }
        }
        #endregion

        #region mostrar informacion del empleado
        public void mostrarInfoEmpleado() {
            this.lblCodigoEmpleado.Visible = true;
            this.lblNombreEmpleado.Visible = true;
            this.lblDepartamento.Visible = true;
            this.lblSueldo.Visible = true;

        }
        #endregion

        #region desactivar informacion empleado
        public void desactivarInfoEmpleado() {
            this.lblCodigoEmpleado.Visible = false;
            this.lblNombreEmpleado.Visible = false;
            this.lblDepartamento.Visible = false;
            this.lblSueldo.Visible = false;
        }

        #endregion

        #region limpiarControles
        public void limpiarControles() {
            desactivarInfoEmpleado();
            this.cbTipoAusencia.SelectedIndex = 0;
            this.cbFechaInicial.Checked = false;
            this.cbFechaFinal.Checked = false;
            nudMontoDias.Value = (decimal)0;
            nudMontoAusencia.Value = (decimal)0;
            this.txtCodigoNomina.Text = "";
        }
        #endregion

        /*editar*/

        #region cargar datos a editar en el formulario
        public void cargarDatosEditar() {

            EmpleadoConexion bdEmpleado = new EmpleadoConexion(conexion);
            this.empleado = bdEmpleado.obtenerEmpleadoCodigo(ausenciaData.CodigoEmpleado);
            this.lblCodigoEmpleado.Text = empleado.Codigo;
            this.lblNombreEmpleado.Text = empleado.Nombre;
            this.lblDepartamento.Text = empleado.nombreDepto;
            this.lblSueldo.Text = empleado.Sueldo.ToString() ;
            mostrarInfoEmpleado();
            DateTime fecha = new DateTime();
            if (!this.ausenciaData.fechaFinal.Equals(fecha))
            {
                this.cbFechaInicial.Checked = true;
                this.cbFechaFinal.Checked = true;
                this.dtpFechaInicial.Value = this.ausenciaData.fechaInicio;
                this.dtpFechaFinal.Value = this.ausenciaData.fechaFinal;
            }
            else
            {
                this.cbFechaFinal.Checked = false;
                this.cbFechaInicial.Checked = true;
                this.dtpFechaInicial.Value = this.ausenciaData.fechaInicio;
               
            }

            nudMontoDias.Value = ausenciaData.diasDeAusencia;
            nudMontoAusencia.Value = ausenciaData.monto;
            this.txtCodigoNomina.Text = ausenciaData.codigoNomina;
        }
        #endregion

        #region guardar
        public void modificar()
        {
            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                DateTime fInicial = new DateTime();
                TipoAusenciaData tipoAusencia = (TipoAusenciaData)cbTipoAusencia.SelectedItem;
                if (bdAusencia.accionesMAusencia("M",empleado.Id,this.ausenciaData.Id, tipoAusencia.id,
                    dtpFechaInicial.Value.Date,
                    dtpFechaFinal.Value.Date, 0,septimo, nudMontoAusencia.Value))
                {
                    MessageBox.Show("Movimiento de Ausencia modificado exitosamente", "Agregar Movimiento Ausencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarControles();
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Error ya existe una ausencia en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
                 if (cbFechaInicial.Checked)
            {
                DateTime fFinal = new DateTime();
                TipoAusenciaData tipoAusencia = (TipoAusenciaData)cbTipoAusencia.SelectedItem;
                if (bdAusencia.accionesMAusencia("M",empleado.Id, this.ausenciaData.Id, tipoAusencia.id,
                    dtpFechaInicial.Value.Date,
                    fFinal, 0, septimo, nudMontoAusencia.Value))
                {
                    MessageBox.Show("Movimiento de Ausencia agregada exitosamente", "Agregar Movimiento Ausencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarControles();
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Error ya existe una ausencia en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        #endregion

        private void FrmAddAusencia_Load(object sender, EventArgs e)
        {
            if (this.Tag == "agregar")
            {
                lblTitulo.Text = "Nueva Ausencia";
                Text = "Nueva Ausencia";
            }
            else
                if (this.Tag == "modificar")
            {
                lblTitulo.Text = "Modificar Ausencia";
                Text = "Modificar Ausencia";
            }
        }

        private void LblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
