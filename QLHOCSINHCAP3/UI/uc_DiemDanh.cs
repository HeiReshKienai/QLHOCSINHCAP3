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
    public partial class uc_DiemDanh : UserControl {
        private string magiaovien;

        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanGiaoVien> collectionTaiKhoanGiaoVien;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        private IMongoCollection<LopHoc> collectionLopHoc;
        private IMongoCollection<HocSinh> collectionHocSinh;
        private IMongoCollection<BuoiHoc> collectionBuoiHoc;
        private IMongoCollection<DiemDanh> collectionDiemDanh;
        public uc_DiemDanh(string magiaovien) {
            InitializeComponent();
            this.magiaovien = magiaovien;

            db = client.GetDatabase("QLHocSinhCap3");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");                    
            collectionBuoiHoc = db.GetCollection<BuoiHoc>("BuoiHoc");
            collectionDiemDanh = db.GetCollection<DiemDanh>("DiemDanh");
            DiemDanhHocSinh();

        }
        private void dtpNgay_ValueChanged(object sender, EventArgs e) {
            DiemDanhHocSinh();
        }
        private void DiemDanhHocSinh() {
            var filter3 = Builders<DiemDanh>.Filter.Eq("Ngay", dtpNgay.Value.Date.AddDays(1));
            var DiemDanh = collectionDiemDanh.Find(filter3).FirstOrDefault();

            if (DiemDanh != null) {
                var dataDiemDanh = collectionDiemDanh.Find(Builders<DiemDanh>.Filter.Eq("Ngay", DiemDanh.Ngay)).ToList();
                dgvDiemDanh.DataSource = dataDiemDanh;

                dgvDiemDanh.Columns["Id"].Visible = false;
                dgvDiemDanh.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvDiemDanh.Columns["MaLop"].Width = 150;
                dgvDiemDanh.Columns["HoTenHocSinh"].HeaderText = "Họ và Tên";
                dgvDiemDanh.Columns["HoTenHocSinh"].Width = 150;
                dgvDiemDanh.Columns["Ngay"].HeaderText = "Ngày";
                dgvDiemDanh.Columns["Ngay"].Width = 120;
                dgvDiemDanh.Columns["TinhTrangDiemDanh"].HeaderText = "Tình Trạng";
                dgvDiemDanh.Columns["TinhTrangDiemDanh"].Width = 150;

            } else {


                var filter = Builders<GiaoVien>.Filter.Eq("MaGV", magiaovien);
                var giaovien = collectionGiaoVien.Find(filter).FirstOrDefault();


                if (giaovien != null) {
                    string TenGV = giaovien.HoTen;

                    var filter1 = Builders<LopHoc>.Filter.Eq("GVCN", TenGV);
                    var lop = collectionLopHoc.Find(filter1).FirstOrDefault();

                    if (lop != null) {
                        string Lop = lop.MaLop;
                        lblthongtin.Text = "Bạn Là GVCN Lớp " + Lop;

                        dgvDiemDanh.Columns.Clear();


                        // load học sinh của lớp lên datagird view
                        var filter2 = Builders<HocSinh>.Filter.Eq("Lop", Lop);
                        var students = collectionHocSinh.Find(filter2).ToList();

                        DataTable dt = new DataTable();
                        dt.Columns.Add("MaLop");
                        dt.Columns.Add("Ngay");
                        dt.Columns.Add("HoTenHocSinh");
                        dt.Columns.Add("TinhTrangDiemDanh");



                        DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                        comboBoxColumn.DataPropertyName = "TinhTrangDiemDanh";
                        comboBoxColumn.HeaderText = "TinhTrangDiemDanh";
                        comboBoxColumn.Name = "TinhTrangDiemDanh";
                        comboBoxColumn.Width = 150;

                        comboBoxColumn.Items.Add("Chưa Điểm Danh");
                        comboBoxColumn.Items.Add("Có Mặt");
                        comboBoxColumn.Items.Add("Vắng Mặt");
                        comboBoxColumn.Items.Add("Trễ");

                        dgvDiemDanh.Columns.Add(comboBoxColumn);





                        foreach (var student in students) {
                            DataRow row = dt.NewRow();
                            row["MaLop"] = Lop;
                            row["Ngay"] = dtpNgay.Value.Date.ToString("dd/MM/yyyy");
                            row["HoTenHocSinh"] = student.HoTen;
                            row["TinhTrangDiemDanh"] = "Chưa Điểm Danh";
                            dt.Rows.Add(row);
                        }


                        //dgvDiemDanh = new DataGridView();


                        dgvDiemDanh.DataSource = dt;
                        dgvDiemDanh.Columns["TinhTrangDiemDanh"].DisplayIndex = dgvDiemDanh.Columns.Count - 1;
             
                        dgvDiemDanh.Columns["MaLop"].HeaderText = "Mã Lớp";
                        dgvDiemDanh.Columns["MaLop"].Width = 150;
                        dgvDiemDanh.Columns["HoTenHocSinh"].HeaderText = "Họ và Tên";
                        dgvDiemDanh.Columns["HoTenHocSinh"].Width = 150;
                        dgvDiemDanh.Columns["Ngay"].HeaderText = "Ngày";
                        dgvDiemDanh.Columns["Ngay"].Width = 120;
                        dgvDiemDanh.Columns["TinhTrangDiemDanh"].HeaderText = "Tình Trạng";
                        dgvDiemDanh.Columns["TinhTrangDiemDanh"].Width = 150;

                    } else {
                        lblthongtin.Text = "Bạn Không Là Giáo Viên Chủ Nhiệm   ";
                        MessageBox.Show("Bạn Không Là Giáo Viên Chủ Nhiệm của 1 lớp nên không thể điểm danh");
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e) {
            // Lặp qua từng dòng trong DataGridView
            foreach (DataGridViewRow row in dgvDiemDanh.Rows) {
                string maLop = row.Cells["MaLop"].Value.ToString();
                string hoTen = row.Cells["HoTenHocSinh"].Value.ToString();
                string tinhTrang = row.Cells["TinhTrangDiemDanh"].FormattedValue.ToString();

                var filter5 = Builders<DiemDanh>.Filter.Eq("Ngay", dtpNgay.Value.Date.AddDays(1));
                var lop = collectionDiemDanh.Find(filter5).FirstOrDefault();

                if (lop != null) {
                    DiemDanh dd = new DiemDanh(maLop, dtpNgay.Value.Date, hoTen, tinhTrang);
                    dd.Id = new ObjectId(dgvDiemDanh.CurrentRow.Cells[0].Value.ToString());

                    var update = Builders<DiemDanh>.Update
                        .Set(s => s.MaLop, dd.MaLop)
                        .Set(s => s.Ngay, dd.Ngay)
                        .Set(s => s.HoTenHocSinh, dd.HoTenHocSinh)
                        .Set(s => s.TinhTrangDiemDanh, dd.TinhTrangDiemDanh);


                    collectionDiemDanh.UpdateOne(s => s.Id == dd.Id, update);


                } else {
                    var diemDanh = new DiemDanh(maLop, dtpNgay.Value.Date.AddDays(1),hoTen, tinhTrang);

                    collectionDiemDanh.InsertOne(diemDanh);

                }
                DiemDanhHocSinh();
            }

            // Hiển thị thông báo thành công
            MessageBox.Show("Đã lưu dữ liệu điểm danh thành công!");
        }

        private void uc_DiemDanh_Load(object sender, EventArgs e) {
            

            db = client.GetDatabase("QLHocSinhCap3");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");
            collectionBuoiHoc = db.GetCollection<BuoiHoc>("BuoiHoc");
            collectionDiemDanh = db.GetCollection<DiemDanh>("DiemDanh");
            DiemDanhHocSinh();
        }
    }
}
