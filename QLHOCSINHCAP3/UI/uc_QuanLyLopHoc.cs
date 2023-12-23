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
        private IMongoCollection<LopHoc> collection;
        
        private void readData() {
            var data = collection.Find(new BsonDocument()).ToList();


            dgvQLLop.DataSource = data;

            dgvQLLop.Columns["Khoi"].HeaderText = "Khối";
            dgvQLLop.Columns["Khoi"].Width = 150;
            dgvQLLop.Columns["MaLop"].HeaderText = "Mã Lớp";
            dgvQLLop.Columns["MaLop"].Width = 150;
            dgvQLLop.Columns["SiSo"].HeaderText = "Sĩ Số";
            dgvQLLop.Columns["SiSo"].Width = 150;
            dgvQLLop.Columns["GVCN"].HeaderText = "Giáo Viên Chủ Nhiệm";
            dgvQLLop.Columns["GVCN"].Width = 150;
        }
        public uc_QuanLyLopHoc() {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");
            collection = db.GetCollection<LopHoc>("LopHoc");
            readData();
            cbbKhoi.SelectedIndex = 0;
            cbbLocKhoi.SelectedIndex = 0;

        }
        private void ClearTextBoxes() {

            txtMaLop.Clear();
            cbbKhoi.SelectedIndex = 0;
            txtGVCN.Clear();

        }
        private void btnThem_Click(object sender, EventArgs e) {
            LopHoc lh = new LopHoc(txtMaLop.Text, cbbKhoi.Text, txtGVCN.Text, "0");

            collection.InsertOne(lh);

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

            collection.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            if (dgvQLLop.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một sinh viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LopHoc lh = new LopHoc(txtMaLop.Text, cbbKhoi.Text, txtGVCN.Text, "0");
            lh.Id = new ObjectId(dgvQLLop.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<LopHoc>.Update
                .Set(s => s.MaLop, lh.MaLop)
                .Set(s => s.Khoi, lh.Khoi)
                .Set(s => s.GVCN, lh.GVCN)
                .Set(s => s.SiSo, lh.SiSo);

            collection.UpdateOne(s => s.Id == lh.Id, update);

            readData();
            ClearTextBoxes();
        }

        private void dgvQLSV_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && dgvQLLop.Rows.Count > 0) {
                int index = e.RowIndex;
                // Kiểm tra xem chỉ số cột có hợp lệ
                if (index < dgvQLLop.Rows[index].Cells.Count) {
                    txtMaLop.Text = dgvQLLop.Rows[index].Cells[1].Value.ToString();
                    cbbKhoi.Text = dgvQLLop.Rows[index].Cells[2].Value.ToString();
                    txtGVCN.Text = dgvQLLop.Rows[index].Cells[3].Value.ToString();
                }
            }
        }

        private void cbbLocKhoi_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedKhoi = cbbLocKhoi.Text;
            var filteredData = collection.Find(new BsonDocument()).ToList();
            if (cbbLocKhoi.Text != "Tất Cả") {
                var filter = Builders<LopHoc>.Filter.Eq("Khoi", selectedKhoi);
                 filteredData = collection.Find(filter).ToList();
            }
            dgvQLLop.DataSource = filteredData;
        }
    }
}
