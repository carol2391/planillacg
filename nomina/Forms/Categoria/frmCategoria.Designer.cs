namespace nomina.Forms.Categoria
{
    partial class frmCategoria
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalarioInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalarioFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.bntQuitar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.rbNombre = new System.Windows.Forms.RadioButton();
            this.rbCodigo = new System.Windows.Forms.RadioButton();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 38);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(305, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Categorias";
            // 
            // dgvCategorias
            // 
            this.dgvCategorias.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.dgvCategorias.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCategorias.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategorias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.CodigoCategoria,
            this.NombreCategoria,
            this.SalarioInicial,
            this.SalarioFinal});
            this.dgvCategorias.Location = new System.Drawing.Point(4, 108);
            this.dgvCategorias.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCategorias.Name = "dgvCategorias";
            this.dgvCategorias.RowHeadersWidth = 51;
            this.dgvCategorias.Size = new System.Drawing.Size(639, 507);
            this.dgvCategorias.TabIndex = 1;
            this.dgvCategorias.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCategorias_CellDoubleClick);
            this.dgvCategorias.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvCategorias_PreviewKeyDown);
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
            // CodigoCategoria
            // 
            this.CodigoCategoria.DataPropertyName = "codigoCategoria";
            this.CodigoCategoria.HeaderText = "Codigo";
            this.CodigoCategoria.MinimumWidth = 6;
            this.CodigoCategoria.Name = "CodigoCategoria";
            this.CodigoCategoria.Width = 125;
            // 
            // NombreCategoria
            // 
            this.NombreCategoria.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.NombreCategoria.DataPropertyName = "NombreCategoria";
            this.NombreCategoria.HeaderText = "Nombre de Categoria";
            this.NombreCategoria.MinimumWidth = 6;
            this.NombreCategoria.Name = "NombreCategoria";
            this.NombreCategoria.Width = 152;
            // 
            // SalarioInicial
            // 
            this.SalarioInicial.DataPropertyName = "SalarioInicial";
            this.SalarioInicial.HeaderText = "Salario Inicial";
            this.SalarioInicial.MinimumWidth = 6;
            this.SalarioInicial.Name = "SalarioInicial";
            this.SalarioInicial.Width = 125;
            // 
            // SalarioFinal
            // 
            this.SalarioFinal.DataPropertyName = "SalarioFinal";
            this.SalarioFinal.HeaderText = "SalarioFinal";
            this.SalarioFinal.MinimumWidth = 6;
            this.SalarioFinal.Name = "SalarioFinal";
            this.SalarioFinal.Width = 125;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Location = new System.Drawing.Point(648, 110);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(112, 49);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // bntQuitar
            // 
            this.bntQuitar.BackColor = System.Drawing.Color.RoyalBlue;
            this.bntQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntQuitar.Location = new System.Drawing.Point(648, 223);
            this.bntQuitar.Margin = new System.Windows.Forms.Padding(4);
            this.bntQuitar.Name = "bntQuitar";
            this.bntQuitar.Size = new System.Drawing.Size(112, 49);
            this.bntQuitar.TabIndex = 3;
            this.bntQuitar.Text = "Quitar";
            this.bntQuitar.UseVisualStyleBackColor = false;
            this.bntQuitar.Click += new System.EventHandler(this.BntQuitar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(648, 166);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(112, 49);
            this.btnModificar.TabIndex = 4;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.BtnModificar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.rbNombre);
            this.groupBox1.Controls.Add(this.rbCodigo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 47);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(633, 57);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(372, 21);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.MaxLength = 30;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(252, 24);
            this.txtNombre.TabIndex = 34;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(95, 23);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.MaxLength = 3;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(132, 24);
            this.txtCodigo.TabIndex = 33;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            // 
            // rbNombre
            // 
            this.rbNombre.AutoSize = true;
            this.rbNombre.Location = new System.Drawing.Point(276, 23);
            this.rbNombre.Margin = new System.Windows.Forms.Padding(4);
            this.rbNombre.Name = "rbNombre";
            this.rbNombre.Size = new System.Drawing.Size(87, 22);
            this.rbNombre.TabIndex = 32;
            this.rbNombre.TabStop = true;
            this.rbNombre.Text = "Nombre:";
            this.rbNombre.UseVisualStyleBackColor = true;
            this.rbNombre.Click += new System.EventHandler(this.RbNombre_Click);
            // 
            // rbCodigo
            // 
            this.rbCodigo.AutoSize = true;
            this.rbCodigo.Location = new System.Drawing.Point(8, 26);
            this.rbCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.rbCodigo.Name = "rbCodigo";
            this.rbCodigo.Size = new System.Drawing.Size(81, 22);
            this.rbCodigo.TabIndex = 31;
            this.rbCodigo.TabStop = true;
            this.rbCodigo.Text = "Código:";
            this.rbCodigo.UseVisualStyleBackColor = true;
            this.rbCodigo.Click += new System.EventHandler(this.rbCodigo_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Location = new System.Drawing.Point(648, 279);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(112, 49);
            this.btnSalir.TabIndex = 30;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // frmCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(763, 617);
            this.Controls.Add(this.dgvCategorias);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.bntQuitar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Categorias";
            this.Load += new System.EventHandler(this.frmCategoria_Load);
            this.Shown += new System.EventHandler(this.frmCategoria_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button bntQuitar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.RadioButton rbNombre;
        private System.Windows.Forms.RadioButton rbCodigo;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalarioInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalarioFinal;
    }
}