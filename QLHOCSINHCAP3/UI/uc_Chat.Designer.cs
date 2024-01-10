namespace QLHOCSINHCAP3.UI
{
    partial class uc_Chat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.name_user = new System.Windows.Forms.Label();
            this.gvsend = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // name_user
            // 
            this.name_user.AutoSize = true;
            this.name_user.Location = new System.Drawing.Point(88, 16);
            this.name_user.Name = "name_user";
            this.name_user.Size = new System.Drawing.Size(0, 16);
            this.name_user.TabIndex = 9;
            // 
            // gvsend
            // 
            this.gvsend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvsend.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gvsend.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvsend.Location = new System.Drawing.Point(58, 0);
            this.gvsend.Margin = new System.Windows.Forms.Padding(8);
            this.gvsend.Name = "gvsend";
            this.gvsend.Size = new System.Drawing.Size(347, 45);
            this.gvsend.TabIndex = 8;
            // 
            // uc_Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.name_user);
            this.Controls.Add(this.gvsend);
            this.Name = "uc_Chat";
            this.Size = new System.Drawing.Size(405, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label name_user;
        private System.Windows.Forms.TextBox gvsend;
    }
}
