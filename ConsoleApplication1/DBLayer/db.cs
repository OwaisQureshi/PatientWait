using System.Configuration;
using LiteDB;

namespace DBLayer {
    public static class DB {
        public static string dbString = ConfigurationManager.AppSettings["dbString"];//prop
        //private LiteDatabase dbInstance;
        //public LiteDatabase Instance { get { return dbInstance; } }

        //public DB() {
        //    using (var db = new LiteDatabase(dbString)) {
        //        dbInstance = db;
        //    }
        //}
    }
}