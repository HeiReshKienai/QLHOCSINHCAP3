using DevExpress.XtraEditors.Filtering.Templates;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QLHOCSINHCAP3.Thuvien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHOCSINHCAP3.UI {
    public partial class uc_ThoiKhoaBieuGV : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017/QLHocSinhCap3");
        private IMongoDatabase db;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        private IMongoCollection<MonHoc> collectionMonHoc;
        private IMongoCollection<LopHoc> collectionLopHoc;
        private IMongoCollection<BuoiHoc> collectionBuoiHoc;

        private DateTime batdautuan;
        private DateTime ketthuctuan;
        private string magiaovien;
        public uc_ThoiKhoaBieuGV(string magiaovien) {
            InitializeComponent();
            this.magiaovien = magiaovien;

            db = client.GetDatabase("QLHocSinhCap3");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            collectionMonHoc = db.GetCollection<MonHoc>("MonHoc");
            collectionLopHoc = db.GetCollection<LopHoc>("LopHoc");
            collectionBuoiHoc = db.GetCollection<BuoiHoc>("BuoiHoc");

            batdautuan = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            ketthuctuan = batdautuan.AddDays(6);
            lblTuan.Text = "Từ ngày " + batdautuan.ToString("dd/MM/yyyy") + " Đến " + ketthuctuan.ToString("dd/MM/yyyy");
            UpdateNgayHocControls();
        }
        private void btnLui_Click(object sender, EventArgs e) {
            batdautuan = batdautuan.AddDays(-7);
            ketthuctuan = batdautuan.AddDays(6);
            lblTuan.Text = "Từ ngày " + batdautuan.ToString("dd/MM/yyyy") + " Đến " + ketthuctuan.ToString("dd/MM/yyyy");
            UpdateNgayHocControls();
        }

        private void btnTien_Click(object sender, EventArgs e) {
            batdautuan = batdautuan.AddDays(7);
            ketthuctuan = batdautuan.AddDays(6);

            lblTuan.Text = "Từ ngày " + batdautuan.ToString("dd/MM/yyyy") + " Đến " + ketthuctuan.ToString("dd/MM/yyyy");
            UpdateNgayHocControls();
        }

        private void UpdateNgayHocControls() {
            resetTKB();

            var filter = Builders<GiaoVien>.Filter.Eq("MaGV", magiaovien);
            var giaovien = collectionGiaoVien.Find(filter).FirstOrDefault();

            string TenGV = giaovien.HoTen;

            var dataBuoiHoc = collectionBuoiHoc.Find(new BsonDocument()).ToList();
            var buoihoc = dataBuoiHoc.Where(a => a.GiaoVien == TenGV && a.Ngay >= batdautuan.Date && a.Ngay <= ketthuctuan.Date).ToList();


            foreach (var item in buoihoc) {
                var thu2 = batdautuan.Date;
                var thu3 = batdautuan.AddDays(1).Date;
                var thu4 = batdautuan.AddDays(2).Date;
                var thu5 = batdautuan.AddDays(3).Date;
                var thu6 = batdautuan.AddDays(4).Date;
                var thu7 = batdautuan.AddDays(5).Date;
                var thu8 = batdautuan.AddDays(6).Date;
               // MessageBox.Show(thu8.ToString());





                if (item.Ngay.Date == thu2 && item.Buoi=="Sáng") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu2tiet1.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu2tiet2.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu2tiet3.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu2tiet4.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu2tiet5.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                if (item.Ngay.Date == thu2 && item.Buoi == "Chiều") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu2tiet6.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu2tiet7.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu2tiet8.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu2tiet9.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu2tiet10.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                //thu3
                if (item.Ngay.Date == thu3 && item.Buoi == "Sáng") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu3tiet1.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu3tiet2.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu3tiet3.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu3tiet4.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu3tiet5.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                if (item.Ngay.Date == thu3 && item.Buoi == "Chiều") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu3tiet6.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu3tiet7.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu3tiet8.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu3tiet9.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu3tiet10.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                //thu4
                if (item.Ngay.Date == thu4 && item.Buoi == "Sáng") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu4tiet1.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu4tiet2.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu4tiet3.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4": 
                            thu4tiet4.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu4tiet5.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                if (item.Ngay.Date == thu4 && item.Buoi == "Chiều") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu4tiet6.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu4tiet7.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu4tiet8.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu4tiet9.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu4tiet10.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                //thu5
                if (item.Ngay.Date == thu5 && item.Buoi == "Sáng") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu5tiet1.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu5tiet2.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu5tiet3.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu5tiet4.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu5tiet5.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                if (item.Ngay.Date == thu5 && item.Buoi == "Chiều") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu5tiet6.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu5tiet7.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu5tiet8.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu5tiet9.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu5tiet10.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }

                //thu6
                if (item.Ngay.Date == thu6 && item.Buoi == "Sáng") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu6tiet1.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu6tiet2.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu6tiet3.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu6tiet4.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu6tiet5.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                if (item.Ngay.Date == thu6 && item.Buoi == "Chiều") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu6tiet6.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu6tiet7.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu6tiet8.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu6tiet9.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu6tiet10.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                //thu7
                if (item.Ngay.Date == thu7 && item.Buoi == "Sáng") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu7tiet1.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu7tiet2.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu7tiet3.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu7tiet4.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu7tiet5.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                if (item.Ngay.Date == thu7 && item.Buoi == "Chiều") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu7tiet6.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu7tiet7.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu7tiet8.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu7tiet9.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu7tiet10.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                //thu8
                if (item.Ngay.Date == thu8 && item.Buoi == "Sáng") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu8tiet1.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu8tiet2.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu8tiet3.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu8tiet4.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu8tiet5.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                }
                if (item.Ngay.Date == thu8 && item.Buoi == "Chiều") {
                    switch (item.Tiet) {
                        case "Tiết 1":
                            thu8tiet6.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 2":
                            thu8tiet7.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 3":
                            thu8tiet8.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 4":
                            thu8tiet9.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                        case "Tiết 5":
                            thu8tiet10.Text = item.MaLop + "\n" + item.MonHoc;
                            break;
                    }
                } else {
                    // Xử lý khi buoihoc là null
                    //MessageBox.Show("Không có buổi học");
                }
            }





        }
        private void resetTKB() {
            thu2tiet1.Text = "";
            thu2tiet2.Text = "";
            thu2tiet3.Text = "";
            thu2tiet4.Text = "";
            thu2tiet5.Text = "";
            thu2tiet6.Text = "";
            thu2tiet7.Text = "";
            thu2tiet8.Text = "";
            thu2tiet9.Text = "";
            thu2tiet10.Text = "";

            thu3tiet1.Text = "";
            thu3tiet2.Text = "";
            thu3tiet3.Text = "";
            thu3tiet4.Text = "";
            thu3tiet5.Text = "";
            thu3tiet6.Text = "";
            thu3tiet7.Text = "";
            thu3tiet8.Text = "";
            thu3tiet9.Text = "";
            thu3tiet10.Text = "";

            thu4tiet1.Text = "";
            thu4tiet2.Text = "";
            thu4tiet3.Text = "";
            thu4tiet4.Text = "";
            thu4tiet5.Text = "";
            thu4tiet6.Text = "";
            thu4tiet7.Text = "";
            thu4tiet8.Text = "";
            thu4tiet9.Text = "";
            thu4tiet10.Text = "";

            thu5tiet1.Text = "";
            thu5tiet2.Text = "";
            thu5tiet3.Text = "";
            thu5tiet4.Text = "";
            thu5tiet5.Text = "";
            thu5tiet6.Text = "";
            thu5tiet7.Text = "";
            thu5tiet8.Text = "";
            thu5tiet9.Text = "";
            thu5tiet10.Text = "";

            thu6tiet1.Text = "";
            thu6tiet2.Text = "";
            thu6tiet3.Text = "";
            thu6tiet4.Text = "";
            thu6tiet5.Text = "";
            thu6tiet6.Text = "";
            thu6tiet7.Text = "";
            thu6tiet8.Text = "";
            thu6tiet9.Text = "";
            thu6tiet10.Text = "";

            thu7tiet1.Text = "";
            thu7tiet2.Text = "";
            thu7tiet3.Text = "";
            thu7tiet4.Text = "";
            thu7tiet5.Text = "";
            thu7tiet6.Text = "";
            thu7tiet7.Text = "";
            thu7tiet8.Text = "";
            thu7tiet9.Text = "";
            thu7tiet10.Text = "";

            thu8tiet1.Text = "";
            thu8tiet2.Text = "";
            thu8tiet3.Text = "";
            thu8tiet4.Text = "";
            thu8tiet5.Text = "";
            thu8tiet6.Text = "";
            thu8tiet7.Text = "";
            thu8tiet8.Text = "";
            thu8tiet9.Text = "";
            thu8tiet10.Text = "";
        }
    }
}
