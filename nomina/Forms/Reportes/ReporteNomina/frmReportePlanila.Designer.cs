namespace nomina.Forms.Reportes.ReporteNomina
{
    partial class frmReportePlanila
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
            this.gbplanila = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAnio = new System.Windows.Forms.ComboBox();
            this.cbMes = new System.Windows.Forms.ComboBox();
            this.gbplanila.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbplanila
            // 
            this.gbplanila.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(214)))), ((int)(((byte)(241)))));
            this.gbplanila.Controls.Add(this.cbMes);
            this.gbplanila.Controls.Add(this.cbAnio);
            this.gbplanila.Controls.Add(this.label1);
            this.gbplanila.Controls.Add(this.btnGenerar);
            this.gbplanila.Controls.Add(this.btnCancelar);
            this.gbplanila.Controls.Add(this.label2);
            this.gbplanila.Location = new System.Drawing.Point(12, 15);
            this.gbplanila.Name = "gbplanila";
            this.gbplanila.Size = new System.Drawing.Size(361, 191);
            this.gbplanila.TabIndex = 1;
            this.gbplanila.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Año:";
            // 
            // btnGenerar
            // 
            this.btnGenerar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnGenerar.ForeColor = System.Drawing.Color.Black;
            this.btnGenerar.Location = new System.Drawing.Point(41, 131);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(139, 48);
            this.btnGenerar.TabIndex = 8;
            this.btnGenerar.Text = "Ver";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(188, 131);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(139, 48);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mes:";
            // 
            // cbAnio
            // 
            this.cbAnio.FormattingEnabled = true;
            this.cbAnio.Location = new System.Drawing.Point(80, 82);
            this.cbAnio.Name = "cbAnio";
            this.cbAnio.Size = new System.Drawing.Size(163, 24);
            this.cbAnio.TabIndex = 11;
            // 
            // cbMes
            // 
            this.cbMes.FormattingEnabled = true;
            this.cbMes.Location = new System.Drawing.Point(80, 33);
            this.cbMes.Name = "cbMes";
            this.cbMes.Size = new System.Drawing.Size(163, 24);
            this.cbMes.TabIndex = 12;
            // 
            // frmReportePlanila
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(195)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(386, 216);
            this.Controls.Add(this.gbplanila);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportePlanila";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReportePlanila";
            this.Load += new System.EventHandler(this.frmReportePlanila_Load);
            this.gbplanila.ResumeLayout(false);
            this.gbplanila.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbplanila;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMes;
        private System.Windows.Forms.ComboBox cbAnio;
    }
}