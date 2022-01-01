namespace OnePushSnap
{
    partial class frmImageType
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
            this.cmbImageType = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblImageType = new System.Windows.Forms.Label();
            this.flpImageType = new System.Windows.Forms.FlowLayoutPanel();
            this.flpImageType.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbImageType
            // 
            this.cmbImageType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbImageType.FormattingEnabled = true;
            this.cmbImageType.Location = new System.Drawing.Point(44, 3);
            this.cmbImageType.Name = "cmbImageType";
            this.cmbImageType.Size = new System.Drawing.Size(51, 21);
            this.cmbImageType.Sorted = true;
            this.cmbImageType.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOK.Location = new System.Drawing.Point(101, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(32, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblImageType
            // 
            this.lblImageType.AutoSize = true;
            this.lblImageType.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblImageType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImageType.Location = new System.Drawing.Point(3, 0);
            this.lblImageType.Name = "lblImageType";
            this.lblImageType.Size = new System.Drawing.Size(35, 29);
            this.lblImageType.TabIndex = 2;
            this.lblImageType.Text = "label1";
            this.lblImageType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpImageType
            // 
            this.flpImageType.AutoSize = true;
            this.flpImageType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpImageType.Controls.Add(this.lblImageType);
            this.flpImageType.Controls.Add(this.cmbImageType);
            this.flpImageType.Controls.Add(this.btnOK);
            this.flpImageType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImageType.Location = new System.Drawing.Point(0, 0);
            this.flpImageType.Name = "flpImageType";
            this.flpImageType.Size = new System.Drawing.Size(258, 44);
            this.flpImageType.TabIndex = 3;
            // 
            // frmImageType
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(258, 44);
            this.Controls.Add(this.flpImageType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmImageType";
            this.Text = "Image format";
            this.Load += new System.EventHandler(this.frmImageType_Load);
            this.flpImageType.ResumeLayout(false);
            this.flpImageType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbImageType;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblImageType;
        private System.Windows.Forms.FlowLayoutPanel flpImageType;
    }
}