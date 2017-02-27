using System.Configuration;
using LiteDB;

namespace Webmap.DBLayer {
    public class DB {
        string dbString = ConfigurationManager.AppSettings["dbString"];//prop
        LiteDatabase dbInstance;

        public DB() {
            dbInstance = new LiteDatabase(dbString);
        }
    }
}