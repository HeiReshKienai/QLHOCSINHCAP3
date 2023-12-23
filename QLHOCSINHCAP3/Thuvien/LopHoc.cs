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
    public class LopHoc {
        public ObjectId Id { get; set; }
        [BsonElement("MaLop")]
        public string MaLop { get; set; }
        [BsonElement("Khoi")]
        public string Khoi { get; set; }
        [BsonElement("GVCN")]
        public string GVCN { get; set; }
        [BsonElement("SiSo")]
        public string SiSo { get; set; }
       

        public LopHoc(string malop, string khoi, string gvcn, string siso) {
            MaLop = malop;
            Khoi = khoi;
            GVCN = gvcn;
            SiSo = siso;
        }
    }
}
