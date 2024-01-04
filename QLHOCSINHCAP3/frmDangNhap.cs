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
using MongoDB.Bson;
using System.Collections.ObjectModel;
using QLHOCSINHCAP3.Thuvien;
using System.Configuration;
using System.Security.Cryptography;

namespace QLHOCSINHCAP3 {
    public partial class frmDangNhap : Form {
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanHocSinh> collectionTaiKhoanHocSinh;
        private IMongoCollection<TaiKhoanGiaoVien> collectionTaiKhoanGiaoVien;
        public frmDangNhap() {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");
            collectionTaiKhoanHocSinh = db.GetCollection<TaiKhoanHocSinh>("TaiKhoanHocSinh");
            collectionTaiKhoanGiaoVien = db.GetCollection<TaiKhoanGiaoVien>("TaiKhoanGiaoVien");

        }

        public static string HashPassword(string password) {
            using (SHA256 sha256 = SHA256.Create()) {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2")); // Chuyển đổi byte thành dạng chuỗi hex
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword) {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return string.Equals(hashedEnteredPassword, storedHashedPassword, StringComparison.OrdinalIgnoreCase);
        }
        public void xoathongtin() {
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            cbbLoaiTaiKhoan.SelectedIndex = 0;
        }
        private void btnDangNhap_Click(object sender, EventArgs e) {
            string tentk = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;
            string hashPassword = HashPassword(matkhau);
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
                MessageBox.Show("Đăng nhập thành công Quản Lý!");
                xoathongtin();
                
                frmQuanLy adminForm = new frmQuanLy();
                this.Hide();
                adminForm.ShowDialog();
                this.Show();
                
                return;
            }

            if (loaitk == "--Chọn loại tài khoản--") {
                //Chưa chọn loại tài khoản
                MessageBox.Show("Vui lòng Chọn loại tài khoản!");
                return;
            }
            if (loaitk == "Giáo Viên") {

                var account = collectionTaiKhoanGiaoVien.Find(a => a.MaGV == tentk && a.MatKhau == hashPassword).ToList();
                if (account.Count > 0) {
                    var maGiaoVien = tentk;
                    MessageBox.Show("Đăng nhập thành công Giáo Viên!");
                    xoathongtin();
                    
                    frmGiaoVien giaovienfrm = new frmGiaoVien(maGiaoVien);                
                    this.Hide();
                    giaovienfrm.ShowDialog();
                    this.Show();
                    
                    return;
                }
            }
            if (loaitk == "Học Sinh") {

                var account = collectionTaiKhoanHocSinh.Find(a => a.MSHS == tentk && a.MatKhau == hashPassword).ToList();
                if (account.Count > 0) {
                    var maHocSinh = tentk;
                    MessageBox.Show("Đăng nhập thành công Học Sinh!");
                    xoathongtin();

                    frmHocSinh hocsinh = new frmHocSinh(maHocSinh);
                    this.Hide();
                    hocsinh.ShowDialog();
                    this.Show();

                    return;
                }
            } else {
                MessageBox.Show("Tên Đăng Nhập Hoặc Mật Khẩu Không Chính Xác");

            }

        }

        private void frmDangNhap_Load(object sender, EventArgs e) {
            cbbLoaiTaiKhoan.SelectedIndex = 0;

        }

        private void llbDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            frmDangKy dangKy = new frmDangKy();
            this.Hide();
            dangKy.ShowDialog();
            this.Show();
        }

        private void llbQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            frmQuenMatKhau quenMatKhau = new frmQuenMatKhau();
            this.Hide();
             quenMatKhau.ShowDialog();
            this.Show();
        }
    }
    
}
