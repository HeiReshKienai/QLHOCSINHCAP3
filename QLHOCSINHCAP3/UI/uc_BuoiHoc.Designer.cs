namespace QLHOCSINHCAP3.UI {
    partial class uc_QuanLyBuoiHoc {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cbbMaLop = new System.Windows.Forms.ComboBox();
            this.aaaa = new System.Windows.Forms.Label();
            this.cbbMonHoc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbGiaoVien = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbBuoiSo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbTiet = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvBuoiHoc = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuoiHoc)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbMaLop
            // 
            this.cbbMaLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMaLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbMaLop.FormattingEnabled = true;
            this.cbbMaLop.Location = new System.Drawing.Point(187, 84);
            this.cbbMaLop.Margin = new System.Windows.Forms.Padding(2);
            this.cbbMaLop.Name = "cbbMaLop";
            this.cbbMaLop.Size = new System.Drawing.Size(176, 32);
            this.cbbMaLop.TabIndex = 55;
            // 
            // aaaa
            // 
            this.aaaa.AutoSize = true;
            this.aaaa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aaaa.Location = new System.Drawing.Point(86, 84);
            this.aaaa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.aaaa.Name = "aaaa";
            this.aaaa.Size = new System.Drawing.Size(73, 24);
            this.aaaa.TabIndex = 54;
            this.aaaa.Text = "Mã Lớp";
            // 
            // cbbMonHoc
            // 
            this.cbbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMonHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbMonHoc.FormattingEnabled = true;
            this.cbbMonHoc.Location = new System.Drawing.Point(187, 138);
            this.cbbMonHoc.Margin = new System.Windows.Forms.Padding(2);
            this.cbbMonHoc.Name = "cbbMonHoc";
            this.cbbMonHoc.Size = new System.Drawing.Size(176, 32);
            this.cbbMonHoc.TabIndex = 57;
            this.cbbMonHoc.SelectedIndexChanged += new System.EventHandler(this.cbbMonHoc_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 142);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 24);
            this.label3.TabIndex = 56;
            this.label3.Text = "Môn Học";
            // 
            // cbbGiaoVien
            // 
            this.cbbGiaoVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGiaoVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbGiaoVien.FormattingEnabled = true;
            this.cbbGiaoVien.Location = new System.Drawing.Point(187, 197);
            this.cbbGiaoVien.Margin = new System.Windows.Forms.Padding(2);
            this.cbbGiaoVien.Name = "cbbGiaoVien";
            this.cbbGiaoVien.Size = new System.Drawing.Size(176, 32);
            this.cbbGiaoVien.TabIndex = 60;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(80, 197);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 24);
            this.label6.TabIndex = 59;
            this.label6.Text = "Giáo Viên";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(412, 80);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 24);
            this.label7.TabIndex = 61;
            this.label7.Text = "Ngày";
            // 
            // cbbBuoiSo
            // 
            this.cbbBuoiSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBuoiSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbBuoiSo.FormattingEnabled = true;
            this.cbbBuoiSo.Items.AddRange(new object[] {
            "Sáng",
            "Chiều"});
            this.cbbBuoiSo.Location = new System.Drawing.Point(482, 144);
            this.cbbBuoiSo.Margin = new System.Windows.Forms.Padding(2);
            this.cbbBuoiSo.Name = "cbbBuoiSo";
            this.cbbBuoiSo.Size = new System.Drawing.Size(183, 32);
            this.cbbBuoiSo.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(412, 142);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 24);
            this.label4.TabIndex = 63;
            this.label4.Text = "Buổi";
            // 
            // cbbTiet
            // 
            this.cbbTiet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbTiet.FormattingEnabled = true;
            this.cbbTiet.Items.AddRange(new object[] {
            "Tiết 1",
            "Tiết 2",
            "Tiết 3",
            "Tiết 4",
            "Tiết 5"});
            this.cbbTiet.Location = new System.Drawing.Point(482, 205);
            this.cbbTiet.Margin = new System.Windows.Forms.Padding(2);
            this.cbbTiet.Name = "cbbTiet";
            this.cbbTiet.Size = new System.Drawing.Size(183, 32);
            this.cbbTiet.TabIndex = 66;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(412, 203);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 24);
            this.label5.TabIndex = 65;
            this.label5.Text = "Tiết";
            // 
            // dgvBuoiHoc
            // 
            this.dgvBuoiHoc.AllowUserToAddRows = false;
            this.dgvBuoiHoc.AllowUserToDeleteRows = false;
            this.dgvBuoiHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuoiHoc.Location = new System.Drawing.Point(49, 255);
            this.dgvBuoiHoc.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBuoiHoc.Name = "dgvBuoiHoc";
            this.dgvBuoiHoc.ReadOnly = true;
            this.dgvBuoiHoc.RowHeadersWidth = 50;
            this.dgvBuoiHoc.RowTemplate.Height = 33;
            this.dgvBuoiHoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBuoiHoc.Size = new System.Drawing.Size(641, 306);
            this.dgvBuoiHoc.TabIndex = 67;
            this.dgvBuoiHoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBuoiHoc_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(336, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 54;
            this.label1.Text = "Buổi Học";
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(711, 204);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(92, 43);
            this.btnSua.TabIndex = 68;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(712, 137);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(92, 43);
            this.btnXoa.TabIndex = 69;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(711, 75);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(93, 43);
            this.btnThem.TabIndex = 70;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dtpNgay
            // 
            this.dtpNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(482, 79);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(183, 29);
            this.dtpNgay.TabIndex = 71;
            // 
            // uc_QuanLyBuoiHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvBuoiHoc);
            this.Controls.Add(this.cbbTiet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbbBuoiSo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbbGiaoVien);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbbMonHoc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbMaLop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aaaa);
            this.Name = "uc_QuanLyBuoiHoc";
            this.Size = new System.Drawing.Size(852, 579);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuoiHoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbMaLop;
        private System.Windows.Forms.Label aaaa;
        private System.Windows.Forms.ComboBox cbbMonHoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbGiaoVien;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbBuoiSo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbTiet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvBuoiHoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DateTimePicker dtpNgay;
    }
}
