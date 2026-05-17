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
using nomina.Forms.Labores;
using nomina.Forms.Empleado;
using nomina.Clases.Empleado;
using nomina.Clases.Labores;
using nomina.Clases.Utilidades;
using nomina.Clases.MovimientoLabores;
using nomina.Forms.Main;
namespace nomina.Forms
{
    public partial class frmAddMovimientoLabores : Form
    {
        Conexion conexion;
        EmpleadoData empleado;
        LaboresData labor;
        bool agrego = false;
        /*cantidad de labor */
        decimal subTotalLabor;
        DateTime fecha;
        DateTime fechaModificar;
        int filaSeleccionada;
        List<MLaboresData> lMovimientosLabores = new List<MLaboresData>();
        MLaboresConexion bd;
        string isr;
        frmMain frmMain;

        public frmAddMovimientoLabores(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            //Utilidad.configurarDataGrid(dgvLabores);
            lblCodigoEmpleado.Enabled = false;
            //btnBuscarEmpleado.BackgroundImageLayout = ImageLayout.Center;
            this.conexion = conexion;
            this.frmMain = frmMain; ;
            bd = new MLaboresConexion(conexion);
            this.nudCantidadLabor.Maximum = (decimal)99999.00;
            this.nudMontoLabor.Maximum = (decimal)999999999999999.99;
            //this.nudTotal.Maximum = (decimal)999999999999999.99;
           // this.btnMoficarLabor.Visible = false;
            this.btnBuscarEmpleado.Select();
             limpiarControlesGuardar();
        }

        #region evento para buscar el empleado y la labor
        private void btnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleado frm = new frmEmpleado(conexion,frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) {
                this.empleado = frm.empleado;
                this.lblCodigoEmpleado.Text = empleado.Codigo;
                this.lblNombreEmpleado.Text = empleado.Nombre;
                this.lblSueldo.Text = empleado.Sueldo.ToString();
                this.lblDepartamento.Text = empleado.nombreDepto;
              
                this.btnBuscarEmpleado.Visible = false;
            }
        }


        /*carga los datos de la labor*/
        private void btnBuscarLabor_Click(object sender, EventArgs e)
        {
            cargarLabor();
        }

        #endregion

        #region eventos para manejar el cambio de control cuando se presiona enter
        private void txtDescripcionLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudMontoLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudCantidadLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnBuscarEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnBuscarLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudCantidadLabor_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
            if (nudCantidadLabor.Value > (decimal)99999.99)
            {
                nudCantidadLabor.Value = (decimal)0.00;
            }
        }
        #endregion

        #region  busca y carga la labor en el formulario
        private void cargarLabor() {
            frmLabores frm = new frmLabores(conexion,frmMain);
            frm.Tag = "Buscar";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.labor = frm.labor;
                this.lblCodigoLabor.Text = labor.Codigo;
                this.lblNombreLabor.Text = labor.Nombre;

                if (labor.TipoPago.descripcion.Substring(0,1).Equals("V"))
                {
                    nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    nudMontoLabor.Value = (decimal)0.00;
                    nudMontoLabor.DecimalPlaces = 2;
                    this.nudMontoLabor.Value = (Decimal)labor.Monto;
                    this.nudCantidadLabor.Enabled = true;
                    this.nudMontoLabor.Enabled = false;
                }
                else
                     if (labor.TipoPago.descripcion.Substring(0,1).Equals("F"))
                {
                    nudMontoLabor.Maximum = (decimal)9999999999.0000000;
                    nudMontoLabor.Value = (decimal)0.00;
                    nudMontoLabor.DecimalPlaces = 7;
                    this.nudMontoLabor.Value = (Decimal)labor.Factor;
                    this.nudCantidadLabor.Enabled = true;
                    this.nudMontoLabor.Enabled = false;
                }
                else
                      if (labor.TipoPago.descripcion.Substring(0,1).Equals("D"))
                {
                    this.nudMontoLabor.Value = (decimal)0.00;
                    nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    nudMontoLabor.DecimalPlaces = 2;
                    this.nudMontoLabor.Enabled = true;
                    this.nudCantidadLabor.Enabled = false;
                }
                else
                      if (labor.TipoPago.descripcion.Substring(0,1).Equals("H"))
                {
                    this.nudCantidadLabor.Enabled = true;
                    this.nudMontoLabor.Enabled = false;
                    this.nudMontoLabor.Value = (decimal)0.00;
                    this.nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    this.nudMontoLabor.DecimalPlaces = 2;
                }
            }
        }
        #endregion

        #region menu
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //this.btnModificar.Enabled = false;
            //if (validar()) {
            //    this.btnModificar.Visible = true;
            //    this.btnMoficarLabor.Visible = false;
            //    this.agregarLaborAlDatagrid();
            //}
        }
        /*cargo la labor seleccionada del datagrid en los controles*/

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if (dgvLabores.Rows.Count > 0) {
            //    btnModificar.Visible = false;
            //    bntQuitar.Location = new Point(797, 423);
            //    btnGuardar.Location = new Point(797, 469);
            //    btnCancelar.Location = new Point(797,514);
            //    btnSalir.Location = new Point(797, 560);
            //    filaSeleccionada = dgvLabores.CurrentRow.Index;
            //    this.btnBuscarEmpleado.Enabled = false;
            //    this.btnBuscarLabor.Enabled = false;
            //    this.btnBuscarEmpleado.Enabled = false;
            //    this.btnGuardar.Enabled = false;
            //    this.btnNuevo.Enabled = false;
            //    this.bntQuitar.Enabled = false;
            //    this.btnBuscarLabor.Visible = false;
            //    btnBuscarEmpleado.Visible = false;
            //    cargarLaborControles();
            //}
        }


        private void bntQuitar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }

        }

        private void btnMoficarLabor_Click(object sender, EventArgs e)
        {
            //this.btnMoficarLabor.Visible = false;
            //this.btnModificar.Visible = true;
            //this.btnModificar.Location = new Point(797, 423);
            //this.bntQuitar.Location = new Point(797, 469);
            //this.btnGuardar.Location = new Point(797, 514);
            //btnCancelar.Location = new Point(797, 560);
            //btnSalir.Location = new Point(797, 606);

            //modificarLabor();


            // agregarLaborAlDatagrid();
        }
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("¿Quiere aplicar esta labor al ISR?", "Aplicar ISR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                this.isr = "S";
            }
            else
                this.isr = "N";

            if(validar())
                insertarLaboresBaseDatos();
        }
        #endregion

        #region validar
        private bool validar() {
            if (String.IsNullOrWhiteSpace(lblCodigoEmpleado.Text)) {
                MessageBox.Show("Seleccione un empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (String.IsNullOrWhiteSpace(lblCodigoEmpleado.Text)) {
                MessageBox.Show("Seleccione una labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtDescripcionLabor.Text))
            {
                MessageBox.Show("Ingrese una descripción de la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((this.nudMontoLabor.Value >(decimal)999999999999999.99))
            {
                this.nudMontoLabor.Value = (decimal)0.00;
                MessageBox.Show("El monto debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudCantidadLabor.Value > (decimal)99999.99)
            {
                this.nudCantidadLabor.Value = (decimal)0.00;
                MessageBox.Show("La cantidad debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((labor.TipoPago.descripcion[0].Equals("F") || labor.TipoPago.descripcion[0].Equals("V") 
                || labor.TipoPago.descripcion[0].Equals("H")) && (double)nudCantidadLabor.Value <= 0.00)
            {
                MessageBox.Show("Ingrese la cantidad de la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (labor.TipoPago.descripcion[0].Equals("D") && (double)this.nudMontoLabor.Value <= 0.00) {

                MessageBox.Show("Ingrese el monto de la Labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtCodigoCuenta.Text)) {
                MessageBox.Show("Seleccione una cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        #endregion

        #region agregar labor al datagrid cuando le da click en el boton agregar
        private void agregarLaborAlDatagrid() {
            //MLaboresData laborData = null;
            //bool existeLabor = false;
            ///*agrega la labor la primera vez que no hay nada en la lista y 
            // si no existe la labor en la base de datos*/
            //existeLabor = bd.existeMLabor(lblCodigoEmpleado.Text, lblCodigoLabor.Text,
            //                this.dtpFecha.Value.Date);


            //if (lMovimientosLabores.Count == 0 && !existeLabor)
            //{
            //    calcularMonto(labor.TipoLabor[0]);
            //    //agrego a la lista de objetos de labores la labor
            //    this.lMovimientosLabores.Add(new MLaboresData(lblCodigoEmpleado.Text, lblCodigoLabor.Text,
            //                              txtDescripcionLabor.Text,
            //                               this.labor.TipoLabor,
            //                                (double)this.nudCantidadLabor.Value, dtpFecha.Value.Date, this.subTotalLabor,
            //                                this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                          ));
            //    this.nudTotal.Value = Convert.ToDecimal(sumarTotal());

            //}

            /*busco en la lista si ya existe la labor
             si me retorna null quiere decir que no existe y la agrego a la lista
             
             si ya existe una labor o mas de una 
             valido si existe en la lista y en la base de datos
            // */
            //else
            // if (lMovimientosLabores.Count > 0)
            //{
            //    laborData = this.lMovimientosLabores.FirstOrDefault(x => x.CodigoEmpleado == this.lblCodigoEmpleado.Text &&
            //                                      x.CodigoLabor == this.lblCodigoLabor.Text && x.FechaLabor == this.dtpFecha.Value.Date);

            //    existeLabor = bd.existeMLabor(lblCodigoEmpleado.Text,
            //                                       lblCodigoLabor.Text,
            //                                       this.dtpFecha.Value.Date);
            //    if (laborData == null && !existeLabor) {
            //        calcularMonto(labor.TipoLabor[0]);
            //        this.lMovimientosLabores.Add(new MLaboresData(lblCodigoEmpleado.Text, lblCodigoLabor.Text,
            //                              txtDescripcionLabor.Text,
            //                              this.labor.TipoLabor,
            //                                (double)this.nudCantidadLabor.Value, dtpFecha.Value.Date, this.subTotalLabor,
            //                                this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                          ));
            //    }

            //    /*MANDAR A LLAMAR AL PROCEDIMIENTO ALMACENADO*/
            //}


            //this.fecha = this.dtpFecha.Value.Date;
            ///*si labor no existe en la lista ni en la base de datos entonces
            // * se puede agregar al datagrid*/
            ///*VALIDACION DE BASE DE DATOS CON UN &&*/
            //if (laborData == null && !existeLabor)
            //{
            //    calcularMonto(this.labor.TipoLabor[0]);
            //    this.dgvLabores.Rows.Add(this.lblCodigoEmpleado.Text, lblCodigoLabor.Text, this.dtpFecha.Value.Date, descripcionLabor(this.labor.TipoLabor[0]),
            //                                this.txtDescripcionLabor.Text,
            //                              this.nudCantidadLabor.Value.ToString(),
            //                               this.subTotalLabor
            //                            );
            //    this.nudTotal.Value = sumarTotal();
            //    limpiarControlesCuandoModifica();
            //    this.btnModificar.Enabled = true;
            //}
            //else
            //{
            //    MessageBox.Show("Ya existe la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
        #endregion 

        #region mofificar labor
        private void modificarLabor()
        {
            //if (!this.fechaModificar.Equals(this.dtpFecha.Value))
            //{
            //    if (this.dgvLabores.Rows.Count == 0) {
            //        this.nudTotal.Value = (decimal)0.00;
            //    }
            //    MLaboresData laborData = null;
            //    /*AGREGARR CAMBIOS*/
            //    laborData = this.lMovimientosLabores.FirstOrDefault(x => x.CodigoEmpleado == this.lblCodigoEmpleado.Text &&
            //                                            x.CodigoLabor == this.lblCodigoLabor.Text && x.FechaLabor == this.dtpFecha.Value.Date);

            //    if (laborData == null)
            //    {
            //        //calcularMonto(labor.TipoLabor[0]);
            //        calcularMonto(this.labor.TipoLabor[0]);

            //        this.lMovimientosLabores.RemoveAt(filaSeleccionada);
            //        this.lMovimientosLabores.Insert(filaSeleccionada, new MLaboresData(lblCodigoEmpleado.Text, lblCodigoLabor.Text,
            //                                      txtDescripcionLabor.Text,
            //                                       this.labor.TipoLabor,
            //                                        (double)this.nudCantidadLabor.Value, dtpFecha.Value.Date, this.subTotalLabor,
            //                                        this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                                  ));

            //        this.dgvLabores.Rows.RemoveAt(filaSeleccionada);
            //        this.dgvLabores.Rows.Insert(filaSeleccionada, this.lblCodigoEmpleado.Text, lblCodigoLabor.Text, this.dtpFecha.Value.Date, descripcionLabor(this.labor.TipoLabor[0]),
            //                                   this.txtDescripcionLabor.Text,
            //                                   this.nudCantidadLabor.Value.ToString(),
            //                                   this.subTotalLabor
            //                               );
            //        MessageBox.Show("Labor Modificada exitosamente", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.nudTotal.Value = Convert.ToDecimal(sumarTotal());
            //        limpiarControlesCuandoModifica();
            //        this.btnBuscarEmpleado.Enabled = true;
            //        this.btnBuscarLabor.Enabled = true;
            //        this.btnGuardar.Enabled = true;
            //        this.btnNuevo.Enabled = true;
            //        this.bntQuitar.Enabled = true;
            //        this.btnBuscarLabor.Visible = true;
            //    }
            //}
            //else
            //{
            //    calcularMonto(this.labor.TipoLabor[0]);

            //    this.lMovimientosLabores.RemoveAt(filaSeleccionada);
            //    this.lMovimientosLabores.Insert(filaSeleccionada, new MLaboresData(lblCodigoEmpleado.Text, lblCodigoLabor.Text,
            //                                  txtDescripcionLabor.Text,
            //                                   this.labor.TipoLabor,
            //                                    (double)this.nudCantidadLabor.Value, dtpFecha.Value.Date, this.subTotalLabor,
            //                                    this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                              ));

            //    this.dgvLabores.Rows.RemoveAt(filaSeleccionada);
            //    this.dgvLabores.Rows.Insert(filaSeleccionada, this.lblCodigoEmpleado.Text, lblCodigoLabor.Text, this.dtpFecha.Value.Date, descripcionLabor(this.labor.TipoLabor[0]),
            //                               this.txtDescripcionLabor.Text,
            //                               this.nudCantidadLabor.Value.ToString(),
            //                               this.subTotalLabor
            //                           );
            //    MessageBox.Show("Labor Modificada exitosamente", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.nudTotal.Value = Convert.ToDecimal(sumarTotal());
            //    limpiarControlesCuandoModifica();
            //    this.btnBuscarEmpleado.Enabled = true;
            //    this.btnBuscarLabor.Enabled = true;
            //    this.btnGuardar.Enabled = true;
            //    this.btnNuevo.Enabled = true;
            //    this.bntQuitar.Enabled = true;
            //    this.btnBuscarLabor.Visible = true;

            //}
        }

        #endregion

        #region mofificar labor prueba
        private void modificarLaborPrueba()
        {
            //calcularMonto(labor.TipoLabor[0]);

            //calcularMonto(this.labor.TipoLabor[0]);
            //this.dgvLabores.Rows.RemoveAt(filaSeleccionada);
            //this.dgvLabores.Rows.Insert(filaSeleccionada, this.lblCodigoEmpleado.Text, lblCodigoLabor.Text, this.dtpFecha.Value.Date, descripcionLabor(this.labor.TipoLabor[0]),
            //                            this.txtDescripcionLabor.Text,
            //                            this.nudCantidadLabor.Value.ToString(),
            //                            this.subTotalLabor
            //                        );
            //this.lMovimientosLabores.RemoveAt(filaSeleccionada);
            //this.lMovimientosLabores.Insert(filaSeleccionada, new MLaboresData(lblCodigoEmpleado.Text, lblCodigoLabor.Text,
            //                              txtDescripcionLabor.Text,
            //                               this.labor.TipoLabor,
            //                                (double)this.nudCantidadLabor.Value, dtpFecha.Value.Date, this.subTotalLabor,
            //                                this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                          ));

            //MessageBox.Show("Labor Modificada exitosamente", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.nudTotal.Value = Convert.ToDecimal(sumarTotal());
            //limpiarControlesCuandoModifica();
            //this.btnBuscarEmpleado.Enabled = true;
            //this.btnBuscarLabor.Enabled = true;
            //this.btnGuardar.Enabled = true;
            //this.btnNuevo.Enabled = true;
            //this.bntQuitar.Enabled = true;
            //this.btnBuscarLabor.Visible = true;
        }

        #endregion

        #region cargo la labor en los controles
        private void cargarLaborControles() {
            //if (dgvLabores.Rows.Count > 0)
            //{
            //    this.btnMoficarLabor.Visible = true;
            //    int fila = dgvLabores.CurrentRow.Index;
            //    txtDescripcionLabor.Text = this.dgvLabores.Rows[fila].Cells["Descripcion"].Value.ToString();
            //    cargarMontoOCantidad(labor.TipoLabor[0], fila);
            //    string[] fecha = this.dgvLabores.Rows[fila].Cells["cFecha"].Value.ToString().ToString().Split('/');
            //    string[] año = fecha[2].Split(' ');
            //    this.fecha = new DateTime(Convert.ToInt32(año[0]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
            //    DateTime fechaLabor = new DateTime(Convert.ToInt32(año[0]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
            //    this.fechaModificar = fechaLabor;
            //    this.dtpFecha.Value = fechaLabor;

            //}
        }

        #endregion

        #region cargar monto y cantidad segun el tipo de labor en editar
        public void cargarMontoOCantidad(char tipoLabor, int fila) {
            //switch (tipoLabor)
            //{
            //    case 'D':
            //        this.nudMontoLabor.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["MontoTotal"].Value.ToString());
            //        this.nudCantidadLabor.Value = (decimal)0.00;
            //        this.nudMontoLabor.Enabled = true;
            //        this.nudCantidadLabor.Enabled = false;
            //        break;
            //    case 'F':
            //        this.nudCantidadLabor.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["CantidadLabor"].Value.ToString());
            //        this.nudMontoLabor.Value = (decimal)this.labor.Factor;
            //        this.nudMontoLabor.Enabled = false;
            //        this.nudCantidadLabor.Enabled = true;
            //        break;
            //    case 'H':
            //        this.nudCantidadLabor.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["CantidadLabor"].Value.ToString());
            //        this.nudMontoLabor.Value = (decimal)0.00;
            //        this.nudMontoLabor.Enabled = false;
            //        this.nudCantidadLabor.Enabled = true;
            //        break;
            //    case 'V':
            //        this.nudCantidadLabor.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["CantidadLabor"].Value.ToString());
            //        this.nudMontoLabor.Value = (decimal)this.labor.Monto;
            //        this.nudMontoLabor.Enabled = false;
            //        this.nudCantidadLabor.Enabled = true;
            //        break;
            //}
        }

        #endregion

        #region calcular el monto del monto de la labor por la cantidad de labor
        private void calcularMonto(string tipoLabor) {
            decimal monto;
            switch (tipoLabor)
            {
                case "D":
                    monto = Decimal.Round(nudMontoLabor.Value, 2);
                    this.subTotalLabor = monto;
                    this.nudMontoTotal.Value = (decimal)this.subTotalLabor;
                    break;

                case "F":
                    monto = Convert.ToDecimal(this.labor.Factor) * Decimal.Round(nudCantidadLabor.Value, 2);
                    this.subTotalLabor = monto;
                    this.nudMontoTotal.Value = (decimal)this.subTotalLabor;
                    break;

                case "H":
                    this.subTotalLabor = ((decimal)this.empleado.Sueldo / 30) / 8 * Decimal.Round(nudCantidadLabor.Value, 2);
                    this.nudMontoTotal.Value = (decimal)this.subTotalLabor;
                    break;


                case "V":
                    this.subTotalLabor = labor.Monto * Decimal.Round(nudCantidadLabor.Value, 2);
                    this.nudMontoTotal.Value = this.subTotalLabor;
                    break;

            }

        }
        #endregion

        #region limpiar controles
        private void limpiarControlesCuandoModifica() {
            if(this.labor!=null)
            {
                this.txtDescripcionLabor.Text = "";
                if (labor.TipoPago.descripcion[0].Equals("H") || labor.TipoPago.descripcion[0].Equals("D"))
                    this.nudMontoLabor.Value = (decimal)0.00;
                    this.nudCantidadLabor.Value = (decimal)0.00;
                     this.btnBuscarEmpleado.Select();
            }
        }

        #endregion

        #region retorna la descripcion de la labor
        public string descripcionLabor(char descripcion) {
            switch (descripcion) {
                case 'D':
                    return "Definido por el usuario";
                case 'F':
                    return "Factor";
                    break;
                case 'H':
                    return "Por hora";
                case 'V':
                    return "Por valor";
                default:
                    return "";
            }
        }

        #endregion

        #region insertar labores
        private void insertarLaboresBaseDatos() {
           
           bool agrego = bd.accionesLabores("N",empleado.Id, labor.Id,
                                 txtDescripcionLabor.Text, labor.TipoPago.id,
                                 nudCantidadLabor.Value, dtpFecha.Value.Date, nudMontoLabor.Value,
                                 0,this.isr,0);
 
                
                //this.btnBuscarEmpleado.Visible = true;
                //this.btnBuscarLabor.Visible = true;
                //this.nudMontoLabor.Value = (decimal)0.00;
                ////this.nudTotal.Value = (decimal)0.00;
                //this.lMovimientosLabores.Clear();
         

            calcularMonto(labor.TipoPago.descripcion.Substring(0,1));
          
            if (agrego)
            {
                MessageBox.Show("Labores agregadas exitosamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnBuscarEmpleado.Visible = true;
                this.btnBuscarLabor.Visible = true;
                this.nudMontoLabor.Value = (decimal)0.00;
                //this.nudTotal.Value = (decimal)0.00;
                // this.lMovimientosLabores.Clear();
                //MessageBox.Show("Labores agregadas exitosamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                limpiarControlesGuardar();
            }
            else
            {
                MessageBox.Show("Ya existe un movimiento de labor en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        #endregion

        #region limpiar controles cuando guarda
       public void limpiarControlesGuardar(){
           // this.dgvLabores.Rows.Clear();
            this.lblCodigoEmpleado.Text = "";
            lblNombreEmpleado.Text = "";
            this.lblCodigoLabor.Text = "";
            this.lblSueldo.Text = "";
            this.lblDepartamento.Text = "";
           this.lblNombreLabor.Text = "";
            txtCodigoCuenta.Text = "";
            this.txtNombreCuenta.Text = "";
            this.btnBuscarEmpleado.Select();

            limpiarControlesCuandoModifica();

        }

        #endregion

        #region calcular total cuando agrega una labor
        public decimal sumarTotal()
        {
            decimal total = 0;
            foreach (MLaboresData mLabor in lMovimientosLabores)
            {
                total += (decimal)mLabor.MontoLabor;
            }
            return total;
        }
        #endregion

        #region restar el total cuando quita una labor
        public decimal restarTotal(decimal montoQuitar)
        {
            decimal total = 0;
            total = sumarTotal();
            total -= montoQuitar;

            return total;
        }
        #endregion

        private void gpEmpleados_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }

        #region
        public void limpiarLabor(char tipoLabor) {
            switch (tipoLabor) {
                case 'D':

                    break;

                case 'F':
                    break;

                case 'H':
                    break;

                case 'V':
                    break;
            }
        }
        #endregion

        private void nudCantidadLabor_ValueChanged(object sender, EventArgs e)
        {
            if (nudCantidadLabor.Value > (decimal)99999.99) {
                nudCantidadLabor.Value = (decimal)0.00;
            }

            calcularMonto(labor.TipoPago.descripcion.Substring(0,1));
        }

        private void nudMontoLabor_ValueChanged(object sender, EventArgs e)
        {
            calcularMonto(labor.TipoPago.descripcion.Substring(0,1));
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtCodigoCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
    }
}
