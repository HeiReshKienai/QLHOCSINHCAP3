using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHOCSINHCAP3.Thuvien {
    public class MonHoc {
        public ObjectId Id { get; set; }
        [BsonElement("MaMon")]
        public string MaMon { get; set; }
        [BsonElement("TenMon")]
        public string TenMon { get; set; }



        public MonHoc(string mamon, string tenmon) {
            MaMon = mamon;
            TenMon = tenmon;

        }
    }
}
