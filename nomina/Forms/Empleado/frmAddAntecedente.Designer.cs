namespace nomina.Forms.Empleado
{
    partial class frmAddAntecedente
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblNumeroAntecedente = new System.Windows.Forms.Label();
            this.txtLugarOrigen = new System.Windows.Forms.TextBox();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.dtpVigencia = new System.Windows.Forms.DateTimePicker();
            this.nudNumeroAntecedente = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeroAntecedente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.lblNumeroAntecedente);
            this.groupBox2.Controls.Add(this.txtLugarOrigen);
            this.groupBox2.Controls.Add(this.dtpFechaVencimiento);
            this.groupBox2.Controls.Add(this.dtpFechaEmision);
            this.groupBox2.Controls.Add(this.dtpVigencia);
            this.groupBox2.Controls.Add(this.nudNumeroAntecedente);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(476, 232);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información Antecedente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Fecha Vencimiento";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 155);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(103, 16);
            this.label26.TabIndex = 9;
            this.label26.Text = "Lugar de Origen";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(276, 93);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(60, 16);
            this.label25.TabIndex = 8;
            this.label25.Text = "Vigencia";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(276, 33);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(96, 16);
            this.label20.TabIndex = 6;
            this.label20.Text = "Fecha Emisión";
            // 
            // lblNumeroAntecedente
            // 
            this.lblNumeroAntecedente.AutoSize = true;
            this.lblNumeroAntecedente.Location = new System.Drawing.Point(8, 33);
            this.lblNumeroAntecedente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumeroAntecedente.Name = "lblNumeroAntecedente";
            this.lblNumeroAntecedente.Size = new System.Drawing.Size(134, 16);
            this.lblNumeroAntecedente.TabIndex = 5;
            this.lblNumeroAntecedente.Text = "Número Antecedente";
            // 
            // txtLugarOrigen
            // 
            this.txtLugarOrigen.Location = new System.Drawing.Point(8, 174);
            this.txtLugarOrigen.Margin = new System.Windows.Forms.Padding(4);
            this.txtLugarOrigen.Name = "txtLugarOrigen";
            this.txtLugarOrigen.Size = new System.Drawing.Size(432, 22);
            this.txtLugarOrigen.TabIndex = 5;
            this.txtLugarOrigen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLugarOrigen_KeyPress);
            this.txtLugarOrigen.Validating += new System.ComponentModel.CancelEventHandler(this.txtLugarOrigen_Validating);
            this.txtLugarOrigen.Validated += new System.EventHandler(this.txtLugarOrigen_Validated);
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(12, 113);
            this.dtpFechaVencimiento.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(160, 22);
            this.dtpFechaVencimiento.TabIndex = 3;
            this.dtpFechaVencimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaVencimiento_KeyPress);
            this.dtpFechaVencimiento.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaVencimiento_Validating);
            this.dtpFechaVencimiento.Validated += new System.EventHandler(this.dtpFechaVencimiento_Validated);
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmision.Location = new System.Drawing.Point(280, 53);
            this.dtpFechaEmision.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Size = new System.Drawing.Size(160, 22);
            this.dtpFechaEmision.TabIndex = 2;
            this.dtpFechaEmision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaEmision_KeyPress);
            this.dtpFechaEmision.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaEmision_Validating);
            this.dtpFechaEmision.Validated += new System.EventHandler(this.dtpFechaEmision_Validated);
            // 
            // dtpVigencia
            // 
            this.dtpVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVigencia.Location = new System.Drawing.Point(280, 113);
            this.dtpVigencia.Margin = new System.Windows.Forms.Padding(4);
            this.dtpVigencia.Name = "dtpVigencia";
            this.dtpVigencia.Size = new System.Drawing.Size(160, 22);
            this.dtpVigencia.TabIndex = 4;
            this.dtpVigencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpVigencia_KeyPress);
            this.dtpVigencia.Validating += new System.ComponentModel.CancelEventHandler(this.dtpVigencia_Validating);
            this.dtpVigencia.Validated += new System.EventHandler(this.dtpVigencia_Validated);
            // 
            // nudNumeroAntecedente
            // 
            this.nudNumeroAntecedente.Location = new System.Drawing.Point(8, 53);
            this.nudNumeroAntecedente.Margin = new System.Windows.Forms.Padding(4);
            this.nudNumeroAntecedente.Name = "nudNumeroAntecedente";
            this.nudNumeroAntecedente.Size = new System.Drawing.Size(160, 22);
            this.nudNumeroAntecedente.TabIndex = 1;
            this.nudNumeroAntecedente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudNumeroAntecedente_KeyPress);
            this.nudNumeroAntecedente.Validating += new System.ComponentModel.CancelEventHandler(this.nudNumeroAntecedente_Validating);
            this.nudNumeroAntecedente.Validated += new System.EventHandler(this.nudNumeroAntecedente_Validated);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(16, 255);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(139, 48);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnGuardar_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(163, 255);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(139, 48);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAddAntecedente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(512, 312);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAddAntecedente";
            this.Text = "frmAntecedente";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeroAntecedente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblNumeroAntecedente;
        private System.Windows.Forms.TextBox txtLugarOrigen;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaEmision;
        private System.Windows.Forms.DateTimePicker dtpVigencia;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.NumericUpDown nudNumeroAntecedente;
        private System.Windows.Forms.Label label1;
    }
}