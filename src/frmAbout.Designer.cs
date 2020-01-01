namespace SinaimgPublisher
{
    partial class frmAbout
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_00 = new System.Windows.Forms.Label();
            this.lb_01 = new System.Windows.Forms.Label();
            this.lb_10 = new System.Windows.Forms.Label();
            this.tx_11 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.lb_00, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_01, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tx_11, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lb_00
            // 
            this.lb_00.AutoSize = true;
            this.lb_00.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_00.Location = new System.Drawing.Point(3, 0);
            this.lb_00.Name = "lb_00";
            this.lb_00.Size = new System.Drawing.Size(74, 20);
            this.lb_00.TabIndex = 0;
            this.lb_00.Text = "版本";
            // 
            // lb_01
            // 
            this.lb_01.AutoSize = true;
            this.lb_01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_01.Location = new System.Drawing.Point(83, 0);
            this.lb_01.Name = "lb_01";
            this.lb_01.Size = new System.Drawing.Size(114, 20);
            this.lb_01.TabIndex = 1;
            this.lb_01.Text = "0.13";
            // 
            // lb_10
            // 
            this.lb_10.AutoSize = true;
            this.lb_10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_10.Location = new System.Drawing.Point(3, 20);
            this.lb_10.Name = "lb_10";
            this.lb_10.Size = new System.Drawing.Size(74, 80);
            this.lb_10.TabIndex = 2;
            this.lb_10.Text = "第三方";
            // 
            // tx_11
            // 
            this.tx_11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tx_11.Enabled = false;
            this.tx_11.Location = new System.Drawing.Point(83, 23);
            this.tx_11.Multiline = true;
            this.tx_11.Name = "tx_11";
            this.tx_11.Size = new System.Drawing.Size(114, 74);
            this.tx_11.TabIndex = 3;
            this.tx_11.Text = "Newtonsoft(json)\r\nRestSharp\r\nrenmengye(base62)";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(222, 125);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAbout_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lb_00;
        private System.Windows.Forms.Label lb_01;
        private System.Windows.Forms.Label lb_10;
        private System.Windows.Forms.TextBox tx_11;
    }
}