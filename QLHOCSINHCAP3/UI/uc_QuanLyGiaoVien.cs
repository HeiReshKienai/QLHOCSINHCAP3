using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLHOCSINHCAP3.Thuvien;
namespace QLHOCSINHCAP3.UI {
    public partial class uc_QuanLyGiaoVien : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017/QLHocSinhCap3");
        private IMongoDatabase db;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        private IMongoCollection<MonHoc> collectionMonHoc;

      
        private void readData() {
            var dataGiaoVien = collectionGiaoVien.Find(new BsonDocument()).ToList();
            dgvQLGV.DataSource = dataGiaoVien;

            var dataMonHoc = collectionMonHoc.Distinct<string>("TenMon", new BsonDocument()).ToList();
            var tempData = new List<string>(dataMonHoc);
            tempData.Insert(0, "Chưa Chọn Môn");
            cbbMonDay.DataSource = tempData;



            var dataTimMon = collectionMonHoc.Distinct<string>("TenMon", new BsonDocument()).ToList();
            var tempData1 = new List<string>(dataMonHoc);
            tempData1.Insert(0, "Tất Cả");
            cbbTimMon.DataSource = tempData1;

            dgvQLGV.Columns["Id"].Visible = false;
            dgvQLGV.Columns["MaGV"].HeaderText = "Mã Giáo Viên";
            dgvQLGV.Columns["MaGV"].Width = 150;
            dgvQLGV.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvQLGV.Columns["HoTen"].Width = 150;
            dgvQLGV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvQLGV.Columns["NgaySinh"].Width = 150;
            dgvQLGV.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvQLGV.Columns["GioiTinh"].Width = 75;
            dgvQLGV.Columns["MonDay"].HeaderText = "Môn Dạy";
            dgvQLGV.Columns["MonDay"].Width = 150;
            dgvQLGV.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvQLGV.Columns["DiaChi"].Width = 150;
            dgvQLGV.Columns["SDT"].HeaderText = "Số Điện Thoại";
            dgvQLGV.Columns["SDT"].Width = 150;
            dgvQLGV.Columns["Email"].HeaderText = "Email";
            dgvQLGV.Columns["Email"].Width = 150;


        }
        public uc_QuanLyGiaoVien() {
            InitializeComponent();

            db = client.GetDatabase("QLHocSinhCap3");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            readData();
            ClearTextBoxes();

            


        }
        private void ClearTextBoxes() {
            txtMa.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtSdt.Clear();
            txtEmail.Clear();

            cbbGioiTinh.SelectedIndex = 0;
            cbbMonDay.SelectedIndex = 0;
            cbbTimMon.SelectedIndex = 0;
            dgvQLGV.ClearSelection();
        }
        private void btnThem_Click(object sender, EventArgs e) {
            //kiem tra rong ma~
            if (string.IsNullOrEmpty(txtMa.Text)) {
                MessageBox.Show("Vui lòng nhập mã GV.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Kiểm tra trùng mã gv
            var maGiaoVienFilter = Builders<GiaoVien>.Filter.Eq("MaGV", txtMa.Text);
            if (collectionGiaoVien.Find(maGiaoVienFilter).Any()) {
                MessageBox.Show("Mã GV đã tồn tại. Vui lòng chọn mã Giáo Viên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtHoTen.Text)) {
                MessageBox.Show("Vui lòng nhập Họ Tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text)) {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSdt.Text)) {
                MessageBox.Show("Vui lòng nhập SDT.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text)) {
                MessageBox.Show("Vui lòng nhập Email.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbMonDay.Text == "Chưa Chọn Môn") {
                MessageBox.Show("Vui lòng chọn môn dạy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            GiaoVien gv = new GiaoVien(txtMa.Text, txtHoTen.Text, dtpNgaySinh.Text, cbbGioiTinh.Text, cbbMonDay.Text,txtDiaChi.Text, txtSdt.Text, txtEmail.Text);

            collectionGiaoVien.InsertOne(gv);

            readData();
            ClearTextBoxes();
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvQLGV.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn Giáo Viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string objectIdToDelete = dgvQLGV.CurrentRow.Cells[0].Value.ToString();
            var filter = Builders<GiaoVien>.Filter.Eq("_id", ObjectId.Parse(objectIdToDelete));

            collectionGiaoVien.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            if (dgvQLGV.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một Giáo viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Kiểm tra trùng mã gv
            var maGVFilter = Builders<GiaoVien>.Filter.Eq("MaLop", txtMa.Text);
            var MaGVHienTai = dgvQLGV.CurrentRow.Cells[1].Value.ToString();
            if (collectionGiaoVien.Find(maGVFilter & Builders<GiaoVien>.Filter.Ne("MaGV", MaGVHienTai)).Any()) {
                MessageBox.Show("Mã lớp đã tồn tại. Vui lòng chọn mã lớp khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtHoTen.Text)) {
                MessageBox.Show("Vui lòng nhập Họ Tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text)) {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSdt.Text)) {
                MessageBox.Show("Vui lòng nhập SDT.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text)) {
                MessageBox.Show("Vui lòng nhập Email.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbMonDay.Text == "Chưa Chọn Môn") {
                MessageBox.Show("Vui lòng chọn môn dạy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GiaoVien gv = new GiaoVien(txtMa.Text, txtHoTen.Text, dtpNgaySinh.Text, cbbGioiTinh.Text, cbbMonDay.Text, txtDiaChi.Text, txtSdt.Text,txtEmail.Text);
            gv.Id = new ObjectId(dgvQLGV.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<GiaoVien>.Update
                .Set(s => s.MaGV, gv.MaGV)
                .Set(s => s.HoTen, gv.HoTen)
                .Set(s => s.NgaySinh, gv.NgaySinh)
                .Set(s => s.GioiTinh, gv.GioiTinh)
                .Set(s => s.MonDay, gv.MonDay)
                .Set(s => s.DiaChi, gv.DiaChi)
                .Set(s => s.SDT, gv.SDT)
                .Set(s => s.Email, gv.Email);
             
        

            collectionGiaoVien.UpdateOne(s => s.Id == gv.Id, update);

            readData();
            ClearTextBoxes();
        }

        private void dgvQLSV_CellClick(object sender, DataGridViewCellEventArgs e) {
            // Kiểm tra xem chỉ số hàng có hợp lệ và DataGridView không rỗng
            if (e.RowIndex >= 0 && e.RowIndex < dgvQLGV.Rows.Count) {
                int index = e.RowIndex;
                    txtMa.Text = dgvQLGV.Rows[index].Cells[1].Value?.ToString();
                    txtHoTen.Text = dgvQLGV.Rows[index].Cells[2].Value?.ToString();
                    dtpNgaySinh.Text = dgvQLGV.Rows[index].Cells[3].Value?.ToString();
                    cbbMonDay.Text = dgvQLGV.Rows[index].Cells[5].Value?.ToString();
                    cbbGioiTinh.Text = dgvQLGV.Rows[index].Cells[4].Value?.ToString();
                    txtDiaChi.Text = dgvQLGV.Rows[index].Cells[6].Value?.ToString();
                    txtSdt.Text = dgvQLGV.Rows[index].Cells[7].Value?.ToString();
                    txtEmail.Text = dgvQLGV.Rows[index].Cells[8].Value?.ToString();
                }            
        }


        private void cbbTimMon_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedMon = cbbTimMon.Text;
            var filteredData = collectionGiaoVien.Find(new BsonDocument()).ToList();
            if (cbbTimMon.Text != "Tất Cả") {
                var filter = Builders<GiaoVien>.Filter.Eq("MonDay", selectedMon);
                filteredData = collectionGiaoVien.Find(filter).ToList();
            }
            dgvQLGV.DataSource = filteredData;
        }

        private void uc_QuanLyGiaoVien_Load(object sender, EventArgs e) {
            db = client.GetDatabase("QLHocSinhCap3");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            readData();
            ClearTextBoxes();
        }
    }
}
