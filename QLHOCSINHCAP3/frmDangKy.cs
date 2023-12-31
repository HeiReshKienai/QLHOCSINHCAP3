using DevExpress.ClipboardSource.SpreadsheetML;
using MongoDB.Bson;
using MongoDB.Driver;
using QLHOCSINHCAP3.Thuvien;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHOCSINHCAP3 {
    public partial class frmDangKy : Form {
        private string verification_code;
        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<TaiKhoanHocSinh> collectionTaiKhoanHocSinh;
        private IMongoCollection<HocSinh> collectionHocSinh;
        private IMongoCollection<TaiKhoanGiaoVien> collectionTaiKhoanGiaoVien;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        public frmDangKy() {
            InitializeComponent();
            //gán mã xác nhận vào biến
            verification_code = MaXacNhan();
            db = client.GetDatabase("QLHocSinhCap3");
            collectionTaiKhoanHocSinh = db.GetCollection<TaiKhoanHocSinh>("TaiKhoanHocSinh");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");
        }
        private string MaXacNhan() {
            Random random = new Random();
            int code = random.Next(000000, 999999);
            return code.ToString();
        }
        private void frmDangKy_Load(object sender, EventArgs e) {
            cbbLoaiTaiKhoan.SelectedIndex = 0;
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

        private void btnDangKy_Click(object sender, EventArgs e) {
            try {
                string tentk = txtTaiKhoan.Text;
                string matkhau = txtMatKhau.Text;
                string nhaplaimatkhau = txtNhapLaiMatKhau.Text;
                string hashPassword = HashPassword(matkhau);
                string loaitk = cbbLoaiTaiKhoan.SelectedItem.ToString();
                string email = txtEmail.Text;
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
                if (string.IsNullOrEmpty(email)) {
                    MessageBox.Show("Vui lòng nhập Email !", "Thông Báo");
                    return;
                }

                //hàm kiểm tra mã nếu không đúng sẽ bắt nhập mã lại
                string userEnteredCode = txtMaXacNhan.Text.Trim();
                if (userEnteredCode != verification_code) {
                    MessageBox.Show("Mã xác nhận sai vui lòng kiểm tra lại Mail.");
                    return;
                }
                //kiểm tra Tai khoan được đăng ký chưa trong cơ sở dữ liệu
                bool accountExists = false;
                bool tontai = true;
                if (loaitk == "Giáo Viên") {
                    var dulieuTKGV = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();
                    var dulieuGV = collectionGiaoVien.Find(new BsonDocument()).ToList();

                    accountExists = dulieuTKGV.Any(gv => gv.MaGV == tentk);
                    tontai = dulieuGV.Any(sv => sv.MaGV == tentk);
                }
                if (loaitk == "Học Sinh") {
                    var dulieuTKHS = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();
                    var dulieuHS = collectionHocSinh.Find(new BsonDocument()).ToList();

                    accountExists = dulieuTKHS.Any(sv => sv.MSHS == tentk);
                    tontai = dulieuHS.Any(gv => gv.MSHS == tentk);
                }

                if (accountExists) {
                    MessageBox.Show("Tài khoản đã tồn tại nếu quên Vui lòng quên mật khẩu để lấy lại mật khẩu.", "Thông Báo");
                    return;
                }
                if (!tontai) {
                    MessageBox.Show("Không tìm thấy mã Giáo Viên hoặc mã học sinh trong hệ thống vui lòng kiểm tra lại.", "Thông Báo");
                    return;
                }

                //kiểm tra email được đăng ký chưa trong cơ sở dữ liệu
                bool emailExists = false;

                if (loaitk == "Giảng Viên") {
                    var dulieuTKGV = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();
                    emailExists = dulieuTKGV.Any(gv => gv.Email == email);
                } else if (loaitk == "Sinh Viên") {
                    var dulieuTKHS = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();
                    emailExists = dulieuTKHS.Any(sv => sv.Email == email);
                } else {
                    MessageBox.Show("Vui long chọn loại tài khoản  vui lòng đăng nhập", "Thông báo");
                    return;
                }
                if (emailExists) {
                    MessageBox.Show("Email đã được đăng ký vui lòng sử dụng email khác.", "Thông Báo");
                    return;
                }

                //Thêm vào cơ sở dử liệu
                if (loaitk == "Giáo Viên") {

                    TaiKhoanGiaoVien tk = new TaiKhoanGiaoVien(tentk, hashPassword, email);

                    collectionTaiKhoanGiaoVien.InsertOne(tk);
                    MessageBox.Show("Đăng ký thành công! Giáo viên", "Thông báo");
                } else if (loaitk == "Sinh Viên") {

                    TaiKhoanHocSinh tk = new TaiKhoanHocSinh(tentk, hashPassword, email);

                    collectionTaiKhoanHocSinh.InsertOne(tk);

                    MessageBox.Show("Đăng ký thành công!Học Sinh vui lòng đăng nhập", "Thông báo");
                } else {
                    MessageBox.Show("Vui long chọn loại tài khoản  ", "Thông báo");
                    return;
                }
                this.Close();
            } catch (Exception ex) {
                //nếu lỗi khi gửi mail sẽ báo lỗi
                MessageBox.Show("Lỗi " + ex.Message);

            }
        }
        private async void btnGui_Click(object sender, EventArgs e) {
            string tentk = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;
            string email = txtEmail.Text;
            string loaitk = cbbLoaiTaiKhoan.SelectedItem.ToString();
            if (string.IsNullOrEmpty(email)) {
                MessageBox.Show("Vui lòng nhập Email!", "Thông Báo");
                return;
            }
            //kiểm tra email được đăng ký chưa trong cơ sở dữ liệu
            bool emailExists = false;

            if (loaitk == "Giáo Viên") {
                //var dulieuTKGV = collectionTaiKhoanGiaoVien.Find(new BsonDocument()).ToList();
                //emailExists = dulieuTKGV.Any(gv => gv.Email == email);
                MessageBox.Show("Thanh Conmg");
            } else if (loaitk == "Học Sinh") {
                var dulieuTKHS = collectionTaiKhoanHocSinh.Find(new BsonDocument()).ToList();
                emailExists = dulieuTKHS.Any(sv => sv.Email == email);
                MessageBox.Show("Thanh Conmg");
            } else {
                MessageBox.Show("Vui lòng chọn loại tài khoản", "Thông báo");
                return;
            }

            if (emailExists) {
                MessageBox.Show("Email đã được đăng ký vui lòng sử dụng email khác.", "Thông Báo");
                return;
            }
            string from = "kienadu123@gmail.com";  // email dùng để gửi mã
            string pass = "dqmhphpedogykeyj";  // key email

            //soạn tin nhắn để gửi
            MailMessage message = new MailMessage();
            message.To.Add(email);
            message.From = new MailAddress(from);
            message.Body = verification_code;
            message.Subject = "Mã xác nhận của bạn";

            //sửa dụng SmtpClient smtp để gửi
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try {
                //gửi
                smtp.Send(message);
                MessageBox.Show("Đang gửi mã xác nhận tới email: " + email, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGui.Enabled = false; //khi gửi sẽ khóa nút gửi
                btnGui.BackColor = Color.Gray; // nút chuyển sang màu xám
                int delayInSeconds = 60;  // cho thời gian 60 giây
                btnThoiGian.Text = delayInSeconds.ToString();

                while (delayInSeconds > 0) {
                    await Task.Delay(1000);  // đợi 1 giây
                    delayInSeconds--;
                    btnThoiGian.Text = delayInSeconds.ToString();
                }
                //mở khóa nút và cho gửi lại
                btnGui.Enabled = true;
                btnGui.BackColor = Color.Teal;

            } catch (Exception ex) {
                //nếu lỗi khi gửi mail sẽ báo lỗi
                MessageBox.Show("Lỗi khi gửi đến mail vui lòng xem lại mail hoặc nhập lại mã xác nhận" + ex.Message);
            }
        }
    }
}
