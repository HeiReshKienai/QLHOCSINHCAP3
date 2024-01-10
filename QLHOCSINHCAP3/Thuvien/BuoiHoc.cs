using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHOCSINHCAP3.Thuvien {
    [Serializable]
    public class BuoiHoc {
        public ObjectId Id { get; set; }
        [BsonElement("MaLop")]
        public string MaLop { get; set; }
        [BsonElement("MonHoc")]
        public string MonHoc { get; set; }
        [BsonElement("GiaoVien")]
        public string GiaoVien { get; set; }

        [BsonElement("Ngay")]
        public DateTime Ngay { get; set; }
        [BsonElement("Buoi")]
        public string Buoi { get; set; }
        [BsonElement("Tiet")]
        public string Tiet { get; set; }





        public BuoiHoc(string malop, string monhoc, string giaovien, DateTime ngay, string buoi, string tiet) {
            MaLop = malop;
            MonHoc = monhoc;
            GiaoVien = giaovien;
            Ngay = ngay;
            Buoi = buoi;
            Tiet = tiet;

        }
    }
}

