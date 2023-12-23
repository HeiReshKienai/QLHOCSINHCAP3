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
    public class TaiKhoanHocSinh {
        public ObjectId Id { get; set; }
        [BsonElement("MSHS")]
        public string MSHS { get; set; }
        [BsonElement("MatKhau")]
        public string MatKhau { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }

       

        public TaiKhoanHocSinh(string mshs, string matkhau, string email) {
            MSHS = mshs;
            MatKhau = matkhau;
            Email = email;

        }
    }
}
