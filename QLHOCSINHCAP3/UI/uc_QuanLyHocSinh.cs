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
    public partial class uc_QuanLyHocSinh : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017/QLHocSinhCap3");
        private IMongoDatabase db;
        private IMongoCollection<HocSinh> collectionHocSinh;
        private IMongoCollection<LopHoc> collectionLopHoc;

      
        private void readData() {
            var data = collectionHocSinh.Find(new BsonDocument()).ToList();

            var data1 = collectionLopHoc.Distinct<string>("MaLop", new BsonDocument()).ToList();

            dgvQLHS.DataSource = data;
            cbbLop.DataSource = data1;

            var tempData = new List<string>(data1);
            tempData.Insert(0, "Tất Cả");

            cbbTimLop.DataSource = tempData;

            dgvQLHS.Columns["MSHS"].HeaderText = "Mã Học Sinh";
            dgvQLHS.Columns["MSHS"].Width = 150;
            dgvQLHS.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvQLHS.Columns["HoTen"].Width = 150;
            dgvQLHS.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvQLHS.Columns["NgaySinh"].Width = 150;
            dgvQLHS.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvQLHS.Columns["GioiTinh"].Width = 75;
            dgvQLHS.Columns["Lop"].HeaderText = "Lớp";
            dgvQLHS.Columns["Lop"].Width = 150;
            dgvQLHS.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvQLHS.Columns["DiaChi"].Width = 150;
            dgvQLHS.Columns["SDT"].HeaderText = "Số Điện Thoại";
            dgvQLHS.Columns["SDT"].Width = 150;
            dgvQLHS.Columns["Email"].HeaderText = "Email";
            dgvQLHS.Columns["Email"].Width = 150;
            dgvQLHS.Columns["HoTenPhuHuynh"].HeaderText = "Họ Tên Phụ Huynh";
            dgvQLHS.Columns["HoTenPhuHuynh"].Width = 150;
            dgvQLHS.Columns["NamHoc"].HeaderText = "Năm Học";
            dgvQLHS.Columns["NamHoc"].Width = 76;

        }
        public uc_QuanLyHocSinh() {
            InitializeComponent();

            db = client.GetDatabase("QLHocSinhCap3");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            readData();
            int currentYear = DateTime.Now.Year;

            for (int year = 2000; year <= currentYear; year++) {
                cbbNamHoc.Items.Add(year.ToString());
            }
            
            cbbLop.SelectedIndex = 0;
            cbbTimLop.SelectedIndex = 0;
            cbbNamHoc.SelectedIndex= cbbNamHoc.Items.Count - 1;
            cbbGioiTinh.SelectedIndex = 0;

        }
        private void ClearTextBoxes() {
            txtMa.Clear();
            txtHoTen.Clear();
   
            cbbLop.SelectedIndex = 0;
            txtDiaChi.Clear();
            txtSdt.Clear();
            txtEmai.Clear();
            txtHoTenPhuHuynh.Clear();
            cbbNamHoc.SelectedIndex = cbbNamHoc.Items.Count - 1;
            cbbGioiTinh.SelectedIndex = 0;

        }
        private void KiemTraRong() { 

        
        
        }
        private void btnThem_Click(object sender, EventArgs e) {
            HocSinh sv = new HocSinh(txtMa.Text, txtHoTen.Text, dtpNgaySinh.Text, cbbGioiTinh.Text, cbbLop.Text,txtDiaChi.Text, txtSdt.Text, txtEmai.Text,txtHoTenPhuHuynh.Text, cbbNamHoc.Text);

            collectionHocSinh.InsertOne(sv);

            readData();
            ClearTextBoxes();
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvQLHS.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn lớp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string objectIdToDelete = dgvQLHS.CurrentRow.Cells[0].Value.ToString();
            var filter = Builders<HocSinh>.Filter.Eq("_id", ObjectId.Parse(objectIdToDelete));

            collectionHocSinh.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            if (dgvQLHS.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một học sinh để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            HocSinh hs = new HocSinh(txtMa.Text, txtHoTen.Text, dtpNgaySinh.Text, cbbGioiTinh.Text, cbbLop.Text, txtDiaChi.Text, txtSdt.Text, cbbNamHoc.Text, txtHoTenPhuHuynh.Text, txtEmai.Text);
            hs.Id = new ObjectId(dgvQLHS.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<HocSinh>.Update
                .Set(s => s.MSHS, hs.MSHS)
                .Set(s => s.HoTen, hs.HoTen)
                .Set(s => s.NgaySinh, hs.NgaySinh)
                .Set(s => s.GioiTinh, hs.GioiTinh)
                .Set(s => s.Lop, hs.Lop)
                .Set(s => s.DiaChi, hs.DiaChi)
                .Set(s => s.SDT, hs.SDT)
                .Set(s => s.NamHoc, hs.NamHoc)
                .Set(s => s.HoTenPhuHuynh, hs.HoTenPhuHuynh)
                .Set(s => s.Email, hs.Email);
             
        

            collectionHocSinh.UpdateOne(s => s.Id == hs.Id, update);

            readData();
            ClearTextBoxes();
        }

        private void dgvQLSV_CellClick(object sender, DataGridViewCellEventArgs e) {
            // Kiểm tra xem chỉ số hàng có hợp lệ và DataGridView không rỗng
            if (e.RowIndex >= 0 && dgvQLHS.Rows.Count > 0) {
                int index = e.RowIndex;
                // Kiểm tra xem chỉ số cột có hợp lệ
                if (index < dgvQLHS.Rows[index].Cells.Count) {
                    txtMa.Text = dgvQLHS.Rows[index].Cells[1].Value?.ToString();
                    txtHoTen.Text = dgvQLHS.Rows[index].Cells[2].Value?.ToString();
                    dtpNgaySinh.Text = dgvQLHS.Rows[index].Cells[3].Value?.ToString();
                    cbbLop.Text = dgvQLHS.Rows[index].Cells[4].Value?.ToString();
                    cbbGioiTinh.Text = dgvQLHS.Rows[index].Cells[5].Value?.ToString();
                    txtDiaChi.Text = dgvQLHS.Rows[index].Cells[6].Value?.ToString();
                    txtSdt.Text = dgvQLHS.Rows[index].Cells[7].Value?.ToString();
                    cbbNamHoc.Text = dgvQLHS.Rows[index].Cells[8].Value?.ToString();
                    txtHoTenPhuHuynh.Text = dgvQLHS.Rows[index].Cells[9].Value?.ToString();
                    txtEmai.Text = dgvQLHS.Rows[index].Cells[10].Value?.ToString();
                }
            }
        }

        private void cbbTimLop_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedLop = cbbTimLop.Text;
            var filteredData = collectionHocSinh.Find(new BsonDocument()).ToList();
            if (cbbTimLop.Text != "Tất Cả") {
                var filter = Builders<HocSinh>.Filter.Eq("Lop", selectedLop);
                filteredData = collectionHocSinh.Find(filter).ToList();
            }
            dgvQLHS.DataSource = filteredData;
        }
    }
}
