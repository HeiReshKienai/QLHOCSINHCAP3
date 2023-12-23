using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Filtering.Templates;

namespace QLHOCSINHCAP3.Thuvien {
    [Serializable]
    public class HocSinh {
        public ObjectId Id { get; set; }
        [BsonElement("MSHS")]
        public string MSHS { get; set; }
        [BsonElement("HoTen")]
        public string HoTen { get; set; }
        [BsonElement("NgaySinh")]
        public string NgaySinh { get; set; }

        [BsonElement("GioiTinh")]
        public string GioiTinh { get; set; }
        [BsonElement("Lop")]
        public string Lop { get; set; }
        [BsonElement("DiaChi")]
        public string DiaChi { get; set; }
        [BsonElement("SDT")]
        public string SDT { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("HoTenPhuHuynh")]
        public string HoTenPhuHuynh { get; set; }
        [BsonElement("NamHoc")]
        public string NamHoc { get; set; }

        public HocSinh(string mshs, string hoten, string ngaysinh,string gioitinh, string lop, string diachi, string sdt, string email,string hotenphuhuynh, string namhoc) {
            MSHS = mshs;
            HoTen = hoten;
            NgaySinh = ngaysinh;
            Lop = lop;
            GioiTinh = gioitinh;           
            DiaChi = diachi;
            SDT = sdt;
            Email = email;        
            HoTenPhuHuynh = hotenphuhuynh;
            NamHoc = namhoc;
        }
    }
}
