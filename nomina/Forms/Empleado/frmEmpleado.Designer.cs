namespace nomina.Forms.Empleado
{
    partial class frmEmpleado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuentaSueldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuentaSeguroSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuentaRegimenEspecial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuentaISR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtraCuent1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtraCuenta2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Identidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaNacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoCivil = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pasaporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RTN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Antecedentes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A_IHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A_FSV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A_SIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A_ISR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bancos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Departamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuestoAsignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sueldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objDepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoEmpleadoNacionalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.rbNombre = new System.Windows.Forms.RadioButton();
            this.rbCodigo = new System.Windows.Forms.RadioButton();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnVerHistorial = new System.Windows.Forms.Button();
            this.btnAntecedente = new System.Windows.Forms.Button();
            this.pnTipoAntecedente = new System.Windows.Forms.Panel();
            this.rbPenal = new System.Windows.Forms.RadioButton();
            this.rbPolicial = new System.Windows.Forms.RadioButton();
            this.pnTitulo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnTipoAntecedente.SuspendLayout();
            this.pnTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(881, 191);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(112, 49);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Location = new System.Drawing.Point(881, 247);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(112, 49);
            this.btnQuitar.TabIndex = 8;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.bntQuitar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Location = new System.Drawing.Point(881, 134);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(112, 49);
            this.btnNuevo.TabIndex = 7;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.dgvDatos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.CuentaSueldo,
            this.CuentaSeguroSocial,
            this.CuentaRegimenEspecial,
            this.CuentaISR,
            this.OtraCuent1,
            this.OtraCuenta2,
            this.Codigo,
            this.Nombre,
            this.Identidad,
            this.FechaNacimiento,
            this.EstadoCivil,
            this.Pasaporte,
            this.RTN,
            this.Antecedentes,
            this.IHS,
            this.Direccion,
            this.Telefono,
            this.FechaIngreso,
            this.Sexo,
            this.TipoEmpleado,
            this.A_IHS,
            this.A_FSV,
            this.A_SIN,
            this.A_ISR,
            this.TipoPago,
            this.Bancos,
            this.NCuenta,
            this.Departamento,
            this.Categoria,
            this.PuestoAsignado,
            this.Sueldo,
            this.objDepto,
            this.objCategoria,
            this.TipoEmpleadoNacionalidad,
            this.FechaInicio,
            this.NumeroCuenta});
            this.dgvDatos.Location = new System.Drawing.Point(16, 128);
            this.dgvDatos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersWidth = 51;
            this.dgvDatos.Size = new System.Drawing.Size(857, 517);
            this.dgvDatos.TabIndex = 6;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellDoubleClick);
            this.dgvDatos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvDatos_PreviewKeyDown);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Visible = false;
            this.Id.Width = 125;
            // 
            // CuentaSueldo
            // 
            this.CuentaSueldo.DataPropertyName = "CuentaSueldo";
            this.CuentaSueldo.HeaderText = "Cuenta Sueldo";
            this.CuentaSueldo.MinimumWidth = 6;
            this.CuentaSueldo.Name = "CuentaSueldo";
            this.CuentaSueldo.Visible = false;
            this.CuentaSueldo.Width = 125;
            // 
            // CuentaSeguroSocial
            // 
            this.CuentaSeguroSocial.DataPropertyName = "CuentaSeguroSocial";
            this.CuentaSeguroSocial.HeaderText = "Cuenta Seguro Social";
            this.CuentaSeguroSocial.MinimumWidth = 6;
            this.CuentaSeguroSocial.Name = "CuentaSeguroSocial";
            this.CuentaSeguroSocial.Visible = false;
            this.CuentaSeguroSocial.Width = 125;
            // 
            // CuentaRegimenEspecial
            // 
            this.CuentaRegimenEspecial.DataPropertyName = "CuentaRegimenEspecial";
            this.CuentaRegimenEspecial.HeaderText = "Cuenta Regimen Especial";
            this.CuentaRegimenEspecial.MinimumWidth = 6;
            this.CuentaRegimenEspecial.Name = "CuentaRegimenEspecial";
            this.CuentaRegimenEspecial.Visible = false;
            this.CuentaRegimenEspecial.Width = 125;
            // 
            // CuentaISR
            // 
            this.CuentaISR.DataPropertyName = "CuentaISR";
            this.CuentaISR.HeaderText = "Cuenta ISR";
            this.CuentaISR.MinimumWidth = 6;
            this.CuentaISR.Name = "CuentaISR";
            this.CuentaISR.Visible = false;
            this.CuentaISR.Width = 125;
            // 
            // OtraCuent1
            // 
            this.OtraCuent1.DataPropertyName = "OtraCuent1";
            this.OtraCuent1.HeaderText = "Otra cuenta 1";
            this.OtraCuent1.MinimumWidth = 6;
            this.OtraCuent1.Name = "OtraCuent1";
            this.OtraCuent1.Visible = false;
            this.OtraCuent1.Width = 125;
            // 
            // OtraCuenta2
            // 
            this.OtraCuenta2.DataPropertyName = "OtraCuenta2";
            this.OtraCuenta2.HeaderText = "Otra Cuenta 2";
            this.OtraCuenta2.MinimumWidth = 6;
            this.OtraCuenta2.Name = "OtraCuenta2";
            this.OtraCuenta2.Visible = false;
            this.OtraCuenta2.Width = 125;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.MinimumWidth = 6;
            this.Codigo.Name = "Codigo";
            this.Codigo.Width = 125;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.Width = 125;
            // 
            // Identidad
            // 
            this.Identidad.DataPropertyName = "Identidad";
            this.Identidad.HeaderText = "Identidad";
            this.Identidad.MinimumWidth = 6;
            this.Identidad.Name = "Identidad";
            this.Identidad.Visible = false;
            this.Identidad.Width = 125;
            // 
            // FechaNacimiento
            // 
            this.FechaNacimiento.DataPropertyName = "FechaNacimiento";
            this.FechaNacimiento.HeaderText = "Fecha Nacimiento";
            this.FechaNacimiento.MinimumWidth = 6;
            this.FechaNacimiento.Name = "FechaNacimiento";
            this.FechaNacimiento.Width = 125;
            // 
            // EstadoCivil
            // 
            this.EstadoCivil.DataPropertyName = "EstadoCivil";
            this.EstadoCivil.HeaderText = "Estado Civil";
            this.EstadoCivil.MinimumWidth = 6;
            this.EstadoCivil.Name = "EstadoCivil";
            this.EstadoCivil.Visible = false;
            this.EstadoCivil.Width = 125;
            // 
            // Pasaporte
            // 
            this.Pasaporte.DataPropertyName = "Pasaporte";
            this.Pasaporte.HeaderText = "Pasaporte";
            this.Pasaporte.MinimumWidth = 6;
            this.Pasaporte.Name = "Pasaporte";
            this.Pasaporte.Visible = false;
            this.Pasaporte.Width = 125;
            // 
            // RTN
            // 
            this.RTN.DataPropertyName = "RTN";
            this.RTN.HeaderText = "RTN";
            this.RTN.MinimumWidth = 6;
            this.RTN.Name = "RTN";
            this.RTN.Visible = false;
            this.RTN.Width = 125;
            // 
            // Antecedentes
            // 
            this.Antecedentes.DataPropertyName = "Antecedentes";
            this.Antecedentes.HeaderText = "Antecedentes";
            this.Antecedentes.MinimumWidth = 6;
            this.Antecedentes.Name = "Antecedentes";
            this.Antecedentes.Visible = false;
            this.Antecedentes.Width = 125;
            // 
            // IHS
            // 
            this.IHS.DataPropertyName = "IHS";
            this.IHS.HeaderText = "IHS";
            this.IHS.MinimumWidth = 6;
            this.IHS.Name = "IHS";
            this.IHS.Visible = false;
            this.IHS.Width = 125;
            // 
            // Direccion
            // 
            this.Direccion.DataPropertyName = "Direccion";
            this.Direccion.HeaderText = "Dirección";
            this.Direccion.MinimumWidth = 6;
            this.Direccion.Name = "Direccion";
            this.Direccion.Visible = false;
            this.Direccion.Width = 125;
            // 
            // Telefono
            // 
            this.Telefono.DataPropertyName = "Telefono";
            this.Telefono.HeaderText = "Telefono";
            this.Telefono.MinimumWidth = 6;
            this.Telefono.Name = "Telefono";
            this.Telefono.Visible = false;
            this.Telefono.Width = 125;
            // 
            // FechaIngreso
            // 
            this.FechaIngreso.DataPropertyName = "FechaIngreso";
            this.FechaIngreso.HeaderText = "Fecha de Ingreso";
            this.FechaIngreso.MinimumWidth = 6;
            this.FechaIngreso.Name = "FechaIngreso";
            this.FechaIngreso.Visible = false;
            this.FechaIngreso.Width = 125;
            // 
            // Sexo
            // 
            this.Sexo.DataPropertyName = "Sexo";
            this.Sexo.HeaderText = "Sexo";
            this.Sexo.MinimumWidth = 6;
            this.Sexo.Name = "Sexo";
            this.Sexo.Visible = false;
            this.Sexo.Width = 125;
            // 
            // TipoEmpleado
            // 
            this.TipoEmpleado.DataPropertyName = "TipoEmpleado";
            this.TipoEmpleado.HeaderText = "Tipo Empleado";
            this.TipoEmpleado.MinimumWidth = 6;
            this.TipoEmpleado.Name = "TipoEmpleado";
            this.TipoEmpleado.Visible = false;
            this.TipoEmpleado.Width = 125;
            // 
            // A_IHS
            // 
            this.A_IHS.DataPropertyName = "A_IHS";
            this.A_IHS.HeaderText = "A_IHS";
            this.A_IHS.MinimumWidth = 6;
            this.A_IHS.Name = "A_IHS";
            this.A_IHS.Visible = false;
            this.A_IHS.Width = 125;
            // 
            // A_FSV
            // 
            this.A_FSV.DataPropertyName = "A_FSV";
            this.A_FSV.HeaderText = "A_FSV";
            this.A_FSV.MinimumWidth = 6;
            this.A_FSV.Name = "A_FSV";
            this.A_FSV.Visible = false;
            this.A_FSV.Width = 125;
            // 
            // A_SIN
            // 
            this.A_SIN.DataPropertyName = "A_SIN";
            this.A_SIN.HeaderText = "A_SIN";
            this.A_SIN.MinimumWidth = 6;
            this.A_SIN.Name = "A_SIN";
            this.A_SIN.Visible = false;
            this.A_SIN.Width = 125;
            // 
            // A_ISR
            // 
            this.A_ISR.DataPropertyName = "A_ISR";
            this.A_ISR.HeaderText = "A_ISR";
            this.A_ISR.MinimumWidth = 6;
            this.A_ISR.Name = "A_ISR";
            this.A_ISR.Visible = false;
            this.A_ISR.Width = 125;
            // 
            // TipoPago
            // 
            this.TipoPago.DataPropertyName = "TipoPago";
            this.TipoPago.HeaderText = "Tipo de Pago";
            this.TipoPago.MinimumWidth = 6;
            this.TipoPago.Name = "TipoPago";
            this.TipoPago.Visible = false;
            this.TipoPago.Width = 125;
            // 
            // Bancos
            // 
            this.Bancos.DataPropertyName = "Bancos";
            this.Bancos.HeaderText = "Bancos";
            this.Bancos.MinimumWidth = 6;
            this.Bancos.Name = "Bancos";
            this.Bancos.Visible = false;
            this.Bancos.Width = 125;
            // 
            // NCuenta
            // 
            this.NCuenta.DataPropertyName = "Ncuenta";
            this.NCuenta.HeaderText = "NCuenta";
            this.NCuenta.MinimumWidth = 6;
            this.NCuenta.Name = "NCuenta";
            this.NCuenta.Visible = false;
            this.NCuenta.Width = 125;
            // 
            // Departamento
            // 
            this.Departamento.DataPropertyName = "nombreDepto";
            this.Departamento.HeaderText = "Departamento";
            this.Departamento.MinimumWidth = 6;
            this.Departamento.Name = "Departamento";
            this.Departamento.Width = 125;
            // 
            // Categoria
            // 
            this.Categoria.DataPropertyName = "nombreCategoria";
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.MinimumWidth = 6;
            this.Categoria.Name = "Categoria";
            this.Categoria.Width = 125;
            // 
            // PuestoAsignado
            // 
            this.PuestoAsignado.DataPropertyName = "PuestoAsignado";
            this.PuestoAsignado.HeaderText = "Puesto Asignado";
            this.PuestoAsignado.MinimumWidth = 6;
            this.PuestoAsignado.Name = "PuestoAsignado";
            this.PuestoAsignado.Visible = false;
            this.PuestoAsignado.Width = 125;
            // 
            // Sueldo
            // 
            this.Sueldo.DataPropertyName = "Sueldo";
            this.Sueldo.HeaderText = "Sueldo";
            this.Sueldo.MinimumWidth = 6;
            this.Sueldo.Name = "Sueldo";
            this.Sueldo.Width = 125;
            // 
            // objDepto
            // 
            this.objDepto.DataPropertyName = "objDepto";
            this.objDepto.HeaderText = "depto";
            this.objDepto.MinimumWidth = 6;
            this.objDepto.Name = "objDepto";
            this.objDepto.Visible = false;
            this.objDepto.Width = 125;
            // 
            // objCategoria
            // 
            this.objCategoria.DataPropertyName = "objCategoria";
            this.objCategoria.HeaderText = "objCate";
            this.objCategoria.MinimumWidth = 6;
            this.objCategoria.Name = "objCategoria";
            this.objCategoria.Visible = false;
            this.objCategoria.Width = 125;
            // 
            // TipoEmpleadoNacionalidad
            // 
            this.TipoEmpleadoNacionalidad.DataPropertyName = "TipoEmpleadoNacionalidad";
            this.TipoEmpleadoNacionalidad.HeaderText = "Tipo Empleado Nacionalidad";
            this.TipoEmpleadoNacionalidad.MinimumWidth = 6;
            this.TipoEmpleadoNacionalidad.Name = "TipoEmpleadoNacionalidad";
            this.TipoEmpleadoNacionalidad.Visible = false;
            this.TipoEmpleadoNacionalidad.Width = 125;
            // 
            // FechaInicio
            // 
            this.FechaInicio.DataPropertyName = "FechaInicio";
            this.FechaInicio.HeaderText = "Fecha Inicio";
            this.FechaInicio.MinimumWidth = 6;
            this.FechaInicio.Name = "FechaInicio";
            this.FechaInicio.Visible = false;
            this.FechaInicio.Width = 125;
            // 
            // NumeroCuenta
            // 
            this.NumeroCuenta.DataPropertyName = "NumeroCuenta";
            this.NumeroCuenta.HeaderText = "Numero cuenta";
            this.NumeroCuenta.MinimumWidth = 6;
            this.NumeroCuenta.Name = "NumeroCuenta";
            this.NumeroCuenta.Visible = false;
            this.NumeroCuenta.Width = 125;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 38);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(305, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empleados";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.rbNombre);
            this.groupBox1.Controls.Add(this.rbCodigo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(857, 63);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(560, 21);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(252, 24);
            this.txtNombre.TabIndex = 34;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(95, 23);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.MaxLength = 5;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(132, 24);
            this.txtCodigo.TabIndex = 33;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            // 
            // rbNombre
            // 
            this.rbNombre.AutoSize = true;
            this.rbNombre.Location = new System.Drawing.Point(455, 25);
            this.rbNombre.Margin = new System.Windows.Forms.Padding(4);
            this.rbNombre.Name = "rbNombre";
            this.rbNombre.Size = new System.Drawing.Size(87, 22);
            this.rbNombre.TabIndex = 32;
            this.rbNombre.TabStop = true;
            this.rbNombre.Text = "Nombre:";
            this.rbNombre.UseVisualStyleBackColor = true;
            this.rbNombre.Click += new System.EventHandler(this.rbNombre_Click);
            // 
            // rbCodigo
            // 
            this.rbCodigo.AutoSize = true;
            this.rbCodigo.Location = new System.Drawing.Point(8, 26);
            this.rbCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.rbCodigo.Name = "rbCodigo";
            this.rbCodigo.Size = new System.Drawing.Size(81, 22);
            this.rbCodigo.TabIndex = 31;
            this.rbCodigo.TabStop = true;
            this.rbCodigo.Text = "Código:";
            this.rbCodigo.UseVisualStyleBackColor = true;
            this.rbCodigo.Click += new System.EventHandler(this.rbCodigo_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Location = new System.Drawing.Point(881, 363);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(112, 49);
            this.btnSalir.TabIndex = 31;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // btnVerHistorial
            // 
            this.btnVerHistorial.BackColor = System.Drawing.Color.SkyBlue;
            this.btnVerHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerHistorial.Location = new System.Drawing.Point(881, 420);
            this.btnVerHistorial.Margin = new System.Windows.Forms.Padding(4);
            this.btnVerHistorial.Name = "btnVerHistorial";
            this.btnVerHistorial.Size = new System.Drawing.Size(112, 49);
            this.btnVerHistorial.TabIndex = 32;
            this.btnVerHistorial.Text = "Ver Historial Aumento";
            this.btnVerHistorial.UseVisualStyleBackColor = false;
            this.btnVerHistorial.Click += new System.EventHandler(this.BtnVerHistorial_Click);
            // 
            // btnAntecedente
            // 
            this.btnAntecedente.BackColor = System.Drawing.Color.SkyBlue;
            this.btnAntecedente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAntecedente.Location = new System.Drawing.Point(881, 306);
            this.btnAntecedente.Margin = new System.Windows.Forms.Padding(4);
            this.btnAntecedente.Name = "btnAntecedente";
            this.btnAntecedente.Size = new System.Drawing.Size(112, 49);
            this.btnAntecedente.TabIndex = 33;
            this.btnAntecedente.Text = "Antecedentes";
            this.btnAntecedente.UseVisualStyleBackColor = false;
            this.btnAntecedente.Click += new System.EventHandler(this.btnAntecedente_Click);
            // 
            // pnTipoAntecedente
            // 
            this.pnTipoAntecedente.Controls.Add(this.rbPenal);
            this.pnTipoAntecedente.Controls.Add(this.rbPolicial);
            this.pnTipoAntecedente.Location = new System.Drawing.Point(239, 247);
            this.pnTipoAntecedente.Margin = new System.Windows.Forms.Padding(4);
            this.pnTipoAntecedente.Name = "pnTipoAntecedente";
            this.pnTipoAntecedente.Size = new System.Drawing.Size(201, 106);
            this.pnTipoAntecedente.TabIndex = 34;
            // 
            // rbPenal
            // 
            this.rbPenal.AutoSize = true;
            this.rbPenal.Location = new System.Drawing.Point(19, 74);
            this.rbPenal.Margin = new System.Windows.Forms.Padding(4);
            this.rbPenal.Name = "rbPenal";
            this.rbPenal.Size = new System.Drawing.Size(63, 20);
            this.rbPenal.TabIndex = 1;
            this.rbPenal.TabStop = true;
            this.rbPenal.Text = "Penal";
            this.rbPenal.UseVisualStyleBackColor = true;
            this.rbPenal.Click += new System.EventHandler(this.rbPenal_Click);
            // 
            // rbPolicial
            // 
            this.rbPolicial.AutoSize = true;
            this.rbPolicial.Location = new System.Drawing.Point(19, 46);
            this.rbPolicial.Margin = new System.Windows.Forms.Padding(4);
            this.rbPolicial.Name = "rbPolicial";
            this.rbPolicial.Size = new System.Drawing.Size(72, 20);
            this.rbPolicial.TabIndex = 0;
            this.rbPolicial.TabStop = true;
            this.rbPolicial.Text = "Policial";
            this.rbPolicial.UseVisualStyleBackColor = true;
            this.rbPolicial.Click += new System.EventHandler(this.rbPolicial_Click);
            // 
            // pnTitulo
            // 
            this.pnTitulo.BackColor = System.Drawing.Color.SkyBlue;
            this.pnTitulo.Controls.Add(this.label2);
            this.pnTitulo.Location = new System.Drawing.Point(239, 247);
            this.pnTitulo.Margin = new System.Windows.Forms.Padding(4);
            this.pnTitulo.Name = "pnTitulo";
            this.pnTitulo.Size = new System.Drawing.Size(201, 38);
            this.pnTitulo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tipo Antecedente";
            // 
            // frmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1014, 660);
            this.Controls.Add(this.pnTitulo);
            this.Controls.Add(this.pnTipoAntecedente);
            this.Controls.Add(this.btnAntecedente);
            this.Controls.Add(this.btnVerHistorial);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmEmpleado";
            this.Text = "frmEmpleado";
            this.Load += new System.EventHandler(this.frmEmpleado_Load);
            this.Shown += new System.EventHandler(this.frmEmpleado_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnTipoAntecedente.ResumeLayout(false);
            this.pnTipoAntecedente.PerformLayout();
            this.pnTitulo.ResumeLayout(false);
            this.pnTitulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.RadioButton rbNombre;
        private System.Windows.Forms.RadioButton rbCodigo;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnVerHistorial;
        private System.Windows.Forms.Button btnAntecedente;
        private System.Windows.Forms.Panel pnTipoAntecedente;
        private System.Windows.Forms.RadioButton rbPenal;
        private System.Windows.Forms.RadioButton rbPolicial;
        private System.Windows.Forms.Panel pnTitulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuentaSueldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuentaSeguroSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuentaRegimenEspecial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuentaISR;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtraCuent1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtraCuenta2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Identidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaNacimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoCivil;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pasaporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn RTN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Antecedentes;
        private System.Windows.Forms.DataGridViewTextBoxColumn IHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaIngreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sexo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn A_IHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn A_FSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn A_SIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn A_ISR;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bancos;
        private System.Windows.Forms.DataGridViewTextBoxColumn NCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Departamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuestoAsignado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sueldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn objDepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn objCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoEmpleadoNacionalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroCuenta;
    }
}