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
        uc_ThongTinCaNhanGV ucThongTinCaNhan;
        uc_QuanLyHocSinh ucQuanLyHocSinh;
        uc_QuanLyLopHoc ucQuanLyLopHoc;
        uc_QuanLyTaiKhoanHocSinh ucQuanLyTaiKhoanHocSinh;
        uc_QuanLyTaiKhoanGiaoVien ucQuanLyTaiKhoanGiaoVien;
        uc_QuanLyGiaoVien ucQuanLyGiaoVien;
        uc_QuanLyMonHoc ucQuanLyMonHoc;
        uc_QuanLyBuoiHoc ucQuanLyBuoiHoc;
        private void mnTrangChu_Click(object sender, EventArgs e) {

            if (ucTrangChu == null) {
                ucTrangChu = new uc_TrangChu();
                ucTrangChu.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucTrangChu);


            } else {
                mainContainer.Controls.Remove(ucTrangChu);
                ucTrangChu = new uc_TrangChu();
                ucTrangChu.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucTrangChu);
            }
            ucTrangChu.BringToFront();
            lblTieuDe.Caption = mnTrangChu.Text;
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


            } else {
                mainContainer.Controls.Remove(ucQuanLyLopHoc);
                ucQuanLyLopHoc = new uc_QuanLyLopHoc();
                ucQuanLyLopHoc.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyLopHoc);

            }
            ucQuanLyLopHoc.BringToFront();
            lblTieuDe.Caption = mnQuanLyLopHoc.Text;
        }

        private void mnQuanLyHocSinh_Click(object sender, EventArgs e) {
            if (ucQuanLyHocSinh == null) {
                ucQuanLyHocSinh = new uc_QuanLyHocSinh();
                ucQuanLyHocSinh.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyHocSinh);


            } else {
                mainContainer.Controls.Remove(ucQuanLyHocSinh);
                ucQuanLyHocSinh = new uc_QuanLyHocSinh();
                ucQuanLyHocSinh.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyHocSinh);

            }
            ucQuanLyHocSinh.BringToFront();
            lblTieuDe.Caption = mnQuanLyHocSinh.Text;
        }

        private void mnQuanLyTaiKhoanHS_Click(object sender, EventArgs e) {
            if (ucQuanLyTaiKhoanHocSinh == null) {
                ucQuanLyTaiKhoanHocSinh = new uc_QuanLyTaiKhoanHocSinh();
                ucQuanLyTaiKhoanHocSinh.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyTaiKhoanHocSinh);


            } else {
                mainContainer.Controls.Remove(ucQuanLyTaiKhoanHocSinh);
                ucQuanLyTaiKhoanHocSinh = new uc_QuanLyTaiKhoanHocSinh();
                ucQuanLyTaiKhoanHocSinh.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyTaiKhoanHocSinh);
            }
            ucQuanLyTaiKhoanHocSinh.BringToFront();
            lblTieuDe.Caption = ucQuanLyTaiKhoanHocSinh.Text;
        }

        private void mnQuanLyTaiKhoanGV_Click(object sender, EventArgs e) {
            if (ucQuanLyTaiKhoanGiaoVien == null) {
                ucQuanLyTaiKhoanGiaoVien = new uc_QuanLyTaiKhoanGiaoVien();
                ucQuanLyTaiKhoanGiaoVien.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyTaiKhoanGiaoVien);


            } else {
                mainContainer.Controls.Remove(ucQuanLyTaiKhoanGiaoVien);
                ucQuanLyTaiKhoanGiaoVien = new uc_QuanLyTaiKhoanGiaoVien();
                ucQuanLyTaiKhoanGiaoVien.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyTaiKhoanGiaoVien);
            }
            ucQuanLyTaiKhoanGiaoVien.BringToFront();
            lblTieuDe.Caption = ucQuanLyTaiKhoanGiaoVien.Text;
        }

        private void mnQuanLyGiaoVien_Click(object sender, EventArgs e) {
            if (ucQuanLyGiaoVien == null) {
                ucQuanLyGiaoVien = new uc_QuanLyGiaoVien();
                ucQuanLyGiaoVien.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyGiaoVien);


            } else {
                mainContainer.Controls.Remove(ucQuanLyGiaoVien);
                ucQuanLyGiaoVien = new uc_QuanLyGiaoVien();
                ucQuanLyGiaoVien.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyGiaoVien);

            }

            ucQuanLyGiaoVien.BringToFront();
            lblTieuDe.Caption = mnQuanLyGiaoVien.Text;
        }

        private void mnQuanLyMonHoc_Click(object sender, EventArgs e) {
            if (ucQuanLyMonHoc == null) {
                ucQuanLyMonHoc = new uc_QuanLyMonHoc();
                ucQuanLyMonHoc.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyMonHoc);


            } else {
                mainContainer.Controls.Remove(ucQuanLyMonHoc);
                ucQuanLyMonHoc = new uc_QuanLyMonHoc();
                ucQuanLyMonHoc.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyMonHoc);
            }
            ucQuanLyMonHoc.BringToFront();
            lblTieuDe.Caption = mnQuanLyMonHoc.Text;
        }

        private void mnQuanLyBuoiHoc_Click(object sender, EventArgs e) {
            if (ucQuanLyBuoiHoc == null) {
                ucQuanLyBuoiHoc = new uc_QuanLyBuoiHoc();
                ucQuanLyBuoiHoc.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyBuoiHoc);


            } else {
                mainContainer.Controls.Remove(ucQuanLyBuoiHoc);
                ucQuanLyBuoiHoc = new uc_QuanLyBuoiHoc();
                ucQuanLyBuoiHoc.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucQuanLyBuoiHoc);
            }
            ucQuanLyBuoiHoc.BringToFront();
            lblTieuDe.Caption = mnQuanLyBuoiHoc.Text;
        }
    }
}
