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
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace QLHOCSINHCAP3 {
    public partial class frmQuanLy : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm {
        
        public frmQuanLy() {
            InitializeComponent();
            
        }

        uc_TrangChu ucTrangChu;
        uc_ThongTinCaNhan ucThongTinCaNhan;
        uc_QuanLyHocSinh ucQuanLyHocSinh;
        uc_QuanLyLopHoc ucQuanLyLopHoc;
        uc_QuanLyTaiKhoanHocSinh ucQuanLyTaiKhoanHocSinh;
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

        private void mnQuanLyLopHoc_Click(object sender, EventArgs e) {
            if (ucQuanLyLopHoc == null) {
                ucQuanLyLopHoc = new uc_QuanLyLopHoc();
                ucQuanLyLopHoc.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyLopHoc);
                ucQuanLyLopHoc.BringToFront();

            } else
                ucQuanLyLopHoc.BringToFront();
            lblTieuDe.Caption = mnQuanLyLopHoc.Text;
        }

        private void mnQuanLyHocSinh_Click(object sender, EventArgs e) {
            if (ucQuanLyHocSinh == null) {
                ucQuanLyHocSinh = new uc_QuanLyHocSinh();
                ucQuanLyHocSinh.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyHocSinh);
                ucQuanLyHocSinh.BringToFront();

            } else
                ucQuanLyHocSinh.BringToFront();
            lblTieuDe.Caption = mnQuanLyHocSinh.Text;
        }

        private void mnQuanLyTaiKhoanHS_Click(object sender, EventArgs e) {
            if (ucQuanLyTaiKhoanHocSinh == null) {
                ucQuanLyTaiKhoanHocSinh = new uc_QuanLyTaiKhoanHocSinh();
                ucQuanLyTaiKhoanHocSinh.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyTaiKhoanHocSinh);
                ucQuanLyTaiKhoanHocSinh.BringToFront();

            } else
                ucQuanLyTaiKhoanHocSinh.BringToFront();
            lblTieuDe.Caption = ucQuanLyTaiKhoanHocSinh.Text;
        }
    }
}
