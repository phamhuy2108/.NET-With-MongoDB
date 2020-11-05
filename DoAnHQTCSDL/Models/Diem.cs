using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHQTCSDL.Models
{
    public class Diem
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public int mabangdiem { get; set; }
        [BsonElement]
        public string mamon { get; set; }
        [BsonElement]
        public string mssv { get; set; }
        [BsonElement]
        public int lanthi { get; set; }
        [BsonElement]
        public double diem { get; set; }
        [BsonElement]
        public string ghichu { get; set; }
        public Diem(int mabangdiem, string mamon, string mssv, int lanthi, double diem, string ghichu)
        {
            this.mabangdiem = mabangdiem;
            this.mamon = mamon;
            this.mssv = mssv;
            this.lanthi = lanthi;
            this.diem = diem;
            this.ghichu = ghichu;
        }
        public Diem()
        {

        }
        private IMongoCollection<Diem> collection;
        public List<Diem> listdiem()
        {
            List<Diem> list = null;
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            list = collection.Find(FilterDefinition<Diem>.Empty).ToList();
            return list;
        }
        public void themDiem(Diem diem)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            collection.InsertOne(diem);
        }
        public Diem timdiem(int mabangdiem)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            Diem diem = collection.Find(e => e.mabangdiem == mabangdiem).FirstOrDefault();
            return diem;
        }
        public List<Diem> timdiem2(double diemtim)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            List<Diem> diem = collection.Find(e => e.diem == diemtim).ToList();
            return diem;
        }
        public Diem timdiem3(string mssv)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            Diem diem = collection.Find(e => e.mssv == mssv).FirstOrDefault();
            return diem;
        }
        public Diem timdiem4(string mamon)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            Diem diem = collection.Find(e => e.mamon == mamon).FirstOrDefault();
            return diem;
        }
        public void suadiem(int mabangdiem, string mamon, string mssv, int lanthi, double diem, string ghichu)
        {
            var filter = Builders<Diem>.Filter.Eq("mabangdiem", mabangdiem);
            var updateDef = Builders<Diem>.Update.Set("mamon", mamon);
            updateDef = updateDef.Set("mssv", mssv);
            updateDef = updateDef.Set("lanthi", lanthi);
            updateDef = updateDef.Set("diem", diem);
            updateDef = updateDef.Set("ghichu", ghichu);
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            Diem d = collection.Find(e => e.mabangdiem == mabangdiem).FirstOrDefault();
            var result = collection.UpdateOne(filter, updateDef);
        }
        public void xoadiem(int mabangdiem)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Diem>("diem");
            //Nganh ng = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            collection.DeleteOne<Diem>(e => e.mabangdiem == mabangdiem);
        }
    }
}