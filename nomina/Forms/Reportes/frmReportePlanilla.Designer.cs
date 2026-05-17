namespace nomina.Forms.Reportes
{
    partial class frmReportePlanilla
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
            this.crvPlanilla = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
           // this.rptPlanilla1 = new nomina.rptPlanilla();
            this.SuspendLayout();
            // 
            // crvPlanilla
            // 
            this.crvPlanilla.ActiveViewIndex = -1;
            this.crvPlanilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvPlanilla.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvPlanilla.DisplayStatusBar = false;
            this.crvPlanilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvPlanilla.Location = new System.Drawing.Point(0, 0);
            this.crvPlanilla.Name = "crvPlanilla";
           // this.crvPlanilla.ReportSource = this.rptPlanilla1;
            this.crvPlanilla.Size = new System.Drawing.Size(872, 469);
            this.crvPlanilla.TabIndex = 0;
            this.crvPlanilla.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmReportePlanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 469);
            this.Controls.Add(this.crvPlanilla);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmReportePlanilla";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Planilla";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvPlanilla;
        private rptPlanilla rptPlanilla1;
    }
}