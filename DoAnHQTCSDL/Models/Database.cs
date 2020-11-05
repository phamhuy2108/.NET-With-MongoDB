using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHQTCSDL.Models
{
    public class Database
    {
        public IMongoDatabase ConnectDB()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("qlsv"); // Cần phải lưu ý viết hoa viết thường khi dùng với database
            return database;
        }
    }
}