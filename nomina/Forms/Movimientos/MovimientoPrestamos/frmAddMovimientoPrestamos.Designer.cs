namespace nomina.Forms.Movimientos.MovimientoPrestamos
{
    partial class frmAddMovimientoPrestamos
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gpPrestamo = new System.Windows.Forms.GroupBox();
            this.cbActivo = new System.Windows.Forms.CheckBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblRazon = new System.Windows.Forms.Label();
            this.nudCuotaPagar = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMonto = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTipoPago = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudTiempo = new System.Windows.Forms.NumericUpDown();
            this.lblMonto = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gpEmpleados = new System.Windows.Forms.GroupBox();
            this.lblSalarioInicial = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSalarioFinal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCodigoEmpleado = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.lblNombreEmpleado = new System.Windows.Forms.Label();
            this.btnBuscarEmpleado = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbTicketId = new System.Windows.Forms.Label();
            this.lbAlertId = new System.Windows.Forms.Label();
            this.gpPrestamo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCuotaPagar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiempo)).BeginInit();
            this.gpEmpleados.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(848, 38);
            this.lblTitulo.TabIndex = 76;
            this.lblTitulo.Text = "Agregar Prestamo";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(124, 631);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(112, 49);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            this.btnCancelar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnCancelar_KeyPress);
            // 
            // gpPrestamo
            // 
            this.gpPrestamo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpPrestamo.Controls.Add(this.cbActivo);
            this.gpPrestamo.Controls.Add(this.lblEstado);
            this.gpPrestamo.Controls.Add(this.txtCodigo);
            this.gpPrestamo.Controls.Add(this.label4);
            this.gpPrestamo.Controls.Add(this.label2);
            this.gpPrestamo.Controls.Add(this.txtDescripcion);
            this.gpPrestamo.Controls.Add(this.lblRazon);
            this.gpPrestamo.Controls.Add(this.nudCuotaPagar);
            this.gpPrestamo.Controls.Add(this.label3);
            this.gpPrestamo.Controls.Add(this.nudMonto);
            this.gpPrestamo.Controls.Add(this.label1);
            this.gpPrestamo.Controls.Add(this.cbTipoPago);
            this.gpPrestamo.Controls.Add(this.label5);
            this.gpPrestamo.Controls.Add(this.nudTiempo);
            this.gpPrestamo.Controls.Add(this.lblMonto);
            this.gpPrestamo.Controls.Add(this.dtpFechaInicial);
            this.gpPrestamo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpPrestamo.Location = new System.Drawing.Point(3, 272);
            this.gpPrestamo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpPrestamo.Name = "gpPrestamo";
            this.gpPrestamo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpPrestamo.Size = new System.Drawing.Size(837, 352);
            this.gpPrestamo.TabIndex = 75;
            this.gpPrestamo.TabStop = false;
            this.gpPrestamo.Text = "Información del Aumento";
            // 
            // cbActivo
            // 
            this.cbActivo.AutoSize = true;
            this.cbActivo.Location = new System.Drawing.Point(364, 181);
            this.cbActivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbActivo.Name = "cbActivo";
            this.cbActivo.Size = new System.Drawing.Size(77, 24);
            this.cbActivo.TabIndex = 8;
            this.cbActivo.Text = "Activo";
            this.cbActivo.UseVisualStyleBackColor = true;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(285, 183);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(66, 20);
            this.lblEstado.TabIndex = 6;
            this.lblEstado.Text = "Estado:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(15, 52);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigo.MaxLength = 5;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(217, 26);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodigo_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 81;
            this.label4.Text = "Código:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(287, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 80;
            this.label2.Text = "Fecha:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(289, 106);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescripcion.MaxLength = 40;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(309, 57);
            this.txtDescripcion.TabIndex = 4;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDescripcion_KeyPress);
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(287, 85);
            this.lblRazon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(104, 20);
            this.lblRazon.TabIndex = 78;
            this.lblRazon.Text = "Descripción:";
            // 
            // nudCuotaPagar
            // 
            this.nudCuotaPagar.DecimalPlaces = 2;
            this.nudCuotaPagar.Enabled = false;
            this.nudCuotaPagar.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCuotaPagar.Location = new System.Drawing.Point(15, 304);
            this.nudCuotaPagar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudCuotaPagar.Maximum = new decimal(new int[] {
            1569324956,
            23283064,
            0,
            131072});
            this.nudCuotaPagar.Name = "nudCuotaPagar";
            this.nudCuotaPagar.Size = new System.Drawing.Size(160, 26);
            this.nudCuotaPagar.TabIndex = 7;
            this.nudCuotaPagar.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 284);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 76;
            this.label3.Text = "Cuota a Pagar:";
            // 
            // nudMonto
            // 
            this.nudMonto.DecimalPlaces = 2;
            this.nudMonto.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMonto.Location = new System.Drawing.Point(13, 176);
            this.nudMonto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudMonto.Maximum = new decimal(new int[] {
            1569324956,
            23283064,
            0,
            131072});
            this.nudMonto.Name = "nudMonto";
            this.nudMonto.Size = new System.Drawing.Size(160, 26);
            this.nudMonto.TabIndex = 5;
            this.nudMonto.ThousandsSeparator = true;
            this.nudMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NudMonto_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 75;
            this.label1.Text = "Monto:";
            // 
            // cbTipoPago
            // 
            this.cbTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoPago.FormattingEnabled = true;
            this.cbTipoPago.Location = new System.Drawing.Point(13, 107);
            this.cbTipoPago.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbTipoPago.Name = "cbTipoPago";
            this.cbTipoPago.Size = new System.Drawing.Size(219, 28);
            this.cbTipoPago.TabIndex = 3;
            this.cbTipoPago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbTipoPago_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 73;
            this.label5.Text = "Tipo Pago:";
            // 
            // nudTiempo
            // 
            this.nudTiempo.Location = new System.Drawing.Point(13, 244);
            this.nudTiempo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudTiempo.Maximum = new decimal(new int[] {
            1569324956,
            23283064,
            0,
            131072});
            this.nudTiempo.Name = "nudTiempo";
            this.nudTiempo.Size = new System.Drawing.Size(160, 26);
            this.nudTiempo.TabIndex = 6;
            this.nudTiempo.ThousandsSeparator = true;
            this.nudTiempo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NudTiempo_KeyPress);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(11, 220);
            this.lblMonto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(135, 20);
            this.lblMonto.TabIndex = 67;
            this.lblMonto.Text = "Tiempo de Pago:";
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(291, 46);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(119, 26);
            this.dtpFechaInicial.TabIndex = 2;
            this.dtpFechaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DtpFechaInicial_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Location = new System.Drawing.Point(4, 631);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(112, 49);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            this.btnGuardar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnGuardar_KeyPress);
            // 
            // gpEmpleados
            // 
            this.gpEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpEmpleados.Controls.Add(this.lblSalarioInicial);
            this.gpEmpleados.Controls.Add(this.label9);
            this.gpEmpleados.Controls.Add(this.lblSalarioFinal);
            this.gpEmpleados.Controls.Add(this.label7);
            this.gpEmpleados.Controls.Add(this.lblCategoria);
            this.gpEmpleados.Controls.Add(this.label6);
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
            this.gpEmpleados.Location = new System.Drawing.Point(1, 48);
            this.gpEmpleados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpEmpleados.Name = "gpEmpleados";
            this.gpEmpleados.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpEmpleados.Size = new System.Drawing.Size(839, 202);
            this.gpEmpleados.TabIndex = 74;
            this.gpEmpleados.TabStop = false;
            this.gpEmpleados.Text = "Información del Empleado";
            // 
            // lblSalarioInicial
            // 
            this.lblSalarioInicial.AutoSize = true;
            this.lblSalarioInicial.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblSalarioInicial.Location = new System.Drawing.Point(175, 165);
            this.lblSalarioInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalarioInicial.Name = "lblSalarioInicial";
            this.lblSalarioInicial.Size = new System.Drawing.Size(53, 20);
            this.lblSalarioInicial.TabIndex = 49;
            this.lblSalarioInicial.Text = "label9";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(171, 144);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 18);
            this.label9.TabIndex = 48;
            this.label9.Text = "Salario Inicial:";
            // 
            // lblSalarioFinal
            // 
            this.lblSalarioFinal.AutoSize = true;
            this.lblSalarioFinal.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblSalarioFinal.Location = new System.Drawing.Point(364, 165);
            this.lblSalarioFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalarioFinal.Name = "lblSalarioFinal";
            this.lblSalarioFinal.Size = new System.Drawing.Size(53, 20);
            this.lblSalarioFinal.TabIndex = 47;
            this.lblSalarioFinal.Text = "label9";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(361, 144);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 18);
            this.label7.TabIndex = 46;
            this.label7.Text = "Salario Final:";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCategoria.Location = new System.Drawing.Point(20, 165);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(53, 20);
            this.lblCategoria.TabIndex = 45;
            this.lblCategoria.Text = "label9";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 144);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 18);
            this.label6.TabIndex = 44;
            this.label6.Text = "Categoría:";
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
            this.btnBuscarEmpleado.Location = new System.Drawing.Point(187, 46);
            this.btnBuscarEmpleado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarEmpleado.Name = "btnBuscarEmpleado";
            this.btnBuscarEmpleado.Size = new System.Drawing.Size(100, 28);
            this.btnBuscarEmpleado.TabIndex = 1;
            this.btnBuscarEmpleado.Text = "Buscar";
            this.btnBuscarEmpleado.UseVisualStyleBackColor = false;
            this.btnBuscarEmpleado.Click += new System.EventHandler(this.BtnBuscarEmpleado_Click);
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
            this.label12.Size = new System.Drawing.Size(129, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "Sueldo Anterior:";
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
            // frmAddMovimientoPrestamos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(848, 686);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gpPrestamo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gpEmpleados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "frmAddMovimientoPrestamos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo Movimiento Prestamo";
            this.Load += new System.EventHandler(this.FrmAddMovimientoPrestamos_Load);
            this.gpPrestamo.ResumeLayout(false);
            this.gpPrestamo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCuotaPagar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiempo)).EndInit();
            this.gpEmpleados.ResumeLayout(false);
            this.gpEmpleados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gpPrestamo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.NumericUpDown nudCuotaPagar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMonto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTipoPago;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudTiempo;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gpEmpleados;
        private System.Windows.Forms.Label lblSalarioInicial;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSalarioFinal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCodigoEmpleado;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Label lblNombreEmpleado;
        private System.Windows.Forms.Button btnBuscarEmpleado;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbTicketId;
        private System.Windows.Forms.Label lbAlertId;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbActivo;
        private System.Windows.Forms.Label lblEstado;
    }
}