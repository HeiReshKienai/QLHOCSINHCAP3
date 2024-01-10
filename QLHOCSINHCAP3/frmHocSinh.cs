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
    public partial class frmHocSinh : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm {
        private string maHocSinh;
        public frmHocSinh(string maHocSinh, ObjectId _userSend, ObjectId _userRecevie) {
            InitializeComponent();
            this.maHocSinh = maHocSinh;
            this.userSend = _userSend;
            this.userRecive = _userRecevie;

        }
        public ObjectId userSend { set; get; }
        public ObjectId userRecive { set; get; }
        uc_TrangChu ucTrangChu;
        uc_ThoiKhoaBieuHS ucThoiKhoaBieu;
        uc_LopDay ucLopDay;
        uc_DoiMatKhau ucDoiMatKhau;
        uc_ThongTinCaNhanHS ucThongTinCaNhan;
        uc_chat_all ucChatAll;
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
                ucThongTinCaNhan = new uc_ThongTinCaNhanHS(maHocSinh);
                ucThongTinCaNhan.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucThongTinCaNhan);
                ucThongTinCaNhan.BringToFront();

            } else
                ucThongTinCaNhan.BringToFront();
            lblTieuDe.Caption = mnThongTinCaNhan.Text;
        }

        private void mnThoiKhoaBieu_Click(object sender, EventArgs e) {
            if (ucThoiKhoaBieu == null) {
                ucThoiKhoaBieu = new uc_ThoiKhoaBieuHS(maHocSinh);
                ucThoiKhoaBieu.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucThoiKhoaBieu);
                ucThoiKhoaBieu.BringToFront();

            } else
                ucThoiKhoaBieu.BringToFront();
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
                ucDoiMatKhau = new uc_DoiMatKhau(maHocSinh);
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
            if (ucChatAll == null)
            {
                ucChatAll = new uc_chat_all(userSend, userRecive);
                ucChatAll.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucChatAll);
                ucChatAll.BringToFront();
            }
            else
                ucChatAll.BringToFront();
            lblTieuDe.Caption = ucChatAll.Text;
        }

        
    }
}
