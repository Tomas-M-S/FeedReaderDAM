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
            this.active =   true;
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
            string sqlsentence = "INSERT INTO RssDirections " +
                "(SaveDate,Direction,Comment,Type,Title) " +
                "VALUES ('" +
                rsstosave.savedate.ToString("d") + "','" +
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
            string sqlsentence = "UPDATE RssDirections " +
                "SET Active = " + rsstoupdate.active + ", " +
                "SaveDate = '" + rsstoupdate.savedate.ToString("d") + "', " +
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

        public static int deleteRss(int id)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence = "DELETE " +
                "FROM RssDirections " +
                "WHERE Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        public static DataTable retrieveRss(int id)
        {
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "rssd.Id AS ID, " +
                "rssd.SaveDate AS Fecha, " +
                "rssd.Direction AS URL, " +
                "rssd.Comment AS Comentario, " +
                "rssd.Type AS Tipo, " +
                "rssd.Title AS Titulo " +
                "FROM RssDirections AS rssd " +
                "WHERE rssd.Active = TRUE " +
                "AND rssd.Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            return dt;
        }

        public static DataTable retrieveAllRss()
        {
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "rssd.Id AS ID, " +
                "rssd.SaveDate AS Fecha, " +
                "rssd.Direction AS URL, " +
                "rssd.Comment AS Comentario, " +
                "rssd.Type AS Tipo, " +
                "rssd.Title AS Titulo " +
                "FROM RssDirections AS rssd " +
                "WHERE rssd.Active = TRUE";
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
