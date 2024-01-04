using DevExpress.ClipboardSource.SpreadsheetML;
using MongoDB.Bson;
using MongoDB.Driver;
using QLHOCSINHCAP3.Thuvien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHOCSINHCAP3.UI {
    public partial class uc_QuanLyTaiKhoanGiaoVien : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanGiaoVien> collectionTaiKhoanGiaoVien;
        private IMongoCollection<MonHoc> collectionMonHoc;
        private IMongoCollection<GiaoVien> collectionGiaoVien;

        private void readData() {
            var dataTaiKhoanGiaoVien = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();
            var dataGiaoVien = collectionGiaoVien.Find(new BsonDocument()).ToList();

            var LocChuaTK = dataGiaoVien.Where(item2 => !dataTaiKhoanGiaoVien.Any(item1 => item1.MaGV == item2.MaGV)).ToList();

            dgvCoTK.DataSource = dataTaiKhoanGiaoVien;
            dgvChuaTK.DataSource = LocChuaTK;



            var dataMonHoc = collectionMonHoc.Distinct<string>("TenMon", new BsonDocument()).ToList();
            var tempData = new List<string>(dataMonHoc);
            tempData.Insert(0, "Tất Cả");

            cbbTimMon.DataSource = tempData;

            dgvCoTK.Columns["Id"].Visible = false;
            dgvCoTK.Columns["MaGV"].HeaderText = "Mã Giáo Viên";
            dgvCoTK.Columns["MaGV"].Width = 150;
            dgvCoTK.Columns["MatKhau"].HeaderText = "Mật Khẩu";
            dgvCoTK.Columns["MatKhau"].Width = 150;

            dgvCoTK.Columns["Email"].Width = 200;

            dgvChuaTK.Columns["Id"].Visible = false;
            dgvChuaTK.Columns["MaGV"].HeaderText = "Mã Giáo Viên";
            dgvChuaTK.Columns["MaGV"].Width = 150;
            dgvChuaTK.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvChuaTK.Columns["HoTen"].Width = 150;
            dgvChuaTK.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvChuaTK.Columns["NgaySinh"].Width = 150;
            dgvChuaTK.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvChuaTK.Columns["GioiTinh"].Width = 75;
            dgvChuaTK.Columns["MonDay"].HeaderText = "Môn Dạy";
            dgvChuaTK.Columns["MonDay"].Width = 150;
            dgvChuaTK.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvChuaTK.Columns["DiaChi"].Width = 150;
            dgvChuaTK.Columns["SDT"].HeaderText = "Số Điện Thoại";
            dgvChuaTK.Columns["SDT"].Width = 150;



        }
        public uc_QuanLyTaiKhoanGiaoVien() {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");
            collectionTaiKhoanGiaoVien = db.GetCollection<TaiKhoanGiaoVien>("TaiKhoanGiaoVien");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            readData();


        }
        public static string HashPassword(string password) {
            using (SHA256 sha256 = SHA256.Create()) {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2")); // Chuyển đổi byte thành dạng chuỗi hex
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword) {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return string.Equals(hashedEnteredPassword, storedHashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        private void ClearTextBoxes() {

            txtMaGV.Clear();
            txtMatKhau.Clear();
            txtEmail.Clear();
            dgvCoTK.ClearSelection();
            dgvChuaTK.ClearSelection();

        }
        private void btnThem_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtMaGV.Text)) {
                MessageBox.Show("Vui lòng nhập Mã Giáo Viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var GiaoVienFilter = Builders<GiaoVien>.Filter.Eq("MaGV", txtMaGV.Text);
            if (!collectionGiaoVien.Find(GiaoVienFilter).Any()) {
                MessageBox.Show("Không tìm thấy Giáo Viên có mã này vui lòng kiểm tra lại mã.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var TaiKhoanGiaoVienFilter = Builders<TaiKhoanGiaoVien>.Filter.Eq("MaGV", txtMaGV.Text);
            if (collectionTaiKhoanGiaoVien.Find(TaiKhoanGiaoVienFilter).Any()) {
                MessageBox.Show("Giáo Viên có Mã Giáo Viên này đã có tài khoản. Vui lòng chọn mã Giáo Viên  khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtMatKhau.Text)) {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string matkhau = txtMatKhau.Text;
            string hashPassword = HashPassword(matkhau);

            if (txtEmail.Text == "") {
                txtEmail.Text = "Chưa Có Email";
            }
            TaiKhoanGiaoVien tk = new TaiKhoanGiaoVien(txtMaGV.Text, hashPassword, txtEmail.Text);

            collectionTaiKhoanGiaoVien.InsertOne(tk);

            readData();
            ClearTextBoxes();
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvCoTK.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn Tài Khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string objectIdToDelete = dgvCoTK.CurrentRow.Cells[0].Value.ToString();
            var filter = Builders<TaiKhoanGiaoVien>.Filter.Eq("_id", ObjectId.Parse(objectIdToDelete));

            collectionTaiKhoanGiaoVien.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            if (dgvCoTK.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một tài Khoản để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.Text)) {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string matkhau = txtMatKhau.Text;
            string hashPassword = HashPassword(matkhau);
            if (txtEmail.Text == "") {
                txtEmail.Text = "Chưa Có Email";
            }

            TaiKhoanGiaoVien tk = new TaiKhoanGiaoVien(txtMaGV.Text, hashPassword, txtEmail.Text);
            tk.Id = new ObjectId(dgvCoTK.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<TaiKhoanGiaoVien>.Update
                .Set(s => s.MaGV, tk.MaGV)
                .Set(s => s.MatKhau, tk.MatKhau)
                .Set(s => s.Email, tk.Email);


            collectionTaiKhoanGiaoVien.UpdateOne(s => s.Id == tk.Id, update);

            readData();
            ClearTextBoxes();
        }
        private void dgvCoTK_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && dgvCoTK.Rows.Count > 0) {
                int index = e.RowIndex;
                // Kiểm tra xem chỉ số cột có hợp lệ
            
                    txtMaGV.Text = dgvCoTK.Rows[index].Cells[1].Value.ToString();

                    txtEmail.Text = dgvCoTK.Rows[index].Cells[3].Value.ToString();
     
            }
        }
        private void dgvChuaTK_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && dgvChuaTK.Rows.Count > 0) {
                int index = e.RowIndex;


                txtMaGV.Text = dgvChuaTK.Rows[index].Cells[1].Value.ToString();

            }
        }

        private void txtTimMa_TextChanged(object sender, EventArgs e) {
            string selectedMa = txtTimMa.Text;

            var filteredData = collectionGiaoVien.Find(new BsonDocument()).ToList();
            var filteredDataMa = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();
            if (txtTimMa.Text != "") {
                var filter = Builders<GiaoVien>.Filter.Eq(x => x.MaGV, selectedMa);
                var filterma = Builders<TaiKhoanGiaoVien>.Filter.Eq(x => x.MaGV, selectedMa);

                filteredData = collectionGiaoVien.Find(filter).ToList();
                filteredDataMa = collectionTaiKhoanGiaoVien.Find(filterma).ToList();
            }
            var filteredData2 = filteredData.Where(item2 => !filteredDataMa.Any(item1 => item1.MaGV == item2.MaGV)).ToList();
            dgvChuaTK.DataSource = filteredData2;
            dgvCoTK.DataSource = filteredDataMa;
        }

        

        private void cbbTimMon_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedMon = cbbTimMon.Text;
            var data = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();
            var filteredData = collectionGiaoVien.Find(new BsonDocument()).ToList();
            if (cbbTimMon.Text != "Tất Cả") {
                var filter = Builders<GiaoVien>.Filter.Eq(x => x.MonDay, selectedMon);
                filteredData = collectionGiaoVien.Find(filter).ToList();
            }

            var filteredData2 = filteredData.Where(item2 => !data.Any(item1 => item1.MaGV == item2.MaGV)).ToList();
            dgvChuaTK.DataSource = filteredData2;
        }

        private void uc_QuanLyTaiKhoanGiaoVien_Load(object sender, EventArgs e) {
            db = client.GetDatabase("QLHocSinhCap3");
            collectionTaiKhoanGiaoVien = db.GetCollection<TaiKhoanGiaoVien>("TaiKhoanGiaoVien");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            readData();
        }
    }
}
