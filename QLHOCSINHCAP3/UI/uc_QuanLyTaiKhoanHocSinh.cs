using MongoDB.Bson;
using MongoDB.Driver;
using QLHOCSINHCAP3.Thuvien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHOCSINHCAP3.UI {
    public partial class uc_QuanLyTaiKhoanHocSinh : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanHocSinh> collection;

        private void readData() {
            var data = collection.Find(new BsonDocument()).ToList();


            dgvCoTK.DataSource = data;

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
            collection = db.GetCollection<TaiKhoanHocSinh>("TaiKhoanHocSinh");
            readData();


        }
        private void ClearTextBoxes() {

            txtMaHS.Clear();
            txtMatKhau.Clear();
            txtEmail.Clear();

        }
        private void btnThem_Click(object sender, EventArgs e) {
            TaiKhoanHocSinh tk = new TaiKhoanHocSinh(txtMaHS.Text,txtMatKhau.Text, txtEmail.Text);

            collection.InsertOne(tk);

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

            collection.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            if (dgvCoTK.SelectedRows.Count == 0) {
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
    }
}
