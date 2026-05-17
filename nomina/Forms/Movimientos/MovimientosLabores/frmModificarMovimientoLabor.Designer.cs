namespace nomina.Forms.MovimientosLabores
{
    partial class frmModificarMovimientoLabor
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
            this.nudMontoTotal = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNombreCuenta = new System.Windows.Forms.Label();
            this.nudCantidadLabor = new System.Windows.Forms.NumericUpDown();
            this.lblNombreLabor = new System.Windows.Forms.Label();
            this.lblCodigoLabor = new System.Windows.Forms.Label();
            this.btnBuscarCuenta = new System.Windows.Forms.Button();
            this.nudMontoLabor = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtCodigoCuenta = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbCreatedBy = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcionLabor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.gpEmpleados = new System.Windows.Forms.GroupBox();
            this.lblCodigoEmpleado = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.lblNombreEmpleado = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbTicketId = new System.Windows.Forms.Label();
            this.lbAlertId = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.gpLabor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadLabor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoLabor)).BeginInit();
            this.gpEmpleados.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpLabor
            // 
            this.gpLabor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpLabor.Controls.Add(this.nudMontoTotal);
            this.gpLabor.Controls.Add(this.label5);
            this.gpLabor.Controls.Add(this.lblNombreCuenta);
            this.gpLabor.Controls.Add(this.nudCantidadLabor);
            this.gpLabor.Controls.Add(this.lblNombreLabor);
            this.gpLabor.Controls.Add(this.lblCodigoLabor);
            this.gpLabor.Controls.Add(this.btnBuscarCuenta);
            this.gpLabor.Controls.Add(this.nudMontoLabor);
            this.gpLabor.Controls.Add(this.label10);
            this.gpLabor.Controls.Add(this.dtpFecha);
            this.gpLabor.Controls.Add(this.txtCodigoCuenta);
            this.gpLabor.Controls.Add(this.label11);
            this.gpLabor.Controls.Add(this.label6);
            this.gpLabor.Controls.Add(this.lbCreatedBy);
            this.gpLabor.Controls.Add(this.label4);
            this.gpLabor.Controls.Add(this.label3);
            this.gpLabor.Controls.Add(this.txtDescripcionLabor);
            this.gpLabor.Controls.Add(this.label2);
            this.gpLabor.Controls.Add(this.label1);
            this.gpLabor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpLabor.Location = new System.Drawing.Point(7, 186);
            this.gpLabor.Name = "gpLabor";
            this.gpLabor.Size = new System.Drawing.Size(750, 199);
            this.gpLabor.TabIndex = 35;
            this.gpLabor.TabStop = false;
            this.gpLabor.Text = "Información de la Labor";
            // 
            // nudMontoTotal
            // 
            this.nudMontoTotal.DecimalPlaces = 2;
            this.nudMontoTotal.Enabled = false;
            this.nudMontoTotal.Location = new System.Drawing.Point(607, 89);
            this.nudMontoTotal.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.nudMontoTotal.Name = "nudMontoTotal";
            this.nudMontoTotal.Size = new System.Drawing.Size(120, 22);
            this.nudMontoTotal.TabIndex = 5;
            this.nudMontoTotal.ThousandsSeparator = true;
            this.nudMontoTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudMontoTotal_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(604, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 64;
            this.label5.Text = "Monto Total:";
            // 
            // lblNombreCuenta
            // 
            this.lblNombreCuenta.AutoSize = true;
            this.lblNombreCuenta.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNombreCuenta.Location = new System.Drawing.Point(273, 162);
            this.lblNombreCuenta.Name = "lblNombreCuenta";
            this.lblNombreCuenta.Size = new System.Drawing.Size(45, 16);
            this.lblNombreCuenta.TabIndex = 62;
            this.lblNombreCuenta.Text = "label9";
            // 
            // nudCantidadLabor
            // 
            this.nudCantidadLabor.DecimalPlaces = 2;
            this.nudCantidadLabor.Enabled = false;
            this.nudCantidadLabor.Location = new System.Drawing.Point(467, 89);
            this.nudCantidadLabor.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            131072});
            this.nudCantidadLabor.Name = "nudCantidadLabor";
            this.nudCantidadLabor.Size = new System.Drawing.Size(120, 22);
            this.nudCantidadLabor.TabIndex = 4;
            this.nudCantidadLabor.ThousandsSeparator = true;
            this.nudCantidadLabor.ValueChanged += new System.EventHandler(this.nudCantidadLabor_ValueChanged);
            this.nudCantidadLabor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudCantidadLabor_KeyPress);
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
            // lblCodigoLabor
            // 
            this.lblCodigoLabor.AutoSize = true;
            this.lblCodigoLabor.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCodigoLabor.Location = new System.Drawing.Point(6, 41);
            this.lblCodigoLabor.Name = "lblCodigoLabor";
            this.lblCodigoLabor.Size = new System.Drawing.Size(45, 16);
            this.lblCodigoLabor.TabIndex = 43;
            this.lblCodigoLabor.Text = "label9";
            // 
            // btnBuscarCuenta
            // 
            this.btnBuscarCuenta.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarCuenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCuenta.Location = new System.Drawing.Point(163, 159);
            this.btnBuscarCuenta.Name = "btnBuscarCuenta";
            this.btnBuscarCuenta.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarCuenta.TabIndex = 6;
            this.btnBuscarCuenta.Text = "Buscar";
            this.btnBuscarCuenta.UseVisualStyleBackColor = false;
            // 
            // nudMontoLabor
            // 
            this.nudMontoLabor.DecimalPlaces = 2;
            this.nudMontoLabor.Enabled = false;
            this.nudMontoLabor.Location = new System.Drawing.Point(276, 89);
            this.nudMontoLabor.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            131072});
            this.nudMontoLabor.Name = "nudMontoLabor";
            this.nudMontoLabor.Size = new System.Drawing.Size(173, 22);
            this.nudMontoLabor.TabIndex = 3;
            this.nudMontoLabor.ThousandsSeparator = true;
            this.nudMontoLabor.ValueChanged += new System.EventHandler(this.nudMontoLabor_ValueChanged);
            this.nudMontoLabor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudMontoLabor_KeyPress);
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
            this.dtpFecha.Location = new System.Drawing.Point(466, 35);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(145, 22);
            this.dtpFecha.TabIndex = 1;
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFecha_KeyPress);
            // 
            // txtCodigoCuenta
            // 
            this.txtCodigoCuenta.Location = new System.Drawing.Point(12, 160);
            this.txtCodigoCuenta.MaxLength = 5;
            this.txtCodigoCuenta.Name = "txtCodigoCuenta";
            this.txtCodigoCuenta.Size = new System.Drawing.Size(145, 22);
            this.txtCodigoCuenta.TabIndex = 56;
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
            this.label6.Location = new System.Drawing.Point(273, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 50;
            this.label6.Text = "Monto:";
            // 
            // lbCreatedBy
            // 
            this.lbCreatedBy.AutoSize = true;
            this.lbCreatedBy.Location = new System.Drawing.Point(464, 18);
            this.lbCreatedBy.Name = "lbCreatedBy";
            this.lbCreatedBy.Size = new System.Drawing.Size(49, 16);
            this.lbCreatedBy.TabIndex = 37;
            this.lbCreatedBy.Text = "Fecha:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(463, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "Cantidad de la Labor:";
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
            // txtDescripcionLabor
            // 
            this.txtDescripcionLabor.Location = new System.Drawing.Point(9, 92);
            this.txtDescripcionLabor.MaxLength = 30;
            this.txtDescripcionLabor.Multiline = true;
            this.txtDescripcionLabor.Name = "txtDescripcionLabor";
            this.txtDescripcionLabor.Size = new System.Drawing.Size(229, 37);
            this.txtDescripcionLabor.TabIndex = 2;
            this.txtDescripcionLabor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcionLabor_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 41;
            this.label2.Text = "Código de la Labor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(270, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Nombre del la Labor:";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Location = new System.Drawing.Point(97, 391);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(84, 40);
            this.btnSalir.TabIndex = 40;
            this.btnSalir.Text = "Cancelar";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(7, 391);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(84, 40);
            this.btnModificar.TabIndex = 39;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // gpEmpleados
            // 
            this.gpEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gpEmpleados.Controls.Add(this.lblCodigoEmpleado);
            this.gpEmpleados.Controls.Add(this.lblDepartamento);
            this.gpEmpleados.Controls.Add(this.lblSueldo);
            this.gpEmpleados.Controls.Add(this.lblNombreEmpleado);
            this.gpEmpleados.Controls.Add(this.label18);
            this.gpEmpleados.Controls.Add(this.label12);
            this.gpEmpleados.Controls.Add(this.lbTicketId);
            this.gpEmpleados.Controls.Add(this.lbAlertId);
            this.gpEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpEmpleados.Location = new System.Drawing.Point(12, 46);
            this.gpEmpleados.Name = "gpEmpleados";
            this.gpEmpleados.Size = new System.Drawing.Size(745, 134);
            this.gpEmpleados.TabIndex = 41;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(7, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 31);
            this.panel1.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(229, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(270, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "Modificar Movimientos Labores";
            // 
            // frmModificarMovimientoLabor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(773, 438);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gpEmpleados);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.gpLabor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmModificarMovimientoLabor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificar Movimiento Labor";
            this.gpLabor.ResumeLayout(false);
            this.gpLabor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadLabor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoLabor)).EndInit();
            this.gpEmpleados.ResumeLayout(false);
            this.gpEmpleados.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpLabor;
        private System.Windows.Forms.NumericUpDown nudCantidadLabor;
        private System.Windows.Forms.Label lblNombreLabor;
        private System.Windows.Forms.Label lblCodigoLabor;
        private System.Windows.Forms.Button btnBuscarCuenta;
        private System.Windows.Forms.NumericUpDown nudMontoLabor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtCodigoCuenta;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbCreatedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcionLabor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.GroupBox gpEmpleados;
        private System.Windows.Forms.Label lblCodigoEmpleado;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Label lblNombreEmpleado;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbTicketId;
        private System.Windows.Forms.Label lbAlertId;
        private System.Windows.Forms.Label lblNombreCuenta;
        private System.Windows.Forms.NumericUpDown nudMontoTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
    }
}