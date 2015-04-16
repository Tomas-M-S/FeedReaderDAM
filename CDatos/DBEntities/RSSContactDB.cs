using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using CDatos.StorageData;

namespace CDatos.DBEntities
{
    public class RSSContactDB
    {
        public int id { get; set; }
        public bool active { get; set; }
        public DateTime savedate { get; set; }
        public string url { get; set; }
        public string comment { get; set; }
        public string type { get; set; }
        public string title { get; set; }

        #region "Constructores"
        public RSSContactDB()
        {
            this.id =       -1;
            this.active =   false;
            this.savedate = new DateTime();
            this.url =      String.Empty;
            this.comment =  String.Empty;
            this.type =     String.Empty;
            this.title =    String.Empty;
        }

        public RSSContactDB(RSSContactDB RSStocopy)
        {
            this.id =       RSStocopy.id;
            this.active =   RSStocopy.active;
            this.savedate = new DateTime(RSStocopy.savedate.Ticks);
            this.url =      RSStocopy.url;
            this.comment =  RSStocopy.comment;
            this.type =     RSStocopy.type;
            this.title =    RSStocopy.title;
        }

        public RSSContactDB(DateTime fecha, string url, string comm, string type, string title)
        {
            this.id =       -1;
            this.active =   false;
            this.savedate = fecha;
            this.url =      url;
            this.comment =  comm;
            this.type =     type;
            this.title =    title;
        }

        public RSSContactDB(int id, bool active, DateTime fecha, string url, string comm, string type, string title)
        {
            this.id =       id;
            this.active =   active;
            this.savedate = fecha;
            this.url =      url;
            this.comment =  comm;
            this.type =     type;
            this.title =    title;
        }
        #endregion

        #region "CRUD from database"
        public static int saveRss(RSSContactDB rsstosave)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence =
                "INSERT INTO RssDirections " +
                "(SaveDate,Direction,Comment,Type,Title) " +
                "VALUES ('" +
                rsstosave.savedate.ToString() + "','" +
                rsstosave.url + "','" +
                rsstosave.comment + "','" +
                rsstosave.type + "','" +
                rsstosave.title + "')";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        public static int updateRss(RSSContactDB rsstoupdate)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence =
                "UPDATE RssDirections " +
                "SET Active = '" + rsstoupdate.active + "', " +
                "SaveDate = '" + rsstoupdate.savedate.ToString() + "', " +
                "Direction = '" + rsstoupdate.url + "', " +
                "Comment = '" + rsstoupdate.comment + "', " +
                "Type = '" + rsstoupdate.type + "', " +
                "Title = '" + rsstoupdate.title + "' " +
                "WHERE Id = " + rsstoupdate.id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        public static int deleteRss(long id)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence =
                "DELETE FROM RssDirections WHERE Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        public static DataTable /*RSSContactDB*/ retrieveRss(long id)
        {
            /*
            DBManager manDB = new DBManager();
            RSSContactDB rss = null;
            OleDbDataReader rssDBansware;
            string sqlsentence =
                "SELECT * FROM RssDirections WHERE Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            rssDBansware = manDB.executeDML(sqlsentence);
            while (rssDBansware.Read())
            {
                int col1 = rssDBansware.GetInt32(0);
                bool col2 = rssDBansware.GetBoolean(1);
                DateTime col3 = rssDBansware.GetDateTime(2);
                String col4 = rssDBansware.GetString(3);
                String col5 = rssDBansware.GetString(4);
                String col6 = rssDBansware.GetString(5);
                String col7 = rssDBansware.GetString(6);
                rss = new RSSContactDB(col1, col2, col3, col4, col5, col6, col7);
            }
            manDB.CloseDB();
            rssDBansware.Close();
             */

            DBManager manDB = new DBManager();
            string sqlsentence =
                "SELECT * FROM RssDirections WHERE Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            return dt;
        }

        public static DataTable /*List<RSSContactDB>*/ retrieveAllRss()
        {
            /*
            DBManager manDB = new DBManager();
            List<RSSContactDB> rssList = new List<RSSContactDB>();
            OleDbDataReader rssDBansware;
            string sqlsentence =
                "SELECT * FROM RssDirections";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            rssDBansware = manDB.executeDML(sqlsentence);
            while (rssDBansware.Read())
            {
                int col1 = rssDBansware.GetInt32(0);
                bool col2 = rssDBansware.GetBoolean(1);
                DateTime col3 = rssDBansware.GetDateTime(2);
                String col4 = rssDBansware.GetString(3);
                String col5 = rssDBansware.GetString(4);
                String col6 = rssDBansware.GetString(5);
                String col7 = rssDBansware.GetString(6);
                rssList.Add(new RSSContactDB(col1, col2, col3, col4, col5, col6, col7));
            }
            manDB.CloseDB();
            rssDBansware.Close();
             */

            DBManager manDB = new DBManager();
            string sqlsentence =
                "SELECT * FROM RssDirections";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            return dt;
        }
        #endregion

        #region "Other methods"
        public static RSSContactDB Clone(RSSContactDB RSStoclone)
        {
            return new RSSContactDB(RSStoclone);
        }

        public override string ToString()
        {
            string message = "{";

            message += "id=" + this.id + ", ";
            message += "active=" + this.active.ToString() + ", ";
            message += "fecha de alta=" + this.savedate.ToString() + ", ";
            message += "url=" + this.url + ", ";
            message += "comment=" + this.comment + ", ";
            message += "type=" + this.type + ", ";
            message += "title=" + this.title + "}";

            return message;
        }
        #endregion
    }
}
