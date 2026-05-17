namespace nomina.Forms.Movimientos.Ausencia
{
    partial class frmAddAusencia
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
            this.gpLabor = new System.Windows.Forms.GroupBox();
            this.txtCodigoNomina = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudMontoAusencia = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMontoDias = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFechaFinal = new System.Windows.Forms.CheckBox();
            this.cbFechaInicial = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.cbTipoAusencia = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gpLabor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAusencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoDias)).BeginInit();
            this.gpEmpleados.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpLabor
            // 
            this.gpLabor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpLabor.Controls.Add(this.txtCodigoNomina);
            this.gpLabor.Controls.Add(this.label4);
            this.gpLabor.Controls.Add(this.nudMontoAusencia);
            this.gpLabor.Controls.Add(this.label3);
            this.gpLabor.Controls.Add(this.nudMontoDias);
            this.gpLabor.Controls.Add(this.label1);
            this.gpLabor.Controls.Add(this.cbFechaFinal);
            this.gpLabor.Controls.Add(this.cbFechaInicial);
            this.gpLabor.Controls.Add(this.dtpFechaFinal);
            this.gpLabor.Controls.Add(this.dtpFechaInicial);
            this.gpLabor.Controls.Add(this.cbTipoAusencia);
            this.gpLabor.Controls.Add(this.label2);
            this.gpLabor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpLabor.Location = new System.Drawing.Point(2, 179);
            this.gpLabor.Name = "gpLabor";
            this.gpLabor.Size = new System.Drawing.Size(586, 317);
            this.gpLabor.TabIndex = 61;
            this.gpLabor.TabStop = false;
            this.gpLabor.Text = "Información de la Ausencia:";
            // 
            // txtCodigoNomina
            // 
            this.txtCodigoNomina.Location = new System.Drawing.Point(11, 279);
            this.txtCodigoNomina.Name = "txtCodigoNomina";
            this.txtCodigoNomina.Size = new System.Drawing.Size(120, 22);
            this.txtCodigoNomina.TabIndex = 8;
            this.txtCodigoNomina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 71;
            this.label4.Text = "Código Nomina:";
            // 
            // nudMontoAusencia
            // 
            this.nudMontoAusencia.DecimalPlaces = 2;
            this.nudMontoAusencia.Enabled = false;
            this.nudMontoAusencia.Location = new System.Drawing.Point(11, 225);
            this.nudMontoAusencia.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.nudMontoAusencia.Name = "nudMontoAusencia";
            this.nudMontoAusencia.Size = new System.Drawing.Size(120, 22);
            this.nudMontoAusencia.TabIndex = 7;
            this.nudMontoAusencia.ThousandsSeparator = true;
            this.nudMontoAusencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NudMontoAusencia_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 16);
            this.label3.TabIndex = 69;
            this.label3.Text = "Valor que representa la Ausencia:";
            // 
            // nudMontoDias
            // 
            this.nudMontoDias.Enabled = false;
            this.nudMontoDias.Location = new System.Drawing.Point(13, 168);
            this.nudMontoDias.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.nudMontoDias.Name = "nudMontoDias";
            this.nudMontoDias.Size = new System.Drawing.Size(120, 22);
            this.nudMontoDias.TabIndex = 6;
            this.nudMontoDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NudMontoDias_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 16);
            this.label1.TabIndex = 67;
            this.label1.Text = "Monto Total en días de Ausencia:";
            // 
            // cbFechaFinal
            // 
            this.cbFechaFinal.AutoSize = true;
            this.cbFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFechaFinal.Location = new System.Drawing.Point(205, 80);
            this.cbFechaFinal.Name = "cbFechaFinal";
            this.cbFechaFinal.Size = new System.Drawing.Size(202, 19);
            this.cbFechaFinal.TabIndex = 4;
            this.cbFechaFinal.Text = "Fecha de Finalización Ausencia:";
            this.cbFechaFinal.UseVisualStyleBackColor = true;
            this.cbFechaFinal.CheckedChanged += new System.EventHandler(this.CbFechaFinal_CheckedChanged);
            this.cbFechaFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbFechaFinal_KeyPress);
            // 
            // cbFechaInicial
            // 
            this.cbFechaInicial.AutoSize = true;
            this.cbFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFechaInicial.Location = new System.Drawing.Point(11, 80);
            this.cbFechaInicial.Name = "cbFechaInicial";
            this.cbFechaInicial.Size = new System.Drawing.Size(148, 19);
            this.cbFechaInicial.TabIndex = 2;
            this.cbFechaInicial.Text = "Fecha Inicio Ausencia:";
            this.cbFechaInicial.UseVisualStyleBackColor = true;
            this.cbFechaInicial.CheckedChanged += new System.EventHandler(this.CbFechaInicial_CheckedChanged);
            this.cbFechaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbFechaInicial_KeyPress);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(206, 106);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(90, 22);
            this.dtpFechaFinal.TabIndex = 5;
            this.dtpFechaFinal.Value = new System.DateTime(2019, 8, 26, 18, 3, 0, 0);
            this.dtpFechaFinal.ValueChanged += new System.EventHandler(this.dtpFechaFinal_ValueChanged);
            this.dtpFechaFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DtpFechaFinal_KeyPress);
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(11, 106);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(90, 22);
            this.dtpFechaInicial.TabIndex = 3;
            this.dtpFechaInicial.ValueChanged += new System.EventHandler(this.dtpFechaInicial_ValueChanged);
            this.dtpFechaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DtpFechaInicial_KeyPress);
            // 
            // cbTipoAusencia
            // 
            this.cbTipoAusencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoAusencia.FormattingEnabled = true;
            this.cbTipoAusencia.Location = new System.Drawing.Point(9, 41);
            this.cbTipoAusencia.Name = "cbTipoAusencia";
            this.cbTipoAusencia.Size = new System.Drawing.Size(206, 24);
            this.cbTipoAusencia.TabIndex = 1;
            this.cbTipoAusencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbTipoAusencia_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 41;
            this.label2.Text = "Tipo Ausencia:";
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
            this.gpEmpleados.Location = new System.Drawing.Point(1, 41);
            this.gpEmpleados.Name = "gpEmpleados";
            this.gpEmpleados.Size = new System.Drawing.Size(587, 132);
            this.gpEmpleados.TabIndex = 60;
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
            this.btnBuscarEmpleado.Click += new System.EventHandler(this.BtnBuscarEmpleado_Click);
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
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(92, 502);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 40);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            this.btnCancelar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnCancelar_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Location = new System.Drawing.Point(2, 502);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(84, 40);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            this.btnGuardar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnGuardar_KeyPress);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(592, 37);
            this.lblTitulo.TabIndex = 62;
            this.lblTitulo.Text = "Nueva Ausencia";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.Click += new System.EventHandler(this.LblTitulo_Click);
            // 
            // frmAddAusencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(592, 546);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gpLabor);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gpEmpleados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmAddAusencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva Ausencia";
            this.Load += new System.EventHandler(this.FrmAddAusencia_Load);
            this.gpLabor.ResumeLayout(false);
            this.gpLabor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAusencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoDias)).EndInit();
            this.gpEmpleados.ResumeLayout(false);
            this.gpEmpleados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpLabor;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cbTipoAusencia;
        private System.Windows.Forms.TextBox txtCodigoNomina;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMontoAusencia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMontoDias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbFechaFinal;
        private System.Windows.Forms.CheckBox cbFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label lblTitulo;
    }
}