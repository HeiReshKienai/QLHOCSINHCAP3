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
    public partial class uc_QuanLyMonHoc : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<MonHoc> collectionMonHoc;
        
        private void readData() {
            
            var dataMonHoc = collectionMonHoc.Find(new BsonDocument()).ToList();


            dgvQLMon.DataSource = dataMonHoc;

            dgvQLMon.Columns["Id"].Visible = false;
            dgvQLMon.Columns["MaMon"].HeaderText = "Mã Môn";
            dgvQLMon.Columns["MaMon"].Width = 150;
            dgvQLMon.Columns["TenMon"].HeaderText = "Tên Môn";
            dgvQLMon.Columns["TenMon"].Width = 150;
        }
        public uc_QuanLyMonHoc() {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            readData();
            ClearTextBoxes();



        }
        private void ClearTextBoxes() {

            txtMaMon.Clear();
            txtTenMon.Clear();

            dgvQLMon.ClearSelection();


        }
        private void btnThem_Click(object sender, EventArgs e) {
            // Kiểm tra trùng mã môn
            var maMonFilter = Builders<MonHoc>.Filter.Eq("MaMon", txtMaMon.Text);
            if (collectionMonHoc.Find(maMonFilter).Any()) {
                MessageBox.Show("Mã môn đã tồn tại. Vui lòng chọn mã môn khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra trùng tên môn
            var tenMonFilter = Builders<MonHoc>.Filter.Eq("TenMon", txtTenMon.Text);
            if (collectionMonHoc.Find(tenMonFilter).Any()) {
                MessageBox.Show("Tên môn đã tồn tại. Vui lòng chọn tên môn khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MonHoc mh = new MonHoc(txtMaMon.Text, txtTenMon.Text);

            collectionMonHoc.InsertOne(mh);

            readData();
            ClearTextBoxes();
            dgvQLMon.ClearSelection();


        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvQLMon.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn lớp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string objectIdToDelete = dgvQLMon.CurrentRow.Cells[0].Value.ToString();
            var filter = Builders<MonHoc>.Filter.Eq("_id", ObjectId.Parse(objectIdToDelete));

            collectionMonHoc.DeleteOne(filter);

            readData();
            ClearTextBoxes();

        }
        private void btnSua_Click(object sender, EventArgs e) {
            // Kiểm tra trùng mã môn
            var maMonFilter = Builders<MonHoc>.Filter.Eq("MaMon", txtMaMon.Text);
            if (collectionMonHoc.Find(maMonFilter).Any()) {
                MessageBox.Show("Mã môn đã tồn tại. Vui lòng chọn mã môn khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra trùng tên môn
            var tenMonFilter = Builders<MonHoc>.Filter.Eq("TenMon", txtTenMon.Text);
            if (collectionMonHoc.Find(tenMonFilter).Any()) {
                MessageBox.Show("Tên môn đã tồn tại. Vui lòng chọn tên môn khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (dgvQLMon.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một môn học để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MonHoc mh = new MonHoc(txtMaMon.Text,txtTenMon.Text);
            mh.Id = new ObjectId(dgvQLMon.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<MonHoc>.Update
                .Set(s => s.MaMon, mh.MaMon)
                .Set(s => s.TenMon, mh.TenMon);

            collectionMonHoc.UpdateOne(s => s.Id == mh.Id, update);

            readData();
            ClearTextBoxes();

        }

        private void dgvQLSV_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < dgvQLMon.Rows.Count) {
                int index = e.RowIndex;
                    txtMaMon.Text = dgvQLMon.Rows[index].Cells[1].Value.ToString();
                    txtTenMon.Text = dgvQLMon.Rows[index].Cells[2].Value.ToString();
                
            }

        }

        private void uc_QuanLyMonHoc_Load(object sender, EventArgs e) {
            db = client.GetDatabase("QLHocSinhCap3");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            readData();
            ClearTextBoxes();
        }
    }
}
