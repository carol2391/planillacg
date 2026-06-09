namespace nomina.Forms.Empleado
{
    partial class frmHistorialAumento
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
            this.CodigoEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SueldoAnterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SueldoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.cbFechaFinal = new System.Windows.Forms.CheckBox();
            this.cbFechaInicial = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.CodigoEmpleado,
            this.NombreEmpleado,
            this.Fecha,
            this.Categoria,
            this.SueldoAnterior,
            this.Monto,
            this.SueldoActual});
            this.dgvDatos.Location = new System.Drawing.Point(1, 154);
            this.dgvDatos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersWidth = 51;
            this.dgvDatos.Size = new System.Drawing.Size(857, 514);
            this.dgvDatos.TabIndex = 32;
            // 
            // CodigoEmpleado
            // 
            this.CodigoEmpleado.DataPropertyName = "CodigoEmpleado";
            this.CodigoEmpleado.HeaderText = "CodigoEmpleado";
            this.CodigoEmpleado.MinimumWidth = 6;
            this.CodigoEmpleado.Name = "CodigoEmpleado";
            this.CodigoEmpleado.Width = 125;
            // 
            // NombreEmpleado
            // 
            this.NombreEmpleado.DataPropertyName = "Nombre";
            this.NombreEmpleado.HeaderText = "Nombre Empleado";
            this.NombreEmpleado.MinimumWidth = 6;
            this.NombreEmpleado.Name = "NombreEmpleado";
            this.NombreEmpleado.Width = 125;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.MinimumWidth = 6;
            this.Fecha.Name = "Fecha";
            this.Fecha.Width = 125;
            // 
            // Categoria
            // 
            this.Categoria.DataPropertyName = "Categoria";
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.MinimumWidth = 6;
            this.Categoria.Name = "Categoria";
            this.Categoria.Width = 125;
            // 
            // SueldoAnterior
            // 
            this.SueldoAnterior.DataPropertyName = "SueldoAnterior";
            this.SueldoAnterior.HeaderText = "Sueldo Anterior";
            this.SueldoAnterior.MinimumWidth = 6;
            this.SueldoAnterior.Name = "SueldoAnterior";
            this.SueldoAnterior.Width = 125;
            // 
            // Monto
            // 
            this.Monto.DataPropertyName = "Monto";
            this.Monto.HeaderText = "Monto";
            this.Monto.MinimumWidth = 6;
            this.Monto.Name = "Monto";
            this.Monto.Width = 125;
            // 
            // SueldoActual
            // 
            this.SueldoActual.DataPropertyName = "SueldoActual";
            this.SueldoActual.HeaderText = "SueldoActual";
            this.SueldoActual.MinimumWidth = 6;
            this.SueldoActual.Name = "SueldoActual";
            this.SueldoActual.Width = 125;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 38);
            this.panel1.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(305, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Historial Aumentos";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Controls.Add(this.cbFechaFinal);
            this.groupBox1.Controls.Add(this.cbFechaInicial);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dtpFechaFinal);
            this.groupBox1.Controls.Add(this.dtpFechaInicial);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1, 46);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(857, 101);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(27, 25);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(137, 18);
            this.lblNombre.TabIndex = 43;
            this.lblNombre.Text = "Nombre Empleado:";
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
            this.cbFechaFinal.CheckedChanged += new System.EventHandler(this.cbFechaFinal_CheckedChanged);
            this.cbFechaFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbFechaFinal_KeyPress);
            // 
            // cbFechaInicial
            // 
            this.cbFechaInicial.AutoSize = true;
            this.cbFechaInicial.Location = new System.Drawing.Point(30, 57);
            this.cbFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.cbFechaInicial.Name = "cbFechaInicial";
            this.cbFechaInicial.Size = new System.Drawing.Size(115, 22);
            this.cbFechaInicial.TabIndex = 2;
            this.cbFechaInicial.Text = "Fecha Inicial:";
            this.cbFechaInicial.UseVisualStyleBackColor = true;
            this.cbFechaInicial.CheckedChanged += new System.EventHandler(this.cbFechaInicial_CheckedChanged);
            this.cbFechaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbFechaInicial_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Location = new System.Drawing.Point(310, 14);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(105, 31);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(539, 55);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(119, 24);
            this.dtpFechaFinal.TabIndex = 5;
            this.dtpFechaFinal.Value = new System.DateTime(2026, 5, 21, 0, 0, 0, 0);
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
            // frmHistorialAumento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(871, 672);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmHistorialAumento";
            this.Text = "frmHistorialAumento";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.CheckBox cbFechaFinal;
        private System.Windows.Forms.CheckBox cbFechaInicial;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn SueldoAnterior;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn SueldoActual;
    }
}