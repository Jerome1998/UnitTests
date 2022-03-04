using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.AntiTests
{
    public class Repository
    {
        private List<DBObject> db = new List<DBObject>();

        public void Add(DBObject Text)
        {
            db.Add(Text);
        }

        public DBObject Get(string id)
        {
            foreach(DBObject dbObject in db)
            {
                if(dbObject.Id == id)
                {
                    return dbObject;
                }
            }
            return null;
        }

        public bool Remove(string id)
        {
            foreach( DBObject dbObject in db)
            {
                if (dbObject.Id == id)
                {
                    db.Remove(dbObject);
                    return true;
                }
            }
            return false;
        }
    }

    public class DBObject
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
