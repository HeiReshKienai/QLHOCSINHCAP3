using MongoDB.Bson;
using MongoDB.Driver;
using QLHOCSINHCAP3.Thuvien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHOCSINHCAP3.UI {
    public partial class uc_DoiMatKhau : UserControl {
        private string maGiaoVien;

        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanGiaoVien> collectionTaiKhoanGiaoVien;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        public uc_DoiMatKhau(String maGiaoVien) {
            InitializeComponent();
            this.maGiaoVien = maGiaoVien;
            //gán mã xác nhận vào biến
            db = client.GetDatabase("QLHocSinhCap3");


            collectionTaiKhoanGiaoVien = db.GetCollection<TaiKhoanGiaoVien>("TaiKhoanGiaoVien");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");

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

        private void uc_DoiMatKhau_Click(object sender, EventArgs e) {
            string tentk = maGiaoVien;
            string matkhaucu = txMatKhauCu.Text;
            string matkhau = txtMatKhau.Text;
            string nhaplaimatkhau = txtNhapLaiMatKhau.Text;
            string hashPassword = HashPassword(matkhau);


            //hàm kiểm tra xem nhập đúng yêu cầu không nếu không sẽ thông báo

            

            if (string.IsNullOrEmpty(matkhaucu)) {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông Báo");
                return;
            }

            if (string.IsNullOrEmpty(matkhau)) {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông Báo");
                return;
            }

            if (nhaplaimatkhau != matkhau) {
                MessageBox.Show("Vui lòng xác nhận mật khẩu chính xác", "Thông Báo");
                return;
            }
            if (matkhaucu==matkhau) {
                MessageBox.Show("Mật khẩu mới không được giống mật khẩu cũ !", "Thông Báo");
                return;
            }




            //kiểm tra Tai khoan Ton tai k


            var dulieuTKGV = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();

            var accountExists1 = dulieuTKGV.Any(gv => gv.MaGV == tentk && gv.MatKhau == HashPassword(matkhaucu));
            if (!accountExists1) {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Thông Báo");
                return;
            }
            var accountExists = dulieuTKGV.Any(gv => gv.MaGV == tentk);
            if (accountExists) {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông Báo");
                

                var filter = Builders<TaiKhoanGiaoVien>.Filter.Where(gv => gv.MaGV == tentk || gv.Email == tentk);
                var update = Builders<TaiKhoanGiaoVien>.Update.Set(gv => gv.MatKhau, hashPassword);

                collectionTaiKhoanGiaoVien.UpdateOne(filter, update);

                


                return;
            }
        }


    }

       
}