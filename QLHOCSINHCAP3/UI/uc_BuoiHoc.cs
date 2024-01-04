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
    public partial class uc_QuanLyBuoiHoc : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017/QLHocSinhCap3");
        private IMongoDatabase db;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        private IMongoCollection<MonHoc> collectionMonHoc;
        private IMongoCollection<LopHoc> collectionLopHoc;
        private IMongoCollection<BuoiHoc> collectionBuoiHoc;


        private void readData() {
            var dataBuoiHoc = collectionBuoiHoc.Find(new BsonDocument()).ToList();
            dgvBuoiHoc.DataSource = dataBuoiHoc;

            var dataLopHoc = collectionLopHoc.Distinct<string>("MaLop", new BsonDocument()).ToList();
            var tempData1 = new List<string>(dataLopHoc);
            tempData1.Insert(0, "Chưa Chọn Lớp");
            cbbMaLop.DataSource = tempData1;

            var dataMonHoc = collectionMonHoc.Distinct<string>("TenMon", new BsonDocument()).ToList();
            var tempData2 = new List<string>(dataMonHoc);
            tempData2.Insert(0, "Chưa Chọn Môn");
            cbbMonHoc.DataSource = tempData2;



            




            dgvBuoiHoc.Columns["Id"].Visible = false;

            dgvBuoiHoc.Columns["MaLop"].HeaderText = "Mã Lớp";
            dgvBuoiHoc.Columns["MaLop"].Width = 150;
            dgvBuoiHoc.Columns["MonHoc"].HeaderText = "Môn Học";
            dgvBuoiHoc.Columns["MonHoc"].Width = 150;
            dgvBuoiHoc.Columns["GiaoVien"].HeaderText = "Giáo Viên";
            dgvBuoiHoc.Columns["GiaoVien"].Width = 150;
            dgvBuoiHoc.Columns["Ngay"].HeaderText = "Ngày Học";
            dgvBuoiHoc.Columns["Ngay"].Width = 150;
            dgvBuoiHoc.Columns["Buoi"].HeaderText = "Buổi";
            dgvBuoiHoc.Columns["Buoi"].Width = 150;
            dgvBuoiHoc.Columns["Tiet"].HeaderText = "Tiết";
            dgvBuoiHoc.Columns["Tiet"].Width = 150;
          


        }
        public uc_QuanLyBuoiHoc() {
            InitializeComponent();

            db = client.GetDatabase("QLHocSinhCap3");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            collectionBuoiHoc = db.GetCollection<BuoiHoc>("BuoiHoc");
            readData();
            ClearTextBoxes();




        }
        private void ClearTextBoxes() {



            cbbMaLop.SelectedIndex = 0;
            cbbMonHoc.SelectedIndex = 0;
            
            cbbBuoiSo.SelectedIndex = 0;
            cbbTiet.SelectedIndex = 0;
            dgvBuoiHoc.ClearSelection();
        }
        private void btnThem_Click(object sender, EventArgs e) {
            //kiem tra rong ma~

            BuoiHoc gv = new BuoiHoc(cbbMaLop.Text, cbbMonHoc.Text,cbbGiaoVien.Text, dtpNgaySinh.Text, cbbBuoiSo.Text, cbbTiet.Text);

            collectionBuoiHoc.InsertOne(gv);

            readData();
            ClearTextBoxes();
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            if (dgvBuoiHoc.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng Buổi Học để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string objectIdToDelete = dgvBuoiHoc.CurrentRow.Cells[0].Value.ToString();
            var filter = Builders<GiaoVien>.Filter.Eq("_id", ObjectId.Parse(objectIdToDelete));

            collectionGiaoVien.DeleteOne(filter);

            readData();
            ClearTextBoxes();
        }
        private void btnSua_Click(object sender, EventArgs e) {
            if (dgvBuoiHoc.SelectedRows.Count == 0) {
                MessageBox.Show("Vui lòng chọn một Giáo viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BuoiHoc gv = new BuoiHoc(cbbMaLop.Text, cbbMonHoc.Text, cbbGiaoVien.Text, dtpNgaySinh.Text, cbbBuoiSo.Text, cbbTiet.Text);
            gv.Id = new ObjectId(dgvBuoiHoc.CurrentRow.Cells[0].Value.ToString());

            var update = Builders<BuoiHoc>.Update
                .Set(s => s.MaLop, gv.MaLop)
                .Set(s => s.MonHoc, gv.MonHoc)
                .Set(s => s.GiaoVien, gv.GiaoVien)
                .Set(s => s.Ngay, gv.Ngay)
                .Set(s => s.Buoi, gv.Buoi)
                .Set(s => s.Tiet, gv.Tiet);



            collectionBuoiHoc.UpdateOne(s => s.Id == gv.Id, update);

            readData();
            ClearTextBoxes();
        }

        private void cbbMonHoc_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbbMonHoc.SelectedIndex > 0) {
                string monhoc = cbbMonHoc.SelectedItem.ToString();

                var dataGiaoVien = collectionGiaoVien.Find(new BsonDocument()).ToList();
                var locgiaovien = dataGiaoVien.Where(t => t.MonDay == monhoc).ToList();

                // var LocChuaTK = dataGiaoVien.Where(item2 => !dataTaiKhoanGiaoVien.Any(item1 => item1.MaGV == item2.MaGV)).ToList();

                var TenGiaoVien = locgiaovien.Select(t => t.HoTen).ToList();


                TenGiaoVien.Insert(0, "Chưa Chọn Giáo Viên");


                cbbGiaoVien.DataSource = TenGiaoVien;


            } else {
                cbbGiaoVien.DataSource = null;
                cbbGiaoVien.Items.Add("Vui Lòng Chọn Môn");
                cbbGiaoVien.SelectedIndex = 0;
            }
        }
    }
}
