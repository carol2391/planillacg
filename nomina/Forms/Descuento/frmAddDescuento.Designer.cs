namespace nomina.Forms.Descuento
{
    partial class frmAddDescuento
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
            this.pnDescuento = new System.Windows.Forms.Panel();
            this.nudMonto = new System.Windows.Forms.NumericUpDown();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTipoJornada = new System.Windows.Forms.ComboBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.cbTipoDescuento = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnDescuento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnDescuento
            // 
            this.pnDescuento.AutoSize = true;
            this.pnDescuento.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnDescuento.Controls.Add(this.nudMonto);
            this.pnDescuento.Controls.Add(this.txtCuenta);
            this.pnDescuento.Controls.Add(this.label4);
            this.pnDescuento.Controls.Add(this.cbTipoJornada);
            this.pnDescuento.Controls.Add(this.lblMonto);
            this.pnDescuento.Controls.Add(this.cbTipoDescuento);
            this.pnDescuento.Controls.Add(this.label3);
            this.pnDescuento.Controls.Add(this.txtNombre);
            this.pnDescuento.Controls.Add(this.txtCodigo);
            this.pnDescuento.Controls.Add(this.label7);
            this.pnDescuento.Controls.Add(this.label2);
            this.pnDescuento.Controls.Add(this.label1);
            this.pnDescuento.Location = new System.Drawing.Point(3, 38);
            this.pnDescuento.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnDescuento.Name = "pnDescuento";
            this.pnDescuento.Size = new System.Drawing.Size(423, 194);
            this.pnDescuento.TabIndex = 47;
            // 
            // nudMonto
            // 
            this.nudMonto.DecimalPlaces = 2;
            this.nudMonto.Location = new System.Drawing.Point(238, 145);
            this.nudMonto.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            131072});
            this.nudMonto.Name = "nudMonto";
            this.nudMonto.Size = new System.Drawing.Size(171, 20);
            this.nudMonto.TabIndex = 5;
            this.nudMonto.ThousandsSeparator = true;
            this.nudMonto.Validating += new System.ComponentModel.CancelEventHandler(this.nudMonto_Validating);
            this.nudMonto.Validated += new System.EventHandler(this.nudMonto_Validated);
            // 
            // txtCuenta
            // 
            this.txtCuenta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.txtCuenta.Location = new System.Drawing.Point(238, 91);
            this.txtCuenta.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtCuenta.MaxLength = 8;
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(171, 17);
            this.txtCuenta.TabIndex = 3;
            this.txtCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCuenta_KeyPress);
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
            this.cbTipoJornada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbTipoJornada_KeyPress);
            this.cbTipoJornada.Validating += new System.ComponentModel.CancelEventHandler(this.cbTipoJornada_Validating);
            this.cbTipoJornada.Validated += new System.EventHandler(this.cbTipoJornada_Validated);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(235, 128);
            this.lblMonto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(96, 13);
            this.lblMonto.TabIndex = 17;
            this.lblMonto.Text = "Monto de la Labor:";
            // 
            // cbTipoDescuento
            // 
            this.cbTipoDescuento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoDescuento.FormattingEnabled = true;
            this.cbTipoDescuento.Location = new System.Drawing.Point(13, 144);
            this.cbTipoDescuento.Name = "cbTipoDescuento";
            this.cbTipoDescuento.Size = new System.Drawing.Size(171, 21);
            this.cbTipoDescuento.TabIndex = 4;
            this.cbTipoDescuento.SelectedValueChanged += new System.EventHandler(this.cbTipoDescuento_SelectedValueChanged);
            this.cbTipoDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbTipoDescuento_KeyPress);
            this.cbTipoDescuento.Validating += new System.ComponentModel.CancelEventHandler(this.cbTipoDescuento_Validating);
            this.cbTipoDescuento.Validated += new System.EventHandler(this.cbTipoDescuento_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Tipo de Descuento:";
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
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtNombre.Validating += new System.ComponentModel.CancelEventHandler(this.txtNombre_Validating);
            this.txtNombre.Validated += new System.EventHandler(this.txtNombre_Validated);
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
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            this.txtCodigo.Validated += new System.EventHandler(this.txtCodigo_Validated);
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
            this.btnGuardar.Location = new System.Drawing.Point(3, 236);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(104, 39);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
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
            this.btnCancelar.Location = new System.Drawing.Point(113, 236);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(104, 39);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.lblTitulo.Size = new System.Drawing.Size(437, 37);
            this.lblTitulo.TabIndex = 46;
            this.lblTitulo.Text = "Nuevo Descuento";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAddDescuento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(437, 286);
            this.Controls.Add(this.pnDescuento);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblTitulo);
            this.Name = "frmAddDescuento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Descuento";
            this.Load += new System.EventHandler(this.frmAddDescuento_Load);
            this.pnDescuento.ResumeLayout(false);
            this.pnDescuento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnDescuento;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTipoJornada;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.ComboBox cbTipoDescuento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.NumericUpDown nudMonto;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}