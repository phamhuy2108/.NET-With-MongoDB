using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHQTCSDL.Models
{
    public class Mon
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string mamon { get; set; }
        [BsonElement]
        public string tenmon { get; set; }
        public Mon(string MaMon, string TenMon)
        {
            this.mamon = MaMon;
            this.tenmon = TenMon;
        }
        public Mon()
        {

        }
        private IMongoCollection<Mon> collection;

        public List<Mon> listmon()
        {
            List<Mon> list = null;
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Mon>("mon");
            list = collection.Find(FilterDefinition<Mon>.Empty).ToList();
            return list;
        }
        public void themMon(Mon mon)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Mon>("mon");
            collection.InsertOne(mon);
        }
        public Mon timmon(string mamon)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Mon>("mon");
            Mon mon = collection.Find(e => e.mamon == mamon).FirstOrDefault();
            return mon;
        }
        public List<Mon> timmon1(string tenmon)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Mon>("mon");
            List<Mon> mon = collection.Find(e => e.tenmon.Contains(tenmon)).ToList();
            return mon;
        }
        //public List<Mon> timmon2(string mamon)
        //{
        //    Database db = new Database();
        //    this.collection = db.ConnectDB().GetCollection<Mon>("mon");
        //    List<Mon> mon = collection.Find(e => e.mamon==mamon).ToList();
        //    return mon;
        //}
        public List<Mon> listmon2(string mamon)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Mon>("mon");
            List<Mon> mon = collection.Find(e => e.mamon != mamon).ToList();
            return mon;
        }
        public void suamon(string mamon, string tenmon)
        {
            var filter = Builders<Mon>.Filter.Eq("mamon", mamon);
            var updateDef = Builders<Mon>.Update.Set("tenmon", tenmon);
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Mon>("mon");
            Mon mon = collection.Find(e => e.mamon == mamon).FirstOrDefault();
            var result = collection.UpdateOne(filter, updateDef);
        }
        public void xoamon(string mamon)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Mon>("mon");
            //Nganh ng = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            collection.DeleteOne<Mon>(e => e.mamon == mamon);
        }
    }
}