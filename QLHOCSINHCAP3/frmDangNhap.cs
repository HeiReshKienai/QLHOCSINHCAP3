using MongoDB.Driver;
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
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.ObjectModel;
using QLHOCSINHCAP3.Thuvien;
using System.Configuration;

namespace QLHOCSINHCAP3 {
    public partial class frmDangNhap : Form {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanHocSinh> collection;

        public frmDangNhap() {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");
            collection = db.GetCollection<TaiKhoanHS>("TaiKhoanHocSinh");
            
        }
        

        private void btnDangNhap_Click(object sender, EventArgs e) {
            string tentk = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;
            string loaitk = cbbLoaiTaiKhoan.SelectedItem.ToString();





            if (string.IsNullOrEmpty(tentk)) {
                // Tên tài khoảng bằng rỗng thì nhập lại
                MessageBox.Show("Vui lòng nhập tài khoản !");
                return;
            }

            if (string.IsNullOrEmpty(matkhau)) {
                // Mật khẩu bằng rỗng thì nhập lại
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                return;
            }
            if (tentk == "admin" && matkhau == "admin") {
                //AdminForm adminForm = new AdminForm();
                //this.Hide();
                //adminForm.ShowDialog();
                //this.Show();
                //return;
            }

            if (loaitk == "--Chọn loại tài khoản--") {
                //Chưa chọn loại tài khoản
                MessageBox.Show("Vui lòng Chọn loại tài khoản!");
                return;
            }
            if (loaitk == "Giáo Viên") {
                
                //var account = collection.Find(a => a.MaGV == tentk && a.MatKhau == matkhau);
                //if (account != null) {
                //    var maGiangVien = account.MaGV;
                //    MessageBox.Show("Đăng nhập thành công!");

                //    //this.Hide();
                //    //giangVienform.ShowDialog();
                //    //this.Show();
                //    return;
                //}
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e) {
            cbbLoaiTaiKhoan.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e) {


            //LopHoc sv = new LopHoc(txtTaiKhoan.Text, txtMatKhau.Text, txtTaiKhoan.Text, txtMatKhau.Text);

            //collection.InsertOne(sv);
            //MessageBox.Show("Them thanh cong!");
        }
    }
}
