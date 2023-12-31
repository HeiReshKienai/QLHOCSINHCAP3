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
    public class TaiKhoanGiaoVien {
        public ObjectId Id { get; set; }
        [BsonElement("MaGV")]
        public string MaGV { get; set; }
        [BsonElement("MatKhau")]
        public string MatKhau { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }

       

        public TaiKhoanGiaoVien(string magv, string matkhau, string email) {
            MaGV = magv;
            MatKhau = matkhau;
            Email = email;

        }
    }
}
