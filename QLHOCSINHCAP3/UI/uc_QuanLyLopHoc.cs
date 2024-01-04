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
    public partial class uc_QuanLyLopHoc : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<LopHoc> collectionLopHoc;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        private void readData() {
            var dataLopHoc = collectionLopHoc.Find(new BsonDocument()).ToList();
            dgvQLLop.DataSource = dataLopHoc;


            var dataGiaoVien = collectionGiaoVien.Distinct<string>("HoTen", new BsonDocument()).ToList();
            var tempData = new List<string>(dataGiaoVien);
            tempData.Insert(0, "Chưa Chọn Giáo Viên");
            tempData.Insert(1, "Trống");
            cbbGVCN.DataSource = tempData;

            dgvQLLop.Columns["Id"].Visible = false;
            dgvQLLop.Columns["Khoi"].HeaderText = "Khối";
            dgvQLLop.Columns["Khoi"].Width = 150;
            dgvQLLop.Columns["MaLop"].HeaderText = "Mã Lớp";
            dgvQLLop.Columns["MaLop"].Width = 150;
            dgvQLLop.Columns["GVCN"].HeaderText = "Giáo Viên Chủ Nhiệm";
            dgvQLLop.Columns["GVCN"].Width = 200;
        }
        public uc_QuanLyLopHoc() {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            readData();
            ClearTextBoxes();

        }
        private void ClearTextBoxes() {

            txtMaLop.Clear();
            cbbKhoi.SelectedIndex = 0;
            cbbGVCN.SelectedIndex = 0;
            cbbLocKhoi.SelectedIndex = 0;
            dgvQLLop.ClearSelection();

        }
        private void btnThem_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtMaLop.Text)) {
                MessageBox.Show("Vui lòng nhập mã lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var maLopFilter = Builders<LopHoc>.Filter.Eq("MaLop", txtMaLop.Text);
            if (collectionLopHoc.Find(maLopFilter).Any()) {
                MessageBox.Show("Mã lớp đã tồn tại. Vui lòng chọn mã lớp khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbGVCN.Text == "Chưa Chọn Giáo Viên") {
                MessageBox.Show("Vui lòng chọn Giáo viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbGVCN.Text != "Trống") {
                var GVCNFilter = Builders<LopHoc>.Filter.Eq("GVCN", cbbGVCN.Text);
                if (collectionLopHoc.Find(GVCNFilter).Any()) {
                    MessageBox.Show("giáo viên này đã là giáo viên chủ nhiệm của 1 lớp . Vui lòng chọn giáo viên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
            }
            LopHoc lh = new LopHoc(txtMaLop.Text, cbbKhoi.Text, cbbGVCN.Text);

            collectionLopHoc.InsertOne(lh);

            readData();
            ClearTextBoxes();
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvQLLop.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn lớp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string objectIdToDelete = dgvQLLop.CurrentRow.Cells[0].Value.ToString();
            var filter = Builders<LopHoc>.Filter.Eq("_id", ObjectId.Parse(objectIdToDelete));

            collectionLopHoc.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            //kiểm tra
            if (dgvQLLop.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một Lớp để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var maLopFilter = Builders<LopHoc>.Filter.Eq("MaLop", txtMaLop.Text);
            var MaLopHienTai = dgvQLLop.CurrentRow.Cells[1].Value.ToString();
            if (collectionLopHoc.Find(maLopFilter & Builders<LopHoc>.Filter.Ne("MaLop", MaLopHienTai)).Any()) {
                MessageBox.Show("Mã lớp đã tồn tại. Vui lòng chọn mã lớp khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbbGVCN.Text == "Chưa Chọn Giáo Viên") {
                MessageBox.Show("Vui lòng chọn Giáo viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (cbbGVCN.Text != "Trống") {
                var GVCNFilter = Builders<LopHoc>.Filter.Eq("GVCN", cbbGVCN.Text);
                if (collectionLopHoc.Find(GVCNFilter).Any()) {
                    MessageBox.Show("giáo viên này đã là giáo viên chủ nhiệm của 1 lớp . Vui lòng chọn giáo viên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }




            LopHoc lh = new LopHoc(txtMaLop.Text, cbbKhoi.Text, cbbGVCN.Text);
            lh.Id = new ObjectId(dgvQLLop.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<LopHoc>.Update
                .Set(s => s.MaLop, lh.MaLop)
                .Set(s => s.Khoi, lh.Khoi)
                .Set(s => s.GVCN, lh.GVCN);

            collectionLopHoc.UpdateOne(s => s.Id == lh.Id, update);

            readData();
            ClearTextBoxes();
        }

        private void dgvQLSV_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < dgvQLLop.Rows.Count) {
                int index = e.RowIndex;
                    txtMaLop.Text = dgvQLLop.Rows[index].Cells[1].Value.ToString();
                    cbbKhoi.Text = dgvQLLop.Rows[index].Cells[2].Value.ToString();
                    cbbGVCN.Text = dgvQLLop.Rows[index].Cells[3].Value.ToString();

            }
        }

        private void cbbLocKhoi_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedKhoi = cbbLocKhoi.Text;
            var filteredData = collectionLopHoc.Find(new BsonDocument()).ToList();
            if (cbbLocKhoi.Text != "Tất Cả") {
                var filter = Builders<LopHoc>.Filter.Eq("Khoi", selectedKhoi);
                 filteredData = collectionLopHoc.Find(filter).ToList();
            }
            dgvQLLop.DataSource = filteredData;
        }

        private void uc_QuanLyLopHoc_Load(object sender, EventArgs e) {
            db = client.GetDatabase("QLHocSinhCap3");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            readData();
            ClearTextBoxes();
        }
    }
}
