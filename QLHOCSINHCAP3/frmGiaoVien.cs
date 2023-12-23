using DevExpress.XtraBars;
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
        public frmGiaoVien() {
            InitializeComponent();
            
        }
        uc_TrangChu ucTrangChu;
        uc_ThoiKhoaBieu ucThoiKhoaBieu;
        uc_LopDay ucLopDay;
        uc_DoiMatKhau ucDoiMatKhau;
        uc_ThongTinCaNhan ucThongTinCaNhan;
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
                ucThongTinCaNhan = new uc_ThongTinCaNhan();
                ucThongTinCaNhan.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucThongTinCaNhan);
                ucThongTinCaNhan.BringToFront();

            } else
                ucThongTinCaNhan.BringToFront();
            lblTieuDe.Caption = mnThongTinCaNhan.Text;
        }

        private void mnThoiKhoaBieu_Click(object sender, EventArgs e) {
            if (ucThoiKhoaBieu == null) {
                ucThoiKhoaBieu = new uc_ThoiKhoaBieu();
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
                ucDoiMatKhau = new uc_DoiMatKhau();
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
    }
}
