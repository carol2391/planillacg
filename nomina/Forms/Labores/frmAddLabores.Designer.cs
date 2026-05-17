namespace nomina.Forms.Labores
{
    partial class frmAddLabores
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
            this.pnLabor = new System.Windows.Forms.Panel();
            this.nudMontoLabor = new System.Windows.Forms.NumericUpDown();
            this.txtCodigoCuenta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTipoJornada = new System.Windows.Forms.ComboBox();
            this.lblMontoLabor = new System.Windows.Forms.Label();
            this.cbTipoLabor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnLabor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoLabor)).BeginInit();
            this.SuspendLayout();
            // 
            // pnLabor
            // 
            this.pnLabor.AutoSize = true;
            this.pnLabor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnLabor.Controls.Add(this.nudMontoLabor);
            this.pnLabor.Controls.Add(this.txtCodigoCuenta);
            this.pnLabor.Controls.Add(this.label4);
            this.pnLabor.Controls.Add(this.cbTipoJornada);
            this.pnLabor.Controls.Add(this.lblMontoLabor);
            this.pnLabor.Controls.Add(this.cbTipoLabor);
            this.pnLabor.Controls.Add(this.label3);
            this.pnLabor.Controls.Add(this.txtNombre);
            this.pnLabor.Controls.Add(this.txtCodigo);
            this.pnLabor.Controls.Add(this.label7);
            this.pnLabor.Controls.Add(this.label2);
            this.pnLabor.Controls.Add(this.label1);
            this.pnLabor.Location = new System.Drawing.Point(1, 43);
            this.pnLabor.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnLabor.Name = "pnLabor";
            this.pnLabor.Size = new System.Drawing.Size(423, 176);
            this.pnLabor.TabIndex = 43;
            // 
            // nudMontoLabor
            // 
            this.nudMontoLabor.DecimalPlaces = 2;
            this.nudMontoLabor.Location = new System.Drawing.Point(238, 144);
            this.nudMontoLabor.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            131072});
            this.nudMontoLabor.Name = "nudMontoLabor";
            this.nudMontoLabor.Size = new System.Drawing.Size(171, 20);
            this.nudMontoLabor.TabIndex = 5;
            this.nudMontoLabor.ThousandsSeparator = true;
            // 
            // txtCodigoCuenta
            // 
            this.txtCodigoCuenta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodigoCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.txtCodigoCuenta.Location = new System.Drawing.Point(238, 91);
            this.txtCodigoCuenta.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtCodigoCuenta.MaxLength = 30;
            this.txtCodigoCuenta.Name = "txtCodigoCuenta";
            this.txtCodigoCuenta.Size = new System.Drawing.Size(171, 17);
            this.txtCodigoCuenta.TabIndex = 3;
            this.txtCodigoCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCuenta_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 75);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Cuenta:";
            // 
            // cbTipoJornada
            // 
            this.cbTipoJornada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoJornada.FormattingEnabled = true;
            this.cbTipoJornada.Location = new System.Drawing.Point(13, 91);
            this.cbTipoJornada.Name = "cbTipoJornada";
            this.cbTipoJornada.Size = new System.Drawing.Size(171, 21);
            this.cbTipoJornada.TabIndex = 2;
            this.cbTipoJornada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbTipoJornada_KeyPress);
            // 
            // lblMontoLabor
            // 
            this.lblMontoLabor.AutoSize = true;
            this.lblMontoLabor.Location = new System.Drawing.Point(235, 128);
            this.lblMontoLabor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMontoLabor.Name = "lblMontoLabor";
            this.lblMontoLabor.Size = new System.Drawing.Size(55, 13);
            this.lblMontoLabor.TabIndex = 17;
            this.lblMontoLabor.Text = "TipoLabor";
            // 
            // cbTipoLabor
            // 
            this.cbTipoLabor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoLabor.FormattingEnabled = true;
            this.cbTipoLabor.Location = new System.Drawing.Point(13, 144);
            this.cbTipoLabor.Name = "cbTipoLabor";
            this.cbTipoLabor.Size = new System.Drawing.Size(171, 21);
            this.cbTipoLabor.TabIndex = 4;
            this.cbTipoLabor.SelectedValueChanged += new System.EventHandler(this.CbTipoLabor_SelectedValueChanged);
            this.cbTipoLabor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CbTipoLabor_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Tipo de Labor:";
            // 
            // txtNombre
            // 
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.txtNombre.Location = new System.Drawing.Point(238, 35);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtNombre.MaxLength = 30;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(171, 17);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNombre_KeyPress);
            // 
            // txtCodigo
            // 
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.txtCodigo.Location = new System.Drawing.Point(11, 35);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtCodigo.MaxLength = 3;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(171, 17);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodigo_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Tipo de Jornada:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(1, 223);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(104, 39);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            this.btnGuardar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnGuardar_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(111, 223);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(104, 39);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            this.btnCancelar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnCancelar_KeyPress);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(431, 37);
            this.lblTitulo.TabIndex = 42;
            this.lblTitulo.Text = "Nueva Labor";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAddLabores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(431, 266);
            this.Controls.Add(this.pnLabor);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmAddLabores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Labores";
            this.Load += new System.EventHandler(this.FrmAddLabores_Load);
            this.pnLabor.ResumeLayout(false);
            this.pnLabor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoLabor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnLabor;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMontoLabor;
        private System.Windows.Forms.ComboBox cbTipoLabor;
        private System.Windows.Forms.TextBox txtCodigoCuenta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTipoJornada;
        private System.Windows.Forms.NumericUpDown nudMontoLabor;
    }
}