namespace nomina.Forms.MovimientoDescuentos
{
    partial class frmAddMovimientoDescuento
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gpLabor = new System.Windows.Forms.GroupBox();
            this.nudMontoTotal = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudCantidadDescuento = new System.Windows.Forms.NumericUpDown();
            this.lblNombreLabor = new System.Windows.Forms.Label();
            this.lblCodigoDescuento = new System.Windows.Forms.Label();
            this.btnBuscarCuenta = new System.Windows.Forms.Button();
            this.txtNombreCuenta = new System.Windows.Forms.TextBox();
            this.nudMontoDescuento = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtCodigoCuenta = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbCreatedBy = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcionDescuento = new System.Windows.Forms.TextBox();
            this.btnBuscarDescuento = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gpEmpleados = new System.Windows.Forms.GroupBox();
            this.lblCodigoEmpleado = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.lblNombreEmpleado = new System.Windows.Forms.Label();
            this.btnBuscarEmpleado = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbTicketId = new System.Windows.Forms.Label();
            this.lbAlertId = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gpLabor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadDescuento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoDescuento)).BeginInit();
            this.gpEmpleados.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(96, 382);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 40);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnCancelar_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Location = new System.Drawing.Point(6, 382);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(84, 40);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnGuardar_KeyPress);
            // 
            // gpLabor
            // 
            this.gpLabor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpLabor.Controls.Add(this.nudMontoTotal);
            this.gpLabor.Controls.Add(this.label5);
            this.gpLabor.Controls.Add(this.nudCantidadDescuento);
            this.gpLabor.Controls.Add(this.lblNombreLabor);
            this.gpLabor.Controls.Add(this.lblCodigoDescuento);
            this.gpLabor.Controls.Add(this.btnBuscarCuenta);
            this.gpLabor.Controls.Add(this.txtNombreCuenta);
            this.gpLabor.Controls.Add(this.nudMontoDescuento);
            this.gpLabor.Controls.Add(this.label10);
            this.gpLabor.Controls.Add(this.dtpFecha);
            this.gpLabor.Controls.Add(this.txtCodigoCuenta);
            this.gpLabor.Controls.Add(this.label11);
            this.gpLabor.Controls.Add(this.label6);
            this.gpLabor.Controls.Add(this.lbCreatedBy);
            this.gpLabor.Controls.Add(this.label4);
            this.gpLabor.Controls.Add(this.label3);
            this.gpLabor.Controls.Add(this.txtDescripcionDescuento);
            this.gpLabor.Controls.Add(this.btnBuscarDescuento);
            this.gpLabor.Controls.Add(this.label2);
            this.gpLabor.Controls.Add(this.label1);
            this.gpLabor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpLabor.Location = new System.Drawing.Point(4, 176);
            this.gpLabor.Name = "gpLabor";
            this.gpLabor.Size = new System.Drawing.Size(878, 199);
            this.gpLabor.TabIndex = 59;
            this.gpLabor.TabStop = false;
            this.gpLabor.Text = "Información de la Labor";
            // 
            // nudMontoTotal
            // 
            this.nudMontoTotal.DecimalPlaces = 2;
            this.nudMontoTotal.Enabled = false;
            this.nudMontoTotal.Location = new System.Drawing.Point(583, 88);
            this.nudMontoTotal.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.nudMontoTotal.Name = "nudMontoTotal";
            this.nudMontoTotal.Size = new System.Drawing.Size(120, 22);
            this.nudMontoTotal.TabIndex = 65;
            this.nudMontoTotal.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(580, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 66;
            this.label5.Text = "Monto Total:";
            // 
            // nudCantidadDescuento
            // 
            this.nudCantidadDescuento.DecimalPlaces = 2;
            this.nudCantidadDescuento.Enabled = false;
            this.nudCantidadDescuento.Location = new System.Drawing.Point(437, 89);
            this.nudCantidadDescuento.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudCantidadDescuento.Name = "nudCantidadDescuento";
            this.nudCantidadDescuento.Size = new System.Drawing.Size(120, 22);
            this.nudCantidadDescuento.TabIndex = 5;
            this.nudCantidadDescuento.ThousandsSeparator = true;
            this.nudCantidadDescuento.ValueChanged += new System.EventHandler(this.nudCantidadDescuento_ValueChanged);
            this.nudCantidadDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudCantidadLabor_KeyPress);
            // 
            // lblNombreLabor
            // 
            this.lblNombreLabor.AutoSize = true;
            this.lblNombreLabor.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNombreLabor.Location = new System.Drawing.Point(273, 41);
            this.lblNombreLabor.Name = "lblNombreLabor";
            this.lblNombreLabor.Size = new System.Drawing.Size(45, 16);
            this.lblNombreLabor.TabIndex = 61;
            this.lblNombreLabor.Text = "label9";
            // 
            // lblCodigoDescuento
            // 
            this.lblCodigoDescuento.AutoSize = true;
            this.lblCodigoDescuento.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCodigoDescuento.Location = new System.Drawing.Point(6, 41);
            this.lblCodigoDescuento.Name = "lblCodigoDescuento";
            this.lblCodigoDescuento.Size = new System.Drawing.Size(45, 16);
            this.lblCodigoDescuento.TabIndex = 43;
            this.lblCodigoDescuento.Text = "label9";
            // 
            // btnBuscarCuenta
            // 
            this.btnBuscarCuenta.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarCuenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCuenta.Location = new System.Drawing.Point(163, 159);
            this.btnBuscarCuenta.Name = "btnBuscarCuenta";
            this.btnBuscarCuenta.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarCuenta.TabIndex = 57;
            this.btnBuscarCuenta.Text = "Buscar";
            this.btnBuscarCuenta.UseVisualStyleBackColor = false;
            this.btnBuscarCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnBuscarCuenta_KeyPress);
            // 
            // txtNombreCuenta
            // 
            this.txtNombreCuenta.Enabled = false;
            this.txtNombreCuenta.Location = new System.Drawing.Point(276, 160);
            this.txtNombreCuenta.MaxLength = 50;
            this.txtNombreCuenta.Name = "txtNombreCuenta";
            this.txtNombreCuenta.Size = new System.Drawing.Size(469, 22);
            this.txtNombreCuenta.TabIndex = 58;
            // 
            // nudMontoDescuento
            // 
            this.nudMontoDescuento.DecimalPlaces = 2;
            this.nudMontoDescuento.Enabled = false;
            this.nudMontoDescuento.Location = new System.Drawing.Point(276, 89);
            this.nudMontoDescuento.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            131072});
            this.nudMontoDescuento.Name = "nudMontoDescuento";
            this.nudMontoDescuento.Size = new System.Drawing.Size(120, 22);
            this.nudMontoDescuento.TabIndex = 4;
            this.nudMontoDescuento.ThousandsSeparator = true;
            this.nudMontoDescuento.ValueChanged += new System.EventHandler(this.nudMontoDescuento_ValueChanged);
            this.nudMontoDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudMontoLabor_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(273, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 15);
            this.label10.TabIndex = 60;
            this.label10.Text = "Nombre de la Cuenta:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(722, 88);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(90, 22);
            this.dtpFecha.TabIndex = 6;
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFecha_KeyPress);
            // 
            // txtCodigoCuenta
            // 
            this.txtCodigoCuenta.Location = new System.Drawing.Point(12, 160);
            this.txtCodigoCuenta.MaxLength = 5;
            this.txtCodigoCuenta.Name = "txtCodigoCuenta";
            this.txtCodigoCuenta.Size = new System.Drawing.Size(145, 22);
            this.txtCodigoCuenta.TabIndex = 7;
            this.txtCodigoCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoCuenta_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(9, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 15);
            this.label11.TabIndex = 59;
            this.label11.Text = "Código Cuenta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(273, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 50;
            this.label6.Text = "Monto:";
            // 
            // lbCreatedBy
            // 
            this.lbCreatedBy.AutoSize = true;
            this.lbCreatedBy.Location = new System.Drawing.Point(719, 71);
            this.lbCreatedBy.Name = "lbCreatedBy";
            this.lbCreatedBy.Size = new System.Drawing.Size(49, 16);
            this.lbCreatedBy.TabIndex = 37;
            this.lbCreatedBy.Text = "Fecha:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(433, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "Cantidad del Descuento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 47;
            this.label3.Text = "Razón:";
            // 
            // txtDescripcionDescuento
            // 
            this.txtDescripcionDescuento.Location = new System.Drawing.Point(9, 92);
            this.txtDescripcionDescuento.MaxLength = 30;
            this.txtDescripcionDescuento.Multiline = true;
            this.txtDescripcionDescuento.Name = "txtDescripcionDescuento";
            this.txtDescripcionDescuento.Size = new System.Drawing.Size(229, 37);
            this.txtDescripcionDescuento.TabIndex = 3;
            this.txtDescripcionDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcionLabor_KeyPress);
            // 
            // btnBuscarDescuento
            // 
            this.btnBuscarDescuento.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarDescuento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarDescuento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarDescuento.Location = new System.Drawing.Point(160, 40);
            this.btnBuscarDescuento.Name = "btnBuscarDescuento";
            this.btnBuscarDescuento.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarDescuento.TabIndex = 2;
            this.btnBuscarDescuento.Text = "Buscar";
            this.btnBuscarDescuento.UseVisualStyleBackColor = false;
            this.btnBuscarDescuento.Click += new System.EventHandler(this.BtnBuscarDescuento_Click);
            this.btnBuscarDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnBuscarLabor_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 15);
            this.label2.TabIndex = 41;
            this.label2.Text = "Código del Descuento:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(270, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Nombre del Descuento:";
            // 
            // gpEmpleados
            // 
            this.gpEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpEmpleados.Controls.Add(this.lblCodigoEmpleado);
            this.gpEmpleados.Controls.Add(this.lblDepartamento);
            this.gpEmpleados.Controls.Add(this.lblSueldo);
            this.gpEmpleados.Controls.Add(this.lblNombreEmpleado);
            this.gpEmpleados.Controls.Add(this.btnBuscarEmpleado);
            this.gpEmpleados.Controls.Add(this.label18);
            this.gpEmpleados.Controls.Add(this.label12);
            this.gpEmpleados.Controls.Add(this.lbTicketId);
            this.gpEmpleados.Controls.Add(this.lbAlertId);
            this.gpEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpEmpleados.Location = new System.Drawing.Point(4, 37);
            this.gpEmpleados.Name = "gpEmpleados";
            this.gpEmpleados.Size = new System.Drawing.Size(874, 133);
            this.gpEmpleados.TabIndex = 58;
            this.gpEmpleados.TabStop = false;
            this.gpEmpleados.Text = "Información del Empleado";
            // 
            // lblCodigoEmpleado
            // 
            this.lblCodigoEmpleado.AutoSize = true;
            this.lblCodigoEmpleado.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCodigoEmpleado.Location = new System.Drawing.Point(9, 44);
            this.lblCodigoEmpleado.Name = "lblCodigoEmpleado";
            this.lblCodigoEmpleado.Size = new System.Drawing.Size(129, 16);
            this.lblCodigoEmpleado.TabIndex = 43;
            this.lblCodigoEmpleado.Text = "lblCodigoEmpleado";
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblDepartamento.Location = new System.Drawing.Point(273, 93);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(45, 16);
            this.lblDepartamento.TabIndex = 42;
            this.lblDepartamento.Text = "label9";
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblSueldo.Location = new System.Drawing.Point(9, 93);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(45, 16);
            this.lblSueldo.TabIndex = 41;
            this.lblSueldo.Text = "label9";
            // 
            // lblNombreEmpleado
            // 
            this.lblNombreEmpleado.AutoSize = true;
            this.lblNombreEmpleado.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNombreEmpleado.Location = new System.Drawing.Point(273, 44);
            this.lblNombreEmpleado.Name = "lblNombreEmpleado";
            this.lblNombreEmpleado.Size = new System.Drawing.Size(45, 16);
            this.lblNombreEmpleado.TabIndex = 39;
            this.lblNombreEmpleado.Text = "label8";
            // 
            // btnBuscarEmpleado
            // 
            this.btnBuscarEmpleado.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarEmpleado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarEmpleado.Location = new System.Drawing.Point(160, 37);
            this.btnBuscarEmpleado.Name = "btnBuscarEmpleado";
            this.btnBuscarEmpleado.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarEmpleado.TabIndex = 1;
            this.btnBuscarEmpleado.Text = "Buscar";
            this.btnBuscarEmpleado.UseVisualStyleBackColor = false;
            this.btnBuscarEmpleado.Click += new System.EventHandler(this.btnBuscarEmpleado_Click);
            this.btnBuscarEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnBuscarEmpleado_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(273, 78);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(89, 15);
            this.label18.TabIndex = 37;
            this.label18.Text = "Departamento:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 16);
            this.label12.TabIndex = 34;
            this.label12.Text = "Sueldo:";
            // 
            // lbTicketId
            // 
            this.lbTicketId.AutoSize = true;
            this.lbTicketId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTicketId.Location = new System.Drawing.Point(273, 25);
            this.lbTicketId.Name = "lbTicketId";
            this.lbTicketId.Size = new System.Drawing.Size(135, 15);
            this.lbTicketId.TabIndex = 6;
            this.lbTicketId.Text = "Nombre del Empleado:";
            // 
            // lbAlertId
            // 
            this.lbAlertId.AutoSize = true;
            this.lbAlertId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAlertId.Location = new System.Drawing.Point(9, 25);
            this.lbAlertId.Name = "lbAlertId";
            this.lbAlertId.Size = new System.Drawing.Size(109, 15);
            this.lbAlertId.TabIndex = 3;
            this.lbAlertId.Text = "Código Empleado:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(889, 31);
            this.lblTitulo.TabIndex = 57;
            this.lblTitulo.Text = "Nuevo Movimiento Descuento";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAddMovimientoDescuento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(889, 432);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gpLabor);
            this.Controls.Add(this.gpEmpleados);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmAddMovimientoDescuento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo Movimientos Descuentos";
            this.Load += new System.EventHandler(this.FrmAddMovimientoDescuento_Load);
            this.gpLabor.ResumeLayout(false);
            this.gpLabor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadDescuento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoDescuento)).EndInit();
            this.gpEmpleados.ResumeLayout(false);
            this.gpEmpleados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gpLabor;
        private System.Windows.Forms.NumericUpDown nudCantidadDescuento;
        private System.Windows.Forms.Label lblNombreLabor;
        private System.Windows.Forms.Label lblCodigoDescuento;
        private System.Windows.Forms.Button btnBuscarCuenta;
        private System.Windows.Forms.TextBox txtNombreCuenta;
        private System.Windows.Forms.NumericUpDown nudMontoDescuento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtCodigoCuenta;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbCreatedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcionDescuento;
        private System.Windows.Forms.Button btnBuscarDescuento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpEmpleados;
        private System.Windows.Forms.Label lblCodigoEmpleado;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Label lblNombreEmpleado;
        private System.Windows.Forms.Button btnBuscarEmpleado;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbTicketId;
        private System.Windows.Forms.Label lbAlertId;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.NumericUpDown nudMontoTotal;
        private System.Windows.Forms.Label label5;
    }
}