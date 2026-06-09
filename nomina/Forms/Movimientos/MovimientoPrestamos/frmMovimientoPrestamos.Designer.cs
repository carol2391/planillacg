namespace nomina.Forms.Movimientos.MovimientoPrestamos
{
    partial class frmMovimientoPrestamos
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
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoPrestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuotaMes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoAumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreDepartamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdTipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbFechaFinal = new System.Windows.Forms.CheckBox();
            this.cbFechaInicial = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.CodigoEmpleado,
            this.NombreEmpleado,
            this.CodigoPrestamo,
            this.Fecha,
            this.Monto,
            this.SaldoActual,
            this.CuotaMes,
            this.Tiempo,
            this.Estado,
            this.TipoAumento,
            this.Descripcion,
            this.NombreDepartamento,
            this.NombreCategoria,
            this.IdTipoPago,
            this.TipoPago,
            this.Cuota});
            this.dgvDatos.Location = new System.Drawing.Point(5, 146);
            this.dgvDatos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersWidth = 51;
            this.dgvDatos.Size = new System.Drawing.Size(1016, 405);
            this.dgvDatos.TabIndex = 82;
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
            this.NombreEmpleado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.NombreEmpleado.DataPropertyName = "NombreEmpleado";
            this.NombreEmpleado.HeaderText = "Nombre Empleado";
            this.NombreEmpleado.MinimumWidth = 6;
            this.NombreEmpleado.Name = "NombreEmpleado";
            this.NombreEmpleado.Width = 138;
            // 
            // CodigoPrestamo
            // 
            this.CodigoPrestamo.DataPropertyName = "CodigoPrestamo";
            this.CodigoPrestamo.HeaderText = "Código Prestamo";
            this.CodigoPrestamo.MinimumWidth = 6;
            this.CodigoPrestamo.Name = "CodigoPrestamo";
            this.CodigoPrestamo.Width = 125;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.MinimumWidth = 6;
            this.Fecha.Name = "Fecha";
            this.Fecha.Width = 125;
            // 
            // Monto
            // 
            this.Monto.DataPropertyName = "Monto";
            this.Monto.HeaderText = "Monto";
            this.Monto.MinimumWidth = 6;
            this.Monto.Name = "Monto";
            this.Monto.Width = 125;
            // 
            // SaldoActual
            // 
            this.SaldoActual.DataPropertyName = "MontoActual";
            this.SaldoActual.HeaderText = "SaldoActual";
            this.SaldoActual.MinimumWidth = 6;
            this.SaldoActual.Name = "SaldoActual";
            this.SaldoActual.Width = 125;
            // 
            // CuotaMes
            // 
            this.CuotaMes.DataPropertyName = "CuotaMes";
            this.CuotaMes.HeaderText = "Cuota Mes";
            this.CuotaMes.MinimumWidth = 6;
            this.CuotaMes.Name = "CuotaMes";
            this.CuotaMes.Width = 125;
            // 
            // Tiempo
            // 
            this.Tiempo.DataPropertyName = "Tiempo";
            this.Tiempo.HeaderText = "Tiempo";
            this.Tiempo.MinimumWidth = 6;
            this.Tiempo.Name = "Tiempo";
            this.Tiempo.Width = 125;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 6;
            this.Estado.Name = "Estado";
            this.Estado.Width = 125;
            // 
            // TipoAumento
            // 
            this.TipoAumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TipoAumento.DataPropertyName = "DescripcionTipoPago";
            this.TipoAumento.HeaderText = "Tipo Pago";
            this.TipoAumento.MinimumWidth = 6;
            this.TipoAumento.Name = "TipoAumento";
            this.TipoAumento.Width = 92;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.MinimumWidth = 6;
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.Width = 125;
            // 
            // NombreDepartamento
            // 
            this.NombreDepartamento.DataPropertyName = "NombreDepartamento";
            this.NombreDepartamento.HeaderText = "Departamento";
            this.NombreDepartamento.MinimumWidth = 6;
            this.NombreDepartamento.Name = "NombreDepartamento";
            this.NombreDepartamento.Width = 125;
            // 
            // NombreCategoria
            // 
            this.NombreCategoria.DataPropertyName = "NombreCategoria";
            this.NombreCategoria.HeaderText = "Categoria";
            this.NombreCategoria.MinimumWidth = 6;
            this.NombreCategoria.Name = "NombreCategoria";
            this.NombreCategoria.Visible = false;
            this.NombreCategoria.Width = 125;
            // 
            // IdTipoPago
            // 
            this.IdTipoPago.DataPropertyName = "IdTipoPago";
            this.IdTipoPago.HeaderText = "IdTipoPago";
            this.IdTipoPago.MinimumWidth = 6;
            this.IdTipoPago.Name = "IdTipoPago";
            this.IdTipoPago.Visible = false;
            this.IdTipoPago.Width = 125;
            // 
            // TipoPago
            // 
            this.TipoPago.DataPropertyName = "TipoPago";
            this.TipoPago.HeaderText = "TipoPago";
            this.TipoPago.MinimumWidth = 6;
            this.TipoPago.Name = "TipoPago";
            this.TipoPago.Visible = false;
            this.TipoPago.Width = 125;
            // 
            // Cuota
            // 
            this.Cuota.DataPropertyName = "Cuota";
            this.Cuota.HeaderText = "Cuota";
            this.Cuota.MinimumWidth = 6;
            this.Cuota.Name = "Cuota";
            this.Cuota.Visible = false;
            this.Cuota.Width = 125;
            // 
            // cbFechaFinal
            // 
            this.cbFechaFinal.AutoSize = true;
            this.cbFechaFinal.Location = new System.Drawing.Point(415, 58);
            this.cbFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.cbFechaFinal.Name = "cbFechaFinal";
            this.cbFechaFinal.Size = new System.Drawing.Size(110, 22);
            this.cbFechaFinal.TabIndex = 4;
            this.cbFechaFinal.Text = "Fecha Final:";
            this.cbFechaFinal.UseVisualStyleBackColor = true;
            this.cbFechaFinal.CheckedChanged += new System.EventHandler(this.CbFechaFinal_CheckedChanged);
            this.cbFechaFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbFechaFinal_KeyPress);
            // 
            // cbFechaInicial
            // 
            this.cbFechaInicial.AutoSize = true;
            this.cbFechaInicial.Location = new System.Drawing.Point(41, 55);
            this.cbFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.cbFechaInicial.Name = "cbFechaInicial";
            this.cbFechaInicial.Size = new System.Drawing.Size(115, 22);
            this.cbFechaInicial.TabIndex = 2;
            this.cbFechaInicial.Text = "Fecha Inicial:";
            this.cbFechaInicial.UseVisualStyleBackColor = true;
            this.cbFechaInicial.CheckedChanged += new System.EventHandler(this.CbFechaInicial_CheckedChanged);
            this.cbFechaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbFechaInicial_KeyPress);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(539, 55);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(119, 24);
            this.dtpFechaFinal.TabIndex = 5;
            this.dtpFechaFinal.Value = new System.DateTime(2019, 8, 19, 18, 3, 0, 0);
            this.dtpFechaFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DtpFechaFinal_KeyPress);
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(172, 53);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(119, 24);
            this.dtpFechaInicial.TabIndex = 3;
            this.dtpFechaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DtpFechaInicial_KeyPress);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(172, 17);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.MaxLength = 5;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(119, 24);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodigo_KeyPress);
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
            this.groupBox1.Location = new System.Drawing.Point(4, 47);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1016, 92);
            this.groupBox1.TabIndex = 81;
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
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Location = new System.Drawing.Point(300, 15);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(105, 31);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            this.btnBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnBuscar_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(4, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 38);
            this.panel1.TabIndex = 80;
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
            this.label1.Text = "Movimiento Prestamos";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.SkyBlue;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Location = new System.Drawing.Point(1028, 316);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(112, 49);
            this.btnSalir.TabIndex = 86;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(1028, 203);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(112, 49);
            this.btnModificar.TabIndex = 85;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.BtnModificar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Location = new System.Drawing.Point(1028, 260);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(112, 49);
            this.btnQuitar.TabIndex = 84;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.BtnQuitar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.SkyBlue;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Location = new System.Drawing.Point(1028, 146);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(112, 49);
            this.btnNuevo.TabIndex = 83;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // frmMovimientoPrestamos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1141, 554);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmMovimientoPrestamos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimiento Prestamos";
            this.Shown += new System.EventHandler(this.frmMovimientoPrestamos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.CheckBox cbFechaFinal;
        private System.Windows.Forms.CheckBox cbFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoPrestamo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuotaMes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tiempo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoAumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreDepartamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTipoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cuota;
    }
}