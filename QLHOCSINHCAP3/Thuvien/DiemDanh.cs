using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHOCSINHCAP3.Thuvien {
    [Serializable]
    public class DiemDanh {
        public ObjectId Id { get; set; }
        [BsonElement("MaLop")]
        public string MaLop { get; set; }

        [BsonElement("Ngay")]
        public DateTime Ngay { get; set; }

        [BsonElement("HoTenHocSinh")]
        public string HoTenHocSinh { get; set; }

        [BsonElement("TinhTrangDiemDanh")]
        public string TinhTrangDiemDanh { get; set; }


        public DiemDanh(string malop, DateTime ngay, string hotenhocsinh,string tinhtrangdiemdanh) {  
            MaLop = malop;
            Ngay = ngay;
            HoTenHocSinh = hotenhocsinh;
            TinhTrangDiemDanh = tinhtrangdiemdanh;
        }
    }
}

