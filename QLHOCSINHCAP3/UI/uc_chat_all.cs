using DevExpress.Entity.Model.DescendantBuilding;
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

namespace QLHOCSINHCAP3.UI
{
    public partial class uc_chat_all : UserControl
    {
        public ObjectId userSend { set; get; }
        public ObjectId userRecive { set; get; }


        private IMongoClient client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase db;
        private IMongoCollection<MessageChat_> collectionMessageChat;
        private IMongoCollection<HocSinh> collectionHocSinh;
        private IMongoCollection<GiaoVien> collectionGiaoVien;
        private IMongoCollection<TaiKhoanGiaoVien> collectionTKGiaoVien;
        private IMongoCollection<TaiKhoanHocSinh> collectionTKHocSinh;






        public uc_chat_all(ObjectId _userSend, ObjectId _userRecevie)
        {
            InitializeComponent();
            db = client.GetDatabase("QLHocSinhCap3");

            collectionMessageChat = db.GetCollection<MessageChat_>("MessageChat");
            collectionGiaoVien = db.GetCollection<GiaoVien>("GiaoVien");
            collectionHocSinh = db.GetCollection<HocSinh>("HocSinh");
            collectionTKGiaoVien = db.GetCollection<TaiKhoanGiaoVien>("TaiKhoanGiaoVien");
            collectionTKHocSinh = db.GetCollection<TaiKhoanHocSinh>("TaiKhoanHocSinh");
            this.userSend = _userSend;
            this.userRecive = _userRecevie;

            GetAllData();
        }


        private void GetAllData()
        {
            // Lọc tin nhắn theo người gửi
            var filter = Builders<MessageChat_>.Filter.Empty;
            var rs = collectionMessageChat.Find(filter).ToList().Where(x => x.UserSend == userSend);


            var hsrs = collectionTKHocSinh.Find(Builders<TaiKhoanHocSinh>.Filter.Empty).ToList().Where(X => X.Id == userSend);
            var gvrs = collectionTKGiaoVien.Find(Builders<TaiKhoanGiaoVien>.Filter.Empty).ToList().Where(X => X.Id == userSend);


            var hslist = new List<TaiKhoanHocSinh>();
            var hlist = new List<HocSinh>();
            var gvlist = new List<TaiKhoanGiaoVien>();
            var glist = new List<GiaoVien>();


            // Kiểm tra nếu người nhận là học sinh
            if (hsrs.Count() == 0 || hsrs == null)
            {
                foreach (var item in rs)
                {
                    hslist.Add(collectionTKHocSinh.Find(Builders<TaiKhoanHocSinh>.Filter.Empty).ToList().Where(x => x.Id == item.UserRecevie).FirstOrDefault());
                }

                foreach (var item in hslist)
                {
                    hlist.Add(collectionHocSinh.Find(Builders<HocSinh>.Filter.Empty).ToList().Where(x => x.MSHS == item.MSHS).FirstOrDefault());
                }

                foreach (var item in collectionHocSinh.Find(Builders<HocSinh>.Filter.Empty).ToList()) {
                    var accountId = collectionTKHocSinh.Find(Builders<TaiKhoanHocSinh>.Filter.Empty).ToList().FirstOrDefault(x => x.MSHS == item.MSHS)?.Id;
                    listBox1.Items.Add($"{item.HoTen}-{accountId}");
                }


            }
            else // Ngược lại, nếu người nhận GV Lấy thông tin người nhận giáo viên và hiển thị
            {
                foreach (var item in rs)
                {
                    gvlist.Add(collectionTKGiaoVien.Find(Builders<TaiKhoanGiaoVien>.Filter.Empty).ToList().Where(x => x.Id == item.UserRecevie).FirstOrDefault());
                }

                foreach (var item in gvlist)
                {
                    glist.Add(collectionGiaoVien.Find(Builders<GiaoVien>.Filter.Empty).ToList().Where(x => x.MaGV == item.MaGV).FirstOrDefault());
                }
                foreach (var item in collectionGiaoVien.Find(Builders<GiaoVien>.Filter.Empty).ToList()) {
                    var accountId = collectionTKGiaoVien.Find(Builders<TaiKhoanGiaoVien>.Filter.Empty).ToList().FirstOrDefault(x => x.MaGV == item.MaGV)?.Id;
                    listBox1.Items.Add($"{item.HoTen}-{accountId}");
                }


            }
        }
        private void send_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(message_send.Text.Trim()))
            {
                var rs = collectionMessageChat.Find(Builders<MessageChat_>.Filter.Empty).ToList().Where(x => x.UserSend == userSend && x.UserRecevie == userRecive).Concat(collectionMessageChat.Find(Builders<MessageChat_>.Filter.Empty).ToList().Where(x => x.UserSend == userRecive && x.UserRecevie == userSend));
                var messageChat_ = new MessageChat_(userSend, userRecive, message_send.Text.Trim(), rs.Count() + 1);
                collectionMessageChat.InsertOne(messageChat_);
            }
            if (userRecive != null && userSend != null)
            {
                GetAllMessage(userSend, userRecive);
            }
            message_send.Clear();
        }


        private void GetAllMessage(ObjectId UserSend, ObjectId UserRecevie)
        {

            flowLayoutPanel1.Controls.Clear();


            var filter = Builders<MessageChat_>.Filter.Empty;
            var rs = collectionMessageChat.Find(filter).ToList().Where(x => x.UserSend == userSend && x.UserRecevie == userRecive);
            var respon = collectionMessageChat.Find(filter).ToList().Where(x => x.UserSend == userRecive && x.UserRecevie == userSend);
            var allress = rs.Concat(respon).OrderBy(x => x.index).ToList();

            uc_Chat[] userControl2s = new uc_Chat[allress.Count()];
            uc_ChatSV[] userControl3s = new uc_ChatSV[allress.Count()];



            for (int i = 0; i < allress.Count(); i++)
            {
                // giao vien
                if (collectionTKGiaoVien.Find(Builders<TaiKhoanGiaoVien>.Filter.Empty).ToList().Where(M => M.Id == allress[i].UserSend).Count() > 0)
                {
                    userControl2s[i] = new uc_Chat();
                    userControl2s[i].Dock = DockStyle.Top;
                    userControl2s[i].BringToFront();
                    userControl2s[i].Title = allress[i].Message;
                    userControl2s[i].SetName(collectionGiaoVien.Find(Builders<GiaoVien>.Filter.Empty).ToList().Where(x => x.MaGV == collectionTKGiaoVien.Find(Builders<TaiKhoanGiaoVien>.Filter.Empty).ToList().Where(M => M.Id == allress[i].UserSend).FirstOrDefault().MaGV).FirstOrDefault().HoTen);

                    flowLayoutPanel1.Controls.Add(userControl2s[i]);
                    flowLayoutPanel1.ScrollControlIntoView(userControl2s[i]);


                }
                // hoc sinh
                else
                {
                    userControl3s[i] = new uc_ChatSV();
                    userControl3s[i].Dock = DockStyle.Top;
                    userControl3s[i].BringToFront();
                    userControl3s[i].Title = allress[i].Message;
                    userControl3s[i].SetName(collectionHocSinh.Find(Builders<HocSinh>.Filter.Empty).ToList().Where(x => x.MSHS == collectionTKHocSinh.Find(Builders<TaiKhoanHocSinh>.Filter.Empty).ToList().Where(M => M.Id == allress[i].UserSend).FirstOrDefault().MSHS).FirstOrDefault().HoTen);


                    flowLayoutPanel1.Controls.Add(userControl3s[i]);
                    flowLayoutPanel1.ScrollControlIntoView(userControl3s[i]);




                }
            }

            // Đảo ngược thứ tự hiển thị tin nhắn
            ReverseControlsOrder(flowLayoutPanel1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null) {
                string selectedItem = listBox1.SelectedItem.ToString();
                string[] keyvalue = selectedItem.Split('-');
                var newid = new ObjectId(keyvalue[1].Trim());
                this.userRecive = newid; 
                if (userRecive != null && userSend != null) {
                    flowLayoutPanel1.Controls.Clear();

                    GetAllMessage(userSend, userRecive);
                }
            }
        }
        // Hàm đảo ngược thứ tự hiển thị các tin nhắn
        private void ReverseControlsOrder(Panel panel)
        {
            List<Control> controls = new List<Control>();

            foreach (Control control in panel.Controls)
            {
                controls.Add(control);
            }

            panel.Controls.Clear();

            for (int i = controls.Count - 1; i >= 0; i--)
            {
                panel.Controls.Add(controls[i]);
            }
        }

        private void uc_chat_all_Load(object sender, EventArgs e)
        {

        }
    }
}
