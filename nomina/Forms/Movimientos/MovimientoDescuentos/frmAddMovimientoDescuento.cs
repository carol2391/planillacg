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
using nomina.Clases.Descuentos;
using nomina.Clases.Utilidades;
using nomina.Clases.MovimientoLabores;
using nomina.Forms.Descuento;
using nomina.Clases.MovimiendoDescuentos;
using nomina.Forms.Main;


namespace nomina.Forms.MovimientoDescuentos
{
    public partial class frmAddMovimientoDescuento : Form
    {
        #region propiedades
        Conexion conexion;
        EmpleadoData empleado;
        DescuentoData descuentoData;
        bool agrego = false;
        /*cantidad de labor */
        decimal subTotalDescuento;
        DateTime fecha;
        DateTime fechaModificar;
        int filaSeleccionada;
        List<MDescuentoData> lMovimientosDescuentos = new List<MDescuentoData>();
        MDescuentoConexion bd;
        frmMain frmMain;
        #endregion

        public frmAddMovimientoDescuento(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            //Utilidad.configurarDataGrid(dgvLabores);
            lblCodigoEmpleado.Enabled = false;
            //btnBuscarEmpleado.BackgroundImageLayout = ImageLayout.Center;
            this.conexion = conexion;
            bd = new MDescuentoConexion(conexion);
            this.frmMain = frmMain;
            this.nudCantidadDescuento.Maximum = (decimal)99999.99;
            this.nudMontoDescuento.Maximum = (decimal)999999999999999.99;
           /// this.nudTotal.Maximum = (decimal)999999999999999.99;
            //this.btnMoficarDescuento.Visible = false;
            this.btnBuscarEmpleado.Select();
            limpiarControlesGuardar();
        }

        #region menu
        private void btnNuevo_Click(object sender, EventArgs e)
        {
           //// this.btnModificar.Enabled = false;
           // if (validar())
           // {
           //     this.btnModificar.Visible = true;
           //     this.btnMoficarDescuento.Visible = false;
           //     this.agregarDescuentoAlDatagrid();
           // }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if(dgvLabores.Rows.Count > 0) {
            //    btnModificar.Visible = false;
            //    bntQuitar.Location = new Point(797, 426);
            //    btnGuardar.Location = new Point(797, 472);
            //    btnCancelar.Location = new Point(797, 517);
            //    btnSalir.Location = new Point(797, 563);
            //    filaSeleccionada = dgvLabores.CurrentRow.Index;
            //    this.btnBuscarEmpleado.Enabled = false;
            //    this.btnBuscarDescuento.Enabled = false;
            //    this.btnBuscarEmpleado.Enabled = false;
            //    this.btnGuardar.Enabled = false;
            //    this.btnNuevo.Enabled = false;
            //    this.bntQuitar.Enabled = false;
            //    this.btnBuscarDescuento.Visible = false;
            //    btnBuscarEmpleado.Visible = false;
            //    cargarLaborControles();
            //}
        }

        private void btnMoficarLabor_Click(object sender, EventArgs e)
        {
            //this.btnMoficarDescuento.Visible = false;
            //this.btnModificar.Visible = true;
            //this.btnModificar.Location = new Point(797, 426);
            //this.bntQuitar.Location = new Point(797, 472);
            //this.btnGuardar.Location = new Point(797, 517);
            //btnCancelar.Location = new Point(797, 563);
            //btnSalir.Location = new Point(797, 609);

            //modificarDescuento();
        }
        private void bntQuitar_Click(object sender, EventArgs e)
        {
            //    if (dgvLabores.RowCount > 0)
            //    {
            //        if (dgvLabores.CurrentRow != null)
            //        {
            //            int fila = dgvLabores.CurrentRow.Index;
            //            MDescuentoData labor;
            //            labor = lMovimientosDescuentos.ElementAt(fila);
            //            this.nudTotal.Value = Convert.ToDecimal(restarTotal(labor.MontoDescuento));
            //            dgvLabores.Rows.RemoveAt(fila);
            //            lMovimientosDescuentos.RemoveAt(fila);
            //            this.btnMoficarDescuento.Visible = false;
            //            this.btnModificar.Visible = true;
            //        }
            //    }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            insertarLaboresBaseDatos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }
    

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        #endregion

        #region restar el total cuando quita un descuento
        public decimal restarTotal(decimal montoQuitar)
        {
            decimal total = 0;
            total = sumarTotal();
            total -= montoQuitar;

            return total;
        }
        #endregion

        #region cambia de control con enter
        private void btnBuscarEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnBuscarLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

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

        private void txtCodigoCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnBuscarCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        #endregion

        #region eventos para buscar empleado y descuento
        private void btnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleado frm = new frmEmpleado(conexion,frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.empleado = frm.empleado;
                this.lblCodigoEmpleado.Text = empleado.Codigo;
                this.lblNombreEmpleado.Text = empleado.Nombre;
                this.lblSueldo.Text = empleado.Sueldo.ToString();
                this.lblDepartamento.Text = empleado.objDepto.NombreDepartamento;
                this.btnBuscarEmpleado.Visible = false;
            }
        }

        
        private void BtnBuscarDescuento_Click(object sender, EventArgs e)
        {
            cargarDescuento();
        }
        #endregion

        #region  busca y carga el descuento en el formulario
        private void cargarDescuento()
        {
            frmDescuento frm = new frmDescuento(conexion,frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.descuentoData = frm.descuento;
                this.lblCodigoDescuento.Text = descuentoData.Codigo;
                this.lblNombreLabor.Text = descuentoData.Nombre;
                if (descuentoData.TipoPago.descripcion.ToUpper().Equals("VALOR"))
                {
                    this.nudMontoDescuento.Value = (Decimal)descuentoData.Monto;
                    this.nudCantidadDescuento.Enabled = true;
                    this.nudMontoDescuento.Enabled = false;
                    nudMontoDescuento.DecimalPlaces = 2;
                    this.nudMontoDescuento.Maximum = (decimal)999999999999999.00;

                }
                else
                     if (descuentoData.TipoPago.descripcion.ToUpper().Equals("FACTOR"))
                {
                    this.nudMontoDescuento.Value = (Decimal)descuentoData.Factor;
                    this.nudCantidadDescuento.Enabled = true;
                    this.nudMontoDescuento.Enabled = false;
                    nudMontoDescuento.DecimalPlaces = 7;
                    this.nudMontoDescuento.Maximum = (decimal)9999999999.00;
                }
                else
                      if (descuentoData.TipoPago.descripcion.ToUpper().Equals("DEFINIDO POR EL USUARIO"))
                {
                    this.nudMontoDescuento.Value = (decimal)0.00;
                    this.nudMontoDescuento.Enabled = true;
                    this.nudCantidadDescuento.Enabled = false;
                    nudMontoDescuento.Maximum = (decimal)999999999999999.00;
                    nudMontoDescuento.DecimalPlaces = 2;
                    
                }
                else
                      if (descuentoData.TipoPago.descripcion.ToUpper().Equals("HORA"))
                {
                    this.nudCantidadDescuento.Enabled = true;
                    this.nudMontoDescuento.Enabled = false;
                    this.nudMontoDescuento.Value = (decimal)0.00;
                    nudMontoDescuento.Maximum = (decimal)999999999999999.00;
                    nudMontoDescuento.DecimalPlaces = 2;
                }
            }
        }
        #endregion

        #region limpiar controles cuando guarda
        public void limpiarControlesGuardar()
        {
            //this.dgvLabores.Rows.Clear();
            this.lblCodigoEmpleado.Text = "";
            lblNombreEmpleado.Text = "";
            this.lblCodigoDescuento.Text = "";
            this.lblSueldo.Text = "";
            this.lblDepartamento.Text = "";
            this.lblNombreLabor.Text = "";
            txtCodigoCuenta.Text = "";
            this.txtNombreCuenta.Text = "";
            this.btnBuscarEmpleado.Select();
            limpiarControlesCuandoModifica();
        }
        #endregion

        #region limpiar controles
        private void limpiarControlesCuandoModifica()
        {
            if (this.descuentoData != null)
            {
                this.txtDescripcionDescuento.Text = "";
                if (this.descuentoData.TipoPago.descripcion.ToUpper().Equals("HORA")
                    || this.descuentoData.TipoPago.descripcion.ToUpper().Equals("DEFINIDO POR EL USUARIO"))
                    this.nudMontoDescuento.Value = (decimal)0.00;
                this.nudCantidadDescuento.Value = (decimal)0.00;
                this.btnBuscarEmpleado.Select();
            }
        }
        #endregion

        #region validar
        private bool validar()
        {
            if (String.IsNullOrWhiteSpace(lblCodigoEmpleado.Text))
            {
                MessageBox.Show("Seleccione un empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (String.IsNullOrWhiteSpace(this.lblCodigoDescuento.Text))
            {
                MessageBox.Show("Seleccione un descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtDescripcionDescuento.Text))
            {
                MessageBox.Show("Ingrese una descripción de la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((this.nudMontoDescuento.Value > (decimal)999999999999999.99))
            {
                this.nudMontoDescuento.Value = (decimal)0.00;
                MessageBox.Show("El monto debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudCantidadDescuento.Value > (decimal)99999.00)
            {
                this.nudCantidadDescuento.Value = (decimal)0.00;
                MessageBox.Show("La cantidad debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((this.descuentoData.TipoPago.descripcion.ToUpper().Equals("FACTOR")
                || this.descuentoData.TipoPago.descripcion.ToUpper().Equals("VALOR")
                || this.descuentoData.TipoPago.descripcion.ToUpper().Equals("HORA")) 
                && (double)nudCantidadDescuento.Value <= 0.00)
            {
                MessageBox.Show("Ingrese la cantidad del Descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.descuentoData.TipoPago.Equals("DEFINIDO POR EL USUARIO") && (double)this.nudMontoDescuento.Value <= 0.00)
            {

                MessageBox.Show("Ingrese el monto del Descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtCodigoCuenta.Text))
            {
                MessageBox.Show("Seleccione una cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region agregar descuentp al datagrid cuando le da click en el boton agregar
        private void agregarDescuentoAlDatagrid()
        {
            //MDescuentoData descuentoData = null;
            //bool existeDescuento = false;
            ///*agrega la labor la primera vez que no hay nada en la lista y 
            // si no existe la labor en la base de datos*/
            //existeDescuento = bd.existeMDescuento(lblCodigoEmpleado.Text, lblCodigoDescuento.Text,
            //                this.dtpFecha.Value.Date);


            //if (lMovimientosDescuentos.Count == 0 && !existeDescuento)
            //{
            //    calcularMonto(this.descuentoData.TipoDescuento[0]);
            //    //agrego a la lista de objetos de labores la labor
            //    this.lMovimientosDescuentos.Add(new MDescuentoData(lblCodigoEmpleado.Text, lblCodigoDescuento.Text,
            //                                txtDescripcionDescuento.Text,
            //                                this.descuentoData.TipoDescuento,
            //                                this.nudCantidadDescuento.Value, dtpFecha.Value.Date, this.subTotalDescuento,
            //                                this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                          ));
            //    this.nudTotal.Value = Convert.ToDecimal(sumarTotal());

            //}

            ///*busco en la lista si ya existe el descuento
            // si me retorna null quiere decir que no existe y la agrego a la lista
             
            // si ya existe un descuento o mas de una 
            // valido si existe en la lista y en la base de datos
            // */
            //else
            // if (lMovimientosDescuentos.Count > 0)
            //{
            //    descuentoData = this.lMovimientosDescuentos.FirstOrDefault(x => x.CodigoEmpleado == this.lblCodigoEmpleado.Text &&
            //                                      x.CodigoDescuento == this.lblCodigoDescuento.Text && x.FechaDescuento == this.dtpFecha.Value.Date);

            //    existeDescuento = bd.existeMDescuento(lblCodigoEmpleado.Text,
            //                                       lblCodigoDescuento.Text,
            //                                       this.dtpFecha.Value.Date);
            //    if (descuentoData == null && !existeDescuento)
            //    {
            //        calcularMonto(this.descuentoData.TipoDescuento[0]);
            //        this.lMovimientosDescuentos.Add(new MDescuentoData(lblCodigoEmpleado.Text, lblCodigoDescuento.Text,
            //                              txtDescripcionDescuento.Text,
            //                              this.descuentoData.TipoDescuento,
            //                                this.nudCantidadDescuento.Value, dtpFecha.Value.Date, this.subTotalDescuento,
            //                                this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                          ));
            //    }

            //    /*MANDAR A LLAMAR AL PROCEDIMIENTO ALMACENADO*/
            //}


            //this.fecha = this.dtpFecha.Value.Date;
            ///*si descuento no existe en la lista ni en la base de datos entonces
            // * se puede agregar al datagrid*/
            ///*VALIDACION DE BASE DE DATOS CON UN &&*/
            //if (descuentoData == null && !existeDescuento)
            //{
            //    calcularMonto(this.descuentoData.TipoDescuento[0]);
            //    this.dgvLabores.Rows.Add(this.lblCodigoEmpleado.Text, lblCodigoDescuento.Text, this.dtpFecha.Value.Date, descripcionDescuento(this.descuentoData.TipoDescuento[0]),
            //                                this.txtDescripcionDescuento.Text,
            //                              this.nudCantidadDescuento.Value.ToString(),
            //                               this.subTotalDescuento
            //                            );
            //    this.nudTotal.Value = sumarTotal();
            //    limpiarControlesCuandoModifica();
            //    this.btnModificar.Enabled = true;
            //}
            //else
            //{
            //    MessageBox.Show("Ya existe el descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
        #endregion

        public string descripcionDescuento(string descripcion)
        {
            switch (descripcion)
            {
                case "D":
                    return "Definido por el usuario";
                case "F":
                    return "Factor";
                    
                case "H":
                    return "Por hora";
                case "V":
                    return "Por valor";
                default:
                    return "";
            }
        }
        #region calcular monto
        private void calcularMonto(string tipoDescuento)
        {
            decimal monto;
            switch (tipoDescuento)
            {
                case "D":
                    monto = Decimal.Round(nudMontoDescuento.Value, 2);
                    this.subTotalDescuento = monto;
                   this.nudMontoTotal.Value = monto;
                    break;

                case "F":
                    monto = Decimal.Round((decimal)this.descuentoData.Factor,2) * Decimal.Round(nudCantidadDescuento.Value, 2);
                    this.subTotalDescuento = monto;
                    this.nudMontoTotal.Value = monto;
                    break;

                case "H":
                    this.subTotalDescuento = (decimal)(this.empleado.Sueldo / 30) / 8 *Decimal.Round(nudCantidadDescuento.Value, 2);
                    this.nudMontoTotal.Value = this.subTotalDescuento;
                    break;


                case "V":
                    this.subTotalDescuento = (decimal)descuentoData.Monto * Decimal.Round(nudCantidadDescuento.Value, 2);
                    this.nudMontoTotal.Value = this.subTotalDescuento;
                    break;

            }
        }
        #endregion

        #region calcular total cuando agrega un descuento
        public decimal sumarTotal()
        {
            decimal total = 0;
            foreach (MDescuentoData mDescuento in lMovimientosDescuentos)
            {
                total += mDescuento.MontoDescuento;
            }
            return total;
        }
        #endregion

        #region insertar descuentos
        private void insertarLaboresBaseDatos()
        {
            if (lMovimientosDescuentos.Count > 0)
            {
                foreach (MDescuentoData descuento in lMovimientosDescuentos)
                {
                    //bool agrego = bd.agregarMDescuento(descuento.CodigoEmpleado, descuento.CodigoDescuento,
                    //             descuento.DescripcionDescuento, descuento.TipoDescuento,
                    //             descuento.CantidadDescuento, descuento.FechaDescuento, descuento.MontoDescuento,
                    //             descuento.CodigoCuenta, descuento.NombreCuenta);
                }
                this.btnBuscarEmpleado.Visible = true;
                this.btnBuscarDescuento.Visible = true;
                this.nudMontoDescuento.Value = (decimal)0.00;
                //this.nudTotal.Value = (decimal)0.00;
                this.lMovimientosDescuentos.Clear();
                MessageBox.Show("Descuentos agregados exitosamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                limpiarControlesGuardar();
            }

            calcularMonto(this.descuentoData.TipoPago.descripcion.Substring(0, 1));
            bool agrego = bd.accionesDescuento("N",0, empleado.Id, descuentoData.Id, txtDescripcionDescuento.Text, 
                descuentoData.TipoPago.id,
                nudCantidadDescuento.Value, dtpFecha.Value.Date, nudMontoDescuento.Value, 0);
            if (agrego)
            {
                MessageBox.Show("Descuento agregado exitosamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnBuscarEmpleado.Visible = true;
                this.btnBuscarDescuento.Visible = true;
                this.nudMontoDescuento.Value = (decimal)0.00;
                //this.nudTotal.Value = (decimal)0.00;
                // this.lMovimientosLabores.Clear();
                //MessageBox.Show("Labores agregadas exitosamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                limpiarControlesGuardar();
            }
            else
            {
                MessageBox.Show("Ya existe un movimiento de descuento en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        #endregion

        #region cargo el descuento en los controles, cuando el usuario le da clik en editar
        private void cargarLaborControles()
        {
            //if (dgvLabores.Rows.Count > 0)
            //{
            //    this.btnMoficarDescuento.Visible = true;
            //    int fila = dgvLabores.CurrentRow.Index;
            //    txtDescripcionDescuento.Text = this.dgvLabores.Rows[fila].Cells["Descripcion"].Value.ToString();
            //    cargarMontoOCantidad(descuentoData.TipoDescuento[0], fila);
            //    string[] fecha = this.dgvLabores.Rows[fila].Cells["cFecha"].Value.ToString().ToString().Split('/');
            //    string[] año = fecha[2].Split(' ');
            //    this.fecha = new DateTime(Convert.ToInt32(año[0]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
            //    DateTime fechaLabor = new DateTime(Convert.ToInt32(año[0]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
            //    this.fechaModificar = fechaLabor;
            //    this.dtpFecha.Value = fechaLabor;

            //}
        }
        #endregion


        #region cargar monto y cantidad segun el tipo de descuento en editar
        public void cargarMontoOCantidad(char tipoLabor, int fila)
        {
            switch (tipoLabor)
            {
                case 'D':
                    //this.nudMontoDescuento.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["MontoTotal"].Value.ToString());
                    this.nudCantidadDescuento.Value = (decimal)0.00;
                    this.nudMontoDescuento.Enabled = true;
                    this.nudCantidadDescuento.Enabled = false;
                    break;
                case 'F':
                    //this.nudCantidadDescuento.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["CantidadLabor"].Value.ToString());
                    this.nudMontoDescuento.Value = (decimal)this.descuentoData.Factor;
                    this.nudMontoDescuento.Enabled = false;
                    this.nudCantidadDescuento.Enabled = true;
                    break;
                case 'H':
                    //this.nudCantidadDescuento.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["CantidadLabor"].Value.ToString());
                    this.nudMontoDescuento.Value = (decimal)0.00;
                    this.nudMontoDescuento.Enabled = false;
                    this.nudCantidadDescuento.Enabled = true;
                    break;
                case 'V':
                   // this.nudCantidadDescuento.Value = Convert.ToDecimal(this.dgvLabores.Rows[fila].Cells["CantidadLabor"].Value.ToString());
                    this.nudMontoDescuento.Value = (decimal)this.descuentoData.Monto;
                    this.nudMontoDescuento.Enabled = false;
                    this.nudCantidadDescuento.Enabled = true;
                    break;
            }
        }

        #endregion


        #region mofificar descuento
        private void modificarDescuento()
        {
            //if (!this.fechaModificar.Equals(this.dtpFecha.Value))
            //{
            //    if (this.dgvLabores.Rows.Count == 0)
            //    {
            //        this.nudTotal.Value = (decimal)0.00;
            //    }
            //    MDescuentoData laborData = null;
            //    /*AGREGARR CAMBIOS*/
            //    laborData = this.lMovimientosDescuentos.FirstOrDefault(x => x.CodigoEmpleado == this.lblCodigoEmpleado.Text &&
            //                                            x.CodigoDescuento == this.lblCodigoDescuento.Text && x.FechaDescuento == this.dtpFecha.Value.Date);

            //    if (laborData == null)
            //    {
            //        //calcularMonto(labor.TipoLabor[0]);
            //        calcularMonto(this.descuentoData.TipoDescuento[0]);

            //        this.lMovimientosDescuentos.RemoveAt(filaSeleccionada);
            //        this.lMovimientosDescuentos.Insert(filaSeleccionada, new MDescuentoData(lblCodigoEmpleado.Text, lblCodigoDescuento.Text,
            //                                      txtDescripcionDescuento.Text,
            //                                       this.descuentoData.TipoDescuento,
            //                                        this.nudCantidadDescuento.Value, dtpFecha.Value.Date, this.subTotalDescuento,
            //                                        this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                                  ));

            //        this.dgvLabores.Rows.RemoveAt(filaSeleccionada);
            //        this.dgvLabores.Rows.Insert(filaSeleccionada, this.lblCodigoEmpleado.Text, lblCodigoDescuento.Text, this.dtpFecha.Value.Date, descripcionDescuento(this.descuentoData.TipoDescuento[0]),
            //                                   this.txtDescripcionDescuento.Text,
            //                                   this.nudCantidadDescuento.Value.ToString(),
            //                                   this.subTotalDescuento
            //                               );
            //        MessageBox.Show("Descuento Modificado exitosamente", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.nudTotal.Value = Convert.ToDecimal(sumarTotal());
            //        limpiarControlesCuandoModifica();
            //        this.btnBuscarEmpleado.Enabled = true;
            //        this.btnBuscarDescuento.Enabled = true;
            //        this.btnGuardar.Enabled = true;
            //        this.btnNuevo.Enabled = true;
            //        this.bntQuitar.Enabled = true;
            //        this.btnBuscarDescuento.Visible = true;
            //    }
            //}
            //else
            //{
            //    calcularMonto(this.descuentoData.TipoDescuento[0]);

            //    this.lMovimientosDescuentos.RemoveAt(filaSeleccionada);
            //    this.lMovimientosDescuentos.Insert(filaSeleccionada, new MDescuentoData(lblCodigoEmpleado.Text, lblCodigoDescuento.Text,
            //                                  txtDescripcionDescuento.Text,
            //                                   this.descuentoData.TipoDescuento,
            //                                    this.nudCantidadDescuento.Value, dtpFecha.Value.Date, this.subTotalDescuento,
            //                                    this.txtCodigoCuenta.Text, this.txtNombreCuenta.Text
            //                              ));

            //    this.dgvLabores.Rows.RemoveAt(filaSeleccionada);
            //    this.dgvLabores.Rows.Insert(filaSeleccionada, this.lblCodigoEmpleado.Text, lblCodigoDescuento.Text, this.dtpFecha.Value.Date, descripcionDescuento(this.descuentoData.TipoDescuento[0]),
            //                               this.txtDescripcionDescuento.Text,
            //                               this.nudCantidadDescuento.Value.ToString(),
            //                               this.subTotalDescuento
            //                           );
            //    MessageBox.Show("Descuento Modificado exitosamente", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.nudTotal.Value = Convert.ToDecimal(sumarTotal());
            //    limpiarControlesCuandoModifica();
            //    this.btnBuscarEmpleado.Enabled = true;
            //    this.btnBuscarDescuento.Enabled = true;
            //    this.btnGuardar.Enabled = true;
            //    this.btnNuevo.Enabled = true;
            //    this.bntQuitar.Enabled = true;
            //    this.btnBuscarDescuento.Visible = true;

            //}
        }


        #endregion

        private void FrmAddMovimientoDescuento_Load(object sender, EventArgs e)
        {
           // calcularMonto(this.descuentoData.TipoDescuento[0]);
        }

        private void nudMontoDescuento_ValueChanged(object sender, EventArgs e)
        {
            calcularMonto(this.descuentoData.TipoPago.descripcion.Substring(0,1));
        }

        private void nudCantidadDescuento_ValueChanged(object sender, EventArgs e)
        {
            calcularMonto(this.descuentoData.TipoPago.descripcion.Substring(0, 1));
        }

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
