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
        private IMongoCollection<MonHoc> collectionLopHoc;

      
        private void readData() {
            var dataHocSinh = collectionHocSinh.Find(new BsonDocument()).ToList();
            dgvQLHS.DataSource = dataHocSinh;

            var dataLopHoc = collectionLopHoc.Distinct<string>("MaLop", new BsonDocument()).ToList();
            var tempData1 = new List<string>(dataLopHoc);
            tempData1.Insert(0, "Chưa Chọn Lop");
            cbbLop.DataSource = tempData1;





            var tempData = new List<string>(dataLopHoc);
            tempData.Insert(0, "Tất Cả");

            cbbTimLop.DataSource = tempData;

            dgvQLHS.Columns["Id"].Visible = false;
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

            dgvQLHS.Columns["HoTenPhuHuynh"].HeaderText = "Họ Tên Phụ Huynh";
            dgvQLHS.Columns["HoTenPhuHuynh"].Width = 150;
            dgvQLHS.Columns["NamHoc"].HeaderText = "Năm Học";
            dgvQLHS.Columns["NamHoc"].Width = 76;

        }
        public uc_QuanLyHocSinh() {
            InitializeComponent();

            db = client.GetDatabase("QLHocSinhCap3");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");
            collectionLopHoc = db.GetCollection<MonHoc>("LopHoc");
            readData();
            ClearTextBoxes();
            
  
        }
        private void ClearTextBoxes() {
            txtMa.Clear();
            txtHoTen.Clear();
   
            cbbLop.SelectedIndex = 0;
            txtDiaChi.Clear();
            txtSdt.Clear();

            txtHoTenPhuHuynh.Clear();
           
            cbbGioiTinh.SelectedIndex = 0;

            cbbNamHoc.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int year = 2000; year <= currentYear; year++) {
                cbbNamHoc.Items.Add(year.ToString());
            }
            cbbNamHoc.SelectedIndex = cbbNamHoc.Items.Count - 1;

            cbbTimLop.SelectedIndex = 0;
            dgvQLHS.ClearSelection();
        }
        private void btnThem_Click(object sender, EventArgs e) {
            //kiem tra rong ma~
            if (string.IsNullOrEmpty(txtMa.Text)) {
                MessageBox.Show("Vui lòng nhập mã Học Sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Kiểm tra trùng mã học sinh
            var maHocSinhFilter = Builders<HocSinh>.Filter.Eq("MSHS", txtMa.Text);
            if (collectionHocSinh.Find(maHocSinhFilter).Any()) {
                MessageBox.Show("Mã Học Sinh đã tồn tại. Vui lòng chọn mã Học Sinh khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (string.IsNullOrEmpty(txtHoTenPhuHuynh.Text)) {
                MessageBox.Show("Vui lòng nhập Họ Tên Phụ Huynh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            HocSinh sv = new HocSinh(txtMa.Text, txtHoTen.Text, dtpNgaySinh.Text, cbbGioiTinh.Text, cbbLop.Text,txtDiaChi.Text, txtSdt.Text,txtHoTenPhuHuynh.Text, cbbNamHoc.Text);

            collectionHocSinh.InsertOne(sv);

            readData();
            ClearTextBoxes();
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvQLHS.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn Học Sinh để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Kiểm tra trùng mã hoc sinh
            var maHSFilter = Builders<HocSinh>.Filter.Eq("MSHS", txtMa.Text);
            var MaHSHienTai = dgvQLHS.CurrentRow.Cells[1].Value.ToString();
            if (collectionHocSinh.Find(maHSFilter & Builders<HocSinh>.Filter.Ne("MSHS", MaHSHienTai)).Any()) {
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

            if (string.IsNullOrEmpty(txtHoTenPhuHuynh.Text)) {
                MessageBox.Show("Vui lòng nhập Họ Tên Phụ Huynh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            HocSinh hs = new HocSinh(txtMa.Text, txtHoTen.Text, dtpNgaySinh.Text, cbbGioiTinh.Text, cbbLop.Text, txtDiaChi.Text, txtSdt.Text,txtHoTenPhuHuynh.Text, cbbNamHoc.Text  );
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
                .Set(s => s.HoTenPhuHuynh, hs.HoTenPhuHuynh);
             
        

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
                    cbbLop.Text = dgvQLHS.Rows[index].Cells[5].Value?.ToString();
                    cbbGioiTinh.Text = dgvQLHS.Rows[index].Cells[4].Value?.ToString();
                    txtDiaChi.Text = dgvQLHS.Rows[index].Cells[6].Value?.ToString();
                    txtSdt.Text = dgvQLHS.Rows[index].Cells[7].Value?.ToString();
                    cbbNamHoc.Text = dgvQLHS.Rows[index].Cells[8].Value?.ToString();
                    txtHoTenPhuHuynh.Text = dgvQLHS.Rows[index].Cells[9].Value?.ToString();

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

        private void uc_QuanLyHocSinh_Load(object sender, EventArgs e) {
            db = client.GetDatabase("QLHocSinhCap3");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");
            collectionLopHoc = db.GetCollection<MonHoc>("LopHoc");
            readData();
            ClearTextBoxes();
        }
    }
}
