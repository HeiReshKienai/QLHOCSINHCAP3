using MongoDB.Bson;
using MongoDB.Driver;
using QLHOCSINHCAP3.Thuvien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Contexts;

namespace QLHOCSINHCAP3 {
    public partial class frmQuenMatKhau : Form {
        private string verification_code;
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanHocSinh> collectionTaiKhoanHocSinh;
        private IMongoCollection<HocSinh> collectionHocSinh;
        private IMongoCollection<TaiKhoanGiaoVien> collectionTaiKhoanGiaoVien;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        public frmQuenMatKhau() {
            InitializeComponent();
            //gán mã xác nhận vào biến

            verification_code = MaXacNhan();
            db = client.GetDatabase("QLHocSinhCap3");
            collectionTaiKhoanHocSinh = db.GetCollection<TaiKhoanHocSinh>("TaiKhoanHocSinh");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");

            collectionTaiKhoanGiaoVien = db.GetCollection<TaiKhoanGiaoVien>("TaiKhoanGiaoVien");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            cbbLoaiTaiKhoan.SelectedIndex = 0;
        }
        private string MaXacNhan() {
            Random random = new Random();
            int code = random.Next(000000, 999999);
            return code.ToString();
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



        private async void btnGui_Click(object sender, EventArgs e) {
            string tentk = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;
            string loaitk = cbbLoaiTaiKhoan.SelectedItem.ToString();
            string email = "";

            // Kiểm tra nếu có @ thì đó là email và gán vào email
            if (tentk.Contains("@")) {
                email = tentk;
            } else {
                // Không thì truy vấn email trong database và gán vào email
                if (loaitk == "Học Sinh") {
                    var dulieuTKHS = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();
                    var account = dulieuTKHS.FirstOrDefault(gv => gv.MSHS == tentk);
                    if (account != null) {
                        email = account.Email;
                    } else {
                        MessageBox.Show("Không tìm thấy tài khoản!", "Thông Báo");
                        return;
                    }
                } else if (loaitk == "Giáo Viên") {
                    var dulieuTKGV = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();
                    var account = dulieuTKGV.FirstOrDefault(gv => gv.MaGV == tentk);
                    if (account != null) {
                        email = account.Email;
                    } else {
                        MessageBox.Show("Không tìm thấy tài khoản!", "Thông Báo");
                        return;
                    }
                } else {
                    // Chưa chọn loại tài khoản
                    MessageBox.Show("Vui lòng chọn loại tài khoản!", "Thông Báo");
                    return;
                }
            }

            string from = "kienadu123@gmail.com";  // Email dùng để gửi mã
            string pass = "dqmhphpedogykeyj";  // Mật khẩu email

            try {
                // Soạn tin nhắn để gửi
                MailMessage message = new MailMessage();
                message.To.Add(email);
                message.From = new MailAddress(from, "Quên Mật Khẩu");
                // Generate the verification code or get it from somewhere
                message.Body = verification_code;
                message.Subject = "Mã xác nhận của bạn";

                // Sử dụng SmtpClient smtp để gửi
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);

                // Gửi email
                await smtp.SendMailAsync(message);

                MessageBox.Show("Đang gửi mã xác nhận tới email: " + email, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnGui.Enabled = false; // Khi gửi sẽ khóa nút gửi
                btnGui.BackColor = Color.Gray; // Nút chuyển sang màu xám

                int delayInSeconds = 60; // Cho thời gian 60 giây
                btnThoiGian.Text = delayInSeconds.ToString();

                while (delayInSeconds > 0) {
                    await Task.Delay(1000); // Đợi 1 giây
                    delayInSeconds--;
                    btnThoiGian.Text = delayInSeconds.ToString();
                }

                // Mở khóa nút và cho gửi lại
                btnGui.Enabled = true;
                btnGui.BackColor = Color.Teal;
            } catch (Exception ex) {
                // Nếu lỗi khi gửi mail sẽ báo lỗi
                MessageBox.Show("Lỗi khi gửi đến mail, vui lòng kiểm tra lại mail hoặc nhập lại mã xác nhận.\n" + ex.Message);
            }
        }



        private void btnLayLaiMatKhau_Click(object sender, EventArgs e) {

            string tentk = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;
            string nhaplaimatkhau = txtNhapLaiMatKhau.Text;
            string hashPassword = HashPassword(matkhau);
            string loaitk = cbbLoaiTaiKhoan.SelectedItem.ToString();

            //hàm kiểm tra xem nhập đúng yêu cầu không nếu không sẽ thông báo
            if (string.IsNullOrEmpty(tentk)) {
                MessageBox.Show("Vui lòng nhập tài khoản !", "Thông Báo");
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


            //hàm kiểm tra mã nếu không đúng sẽ bắt nhập mã lại
            string userEnteredCode = txtMaXacNhan.Text.Trim();
            if (userEnteredCode != verification_code) {
                MessageBox.Show("Mã xác nhận sai vui lòng kiểm tra lại Mail.");
                return;
            }
            //kiểm tra Tai khoan Ton tai k

            if (loaitk == "Giáo Viên") {
                var dulieuTKGV = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();

                var accountExists = dulieuTKGV.Any(gv => gv.MaGV == tentk);
                var emailExists = dulieuTKGV.Any(gv => gv.Email == tentk);
                if (accountExists || emailExists) {
                    MessageBox.Show("Lấy lại mật khẩu thành công!", "Thông Báo");

                    var filter = Builders<TaiKhoanGiaoVien>.Filter.Where(gv => gv.MaGV == tentk || gv.Email == tentk);
                    var update = Builders<TaiKhoanGiaoVien>.Update.Set(gv => gv.MatKhau, hashPassword);

                    collectionTaiKhoanGiaoVien.UpdateOne(filter, update);

                    this.Close();
                    return;
                }
            }

            if (loaitk == "Học Sinh") {
                var dulieuTKHS = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();

                var accountExists = dulieuTKHS.Any(hs => hs.MSHS == tentk);
                var emailExists = dulieuTKHS.Any(hs => hs.Email == tentk);
                if (accountExists || emailExists) {
                    MessageBox.Show("Lấy lại mật khẩu thành công!", "Thông Báo");

                    var filter = Builders<TaiKhoanHocSinh>.Filter.Where(hs => hs.MSHS == tentk || hs.Email == tentk);
                    var update = Builders<TaiKhoanHocSinh>.Update.Set(hs => hs.MatKhau, hashPassword);

                    collectionTaiKhoanHocSinh.UpdateOne(filter, update);

                    this.Close();
                    return;
                }
            }




        }
    } 

    }


