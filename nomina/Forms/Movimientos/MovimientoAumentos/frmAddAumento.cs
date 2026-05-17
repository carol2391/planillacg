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
using nomina.Clases.Movimientos.MovimientoAumento;
using nomina.Forms.Empleado;
using nomina.Clases.Empleado;
using nomina.Clases.Utilidades;
using nomina.Forms.Main;
using nomina.Clases.TipoAumento;

namespace nomina.Forms.Movimientos.MovimientoAumentos
{
    public partial class frmAddAumento : Form
    {
        EmpleadoData empleado;
        AumentoData aumentoData = new AumentoData();
        AumentoConexion bdAumento;
        DateTime fechaAntigua;
        Conexion conexion;
        decimal monto = 0;
        frmMain frmMain;
        /*formulario para editar un nuevo aumento*/
        public frmAddAumento(Conexion conexion, AumentoData aumento, frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdAumento = new AumentoConexion(conexion);
            this.frmMain = frmMain;
            this.fechaAntigua = aumentoData.Fecha;
            desactivarInfoEmpleado();
            configurarComboboxs();
            this.btnBuscarEmpleado.Visible = false;
            this.aumentoData = bdAumento.obtenerAumento(aumento.Id);
            cargarDatosEditar();
            cbTipoAumento.SelectedIndex = aumentoData.IdTipoAumento;
            cbTipoAumento.Select();
        }
        /*formulario para agregar un nuevo aumento*/
        public frmAddAumento(Conexion conexion)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdAumento = new AumentoConexion(conexion);
            desactivarInfoEmpleado();
            configurarComboboxs();
            cbTipoAumento.Select();
            btnBuscarEmpleado.Select();
            //btnBuscarEmpleado.Select();
        }

        #region eventos
        private void frmAddMovimientoAumento_Load(object sender, EventArgs e)
        {

            if (this.Tag == "agregar")
            {
                Text = "Nuevo Aumento";
                lblTitulo.Text = "Nuevo Aumento";
            }
            else
                  if (this.Tag == "modificar")
            {
                Text = "Modificar Aumento";
                lblTitulo.Text = "Modificar Aumento";
            }
            else
                    if (this.Tag == "ver")
            {
                lblTitulo.Text = "Ver Aumento";
                Text = "Ver ";
            }
        }

        private void cbTipoAumento_SelectedValueChanged(object sender, EventArgs e)
        {
            mostrarDescripcionMonto();
        }

        #endregion

        public void cambiarControlEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
                if (this.cbTipoAumento.SelectedValue != null)
                {
                    TipoAumentoData tipo = (TipoAumentoData)cbTipoAumento.SelectedItem;
                    calcularMontoPorcentaje(tipo.Descripcion[0]);
                }
            }
        }

        #region eventos tecla enter
        private void btnBuscarEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
              cbTipoAumento.Select();
            }
        }

        private void cbTipoAumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudMontoAumentoPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }
        #endregion

        #region menu
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                if (this.Tag == "agregar")
                    nuevo();
                else
                      if (this.Tag == "modificar")
                      {
                        DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Aumento?", "Modificar Aumento", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            //cargarDatosEditar();
                            //DateTime fechaActual = DateTime.Now;
                            //int mesActual = fechaActual.Month;
                            //int añoActual = fechaActual.Year;
                            //if (this.aumentoData.Fecha.Month == mesActual && this.aumentoData.Fecha.Year == añoActual)
                           modificar();
                            
                      }
                 }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                limpiarControles();
                Dispose();

            }
        }
        #endregion

        #region desactivar informacion empleado
        public void desactivarInfoEmpleado()
        {
            this.lblCodigoEmpleado.Visible = false;
            this.lblNombreEmpleado.Visible = false;
            this.lblDepartamento.Visible = false;
            this.lblSueldo.Visible = false;
            this.lblCategoria.Visible = false;
            this.lblSalarioInicial.Visible = false;
            this.lblSalarioFinal.Visible = false;

            this.lblCodigoEmpleado.Text = "";        }

        #endregion

        #region mostrar informacion del empleado
        public void mostrarInfoEmpleado()
        {
            this.lblCodigoEmpleado.Visible = true;
            this.lblNombreEmpleado.Visible = true;
            this.lblDepartamento.Visible = true;
            this.lblSueldo.Visible = true;
            this.lblCategoria.Visible = true;
            this.lblSalarioInicial.Visible = true;
            this.lblSalarioFinal.Visible = true;

        }
        #endregion

        #region configurar combobox
        private void configurarComboboxs()
        {
            List<TipoAumentoData> ltipoAumento = new List<TipoAumentoData>();
            configurarPropiedadesCombo(this.cbTipoAumento);
            TipoAumentoConexion bdTipoA = new TipoAumentoConexion(conexion);
            ltipoAumento = bdTipoA.obtenerTipoAumentos();
            ltipoAumento.Insert(0, new TipoAumentoData(0, "Seleccione una opción..."));       
            this.cbTipoAumento.DataSource = ltipoAumento;
            this.cbTipoAumento.SelectedIndex = 0;
        }

        private void configurarPropiedadesCombo(ComboBox cb)
        {
            cb.DisplayMember = "descripcion";
            cb.ValueMember = "id";
        }
        #endregion

        #region buscar empleado
        private void btnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleado frm = new frmEmpleado(conexion,frmMain);
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
                this.lblCategoria.Text = empleado.objCategoria.NombreCategoria;
                this.lblSalarioInicial.Text = empleado.objCategoria.SalarioInicial.ToString();
                this.lblSalarioFinal.Text = empleado.objCategoria.SalarioFinal.ToString();
                cbTipoAumento.Select();
            }

        }
        #endregion

        #region buscarEmpleado
        public void buscarEmpleado() {

        }
        #endregion

        #region limpiarControles
        public void limpiarControles()
        {
            desactivarInfoEmpleado();
            this.cbTipoAumento.SelectedIndex = 0;
            this.nudMontoAumentoPorcentaje.Value = (decimal)0.0;
            this.txtDescripcion.Text = "";
            this.nudMontoAumento.Value = (decimal)0.0;
            this.nudNuevoSueldo.Value = (decimal)0.0;
        }
        #endregion

        #region validar
        public bool validar()
        {

            if (String.IsNullOrWhiteSpace(lblCodigoEmpleado.Text))
            {
                MessageBox.Show("Seleccione un empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((int)cbTipoAumento.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un Tipo de Aumento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (nudMontoAumentoPorcentaje.Value==(decimal)0.0)
            {
                string tipo = cbTipoAumento.SelectedValue.ToString();
                switch (tipo[0]) {
                    case 'M':
                        MessageBox.Show("Ingrese el Monto Fijo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                        
                    case 'P':
                        MessageBox.Show("Ingrese el Porcentaje", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }
            }

            decimal salarioInicial = Convert.ToDecimal(empleado.objCategoria.SalarioInicial);
            decimal salarioFinal = Convert.ToDecimal(empleado.objCategoria.SalarioFinal);

            if ( nudMontoAumento.Value< salarioInicial || nudMontoAumento.Value > salarioFinal ) {
                MessageBox.Show("El monto debe de estar en el rango de la Categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudMontoAumentoPorcentaje.Value = (decimal)0.0;
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Escriba una descripción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region mostra en la etiqueta si es un monto o porcentaje
        public void mostrarDescripcionMonto() {
            if (this.cbTipoAumento.SelectedValue != null)
            {
                TipoAumentoData tipo = (TipoAumentoData)cbTipoAumento.SelectedItem;
                switch (tipo.Descripcion[0])
                {
                    case 'M':
                        this.lblMonto.Text = "Monto Fijo:";
                        this.nudMontoAumentoPorcentaje.Enabled = true;
                        break;

                    case 'P':
                        this.lblMonto.Text = "Porcentaje:";
                        this.nudMontoAumentoPorcentaje.Enabled = true;
                        break;

                    default:
                        this.lblMonto.Text = "Monto:";
                        this.nudMontoAumentoPorcentaje.Enabled = false;
                        break;
                }
            }
        }
        #endregion

        #region guardar
        public void nuevo() {
            bool agrego=false;
            TipoAumentoData tipoAumento = (TipoAumentoData)cbTipoAumento.SelectedItem;
            switch (tipoAumento.Descripcion[0])
            {
                /*monto fijo*/
                case 'M':
                    
                    agrego = bdAumento.accionesAumentos("N",0,empleado.Id,empleado.objCategoria.Id,
                                     dtpFechaInicial.Value.Date,
                                    empleado.Sueldo, nudNuevoSueldo.Value, 1,
                                     (decimal)0.00, nudMontoAumentoPorcentaje.Value, 
                                     monto,
                                     txtDescripcion.Text);
                    //limpiarControles();
                    //this.DialogResult = DialogResult.OK;
                    break;
              /*porcentaje*/
                case 'P':
                     agrego = bdAumento.accionesAumentos("N",0, empleado.Id, empleado.objCategoria.Id,
                                     dtpFechaInicial.Value.Date,
                                    empleado.Sueldo, nudNuevoSueldo.Value, 2,
                                     nudMontoAumentoPorcentaje.Value, (decimal)0.00,
                                     monto,
                                     txtDescripcion.Text);
                    // limpiarControles();
                    // this.DialogResult = DialogResult.OK;
                    break;
            }

            if(agrego){
                MessageBox.Show("Movimiento de Aumento agregado exitosamente", "Agregar Movimiento Aumento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles();
                //this.DialogResult = DialogResult.OK;
            }
                else
                    MessageBox.Show("Error ya existe un aumento en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region calcular monto
        public void calcularMontoPorcentaje(char tipo ) {
            
            decimal salarioInicial = Convert.ToDecimal(empleado.objCategoria.SalarioInicial);
            decimal salarioFinal = Convert.ToDecimal(empleado.objCategoria.SalarioFinal);
            switch (tipo) {

                case 'M':
                    monto = nudMontoAumentoPorcentaje.Value;
                           // + Convert.ToDecimal(empleado.Sueldo);
                    nudMontoAumento.Value = monto;
                    if (monto >= salarioInicial && monto <= salarioFinal)
                    { 
                        nudNuevoSueldo.Value = monto+Convert.ToDecimal(empleado.Sueldo);
                        //this.monto = nudMontoAumento.Value;
                    }
                    else {
                        MessageBox.Show("El Monto Aumento debe de estar en el rango de la Categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.nudMontoAumentoPorcentaje.Value = (decimal)0.0;
                        this.nudMontoAumento.Value = (decimal)0.0;
                        this.nudNuevoSueldo.Value = (decimal)0.0;
                    }
                    break;    

                case 'P':
                    monto = (Convert.ToDecimal(empleado.Sueldo) *
                              nudMontoAumentoPorcentaje.Value); 
                           // +
                           //Convert.ToDecimal(empleado.Sueldo);
                    nudMontoAumento.Value = monto;
                    if (monto >= salarioInicial && monto <= salarioFinal)
                    {
                        nudNuevoSueldo.Value = monto+ Convert.ToDecimal(empleado.Sueldo);
                        
                    }
                    else
                    {
                        MessageBox.Show("El Monto Aumento debe de estar en el rango de la Categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.nudMontoAumento.Value = (decimal)0.0;
                    }

                    break;      
            }
            
        }
        #endregion

        /*editar*/

        #region cargar datos a editar en el formulario
        public void cargarDatosEditar()
        {
            EmpleadoConexion bdEmpleado = new EmpleadoConexion(conexion);
            this.empleado = bdEmpleado.obtenerEmpleadoCodigo(aumentoData.CodigoEmpleado);
            this.lblCodigoEmpleado.Text = empleado.Codigo;
            this.lblNombreEmpleado.Text = empleado.Nombre;
            this.lblDepartamento.Text = empleado.nombreDepto;
            this.lblSueldo.Text = empleado.Sueldo.ToString();
            this.dtpFechaInicial.Value = aumentoData.Fecha;

            lblCategoria.Text = empleado.objCategoria.NombreCategoria;
            this.lblSalarioInicial.Text = empleado.objCategoria.SalarioInicial.ToString();
            this.lblSalarioFinal.Text = empleado.objCategoria.SalarioFinal.ToString();

            mostrarInfoEmpleado();

            this.txtDescripcion.Text = aumentoData.Descripcion;
            this.nudMontoAumento.Value = aumentoData.Monto; 
            this.nudNuevoSueldo.Value = aumentoData.SueldoActual;
            this.cbTipoAumento.SelectedValue = aumentoData.IdTipoAumento;

            switch (aumentoData.DescripcionTipoAumento[0]) {
                case 'M':
                    this.lblMonto.Text = "Monto Fijo";
                    nudMontoAumentoPorcentaje.Value = aumentoData.MontoAumento;
                    break;

                case 'P':
                    this.lblMonto.Text = "Permanente";
                    nudMontoAumentoPorcentaje.Value = aumentoData.Porcentaje;
                    break;
            }
        }
        #endregion

        #region modificar
        public void modificar()
        {
            bool agrego = false;
            TipoAumentoData tipo = (TipoAumentoData)cbTipoAumento.SelectedItem;
            switch (tipo.Descripcion[0])
            {
                case 'M':
                    agrego = bdAumento.accionesAumentos("M",aumentoData.Id, empleado.Id, empleado.objCategoria.Id,
                                     dtpFechaInicial.Value.Date,
                                    empleado.Sueldo, nudNuevoSueldo.Value, 1,
                                     nudMontoAumentoPorcentaje.Value, (decimal)0.00,
                                     monto,
                                     txtDescripcion.Text);
                    // limpiarControles();
                    //this.DialogResult = DialogResult.OK;
                    break;

                case 'P':
                    agrego = bdAumento.accionesAumentos("M",aumentoData.Id, empleado.Id, empleado.objCategoria.Id,
                                     dtpFechaInicial.Value.Date,
                                    empleado.Sueldo, nudNuevoSueldo.Value, 2,
                                     nudMontoAumentoPorcentaje.Value, (decimal)0.00,
                                     monto,
                                     txtDescripcion.Text);
                    //limpiarControles();
                    //this.DialogResult = DialogResult.OK;
                    break;
            }

            if (agrego)
            {
                MessageBox.Show("Movimiento de Aumento modificado exitosamente", "Agregar Movimiento Aumento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles();
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error ya existe un aumento en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
