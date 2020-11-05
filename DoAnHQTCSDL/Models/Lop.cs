using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHQTCSDL.Models
{
    public class Lop
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string malop { get; set; }
        [BsonElement]
        public string GVphutrach { get; set; }
        [BsonElement]
        public string tenlop { get; set; }
        [BsonElement]
        public int siso { get; set; }
        public Lop(string MaLop, string GVPhuTrach,string TenLop,int SiSo)
        {
            this.malop = MaLop;
            this.GVphutrach = GVPhuTrach;
            this.tenlop = TenLop;
            this.siso = SiSo;
        }
        public Lop()
        {

        }
        private IMongoCollection<Lop> collection;
        public List<Lop> listlop()
        {
            List<Lop> list = null;
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Lop>("lop");
            list = collection.Find(FilterDefinition<Lop>.Empty).ToList();
            return list;
        }
        public void themLop(Lop lop)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Lop>("lop");
            collection.InsertOne(lop);
        }
        public Lop timlop(string malop)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Lop>("lop");
            Lop lop = collection.Find(e => e.malop == malop).FirstOrDefault();
            return lop;
        }
        public List<Lop> timlop2(string malop)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Lop>("lop");
            List<Lop> lop = collection.Find(e => e.malop != malop).ToList();
            return lop;
        }
        //public List<Lop> timlop1(string tenlop)
        //{
        //    Database db = new Database();
        //    this.collection = db.ConnectDB().GetCollection<Lop>("lop");
        //    List<Lop> lop = collection.Find(e => e.tenlop.Contains(tenlop)).ToList();
        //    return lop;
        //}
        public void sualop(string malop,string gvphutrach,string tenlop,int siso)
        {
            var filter = Builders<Lop>.Filter.Eq("malop", malop);
            var updateDef = Builders<Lop>.Update.Set("GVphutrach", gvphutrach);
            updateDef =  updateDef.Set("tenlop", tenlop);
            updateDef = updateDef.Set("siso", siso);
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Lop>("lop");
            Lop lop = collection.Find(e => e.malop == malop).FirstOrDefault();
            var result = collection.UpdateOne(filter,updateDef);
        }
        public void xoalop(string malop)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Lop>("lop");
            //Nganh ng = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            collection.DeleteOne<Lop>(e => e.malop == malop);
        }
    }
}