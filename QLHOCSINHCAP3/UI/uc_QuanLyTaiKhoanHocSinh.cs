﻿using DevExpress.ClipboardSource.SpreadsheetML;
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
    public partial class uc_QuanLyTaiKhoanHocSinh : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanHocSinh> collectionTaiKhoanHocSinh;
        private IMongoCollection<LopHoc> collectionLopHoc;
        private IMongoCollection<HocSinh> collectionHocSinh;

        private void readData() {
            var data = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();
            var data2 = collectionHocSinh.Find(new BsonDocument()).ToList();

            var filteredData2 = data2.Where(item2 => !data.Any(item1 => item1.MSHS == item2.MSHS)).ToList();

            dgvCoTK.DataSource = data;
            dgvChuaTK.DataSource = filteredData2;



            var data1 = collectionLopHoc.Distinct<string>("MaLop", new BsonDocument()).ToList();
            var tempData = new List<string>(data1);
            tempData.Insert(0, "Tất Cả");

            cbbTimLop.DataSource = tempData;


            //dgvQLLop.Columns["Khoi"].HeaderText = "Khối";
            //dgvQLLop.Columns["Khoi"].Width = 150;
            //dgvQLLop.Columns["MaLop"].HeaderText = "Mã Lớp";
            //dgvQLLop.Columns["MaLop"].Width = 150;
            //dgvQLLop.Columns["SiSo"].HeaderText = "Sĩ Số";
            //dgvQLLop.Columns["SiSo"].Width = 150;
            //dgvQLLop.Columns["GVCN"].HeaderText = "Giáo Viên Chủ Nhiệm";
            //dgvQLLop.Columns["GVCN"].Width = 150;
        }
        public uc_QuanLyTaiKhoanHocSinh() {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");
            collectionTaiKhoanHocSinh = db.GetCollection<TaiKhoanHocSinh>("TaiKhoanHocSinh");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");
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

            txtMaHS.Clear();
            txtMatKhau.Clear();
            txtEmail.Clear();

        }
        private void btnThem_Click(object sender, EventArgs e) {
            string matkhau = txtMatKhau.Text;
            string hashPassword = HashPassword(matkhau);
            TaiKhoanHocSinh tk = new TaiKhoanHocSinh(txtMaHS.Text, hashPassword, txtEmail.Text);

            collectionTaiKhoanHocSinh.InsertOne(tk);

            readData();
            ClearTextBoxes();
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvCoTK.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn Tài Khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string objectIdToDelete = dgvCoTK.CurrentRow.Cells[0].Value.ToString();
            var filter = Builders<TaiKhoanHocSinh>.Filter.Eq("_id", ObjectId.Parse(objectIdToDelete));

            collectionTaiKhoanHocSinh.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            if (dgvCoTK.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một tài Khoản để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TaiKhoanHocSinh tk = new TaiKhoanHocSinh(txtMaHS.Text, txtMatKhau.Text, txtEmail.Text);
            tk.Id = new ObjectId(dgvCoTK.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<TaiKhoanHocSinh>.Update
                .Set(s => s.MSHS, tk.MSHS)
                .Set(s => s.MatKhau, tk.MatKhau)
                .Set(s => s.Email, tk.Email);


            collectionTaiKhoanHocSinh.UpdateOne(s => s.Id == tk.Id, update);

            readData();
            ClearTextBoxes();
        }
        private void dgvCoTK_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && dgvCoTK.Rows.Count > 0) {
                int index = e.RowIndex;
                // Kiểm tra xem chỉ số cột có hợp lệ
                if (index < dgvCoTK.Rows[index].Cells.Count) {
                    txtMaHS.Text = dgvCoTK.Rows[index].Cells[1].Value.ToString();
                    txtMatKhau.Text = dgvCoTK.Rows[index].Cells[2].Value.ToString();
                    txtEmail.Text = dgvCoTK.Rows[index].Cells[3].Value.ToString();
                }
            }
        }
        private void cbbTimLop_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedLop = cbbTimLop.Text;
            var data = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();
            var filteredData = collectionHocSinh.Find(new BsonDocument()).ToList();
            if (cbbTimLop.Text != "Tất Cả") {
                var filter = Builders<HocSinh>.Filter.Eq(x => x.Lop, selectedLop);
                filteredData = collectionHocSinh.Find(filter).ToList();
            }
            
            var filteredData2 = filteredData.Where(item2 => !data.Any(item1 => item1.MSHS == item2.MSHS)).ToList();
            dgvChuaTK.DataSource = filteredData2;


        }

        private void txtTimMa_TextChanged(object sender, EventArgs e) {
            string selectedMa = txtTimMa.Text;

            var filteredData = collectionHocSinh.Find(new BsonDocument()).ToList();
            var filteredDataMa = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();
            if (txtTimMa.Text != "") {
                var filter = Builders<HocSinh>.Filter.Eq(x => x.MSHS, selectedMa);
                var filterma = Builders<TaiKhoanHocSinh>.Filter.Eq(x => x.MSHS, selectedMa);

                filteredData = collectionHocSinh.Find(filter).ToList();
                filteredDataMa = collectionTaiKhoanHocSinh.Find(filterma).ToList();
            }
            var filteredData2 = filteredData.Where(item2 => !filteredDataMa.Any(item1 => item1.MSHS == item2.MSHS)).ToList();
            dgvChuaTK.DataSource = filteredData2;
            dgvCoTK.DataSource = filteredDataMa;
        }

        private void dgvChuaTK_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && dgvChuaTK.Rows.Count > 0) {
                int index = e.RowIndex;
                // Kiểm tra xem chỉ số cột có hợp lệ
                if (index < dgvChuaTK.Rows[index].Cells.Count) {
                    txtMaHS.Text = dgvChuaTK.Rows[index].Cells[1].Value.ToString();
                }
            }
        }
    }
}