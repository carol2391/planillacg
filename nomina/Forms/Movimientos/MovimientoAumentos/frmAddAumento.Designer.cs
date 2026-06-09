namespace nomina.Forms.Movimientos.MovimientoAumentos
{
    partial class frmAddAumento
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
            this.lbAlertId = new System.Windows.Forms.Label();
            this.lbTicketId = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnBuscarEmpleado = new System.Windows.Forms.Button();
            this.lblNombreEmpleado = new System.Windows.Forms.Label();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblCodigoEmpleado = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSalarioFinal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSalarioInicial = new System.Windows.Forms.Label();
            this.gpEmpleados = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lblMonto = new System.Windows.Forms.Label();
            this.nudMontoAumentoPorcentaje = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTipoAumento = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMontoAumento = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudNuevoSueldo = new System.Windows.Forms.NumericUpDown();
            this.lblRazon = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gpLabor = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gpEmpleados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAumentoPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAumento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNuevoSueldo)).BeginInit();
            this.gpLabor.SuspendLayout();
            this.SuspendLayout();
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
            // btnBuscarEmpleado
            // 
            this.btnBuscarEmpleado.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarEmpleado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarEmpleado.Location = new System.Drawing.Point(187, 46);
            this.btnBuscarEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarEmpleado.Name = "btnBuscarEmpleado";
            this.btnBuscarEmpleado.Size = new System.Drawing.Size(100, 28);
            this.btnBuscarEmpleado.TabIndex = 1;
            this.btnBuscarEmpleado.Text = "Buscar";
            this.btnBuscarEmpleado.UseVisualStyleBackColor = false;
            this.btnBuscarEmpleado.Click += new System.EventHandler(this.btnBuscarEmpleado_Click);
            this.btnBuscarEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnBuscarEmpleado_KeyPress);
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
            this.gpEmpleados.Location = new System.Drawing.Point(1, 42);
            this.gpEmpleados.Margin = new System.Windows.Forms.Padding(4);
            this.gpEmpleados.Name = "gpEmpleados";
            this.gpEmpleados.Padding = new System.Windows.Forms.Padding(4);
            this.gpEmpleados.Size = new System.Drawing.Size(839, 202);
            this.gpEmpleados.TabIndex = 69;
            this.gpEmpleados.TabStop = false;
            this.gpEmpleados.Text = "Información del Empleado";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Location = new System.Drawing.Point(4, 578);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(112, 49);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnGuardar_KeyPress);
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(287, 46);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(119, 26);
            this.dtpFechaInicial.TabIndex = 3;
            this.dtpFechaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicial_KeyPress);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(15, 94);
            this.lblMonto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(92, 20);
            this.lblMonto.TabIndex = 67;
            this.lblMonto.Text = "Monto Fijo:";
            // 
            // nudMontoAumentoPorcentaje
            // 
            this.nudMontoAumentoPorcentaje.DecimalPlaces = 2;
            this.nudMontoAumentoPorcentaje.Location = new System.Drawing.Point(16, 117);
            this.nudMontoAumentoPorcentaje.Margin = new System.Windows.Forms.Padding(4);
            this.nudMontoAumentoPorcentaje.Maximum = new decimal(new int[] {
            1569324956,
            23283064,
            0,
            131072});
            this.nudMontoAumentoPorcentaje.Name = "nudMontoAumentoPorcentaje";
            this.nudMontoAumentoPorcentaje.Size = new System.Drawing.Size(160, 26);
            this.nudMontoAumentoPorcentaje.TabIndex = 4;
            this.nudMontoAumentoPorcentaje.ThousandsSeparator = true;
            this.nudMontoAumentoPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudMontoAumentoPorcentaje_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 18);
            this.label5.TabIndex = 73;
            this.label5.Text = "Tipo Aumento:";
            // 
            // cbTipoAumento
            // 
            this.cbTipoAumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoAumento.FormattingEnabled = true;
            this.cbTipoAumento.Location = new System.Drawing.Point(13, 48);
            this.cbTipoAumento.Margin = new System.Windows.Forms.Padding(4);
            this.cbTipoAumento.Name = "cbTipoAumento";
            this.cbTipoAumento.Size = new System.Drawing.Size(219, 28);
            this.cbTipoAumento.TabIndex = 2;
            this.cbTipoAumento.SelectedValueChanged += new System.EventHandler(this.cbTipoAumento_SelectedValueChanged);
            this.cbTipoAumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbTipoAumento_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 166);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 75;
            this.label1.Text = "Monto Aumento:";
            // 
            // nudMontoAumento
            // 
            this.nudMontoAumento.DecimalPlaces = 2;
            this.nudMontoAumento.Enabled = false;
            this.nudMontoAumento.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMontoAumento.Location = new System.Drawing.Point(15, 187);
            this.nudMontoAumento.Margin = new System.Windows.Forms.Padding(4);
            this.nudMontoAumento.Maximum = new decimal(new int[] {
            1569324956,
            23283064,
            0,
            131072});
            this.nudMontoAumento.Name = "nudMontoAumento";
            this.nudMontoAumento.Size = new System.Drawing.Size(160, 26);
            this.nudMontoAumento.TabIndex = 5;
            this.nudMontoAumento.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 235);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 20);
            this.label3.TabIndex = 76;
            this.label3.Text = "Nuevo Sueldo:";
            // 
            // nudNuevoSueldo
            // 
            this.nudNuevoSueldo.DecimalPlaces = 2;
            this.nudNuevoSueldo.Enabled = false;
            this.nudNuevoSueldo.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudNuevoSueldo.Location = new System.Drawing.Point(15, 255);
            this.nudNuevoSueldo.Margin = new System.Windows.Forms.Padding(4);
            this.nudNuevoSueldo.Maximum = new decimal(new int[] {
            1569324956,
            23283064,
            0,
            131072});
            this.nudNuevoSueldo.Name = "nudNuevoSueldo";
            this.nudNuevoSueldo.Size = new System.Drawing.Size(160, 26);
            this.nudNuevoSueldo.TabIndex = 77;
            this.nudNuevoSueldo.ThousandsSeparator = true;
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(283, 94);
            this.lblRazon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(57, 20);
            this.lblRazon.TabIndex = 78;
            this.lblRazon.Text = "Razon";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(285, 114);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescripcion.MaxLength = 30;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(309, 57);
            this.txtDescripcion.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(281, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 80;
            this.label2.Text = "Fecha:";
            // 
            // gpLabor
            // 
            this.gpLabor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpLabor.Controls.Add(this.label2);
            this.gpLabor.Controls.Add(this.txtDescripcion);
            this.gpLabor.Controls.Add(this.lblRazon);
            this.gpLabor.Controls.Add(this.nudNuevoSueldo);
            this.gpLabor.Controls.Add(this.label3);
            this.gpLabor.Controls.Add(this.nudMontoAumento);
            this.gpLabor.Controls.Add(this.label1);
            this.gpLabor.Controls.Add(this.cbTipoAumento);
            this.gpLabor.Controls.Add(this.label5);
            this.gpLabor.Controls.Add(this.nudMontoAumentoPorcentaje);
            this.gpLabor.Controls.Add(this.lblMonto);
            this.gpLabor.Controls.Add(this.dtpFechaInicial);
            this.gpLabor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpLabor.Location = new System.Drawing.Point(3, 266);
            this.gpLabor.Margin = new System.Windows.Forms.Padding(4);
            this.gpLabor.Name = "gpLabor";
            this.gpLabor.Padding = new System.Windows.Forms.Padding(4);
            this.gpLabor.Size = new System.Drawing.Size(837, 304);
            this.gpLabor.TabIndex = 70;
            this.gpLabor.TabStop = false;
            this.gpLabor.Text = "Información del Aumento";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(124, 578);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(112, 49);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnCancelar_KeyPress);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.SkyBlue;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(848, 38);
            this.lblTitulo.TabIndex = 71;
            this.lblTitulo.Text = "Agregar Aumento";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAddAumento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(848, 640);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gpLabor);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gpEmpleados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmAddAumento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimiento Descuento";
            this.Load += new System.EventHandler(this.frmAddMovimientoAumento_Load);
            this.gpEmpleados.ResumeLayout(false);
            this.gpEmpleados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAumentoPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAumento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNuevoSueldo)).EndInit();
            this.gpLabor.ResumeLayout(false);
            this.gpLabor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbAlertId;
        private System.Windows.Forms.Label lbTicketId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnBuscarEmpleado;
        private System.Windows.Forms.Label lblNombreEmpleado;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label lblCodigoEmpleado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSalarioFinal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSalarioInicial;
        private System.Windows.Forms.GroupBox gpEmpleados;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.NumericUpDown nudMontoAumentoPorcentaje;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTipoAumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMontoAumento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudNuevoSueldo;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gpLabor;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTitulo;
    }
}