using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHQTCSDL.Models
{
    public class Nganh
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string manganh { get; set; }
        [BsonElement]
        public string tennganh { get; set; }
        public Nganh(string MaNganh, string TenNganh)
        {
            this.manganh = MaNganh;
            this.tennganh = TenNganh;
        }
        public Nganh()
        {

        }

        private IMongoCollection<Nganh> collection;

        public List<Nganh> listnganh()
        {
            List<Nganh> list = null;
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Nganh>("nganh");
            list = collection.Find(FilterDefinition<Nganh>.Empty).ToList();
            return list;
        }
        public void themnganh(Nganh ngh)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Nganh>("nganh");
            collection.InsertOne(ngh);
        }
        public Nganh timnganh(string manganh)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Nganh>("nganh");
            Nganh ng = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            return ng;
        }
        public List<Nganh> timnganh2(string manganh)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Nganh>("nganh");
            List<Nganh> ng = collection.Find(e => e.manganh != manganh).ToList();
            return ng;
        }
        public void suanganh(string manganh, string tennganh)
        {
            var filter = Builders<Nganh>.Filter.Eq("manganh", manganh);
            var updateDef = Builders<Nganh>.Update.Set("tennganh", tennganh);
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Nganh>("nganh");
            Nganh ng = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            var result = collection.UpdateOne(filter, updateDef);
        }
        public void xoanganh(string manganh)
        {
            Database db = new Database();
            this.collection = db.ConnectDB().GetCollection<Nganh>("nganh");
            //Nganh ng = collection.Find(e => e.manganh == manganh).FirstOrDefault();
            collection.DeleteOne<Nganh>(e=>e.manganh==manganh);
        }
    }
}