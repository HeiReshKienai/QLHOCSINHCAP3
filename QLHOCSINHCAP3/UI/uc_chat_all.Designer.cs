namespace QLHOCSINHCAP3.UI
{
    partial class uc_chat_all
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
            this.flowLayoutPanel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.send_btn = new System.Windows.Forms.Button();
            this.message_send = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(192, 114);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(528, 360);
            this.flowLayoutPanel1.TabIndex = 23;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(759, 114);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(105, 276);
            this.listBox1.TabIndex = 27;
            this.listBox1.Click += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(192, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 53);
            this.panel1.TabIndex = 24;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(672, 498);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(132, 32);
            this.send_btn.TabIndex = 26;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // message_send
            // 
            this.message_send.Location = new System.Drawing.Point(354, 503);
            this.message_send.Name = "message_send";
            this.message_send.Size = new System.Drawing.Size(299, 22);
            this.message_send.TabIndex = 25;
            // 
            // uc_chat_all
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.message_send);
            this.Name = "uc_chat_all";
            this.Size = new System.Drawing.Size(935, 553);
            this.Load += new System.EventHandler(this.uc_chat_all_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel flowLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.TextBox message_send;
    }
}
