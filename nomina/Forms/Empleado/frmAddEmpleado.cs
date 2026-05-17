using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Empleado;
using nomina.Clases.ConexionManager;
using nomina.Forms.Main;
using nomina.Clases.Categoria;
using nomina.Clases.Departamento;
using nomina.Clases.EstadoCivil;
using nomina.Clases.Utilidades;
using nomina.Clases.Opciones;
using nomina.Clases.TipoPago;

namespace nomina.Forms.Empleado
{
    enum TipoEmpleado {
        Local,Extranjero
    }
    public partial class frmAddEmpleado : Form
    {
        Conexion conexion;
        EmpleadoData empleado;
        frmMain frmMain;
        List<string> listaSexo = new List<string>();
        EmpleadoConexion bdEmpleado;
        TipoEmpleado tipoEmpleado { set; get; }
        /*formulario para agregar un empleado*/
        public frmAddEmpleado(Conexion conexion)
        {
            InitializeComponent();
            colorForm();
            this.conexion = conexion;
            bdEmpleado = new EmpleadoConexion(conexion);
            configurarComboboxs();
            cargarCategorias();
            cargarDepartamentos();
            txtCodigo.Select();
            desactivarBancoCuenta();
            this.pnEmpleado2.AutoSize = true;
            pnEmpleado1.Enabled = false;
            pnEmpleado2.Enabled = false;
            pnInformacionContable.Enabled = false;
        }
        /*formulario para modificar*/
        public frmAddEmpleado(Conexion conexion, frmMain frmMain, EmpleadoData empleado)
        {
            InitializeComponent();
            colorForm();
            this.conexion = conexion;
            this.empleado = empleado;
            this.frmMain = frmMain;
            bdEmpleado = new EmpleadoConexion(conexion);
            this.empleado = bdEmpleado.obtenerEmpleado(empleado.Id);
            if (this.empleado.TipoEmpleadoNacionalidad.Equals("L"))
            {
                this.rbLocal.Checked = true;
            }
            else
                 if (this.empleado.TipoEmpleadoNacionalidad.Equals("E"))
            {
                this.rbExtranjero.Checked = true;
            }
            configurarComboboxs();
            cargarCategorias();
            cargarDepartamentos();
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
        #region eventos
        private void frmAddEmpleado_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();
            if (this.Tag == "agregar")
            {
                Text = "Nuevo Empleado";
                lblTitulo.Text = "Nuevo Empleado";
            }
            else
                  if (this.Tag == "modificar")
                {
                     Text = "Modificar Empleado";
                    lblTitulo.Text = "Modificar Empleado";
                }
                else
                    if (this.Tag == "ver")
                    {
                      lblTitulo.Text = "Ver Empleado";
                        Text = "Ver Empleado";
                    }

            // cargarCategorias();
            //cargarDepartamentos();
            //configurarComboboxs();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                if (this.Tag == "agregar")
                    nuevo();
                else
                   if (this.Tag == "modificar")
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Empleado?", "Modificar Empleado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                        modificar();
                    }
                }
                // 
            }
        }

        #region limpiar controles
        private void limpiarControles()
        {
            foreach (Control c in pnEmpleado1.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    txt.Text = "";

                }

                if (c is ComboBox) {
                    ComboBox cb = (ComboBox)c;
                    cb.SelectedIndex = 0;
                }

                if (c is MaskedTextBox) {
                    MaskedTextBox mt = (MaskedTextBox)c;
                    mt.Text = "";
                }
            }

            foreach (Control c in pnEmpleado2.Controls)
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
                    nud.Value =(decimal)0.00;
                }

                if (c is MaskedTextBox)
                {
                    MaskedTextBox mt = (MaskedTextBox)c;
                    mt.Text = "";
                }
            }
        }
        #endregion
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.esLetra(e);
            cambiarControlEnter(e);
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.esDouble(e);
            cambiarControlEnter(e);
        }

        public void esDouble(KeyPressEventArgs e) {
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
        private void txtPuestoTrb_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.esLetra(e);
            cambiarControlEnter(e);
        }

        private void txtBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.esLetra(e);
            cambiarControlEnter(e);
        }

        private void cbDepartamento_MouseClick(object sender, MouseEventArgs e)
        {
            cargarDepartamentos();
        }

        private void cbCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            cargarCategorias();
        }
        #endregion

        #region funcion donde solo se permiten letras
        private void esLetra(KeyPressEventArgs e) {
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
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }
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

        #region cargar combobox
        private void cargarCategorias() {
            //if (this.bd != null) {
                this.cbCategoria.DisplayMember = "NombreCategoria";
                this.cbCategoria.ValueMember = "Id";
                //this.frmMain.categorias.Insert(0, new CategoriaData(0, "", "Seleccione una opción...", 0, 0));
                List<CategoriaData> categorias = new List<CategoriaData>();
                CategoriaConexion bd = new CategoriaConexion(conexion);
                categorias = bd.obtenerCategorias();
                categorias.Insert(0, new CategoriaData(0, "", "Seleccione una opción...", 0, 0));
                this.cbCategoria.DataSource = categorias;


                //this.cbCategoria.DataSource = frmMain.categorias;
                //this.cbCategoria.SelectedValue = 0;
            //}

        }

        private void cargarDepartamentos() {
            //if (this.frmMain.departamentos != null)
            //{
                this.cbDepartamento.DisplayMember = "NombreDepartamento";
                this.cbDepartamento.ValueMember = "Id";
                //this.frmMain.departamentos.Insert(0, new DepartamentoData(0, "", "Seleccione una opción...", "", ""));
                //this.cbDepartamento.DataSource = this.frmMain.departamentos;
                DepartamentoConexion bd = new DepartamentoConexion(conexion);
                List<DepartamentoData> deptos = new List<DepartamentoData>();
                deptos = bd.obtenerDepartamentos();
                deptos.Insert(0, new DepartamentoData(0, "", "Seleccione una opción...","0", 0));
                this.cbDepartamento.DataSource = deptos;

                // this.cbDepartamento.SelectedValue = 0;
            //}
        }

        private void configurarComboboxs() {
            List<CEstadoCivil> estadoCiviles = new List<CEstadoCivil>();
            //this.cbEstadoCivil.DisplayMember = "nombre";
            //this.cbEstadoCivil.ValueMember = "id";
            configurarPropiedadesCombo(cbEstadoCivil);
            estadoCiviles.Insert(0, new CEstadoCivil("0", "Seleccione una opción..."));
            estadoCiviles.Add(new CEstadoCivil("S", "Soltero"));
            estadoCiviles.Add(new CEstadoCivil("C", "Casado"));
            estadoCiviles.Add(new CEstadoCivil("U", "Unión Libre"));
            this.cbEstadoCivil.DataSource = estadoCiviles;
            this.cbEstadoCivil.SelectedIndex = 0;


            List<Sexo> sexos = new List<Sexo>();
            //this.cbSexo.DisplayMember = "nombre";
            //this.cbSexo.ValueMember = "id";
           
            sexos.Insert(0, new Sexo("0", "Seleccione una opción..."));
            sexos.Add(new Sexo("F", "Femenino"));
            sexos.Add(new Sexo("M", "Masculino"));
           
            this.cbSexo.DataSource = sexos;
            configurarPropiedadesCombo(cbSexo);
            this.cbSexo.SelectedIndex = 0;

            List<LOpciones> tiposEmpleados = new List<LOpciones>();
            //this.cbTipoEmpleado.DisplayMember = "nombre";
            //this.cbTipoEmpleado.ValueMember = "id";
             tiposEmpleados = bdEmpleado.obtenerTipoEmpleado();
            tiposEmpleados.Insert(0, new LOpciones(0, "Seleccione una opción..."));
            this.cbTipoEmpleado.DataSource = tiposEmpleados;
            cbTipoEmpleado.DisplayMember = "descripcion";
            cbTipoEmpleado.ValueMember = "idEntero";
            this.cbTipoEmpleado.SelectedIndex = 0;

            List<Opciones> opciones = new List<Opciones>();
            configurarPropiedadesCombo(cbA_IHS);
            opciones.Insert(0, new Opciones("0", "Seleccione una opción..."));
            opciones.Add(new Opciones("S", "Si"));
            opciones.Add(new Opciones("N", "No"));
            this.cbA_IHS.DataSource = opciones;
            this.cbA_IHS.SelectedIndex = 0;

            List<Opciones> opciones1 = new List<Opciones>();
            configurarPropiedadesCombo(cb_A_RAP);
            opciones1.Insert(0, new Opciones("0", "Seleccione una opción..."));
            opciones1.Add(new Opciones("S", "Si"));
            opciones1.Add(new Opciones("N", "No"));
            this.cb_A_RAP.DataSource = opciones1;
            this.cb_A_RAP.SelectedIndex = 0;

            List<Opciones> opciones2 = new List<Opciones>();
            opciones2.Insert(0, new Opciones("0", "Seleccione una opción..."));
            opciones2.Add(new Opciones("S", "Si"));
            opciones2.Add(new Opciones("N", "No"));
            configurarPropiedadesCombo(cb_A_SIN);
            this.cb_A_SIN.DataSource = opciones2;
            this.cb_A_SIN.SelectedIndex = 0;

            List<Opciones> opciones3 = new List<Opciones>();
            opciones3.Insert(0, new Opciones("0", "Seleccione una opción..."));
            opciones3.Add(new Opciones("S", "Si"));
            opciones3.Add(new Opciones("N", "No"));
            configurarPropiedadesCombo(this.cb_A_ISR);
            cb_A_ISR.DataSource = opciones3;
            this.cb_A_ISR.SelectedIndex = 0;

            List<LOpciones> tipoPagoEmpleado = new List<LOpciones>();
            
           tipoPagoEmpleado = bdEmpleado.obtenerTipoPagoEmpleado();
           tipoPagoEmpleado.Insert(0, new LOpciones(0, "Seleccione una opción..."));
            this.cbTipoPago.DataSource = tipoPagoEmpleado;
            cbTipoPago.DisplayMember = "descripcion";
            cbTipoPago.ValueMember = "idEntero";
            this.cbTipoPago.SelectedIndex = 0;

        }

        private void configurarPropiedadesCombo(ComboBox cb) {
            cb.DisplayMember = "nombre";
            cb.ValueMember = "id";
        }
        #endregion

        #region validar
        private bool validar() {
            
            if (String.IsNullOrWhiteSpace(txtCodigo.Text)) {
                MessageBox.Show("Ingrese el código de Empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del Empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.tipoEmpleado.Equals(TipoEmpleado.Local) && String.IsNullOrWhiteSpace(txtIdentidad.Text))
            {
                MessageBox.Show("Ingrese la identidad del empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbEstadoCivil.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un estado civil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbSexo.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un sexo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            /*if (String.IsNullOrWhiteSpace(txtPasaporte.Text)) {
                MessageBox.Show("Ingrese el pasaporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }*/

            /*if (String.IsNullOrWhiteSpace(txtRTN.Text)) {
                MessageBox.Show("Ingrese el RTN", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAntecedentes.Text))
            {
                MessageBox.Show("Ingrese el Antecedente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }*/

            /*if (String.IsNullOrWhiteSpace(txtIHS.Text)) {
                MessageBox.Show("Ingrese el IHS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }*/

            if (String.IsNullOrWhiteSpace(txtDireccion.Text)) {
                MessageBox.Show("Ingrese la dirección", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!mtbTelefono.MaskFull) {
                MessageBox.Show("Telefono incompleto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbTipoEmpleado.SelectedIndex == 0) {
                MessageBox.Show("Seleccione un tipo de empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbDepartamento.SelectedValue == 0) {
                MessageBox.Show("Seleccione un departamento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbCategoria.SelectedValue == 0) {
                MessageBox.Show("Seleccione una categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtPuestoTrb.Text)) {
                MessageBox.Show("Ingrese el puesto de trabajo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nudSueldo.Value>(decimal)999999999999999.99) {
                MessageBox.Show("El sueldo debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nudSueldo.Value == (decimal)0.00)
            {
                DialogResult resultado = MessageBox.Show("Desea dejar el sueldo en 0", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (resultado != System.Windows.Forms.DialogResult.Yes)

                    if (!validarSueldoCategoria())
                    {
                        MessageBox.Show("El sueldo debe de estar en el rango de la categoria del empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
            }
            else if (!validarSueldoCategoria())
            {
                MessageBox.Show("El sueldo debe de estar en el rango de la categoria del empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           
           

            if ((int)cbA_IHS.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione si Afecta al Seguro Social", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if ((int)cb_A_RAP.SelectedIndex == 0) {
                MessageBox.Show("Seleccione si afecta al rap", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cb_A_SIN.SelectedIndex == 0) {
                MessageBox.Show("Seleccione si afecta al sindicato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cb_A_ISR.SelectedIndex == 0) {
                MessageBox.Show("Seleccione si afecta al impuesto sobre la renta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbTipoPago.SelectedIndex == 0) {
                MessageBox.Show("Seleccione un tipo de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return false;
                
            }

            if ((int)cbTipoPago.SelectedIndex == 3)
            {
                if (String.IsNullOrWhiteSpace(txtBanco.Text) || string.IsNullOrWhiteSpace(txtNumeroCuenta.Text))
                {
                    MessageBox.Show("Escriba el nombre del banco y el número de cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region obtener los datos de los controles

        #endregion

        #region guardar nuevo empleado
        private void nuevo() {
            EmpleadoConexion empConexion = new EmpleadoConexion(conexion);
            string estadoCivil = cbEstadoCivil.SelectedValue.ToString();
            string sexo = cbSexo.SelectedValue.ToString();
            LOpciones tipo = (LOpciones)cbTipoEmpleado.SelectedItem;
            int idDepto = (int)cbDepartamento.SelectedValue;
            int idCategoria = (int)cbCategoria.SelectedValue;
            string A_IHS = cbA_IHS.SelectedValue.ToString();
            string A_FSV = this.cb_A_RAP.SelectedValue.ToString();
            string A_SIN = this.cb_A_SIN.SelectedValue.ToString();
            string A_ISR = this.cb_A_ISR.SelectedValue.ToString();
            LOpciones tipoPago = (LOpciones)cbTipoPago.SelectedItem;
            
            double sueldo = Convert.ToDouble(nudSueldo.Text);
            string tipoEmpleadoNa;
            if (rbExtranjero.Checked)
            {
                 tipoEmpleadoNa = "E";
            }
            else
                tipoEmpleadoNa = "L";
         
            bool agrego = empConexion.accionesEmpleado("N",-1,txtCodigo.Text, txtNombre.Text,
                           this.dtpFechaNacimiento.Value.Date,
                           this.txtIdentidad.Text, estadoCivil, txtPasaporte.Text, txtRTN.Text,
                           this.txtAntecedentes.Text,
                           this.txtIHS.Text, txtDireccion.Text, this.mtbTelefono.Text, this.dtpFechaIngreso.Value.Date,
                           sexo, tipo.idEntero, idDepto, idCategoria,
                           this.txtPuestoTrb.Text,
                           sueldo, A_IHS, A_FSV, A_SIN, A_ISR,
                            tipoPago.idEntero, this.txtBanco.Text, this.txtNumeroCuenta.Text,
                           mtbCelular.Text,txtResidencia.Text,txtLicencia.Text,dtpFechaInicio.Value.Date,tipoEmpleadoNa,
                            txtCuentaSueldo.Text, txtCuentaISR.Text,txtCuentaRegimen.Text,txtCuentaISR.Text,
                           txtOtraCuenta1.Text,txtOtraCuenta2.Text);
            if (agrego)
            {
                //cargar en el formulario principal los datos
                //this.DialogResult = DialogResult.OK;
                MessageBox.Show("Empleado agregado exitosamente", "Agregar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles();

            }
            else
                MessageBox.Show("Error ya existe un empleado con ese código o identidad ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        #endregion

        #region modifiicar
        private void modificar() {
            EmpleadoConexion empConexion = new EmpleadoConexion(conexion);
            string estadoCivil = cbEstadoCivil.SelectedValue.ToString();
            string sexo = cbSexo.SelectedValue.ToString();
            LOpciones tipo = (LOpciones)cbTipoEmpleado.SelectedItem;
            int idDepto = (int)cbDepartamento.SelectedValue;
            int idCategoria = (int)cbCategoria.SelectedValue;
            string A_IHS = cbA_IHS.SelectedValue.ToString();
            string A_FSV = this.cb_A_RAP.SelectedValue.ToString();
            string A_SIN = this.cb_A_SIN.SelectedValue.ToString();
            string A_ISR = this.cb_A_ISR.SelectedValue.ToString();
            LOpciones tipoPago = (LOpciones)cbTipoPago.SelectedItem;
            string tipoEmpleadoNa;
            if (rbExtranjero.Checked)
            {
                tipoEmpleadoNa = "E";
            }
            else
                tipoEmpleadoNa = "L";
            double sueldo = Convert.ToDouble(this.nudSueldo.Value);
            bool modifico = empConexion.accionesEmpleado("M",empleado.Id,txtCodigo.Text,
                           txtNombre.Text,
                           this.dtpFechaNacimiento.Value.Date,
                           this.txtIdentidad.Text,
                           estadoCivil,
                           txtPasaporte.Text,
                           txtRTN.Text,
                           this.txtAntecedentes.Text,
                           this.txtIHS.Text, txtDireccion.Text,
                           this.mtbTelefono.Text,
                           this.dtpFechaIngreso.Value.Date,
                           sexo, tipo.idEntero, idDepto, idCategoria,
                           this.txtPuestoTrb.Text,
                           sueldo, A_IHS, A_FSV, A_SIN, A_ISR,
                            tipoPago.idEntero, this.txtBanco.Text, this.txtNumeroCuenta.Text,                           
                            mtbCelular.Text, txtResidencia.Text,txtLicencia.Text, dtpFechaInicio.Value.Date,tipoEmpleadoNa,
                            txtCuentaSueldo.Text, txtCuentaISR.Text, txtCuentaRegimen.Text, txtCuentaISR.Text,
                           txtOtraCuenta1.Text, txtOtraCuenta2.Text
                            );
            if (modifico)
            {
                //cargar en el formulario principal los datos
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Empleado modificado exitosamente", "Agregar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error ya existe un empleado con ese código o identidad ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region desactivar banco y numero de cuenta si no esta seleccionado cheque bancario
        private void desactivarBancoCuenta() {
            if (this.cbTipoPago.SelectedValue != null) {
                string tipoPago = this.cbTipoPago.SelectedValue.ToString();
                if (!tipoPago.Equals("B"))
                {
                    txtBanco.Visible = false;
                    txtNumeroCuenta.Visible = false;
                    lblBanco.Visible = false;
                    lblCuenta.Visible = false;
                    this.pnEmpleado2.AutoSize = true;
                    txtBanco.Text = "";
                    txtNumeroCuenta.Text = "";
                }
                else
                {
                    txtBanco.Visible = true;
                    txtNumeroCuenta.Visible = true;
                    lblBanco.Visible = true;
                    lblCuenta.Visible = true;
                }
            }
        }

        #endregion
        #region bloquear controles
        public void bloquearControles() {
            this.pnEmpleado1.Enabled = false;
            this.pnEmpleado2.Enabled = false;
            this.btnGuardar.Visible = false;
            this.btnCancelar.Visible = false;
            this.AutoSize = true;
        }
        #endregion

        #region cargar los datos del empleado en el formulario editar
        private void cargarDatosEditar() {
            //configurarComboboxs();

            this.txtCodigo.Text = empleado.Codigo.ToString();
            this.txtNombre.Text = empleado.Nombre;
            this.dtpFechaNacimiento.Value = empleado.FechaNacimiento;
            this.txtIdentidad.Text = empleado.Identidad;

            this.cbEstadoCivil.SelectedValue = empleado.EstadoCivil;
            this.cbSexo.SelectedValue = empleado.Sexo;
            this.txtPasaporte.Text = empleado.Pasaporte;
            this.txtRTN.Text = empleado.RTN;
            this.txtAntecedentes.Text = empleado.Antecedentes;
            this.txtIHS.Text = empleado.IHS;
            this.txtDireccion.Text = empleado.Direccion;
            this.mtbTelefono.Text = empleado.Telefono;
            this.txtPuestoTrb.Text = empleado.PuestoAsignado;
            this.nudSueldo.Value = (decimal)empleado.Sueldo; ;
            this.txtResidencia.Text = empleado.Residencia;
            mtbCelular.Text = empleado.Celular;
            txtLicencia.Text = empleado.Licencia;

            this.cbTipoEmpleado.SelectedValue= empleado.TipoEmpleado.idEntero;

            this.cbDepartamento.SelectedValue = empleado.objDepto.Id;
            this.cbCategoria.SelectedValue = empleado.objCategoria.Id;

            this.cbA_IHS.SelectedValue = empleado.A_IHS;
            this.cb_A_RAP.SelectedValue = empleado.A_FSV;
            this.cb_A_SIN.SelectedValue = empleado.A_SIN;
            this.cb_A_ISR.SelectedValue = empleado.A_ISR;
            this.cbTipoPago.SelectedValue = empleado.TipoPago.idEntero;

            this.txtBanco.Text = empleado.Bancos;
            this.txtNumeroCuenta.Text = empleado.NCuenta;

            this.dtpFechaIngreso.Value = empleado.FechaIngreso;
            this.dtpFechaInicio.Value = empleado.FechaInicio;
            this.txtNumeroCuenta.Text = empleado.NumeroCuenta.ToString();

            this.txtCuentaSueldo.Text = empleado.CuentaSueldo.ToString();
            this.txtOtraCuenta1.Text = empleado.OtraCuent1.ToString();
            this.txtOtraCuenta2.Text = empleado.OtraCuenta2.ToString();
            this.txtCuentaSeguroSocial.Text = empleado.CuentaSeguroSocial.ToString();
            this.txtCuentaRegimen.Text = empleado.CuentaRegimenEspecial.ToString();
            this.txtCuentaISR.Text = empleado.CuentaISR.ToString();
        }
        #endregion


        #region validar sueldo de acuerdo a la categoria
        /*el sueldo debe de estar en el rango de la categoria*/
        private bool validarSueldoCategoria() {
            CategoriaData categoria = (CategoriaData)cbCategoria.SelectedItem;
            if (nudSueldo.Value >= categoria.SalarioInicial &&
              nudSueldo.Value <= categoria.SalarioFinal)
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        private void cb_A_SIN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cambiarControlEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter( e);
        }

        private void dtpFechaNacimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtIdentidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtResidencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cbEstadoCivil_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cbSexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtPasaporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtAntecedentes_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtLicencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtIHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void mtbTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void mtbCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void dtpFechaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cbTipoEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cbDepartamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cbCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cbA_IHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cb_A_RAP_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cb_A_SIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cb_A_ISR_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);

        }

        private void cbTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void txtNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void cbTipoPago_SelectedValueChanged(object sender, EventArgs e)
        {
            desactivarBancoCuenta();
        }

        private void nudSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);

        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                dtpFechaIngreso.Select();
            else
                 if (tabControl1.SelectedIndex == 2)
                      txtCuentaSueldo.Select();
        }

        private void rbLocal_Click(object sender, EventArgs e)
        {
            tipoEmpleado = TipoEmpleado.Local;
            
            pnEmpleado1.Enabled = true;
            pnEmpleado2.Enabled = true;
            pnInformacionContable.Enabled = true;
            txtCodigo.Focus();
        }

        //extranjero
        private void radioButton3_Click(object sender, EventArgs e)
        {
            tipoEmpleado = TipoEmpleado.Extranjero;
            pnEmpleado1.Enabled = true;
            pnEmpleado2.Enabled = true;
            pnInformacionContable.Enabled = true;
            txtCodigo.Focus();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            
        }

        private void txtCuentaSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.aceptaSoloNumerosDecimales(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void txtCuentaSeguroSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.aceptaSoloNumerosDecimales(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void txtCuentaRegimen_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.aceptaSoloNumerosDecimales(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void txtCuentaISR_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.aceptaSoloNumerosDecimales(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void txtOtraCuenta1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.aceptaSoloNumerosDecimales(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void txtOtraCuenta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.aceptaSoloNumerosDecimales(e);
            Utilidad.cambiarControlEnter(e);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
    }//fin class

    #region clases
    public class Sexo {
        public string id { set; get; }
        public string nombre { set; get; }

        public Sexo(string id, string nombre)
        {
            this.nombre = nombre;
            this.id = id;
        }

    }

    public class Opciones {
        public string id { set; get; }
        public string nombre { set; get; }

        public Opciones(string id, string nombre)
        {
            this.nombre = nombre;
            this.id = id;
        }
    }

    public class TipoEmpleadoCombo
    {
        public string id { set; get; }
        public string nombre { set; get; }

        public TipoEmpleadoCombo(string id, string nombre)
        {
            this.nombre = nombre;
            this.id = id;
        }
    }

    public class TipoPago
    {
        public string id { set; get; }
        public string nombre { set; get; }

        public TipoPago(string id, string nombre)
        {
            this.nombre = nombre;
            this.id = id;
        }
    }
    #endregion
}
