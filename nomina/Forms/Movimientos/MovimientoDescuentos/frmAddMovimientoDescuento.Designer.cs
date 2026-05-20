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
            this.btnCancelar.Location = new System.Drawing.Point(128, 470);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(112, 49);
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
            this.btnGuardar.Location = new System.Drawing.Point(8, 470);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(112, 49);
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
            this.gpLabor.Location = new System.Drawing.Point(5, 217);
            this.gpLabor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpLabor.Name = "gpLabor";
            this.gpLabor.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpLabor.Size = new System.Drawing.Size(1171, 245);
            this.gpLabor.TabIndex = 59;
            this.gpLabor.TabStop = false;
            this.gpLabor.Text = "Información de la Labor";
            this.gpLabor.Visible = false;
            // 
            // nudMontoTotal
            // 
            this.nudMontoTotal.DecimalPlaces = 2;
            this.nudMontoTotal.Enabled = false;
            this.nudMontoTotal.Location = new System.Drawing.Point(777, 108);
            this.nudMontoTotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudMontoTotal.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.nudMontoTotal.Name = "nudMontoTotal";
            this.nudMontoTotal.Size = new System.Drawing.Size(160, 26);
            this.nudMontoTotal.TabIndex = 65;
            this.nudMontoTotal.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(773, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 18);
            this.label5.TabIndex = 66;
            this.label5.Text = "Monto Total:";
            // 
            // nudCantidadDescuento
            // 
            this.nudCantidadDescuento.DecimalPlaces = 2;
            this.nudCantidadDescuento.Enabled = false;
            this.nudCantidadDescuento.Location = new System.Drawing.Point(583, 110);
            this.nudCantidadDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudCantidadDescuento.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudCantidadDescuento.Name = "nudCantidadDescuento";
            this.nudCantidadDescuento.Size = new System.Drawing.Size(160, 26);
            this.nudCantidadDescuento.TabIndex = 5;
            this.nudCantidadDescuento.ThousandsSeparator = true;
            this.nudCantidadDescuento.ValueChanged += new System.EventHandler(this.nudCantidadDescuento_ValueChanged);
            this.nudCantidadDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudCantidadLabor_KeyPress);
            // 
            // lblNombreLabor
            // 
            this.lblNombreLabor.AutoSize = true;
            this.lblNombreLabor.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNombreLabor.Location = new System.Drawing.Point(364, 50);
            this.lblNombreLabor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreLabor.Name = "lblNombreLabor";
            this.lblNombreLabor.Size = new System.Drawing.Size(53, 20);
            this.lblNombreLabor.TabIndex = 61;
            this.lblNombreLabor.Text = "label9";
            // 
            // lblCodigoDescuento
            // 
            this.lblCodigoDescuento.AutoSize = true;
            this.lblCodigoDescuento.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCodigoDescuento.Location = new System.Drawing.Point(8, 50);
            this.lblCodigoDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigoDescuento.Name = "lblCodigoDescuento";
            this.lblCodigoDescuento.Size = new System.Drawing.Size(53, 20);
            this.lblCodigoDescuento.TabIndex = 43;
            this.lblCodigoDescuento.Text = "label9";
            // 
            // btnBuscarCuenta
            // 
            this.btnBuscarCuenta.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarCuenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCuenta.Location = new System.Drawing.Point(217, 196);
            this.btnBuscarCuenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarCuenta.Name = "btnBuscarCuenta";
            this.btnBuscarCuenta.Size = new System.Drawing.Size(100, 28);
            this.btnBuscarCuenta.TabIndex = 57;
            this.btnBuscarCuenta.Text = "Buscar";
            this.btnBuscarCuenta.UseVisualStyleBackColor = false;
            this.btnBuscarCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnBuscarCuenta_KeyPress);
            // 
            // txtNombreCuenta
            // 
            this.txtNombreCuenta.Enabled = false;
            this.txtNombreCuenta.Location = new System.Drawing.Point(368, 197);
            this.txtNombreCuenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombreCuenta.MaxLength = 50;
            this.txtNombreCuenta.Name = "txtNombreCuenta";
            this.txtNombreCuenta.Size = new System.Drawing.Size(624, 26);
            this.txtNombreCuenta.TabIndex = 58;
            // 
            // nudMontoDescuento
            // 
            this.nudMontoDescuento.DecimalPlaces = 2;
            this.nudMontoDescuento.Enabled = false;
            this.nudMontoDescuento.Location = new System.Drawing.Point(368, 110);
            this.nudMontoDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudMontoDescuento.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            131072});
            this.nudMontoDescuento.Name = "nudMontoDescuento";
            this.nudMontoDescuento.Size = new System.Drawing.Size(160, 26);
            this.nudMontoDescuento.TabIndex = 4;
            this.nudMontoDescuento.ThousandsSeparator = true;
            this.nudMontoDescuento.ValueChanged += new System.EventHandler(this.nudMontoDescuento_ValueChanged);
            this.nudMontoDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudMontoLabor_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(364, 175);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 18);
            this.label10.TabIndex = 60;
            this.label10.Text = "Nombre de la Cuenta:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(963, 108);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(119, 26);
            this.dtpFecha.TabIndex = 6;
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFecha_KeyPress);
            // 
            // txtCodigoCuenta
            // 
            this.txtCodigoCuenta.Location = new System.Drawing.Point(16, 197);
            this.txtCodigoCuenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigoCuenta.MaxLength = 5;
            this.txtCodigoCuenta.Name = "txtCodigoCuenta";
            this.txtCodigoCuenta.Size = new System.Drawing.Size(192, 26);
            this.txtCodigoCuenta.TabIndex = 7;
            this.txtCodigoCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoCuenta_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 175);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 18);
            this.label11.TabIndex = 59;
            this.label11.Text = "Código Cuenta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(364, 91);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 18);
            this.label6.TabIndex = 50;
            this.label6.Text = "Monto:";
            // 
            // lbCreatedBy
            // 
            this.lbCreatedBy.AutoSize = true;
            this.lbCreatedBy.Location = new System.Drawing.Point(959, 87);
            this.lbCreatedBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCreatedBy.Name = "lbCreatedBy";
            this.lbCreatedBy.Size = new System.Drawing.Size(60, 20);
            this.lbCreatedBy.TabIndex = 37;
            this.lbCreatedBy.Text = "Fecha:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(577, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 18);
            this.label4.TabIndex = 48;
            this.label4.Text = "Cantidad del Descuento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 47;
            this.label3.Text = "Razón:";
            // 
            // txtDescripcionDescuento
            // 
            this.txtDescripcionDescuento.Location = new System.Drawing.Point(12, 113);
            this.txtDescripcionDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescripcionDescuento.MaxLength = 30;
            this.txtDescripcionDescuento.Multiline = true;
            this.txtDescripcionDescuento.Name = "txtDescripcionDescuento";
            this.txtDescripcionDescuento.Size = new System.Drawing.Size(304, 45);
            this.txtDescripcionDescuento.TabIndex = 3;
            this.txtDescripcionDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcionLabor_KeyPress);
            // 
            // btnBuscarDescuento
            // 
            this.btnBuscarDescuento.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarDescuento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarDescuento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarDescuento.Location = new System.Drawing.Point(213, 49);
            this.btnBuscarDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarDescuento.Name = "btnBuscarDescuento";
            this.btnBuscarDescuento.Size = new System.Drawing.Size(100, 28);
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
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 18);
            this.label2.TabIndex = 41;
            this.label2.Text = "Código del Descuento:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(360, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 18);
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
            this.gpEmpleados.Location = new System.Drawing.Point(5, 46);
            this.gpEmpleados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpEmpleados.Name = "gpEmpleados";
            this.gpEmpleados.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpEmpleados.Size = new System.Drawing.Size(1165, 164);
            this.gpEmpleados.TabIndex = 58;
            this.gpEmpleados.TabStop = false;
            this.gpEmpleados.Text = "Información del Empleado";
            // 
            // lblCodigoEmpleado
            // 
            this.lblCodigoEmpleado.AutoSize = true;
            this.lblCodigoEmpleado.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCodigoEmpleado.Location = new System.Drawing.Point(12, 54);
            this.lblCodigoEmpleado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigoEmpleado.Name = "lblCodigoEmpleado";
            this.lblCodigoEmpleado.Size = new System.Drawing.Size(152, 20);
            this.lblCodigoEmpleado.TabIndex = 43;
            this.lblCodigoEmpleado.Text = "lblCodigoEmpleado";
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblDepartamento.Location = new System.Drawing.Point(364, 114);
            this.lblDepartamento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(53, 20);
            this.lblDepartamento.TabIndex = 42;
            this.lblDepartamento.Text = "label9";
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblSueldo.Location = new System.Drawing.Point(12, 114);
            this.lblSueldo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(53, 20);
            this.lblSueldo.TabIndex = 41;
            this.lblSueldo.Text = "label9";
            // 
            // lblNombreEmpleado
            // 
            this.lblNombreEmpleado.AutoSize = true;
            this.lblNombreEmpleado.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNombreEmpleado.Location = new System.Drawing.Point(364, 54);
            this.lblNombreEmpleado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreEmpleado.Name = "lblNombreEmpleado";
            this.lblNombreEmpleado.Size = new System.Drawing.Size(53, 20);
            this.lblNombreEmpleado.TabIndex = 39;
            this.lblNombreEmpleado.Text = "label8";
            // 
            // btnBuscarEmpleado
            // 
            this.btnBuscarEmpleado.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarEmpleado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarEmpleado.Location = new System.Drawing.Point(213, 46);
            this.btnBuscarEmpleado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarEmpleado.Name = "btnBuscarEmpleado";
            this.btnBuscarEmpleado.Size = new System.Drawing.Size(100, 28);
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
            this.label18.Location = new System.Drawing.Point(364, 96);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(106, 18);
            this.label18.TabIndex = 37;
            this.label18.Text = "Departamento:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 95);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "Sueldo:";
            // 
            // lbTicketId
            // 
            this.lbTicketId.AutoSize = true;
            this.lbTicketId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTicketId.Location = new System.Drawing.Point(364, 31);
            this.lbTicketId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTicketId.Name = "lbTicketId";
            this.lbTicketId.Size = new System.Drawing.Size(160, 18);
            this.lbTicketId.TabIndex = 6;
            this.lbTicketId.Text = "Nombre del Empleado:";
            // 
            // lbAlertId
            // 
            this.lbAlertId.AutoSize = true;
            this.lbAlertId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAlertId.Location = new System.Drawing.Point(12, 31);
            this.lbAlertId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAlertId.Name = "lbAlertId";
            this.lbAlertId.Size = new System.Drawing.Size(131, 18);
            this.lbAlertId.TabIndex = 3;
            this.lbAlertId.Text = "Código Empleado:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1185, 38);
            this.lblTitulo.TabIndex = 57;
            this.lblTitulo.Text = "Nuevo Movimiento Descuento";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAddMovimientoDescuento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1185, 532);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gpLabor);
            this.Controls.Add(this.gpEmpleados);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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