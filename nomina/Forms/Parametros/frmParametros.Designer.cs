namespace nomina.Forms.Parametros
{
    partial class frmParametros
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPeriodo = new System.Windows.Forms.NumericUpDown();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Periodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Excento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SueldoPromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoInicial10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoFinal10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoInicial15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoFinal15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoInicial20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoFinal20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoInicial25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoFinal25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeriodo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Location = new System.Drawing.Point(624, 222);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(84, 40);
            this.btnSalir.TabIndex = 51;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudPeriodo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 46);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 33;
            this.label2.Text = "Periodo:";
            // 
            // nudPeriodo
            // 
            this.nudPeriodo.Location = new System.Drawing.Point(80, 19);
            this.nudPeriodo.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudPeriodo.Name = "nudPeriodo";
            this.nudPeriodo.Size = new System.Drawing.Size(120, 21);
            this.nudPeriodo.TabIndex = 32;
            this.nudPeriodo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NudPeriodo_KeyPress);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(624, 130);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(84, 40);
            this.btnModificar.TabIndex = 49;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.BtnModificar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Location = new System.Drawing.Point(624, 176);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(84, 40);
            this.btnQuitar.TabIndex = 48;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.BtnQuitar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Location = new System.Drawing.Point(624, 84);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(84, 40);
            this.btnNuevo.TabIndex = 47;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.dgvDatos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Periodo,
            this.Excento,
            this.SueldoPromedio,
            this.RangoInicial10,
            this.RangoFinal10,
            this.RangoInicial15,
            this.RangoFinal15,
            this.RangoInicial20,
            this.RangoFinal20,
            this.RangoInicial25,
            this.RangoFinal25});
            this.dgvDatos.Location = new System.Drawing.Point(3, 84);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(615, 412);
            this.dgvDatos.TabIndex = 46;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 31);
            this.panel1.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(316, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parametro";
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // Periodo
            // 
            this.Periodo.DataPropertyName = "Periodo";
            this.Periodo.HeaderText = "Periodo";
            this.Periodo.Name = "Periodo";
            this.Periodo.Width = 70;
            // 
            // Excento
            // 
            this.Excento.DataPropertyName = "Excento";
            this.Excento.HeaderText = "Excento";
            this.Excento.Name = "Excento";
            // 
            // SueldoPromedio
            // 
            this.SueldoPromedio.DataPropertyName = "SueldoPromedio";
            this.SueldoPromedio.HeaderText = "Sueldo Minimo Promedio";
            this.SueldoPromedio.Name = "SueldoPromedio";
            // 
            // RangoInicial10
            // 
            this.RangoInicial10.DataPropertyName = "RangoInicial10";
            this.RangoInicial10.HeaderText = "Rango Inicial 10";
            this.RangoInicial10.Name = "RangoInicial10";
            // 
            // RangoFinal10
            // 
            this.RangoFinal10.DataPropertyName = "RangoFinal10";
            this.RangoFinal10.HeaderText = "Rango Final 10";
            this.RangoFinal10.Name = "RangoFinal10";
            // 
            // RangoInicial15
            // 
            this.RangoInicial15.DataPropertyName = "RangoInicial15";
            this.RangoInicial15.HeaderText = "Rango Inicial 15";
            this.RangoInicial15.Name = "RangoInicial15";
            // 
            // RangoFinal15
            // 
            this.RangoFinal15.DataPropertyName = "RangoFinal15";
            this.RangoFinal15.HeaderText = "Rango Final 15";
            this.RangoFinal15.Name = "RangoFinal15";
            // 
            // RangoInicial20
            // 
            this.RangoInicial20.DataPropertyName = "RangoInicial20";
            this.RangoInicial20.HeaderText = "Rango Inicial 20";
            this.RangoInicial20.Name = "RangoInicial20";
            // 
            // RangoFinal20
            // 
            this.RangoFinal20.DataPropertyName = "RangoFinal20";
            this.RangoFinal20.HeaderText = "Rango Final 20";
            this.RangoFinal20.Name = "RangoFinal20";
            // 
            // RangoInicial25
            // 
            this.RangoInicial25.DataPropertyName = "RangoInicial25";
            this.RangoInicial25.HeaderText = "Rango Inicial 25";
            this.RangoInicial25.Name = "RangoInicial25";
            // 
            // RangoFinal25
            // 
            this.RangoFinal25.DataPropertyName = "RangoFinal25";
            this.RangoFinal25.HeaderText = "Rango Final 25";
            this.RangoFinal25.Name = "RangoFinal25";
            // 
            // frmParametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(712, 515);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "frmParametros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parametros";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeriodo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPeriodo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Periodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Excento;
        private System.Windows.Forms.DataGridViewTextBoxColumn SueldoPromedio;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoInicial10;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoFinal10;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoInicial15;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoFinal15;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoInicial20;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoFinal20;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoInicial25;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoFinal25;
    }
}