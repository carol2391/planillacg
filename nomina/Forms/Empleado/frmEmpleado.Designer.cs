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
            this.button1 = new System.Windows.Forms.Button();
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
            this.btnModificar.Location = new System.Drawing.Point(661, 155);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(84, 40);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Location = new System.Drawing.Point(661, 201);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(84, 40);
            this.btnQuitar.TabIndex = 8;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.bntQuitar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Location = new System.Drawing.Point(661, 109);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(84, 40);
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
            this.dgvDatos.Location = new System.Drawing.Point(12, 104);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(643, 420);
            this.dgvDatos.TabIndex = 6;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellDoubleClick);
            this.dgvDatos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvDatos_PreviewKeyDown);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // CuentaSueldo
            // 
            this.CuentaSueldo.DataPropertyName = "CuentaSueldo";
            this.CuentaSueldo.HeaderText = "Cuenta Sueldo";
            this.CuentaSueldo.Name = "CuentaSueldo";
            this.CuentaSueldo.Visible = false;
            // 
            // CuentaSeguroSocial
            // 
            this.CuentaSeguroSocial.DataPropertyName = "CuentaSeguroSocial";
            this.CuentaSeguroSocial.HeaderText = "Cuenta Seguro Social";
            this.CuentaSeguroSocial.Name = "CuentaSeguroSocial";
            this.CuentaSeguroSocial.Visible = false;
            // 
            // CuentaRegimenEspecial
            // 
            this.CuentaRegimenEspecial.DataPropertyName = "CuentaRegimenEspecial";
            this.CuentaRegimenEspecial.HeaderText = "Cuenta Regimen Especial";
            this.CuentaRegimenEspecial.Name = "CuentaRegimenEspecial";
            this.CuentaRegimenEspecial.Visible = false;
            // 
            // CuentaISR
            // 
            this.CuentaISR.DataPropertyName = "CuentaISR";
            this.CuentaISR.HeaderText = "Cuenta ISR";
            this.CuentaISR.Name = "CuentaISR";
            this.CuentaISR.Visible = false;
            // 
            // OtraCuent1
            // 
            this.OtraCuent1.DataPropertyName = "OtraCuent1";
            this.OtraCuent1.HeaderText = "Otra cuenta 1";
            this.OtraCuent1.Name = "OtraCuent1";
            this.OtraCuent1.Visible = false;
            // 
            // OtraCuenta2
            // 
            this.OtraCuenta2.DataPropertyName = "OtraCuenta2";
            this.OtraCuenta2.HeaderText = "Otra Cuenta 2";
            this.OtraCuenta2.Name = "OtraCuenta2";
            this.OtraCuenta2.Visible = false;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            // 
            // Identidad
            // 
            this.Identidad.DataPropertyName = "Identidad";
            this.Identidad.HeaderText = "Identidad";
            this.Identidad.Name = "Identidad";
            this.Identidad.Visible = false;
            // 
            // FechaNacimiento
            // 
            this.FechaNacimiento.DataPropertyName = "FechaNacimiento";
            this.FechaNacimiento.HeaderText = "Fecha Nacimiento";
            this.FechaNacimiento.Name = "FechaNacimiento";
            // 
            // EstadoCivil
            // 
            this.EstadoCivil.DataPropertyName = "EstadoCivil";
            this.EstadoCivil.HeaderText = "Estado Civil";
            this.EstadoCivil.Name = "EstadoCivil";
            this.EstadoCivil.Visible = false;
            // 
            // Pasaporte
            // 
            this.Pasaporte.DataPropertyName = "Pasaporte";
            this.Pasaporte.HeaderText = "Pasaporte";
            this.Pasaporte.Name = "Pasaporte";
            this.Pasaporte.Visible = false;
            // 
            // RTN
            // 
            this.RTN.DataPropertyName = "RTN";
            this.RTN.HeaderText = "RTN";
            this.RTN.Name = "RTN";
            this.RTN.Visible = false;
            // 
            // Antecedentes
            // 
            this.Antecedentes.DataPropertyName = "Antecedentes";
            this.Antecedentes.HeaderText = "Antecedentes";
            this.Antecedentes.Name = "Antecedentes";
            this.Antecedentes.Visible = false;
            // 
            // IHS
            // 
            this.IHS.DataPropertyName = "IHS";
            this.IHS.HeaderText = "IHS";
            this.IHS.Name = "IHS";
            this.IHS.Visible = false;
            // 
            // Direccion
            // 
            this.Direccion.DataPropertyName = "Direccion";
            this.Direccion.HeaderText = "Dirección";
            this.Direccion.Name = "Direccion";
            this.Direccion.Visible = false;
            // 
            // Telefono
            // 
            this.Telefono.DataPropertyName = "Telefono";
            this.Telefono.HeaderText = "Telefono";
            this.Telefono.Name = "Telefono";
            this.Telefono.Visible = false;
            // 
            // FechaIngreso
            // 
            this.FechaIngreso.DataPropertyName = "FechaIngreso";
            this.FechaIngreso.HeaderText = "Fecha de Ingreso";
            this.FechaIngreso.Name = "FechaIngreso";
            this.FechaIngreso.Visible = false;
            // 
            // Sexo
            // 
            this.Sexo.DataPropertyName = "Sexo";
            this.Sexo.HeaderText = "Sexo";
            this.Sexo.Name = "Sexo";
            this.Sexo.Visible = false;
            // 
            // TipoEmpleado
            // 
            this.TipoEmpleado.DataPropertyName = "TipoEmpleado";
            this.TipoEmpleado.HeaderText = "Tipo Empleado";
            this.TipoEmpleado.Name = "TipoEmpleado";
            this.TipoEmpleado.Visible = false;
            // 
            // A_IHS
            // 
            this.A_IHS.DataPropertyName = "A_IHS";
            this.A_IHS.HeaderText = "A_IHS";
            this.A_IHS.Name = "A_IHS";
            this.A_IHS.Visible = false;
            // 
            // A_FSV
            // 
            this.A_FSV.DataPropertyName = "A_FSV";
            this.A_FSV.HeaderText = "A_FSV";
            this.A_FSV.Name = "A_FSV";
            this.A_FSV.Visible = false;
            // 
            // A_SIN
            // 
            this.A_SIN.DataPropertyName = "A_SIN";
            this.A_SIN.HeaderText = "A_SIN";
            this.A_SIN.Name = "A_SIN";
            this.A_SIN.Visible = false;
            // 
            // A_ISR
            // 
            this.A_ISR.DataPropertyName = "A_ISR";
            this.A_ISR.HeaderText = "A_ISR";
            this.A_ISR.Name = "A_ISR";
            this.A_ISR.Visible = false;
            // 
            // TipoPago
            // 
            this.TipoPago.DataPropertyName = "TipoPago";
            this.TipoPago.HeaderText = "Tipo de Pago";
            this.TipoPago.Name = "TipoPago";
            this.TipoPago.Visible = false;
            // 
            // Bancos
            // 
            this.Bancos.DataPropertyName = "Bancos";
            this.Bancos.HeaderText = "Bancos";
            this.Bancos.Name = "Bancos";
            this.Bancos.Visible = false;
            // 
            // NCuenta
            // 
            this.NCuenta.DataPropertyName = "Ncuenta";
            this.NCuenta.HeaderText = "NCuenta";
            this.NCuenta.Name = "NCuenta";
            this.NCuenta.Visible = false;
            // 
            // Departamento
            // 
            this.Departamento.DataPropertyName = "nombreDepto";
            this.Departamento.HeaderText = "Departamento";
            this.Departamento.Name = "Departamento";
            // 
            // Categoria
            // 
            this.Categoria.DataPropertyName = "nombreCategoria";
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.Name = "Categoria";
            // 
            // PuestoAsignado
            // 
            this.PuestoAsignado.DataPropertyName = "PuestoAsignado";
            this.PuestoAsignado.HeaderText = "Puesto Asignado";
            this.PuestoAsignado.Name = "PuestoAsignado";
            this.PuestoAsignado.Visible = false;
            // 
            // Sueldo
            // 
            this.Sueldo.DataPropertyName = "Sueldo";
            this.Sueldo.HeaderText = "Sueldo";
            this.Sueldo.Name = "Sueldo";
            // 
            // objDepto
            // 
            this.objDepto.DataPropertyName = "objDepto";
            this.objDepto.HeaderText = "depto";
            this.objDepto.Name = "objDepto";
            this.objDepto.Visible = false;
            // 
            // objCategoria
            // 
            this.objCategoria.DataPropertyName = "objCategoria";
            this.objCategoria.HeaderText = "objCate";
            this.objCategoria.Name = "objCategoria";
            this.objCategoria.Visible = false;
            // 
            // TipoEmpleadoNacionalidad
            // 
            this.TipoEmpleadoNacionalidad.DataPropertyName = "TipoEmpleadoNacionalidad";
            this.TipoEmpleadoNacionalidad.HeaderText = "Tipo Empleado Nacionalidad";
            this.TipoEmpleadoNacionalidad.Name = "TipoEmpleadoNacionalidad";
            this.TipoEmpleadoNacionalidad.Visible = false;
            // 
            // FechaInicio
            // 
            this.FechaInicio.DataPropertyName = "FechaInicio";
            this.FechaInicio.HeaderText = "Fecha Inicio";
            this.FechaInicio.Name = "FechaInicio";
            this.FechaInicio.Visible = false;
            // 
            // NumeroCuenta
            // 
            this.NumeroCuenta.DataPropertyName = "NumeroCuenta";
            this.NumeroCuenta.HeaderText = "Numero cuenta";
            this.NumeroCuenta.Name = "NumeroCuenta";
            this.NumeroCuenta.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 31);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(229, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(643, 51);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(420, 17);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(190, 21);
            this.txtNombre.TabIndex = 34;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(71, 19);
            this.txtCodigo.MaxLength = 5;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 21);
            this.txtCodigo.TabIndex = 33;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            // 
            // rbNombre
            // 
            this.rbNombre.AutoSize = true;
            this.rbNombre.Location = new System.Drawing.Point(341, 20);
            this.rbNombre.Name = "rbNombre";
            this.rbNombre.Size = new System.Drawing.Size(73, 19);
            this.rbNombre.TabIndex = 32;
            this.rbNombre.TabStop = true;
            this.rbNombre.Text = "Nombre:";
            this.rbNombre.UseVisualStyleBackColor = true;
            this.rbNombre.Click += new System.EventHandler(this.rbNombre_Click);
            // 
            // rbCodigo
            // 
            this.rbCodigo.AutoSize = true;
            this.rbCodigo.Location = new System.Drawing.Point(6, 21);
            this.rbCodigo.Name = "rbCodigo";
            this.rbCodigo.Size = new System.Drawing.Size(67, 19);
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
            this.btnSalir.Location = new System.Drawing.Point(661, 295);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(84, 40);
            this.btnSalir.TabIndex = 31;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // btnVerHistorial
            // 
            this.btnVerHistorial.BackColor = System.Drawing.Color.SkyBlue;
            this.btnVerHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerHistorial.Location = new System.Drawing.Point(661, 341);
            this.btnVerHistorial.Name = "btnVerHistorial";
            this.btnVerHistorial.Size = new System.Drawing.Size(84, 40);
            this.btnVerHistorial.TabIndex = 32;
            this.btnVerHistorial.Text = "Ver Historial Aumento";
            this.btnVerHistorial.UseVisualStyleBackColor = false;
            this.btnVerHistorial.Visible = false;
            this.btnVerHistorial.Click += new System.EventHandler(this.BtnVerHistorial_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(661, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 40);
            this.button1.TabIndex = 33;
            this.button1.Text = "Antecedentes";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnAntecedente_Click);
            // 
            // pnTipoAntecedente
            // 
            this.pnTipoAntecedente.Controls.Add(this.rbPenal);
            this.pnTipoAntecedente.Controls.Add(this.rbPolicial);
            this.pnTipoAntecedente.Location = new System.Drawing.Point(179, 201);
            this.pnTipoAntecedente.Name = "pnTipoAntecedente";
            this.pnTipoAntecedente.Size = new System.Drawing.Size(151, 86);
            this.pnTipoAntecedente.TabIndex = 34;
            // 
            // rbPenal
            // 
            this.rbPenal.AutoSize = true;
            this.rbPenal.Location = new System.Drawing.Point(14, 60);
            this.rbPenal.Name = "rbPenal";
            this.rbPenal.Size = new System.Drawing.Size(52, 17);
            this.rbPenal.TabIndex = 1;
            this.rbPenal.TabStop = true;
            this.rbPenal.Text = "Penal";
            this.rbPenal.UseVisualStyleBackColor = true;
            this.rbPenal.Click += new System.EventHandler(this.rbPenal_Click);
            // 
            // rbPolicial
            // 
            this.rbPolicial.AutoSize = true;
            this.rbPolicial.Location = new System.Drawing.Point(14, 37);
            this.rbPolicial.Name = "rbPolicial";
            this.rbPolicial.Size = new System.Drawing.Size(58, 17);
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
            this.pnTitulo.Location = new System.Drawing.Point(179, 201);
            this.pnTitulo.Name = "pnTitulo";
            this.pnTitulo.Size = new System.Drawing.Size(151, 31);
            this.pnTitulo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tipo Antecedente";
            // 
            // frmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(753, 538);
            this.Controls.Add(this.pnTitulo);
            this.Controls.Add(this.pnTipoAntecedente);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnVerHistorial);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "frmEmpleado";
            this.Text = "frmEmpleado";
            this.Load += new System.EventHandler(this.frmEmpleado_Load);
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
        private System.Windows.Forms.Button button1;
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