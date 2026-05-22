namespace nomina.Forms.MovimientoDescuentos
{
    partial class frmMovimientoDescuento
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
            this.pnTotal = new System.Windows.Forms.GroupBox();
            this.nudTotal = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvDescuentos = new System.Windows.Forms.DataGridView();
            this.CodigoEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipodePago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadLabor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idMdescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaDescuento1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objTipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPagoD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFechaFinal = new System.Windows.Forms.CheckBox();
            this.cbFechaInicial = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescuentos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTotal
            // 
            this.pnTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.pnTotal.Controls.Add(this.nudTotal);
            this.pnTotal.Controls.Add(this.label5);
            this.pnTotal.Controls.Add(this.label7);
            this.pnTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnTotal.Location = new System.Drawing.Point(-1, 559);
            this.pnTotal.Margin = new System.Windows.Forms.Padding(4);
            this.pnTotal.Name = "pnTotal";
            this.pnTotal.Padding = new System.Windows.Forms.Padding(4);
            this.pnTotal.Size = new System.Drawing.Size(1059, 57);
            this.pnTotal.TabIndex = 58;
            this.pnTotal.TabStop = false;
            // 
            // nudTotal
            // 
            this.nudTotal.DecimalPlaces = 2;
            this.nudTotal.Enabled = false;
            this.nudTotal.Location = new System.Drawing.Point(809, 18);
            this.nudTotal.Margin = new System.Windows.Forms.Padding(4);
            this.nudTotal.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.nudTotal.Name = "nudTotal";
            this.nudTotal.Size = new System.Drawing.Size(231, 24);
            this.nudTotal.TabIndex = 50;
            this.nudTotal.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(764, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 18);
            this.label5.TabIndex = 49;
            this.label5.Text = "Total:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 91);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 18);
            this.label7.TabIndex = 47;
            // 
            // dgvDescuentos
            // 
            this.dgvDescuentos.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.dgvDescuentos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDescuentos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDescuentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDescuentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoEmpleado,
            this.NombreEmpleado,
            this.CodigoDescuento,
            this.TipodePago,
            this.Descripcion,
            this.CantidadLabor,
            this.Monto,
            this.MontoTotal,
            this.NombreCuenta,
            this.CodigoCuenta,
            this.idDescuento,
            this.idMdescuento,
            this.FechaDescuento1,
            this.IdCuenta,
            this.objTipoPago,
            this.idEmpleado,
            this.objDescuento,
            this.TipoPagoD,
            this.objEmpleado});
            this.dgvDescuentos.Location = new System.Drawing.Point(-1, 145);
            this.dgvDescuentos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDescuentos.Name = "dgvDescuentos";
            this.dgvDescuentos.RowHeadersWidth = 51;
            this.dgvDescuentos.Size = new System.Drawing.Size(1059, 406);
            this.dgvDescuentos.TabIndex = 57;
            // 
            // CodigoEmpleado
            // 
            this.CodigoEmpleado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CodigoEmpleado.DataPropertyName = "CodigoEmpleado";
            this.CodigoEmpleado.HeaderText = "Código Empleado";
            this.CodigoEmpleado.MinimumWidth = 6;
            this.CodigoEmpleado.Name = "CodigoEmpleado";
            this.CodigoEmpleado.Width = 134;
            // 
            // NombreEmpleado
            // 
            this.NombreEmpleado.DataPropertyName = "NombreEmpleado";
            this.NombreEmpleado.HeaderText = "Nombre Empleado";
            this.NombreEmpleado.MinimumWidth = 6;
            this.NombreEmpleado.Name = "NombreEmpleado";
            this.NombreEmpleado.Width = 125;
            // 
            // CodigoDescuento
            // 
            this.CodigoDescuento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CodigoDescuento.DataPropertyName = "CodigoDescuento";
            this.CodigoDescuento.HeaderText = "Código Descuento";
            this.CodigoDescuento.MinimumWidth = 6;
            this.CodigoDescuento.Name = "CodigoDescuento";
            this.CodigoDescuento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CodigoDescuento.Visible = false;
            this.CodigoDescuento.Width = 125;
            // 
            // TipodePago
            // 
            this.TipodePago.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TipodePago.DataPropertyName = "TipoPagoD";
            this.TipodePago.HeaderText = "Tipo de Descuento";
            this.TipodePago.MinimumWidth = 6;
            this.TipodePago.Name = "TipodePago";
            this.TipodePago.Width = 138;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "DescripcionDescuento";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.MinimumWidth = 6;
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.Width = 195;
            // 
            // CantidadLabor
            // 
            this.CantidadLabor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CantidadLabor.DataPropertyName = "CantidadDescuento";
            this.CantidadLabor.HeaderText = "Cantidad";
            this.CantidadLabor.MinimumWidth = 6;
            this.CantidadLabor.Name = "CantidadLabor";
            this.CantidadLabor.Width = 90;
            // 
            // Monto
            // 
            this.Monto.DataPropertyName = "MontoDescuento";
            this.Monto.HeaderText = "Monto";
            this.Monto.MinimumWidth = 6;
            this.Monto.Name = "Monto";
            this.Monto.Width = 150;
            // 
            // MontoTotal
            // 
            this.MontoTotal.DataPropertyName = "Total";
            this.MontoTotal.HeaderText = "Monto Total";
            this.MontoTotal.MinimumWidth = 6;
            this.MontoTotal.Name = "MontoTotal";
            this.MontoTotal.Width = 125;
            // 
            // NombreCuenta
            // 
            this.NombreCuenta.DataPropertyName = "NombreCuenta";
            this.NombreCuenta.HeaderText = "Nombre Cuenta";
            this.NombreCuenta.MinimumWidth = 6;
            this.NombreCuenta.Name = "NombreCuenta";
            this.NombreCuenta.Visible = false;
            this.NombreCuenta.Width = 125;
            // 
            // CodigoCuenta
            // 
            this.CodigoCuenta.DataPropertyName = "CodigoCuenta";
            this.CodigoCuenta.HeaderText = "Código Cuenta";
            this.CodigoCuenta.MinimumWidth = 6;
            this.CodigoCuenta.Name = "CodigoCuenta";
            this.CodigoCuenta.Visible = false;
            this.CodigoCuenta.Width = 125;
            // 
            // idDescuento
            // 
            this.idDescuento.DataPropertyName = "idDescuento";
            this.idDescuento.HeaderText = "idDescuento";
            this.idDescuento.MinimumWidth = 6;
            this.idDescuento.Name = "idDescuento";
            this.idDescuento.Visible = false;
            this.idDescuento.Width = 125;
            // 
            // idMdescuento
            // 
            this.idMdescuento.DataPropertyName = "idMDescuento";
            this.idMdescuento.HeaderText = "idMDescuento";
            this.idMdescuento.MinimumWidth = 6;
            this.idMdescuento.Name = "idMdescuento";
            this.idMdescuento.Visible = false;
            this.idMdescuento.Width = 125;
            // 
            // FechaDescuento1
            // 
            this.FechaDescuento1.DataPropertyName = "FechaDescuento";
            this.FechaDescuento1.HeaderText = "FechaDescuento";
            this.FechaDescuento1.MinimumWidth = 6;
            this.FechaDescuento1.Name = "FechaDescuento1";
            this.FechaDescuento1.Width = 125;
            // 
            // IdCuenta
            // 
            this.IdCuenta.DataPropertyName = "IdCuenta";
            this.IdCuenta.HeaderText = "IdCuenta";
            this.IdCuenta.MinimumWidth = 6;
            this.IdCuenta.Name = "IdCuenta";
            this.IdCuenta.Visible = false;
            this.IdCuenta.Width = 125;
            // 
            // objTipoPago
            // 
            this.objTipoPago.DataPropertyName = "objTipoPago";
            this.objTipoPago.HeaderText = "objTipoPago";
            this.objTipoPago.MinimumWidth = 6;
            this.objTipoPago.Name = "objTipoPago";
            this.objTipoPago.Visible = false;
            this.objTipoPago.Width = 125;
            // 
            // idEmpleado
            // 
            this.idEmpleado.DataPropertyName = "idEmpleado";
            this.idEmpleado.HeaderText = "idEmpleado";
            this.idEmpleado.MinimumWidth = 6;
            this.idEmpleado.Name = "idEmpleado";
            this.idEmpleado.Visible = false;
            this.idEmpleado.Width = 125;
            // 
            // objDescuento
            // 
            this.objDescuento.DataPropertyName = "objDescuento";
            this.objDescuento.HeaderText = "objDescuento";
            this.objDescuento.MinimumWidth = 6;
            this.objDescuento.Name = "objDescuento";
            this.objDescuento.Visible = false;
            this.objDescuento.Width = 125;
            // 
            // TipoPagoD
            // 
            this.TipoPagoD.DataPropertyName = "TipoPagoD";
            this.TipoPagoD.HeaderText = "TipoPagoD";
            this.TipoPagoD.MinimumWidth = 6;
            this.TipoPagoD.Name = "TipoPagoD";
            this.TipoPagoD.Visible = false;
            this.TipoPagoD.Width = 125;
            // 
            // objEmpleado
            // 
            this.objEmpleado.DataPropertyName = "objEmpleado";
            this.objEmpleado.HeaderText = "objEmpleado";
            this.objEmpleado.MinimumWidth = 6;
            this.objEmpleado.Name = "objEmpleado";
            this.objEmpleado.Visible = false;
            this.objEmpleado.Width = 125;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Location = new System.Drawing.Point(1065, 318);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(112, 49);
            this.btnSalir.TabIndex = 56;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbFechaFinal);
            this.groupBox1.Controls.Add(this.cbFechaInicial);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dtpFechaFinal);
            this.groupBox1.Controls.Add(this.dtpFechaInicial);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(-1, 46);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1059, 92);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 18);
            this.label2.TabIndex = 43;
            this.label2.Text = "Código Empleado:";
            // 
            // cbFechaFinal
            // 
            this.cbFechaFinal.AutoSize = true;
            this.cbFechaFinal.Location = new System.Drawing.Point(415, 50);
            this.cbFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.cbFechaFinal.Name = "cbFechaFinal";
            this.cbFechaFinal.Size = new System.Drawing.Size(110, 22);
            this.cbFechaFinal.TabIndex = 4;
            this.cbFechaFinal.Text = "Fecha Final:";
            this.cbFechaFinal.UseVisualStyleBackColor = true;
            this.cbFechaFinal.CheckedChanged += new System.EventHandler(this.cbFechaFinal_CheckedChanged);
            // 
            // cbFechaInicial
            // 
            this.cbFechaInicial.AutoSize = true;
            this.cbFechaInicial.Location = new System.Drawing.Point(30, 55);
            this.cbFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.cbFechaInicial.Name = "cbFechaInicial";
            this.cbFechaInicial.Size = new System.Drawing.Size(115, 22);
            this.cbFechaInicial.TabIndex = 2;
            this.cbFechaInicial.Text = "Fecha Inicial:";
            this.cbFechaInicial.UseVisualStyleBackColor = true;
            this.cbFechaInicial.CheckedChanged += new System.EventHandler(this.cbFechaInicial_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Location = new System.Drawing.Point(300, 12);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 29);
            this.btnBuscar.TabIndex = 40;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(518, 50);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(119, 24);
            this.dtpFechaFinal.TabIndex = 5;
            this.dtpFechaFinal.Value = new System.DateTime(2019, 8, 19, 18, 3, 0, 0);
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(172, 53);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(119, 24);
            this.dtpFechaInicial.TabIndex = 3;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(172, 17);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.MaxLength = 5;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(119, 24);
            this.txtCodigo.TabIndex = 1;
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(1065, 204);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(112, 49);
            this.btnModificar.TabIndex = 54;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Location = new System.Drawing.Point(1065, 261);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(112, 49);
            this.btnQuitar.TabIndex = 53;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Location = new System.Drawing.Point(1065, 148);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(112, 49);
            this.btnNuevo.TabIndex = 52;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1059, 38);
            this.panel1.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(305, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Movimiento Descuento";
            // 
            // frmMovimientoDescuento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1177, 615);
            this.Controls.Add(this.pnTotal);
            this.Controls.Add(this.dgvDescuentos);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmMovimientoDescuento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimientos Descuentos";
            this.Shown += new System.EventHandler(this.frmMovimientoDescuento_Shown);
            this.pnTotal.ResumeLayout(false);
            this.pnTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescuentos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox pnTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvDescuentos;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbFechaFinal;
        private System.Windows.Forms.CheckBox cbFechaInicial;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipodePago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadLabor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMdescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaDescuento1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn objTipoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn objDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPagoD;
        private System.Windows.Forms.DataGridViewTextBoxColumn objEmpleado;
    }
}