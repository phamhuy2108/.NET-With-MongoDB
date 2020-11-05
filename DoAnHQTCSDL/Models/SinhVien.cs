using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHQTCSDL.Models
{
    public class SinhVien
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string mssv { get; set; }
        [BsonElement]
        public string hoten { get; set; }
        [BsonElement]
        public DateTime ngaysinh { get; set; }
        [BsonElement]
        public string gioitinh { get; set; }
        [BsonElement]
        public double diemchuan { get; set; }
        [BsonElement]
        public double diemthi { get; set; }
        [BsonElement]
        public string truongTHPT { get; set; }
        [BsonElement]
        public string manganh { get; set; }
        [BsonElement]
        public string malop { get; set; }
        [BsonElement]
        public int khoa { get; set; }
        public SinhVien(string mssv, string hoten, DateTime ngaysinh, string gioitinh, double diemchuan, double diemthi, string truongTHPT, string manganh, string malop,int khoa)
        {
            this.mssv = mssv;
            this.hoten = hoten;
            this.ngaysinh = ngaysinh;
            this.gioitinh = gioitinh;
            this.diemchuan = diemchuan;
            this.diemthi = diemthi;
            this.truongTHPT = truongTHPT;
            this.manganh = manganh;
            this.malop = malop;
            this.khoa = khoa;
        }
        public SinhVien()
        {

        }

        private IMongoCollection<SinhVien> collection;
        public List<SinhVien> listSinhVien()
        {
            List<SinhVien> list = null;
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            list = collection.Find(FilterDefinition<SinhVien>.Empty).ToList();
            return list;
        }
        public void themSinhVien(SinhVien sinhvien)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            collection.InsertOne(sinhvien);
        }
        public SinhVien timsinhvien(string mssv)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            SinhVien sinhvien = collection.Find(e => e.mssv == mssv).FirstOrDefault();
            return sinhvien;
        }
        public List<SinhVien> timsinhvien1(string hoten)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            List<SinhVien> sinhvien = collection.Find(e => e.hoten.Contains(hoten)).ToList();
            return sinhvien;
        }
        public List<SinhVien> timsinhvien2(string mssv)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            List<SinhVien> sinhvien = collection.Find(e => e.mssv==mssv).ToList();
            return sinhvien;
        }
        public SinhVien timsinhvien3(string manganh)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            SinhVien sinhvien = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            return sinhvien;
        }
        public SinhVien timsinhvien4(string malop)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            SinhVien sinhvien = collection.Find(e => e.malop == malop).FirstOrDefault();
            return sinhvien;
        }
        public List<SinhVien> listsinhvien2(string mssv)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            List<SinhVien> sinhvien = collection.Find(e => e.mssv != mssv).ToList();
            return sinhvien;
        }
        public void suasinhvien(string mssv, string hoten, DateTime ngaysinh, string gioitinh, double diemchuan, double diemthi, string truongTHPT, string manganh, string malop)
        {
            var filter = Builders<SinhVien>.Filter.Eq("mssv", mssv);
            var updateDef = Builders<SinhVien>.Update.Set("hoten", hoten);
            updateDef = updateDef.Set("ngaysinh", ngaysinh);
            updateDef = updateDef.Set("gioitinh", gioitinh);
            updateDef = updateDef.Set("diemchuan", diemchuan);
            updateDef = updateDef.Set("diemthi", diemthi);
            updateDef = updateDef.Set("truongTHPT", truongTHPT);
            updateDef = updateDef.Set("manganh", manganh);
            updateDef = updateDef.Set("malop", malop);
            updateDef = updateDef.Set("khoa", khoa);
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            SinhVien sinhvien = collection.Find(e => e.mssv == mssv).FirstOrDefault();
            var result = collection.UpdateOne(filter, updateDef);
        }
        public void xoasinhvien(string masinhvien)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<SinhVien>("sinhvien");
            //Nganh ng = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            collection.DeleteOne<SinhVien>(e => e.mssv == masinhvien);
        }
    }
}