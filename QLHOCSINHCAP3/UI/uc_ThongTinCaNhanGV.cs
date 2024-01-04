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
    public partial class uc_ThongTinCaNhanGV : UserControl {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        public uc_ThongTinCaNhanGV(string magiaovien) {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");

            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");

            var dulieuTKGV = collectionGiaoVien.Find(new BsonDocument()).ToList();
            var account = dulieuTKGV.FirstOrDefault(gv => gv.MaGV == magiaovien);
            lblMaGiangVien.Text = account.MaGV;
            lblHoTen.Text = account.HoTen;
            lblNgaySinh.Text =account.NgaySinh;
            lblGioiTinh.Text = account.GioiTinh;
            lblMonDay.Text = account.MonDay;
            lblDiaChi.Text = account.DiaChi;
            lblSDT.Text = account.SDT;



        }
    }
}
