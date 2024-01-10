using DevExpress.XtraBars;
using MongoDB.Bson;
using QLHOCSINHCAP3.Thuvien;
using QLHOCSINHCAP3.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLHOCSINHCAP3 {
    public partial class frmGiaoVien : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm {
        private string maGiaoVien;
        public ObjectId userSend { set; get; }
        public ObjectId userRecive { set; get; }
        public frmGiaoVien(string maGiaoVien, ObjectId _userSend, ObjectId _userRecevie) {
            InitializeComponent();
            this.maGiaoVien = maGiaoVien;
            this.userSend = _userSend;
            this.userRecive = _userRecevie;

        }
        uc_TrangChu ucTrangChu;
        uc_ThoiKhoaBieuGV ucThoiKhoaBieuGV;
        uc_LopDay ucLopDay;
        uc_DoiMatKhau ucDoiMatKhau;
        uc_ThongTinCaNhanGV ucThongTinCaNhan;
        uc_Chat ucChat;
        uc_ChatSV ucChatSV;
        uc_chat_all ucchatall;
        private ObjectId _objectId;

        public void SetObjects(ObjectId objectId)
        {
            _objectId = objectId;
        }
        private void mnTrangChu_Click(object sender, EventArgs e) {

            if (ucTrangChu == null) { 
                ucTrangChu = new uc_TrangChu();
                ucTrangChu.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucTrangChu);
                ucTrangChu.BringToFront();

            }
            else
                ucTrangChu.BringToFront();
            lblTieuDe.Caption = mnTrangChu.Text;
        }

        private void mnThongTinCaNhan_Click(object sender, EventArgs e) {
            if (ucThongTinCaNhan == null) {
                ucThongTinCaNhan = new uc_ThongTinCaNhanGV(maGiaoVien);
                ucThongTinCaNhan.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucThongTinCaNhan);
                ucThongTinCaNhan.BringToFront();

            } else
                ucThongTinCaNhan.BringToFront();
            lblTieuDe.Caption = mnThongTinCaNhan.Text;
        }

        private void mnThoiKhoaBieu_Click(object sender, EventArgs e) {
            if (ucThoiKhoaBieuGV == null) {
                ucThoiKhoaBieuGV = new uc_ThoiKhoaBieuGV(maGiaoVien);
                ucThoiKhoaBieuGV.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucThoiKhoaBieuGV);
                ucThoiKhoaBieuGV.BringToFront();

            } else
                ucThoiKhoaBieuGV.BringToFront();
            lblTieuDe.Caption = mnThoiKhoaBieu.Text;
        }

        private void mnLopDay_Click(object sender, EventArgs e) {
            if (ucLopDay == null) {
                ucLopDay = new uc_LopDay();
                ucLopDay.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucLopDay);
                ucLopDay.BringToFront();

            } else
                ucLopDay.BringToFront();
            lblTieuDe.Caption = mnLopDay.Text;
        }

        private void mnDoiMatKhau_Click(object sender, EventArgs e) {
            if (ucDoiMatKhau == null) {
                ucDoiMatKhau = new uc_DoiMatKhau(maGiaoVien);
                ucDoiMatKhau.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucDoiMatKhau);
                ucDoiMatKhau.BringToFront();

            } else
                ucDoiMatKhau.BringToFront();
            lblTieuDe.Caption = mnDoiMatKhau.Text;
        }

        private void mnDangXuat_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void frmGiaoVien_Load(object sender, EventArgs e) { 
            ucTrangChu = new uc_TrangChu();
            ucTrangChu.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucTrangChu);
            ucTrangChu.BringToFront();
            lblTieuDe.Caption = mnTrangChu.Text;
        }

        private void mnchat_Click(object sender, EventArgs e)
        {
            if (ucchatall == null)
            {
                ucchatall = new uc_chat_all(userSend, new ObjectId());
                ucchatall.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucchatall);
                ucchatall.BringToFront();
            }
            else
                ucchatall.BringToFront();
            lblTieuDe.Caption = ucchatall.Text;
        }
    }
}

