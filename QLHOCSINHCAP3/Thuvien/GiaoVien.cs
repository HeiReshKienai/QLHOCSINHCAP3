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
    public class GiaoVien {
        public ObjectId Id { get; set; }
        [BsonElement("MaGV")]
        public string MaGV { get; set; }
        [BsonElement("HoTen")]
        public string HoTen { get; set; }
        [BsonElement("NgaySinh")]
        public string NgaySinh { get; set; }

        [BsonElement("GioiTinh")]
        public string GioiTinh { get; set; }
        [BsonElement("MonDay")]
        public string MonDay { get; set; }
        [BsonElement("DiaChi")]
        public string DiaChi { get; set; }
        [BsonElement("SDT")]
        public string SDT { get; set; }




        public GiaoVien(string magv, string hoten, string ngaysinh,string gioitinh, string monday, string diachi, string sdt) {
            MaGV = magv;
            HoTen = hoten;
            NgaySinh = ngaysinh;
            MonDay = monday;
            GioiTinh = gioitinh;           
            DiaChi = diachi;
            SDT = sdt;
      

        }
    }
}
