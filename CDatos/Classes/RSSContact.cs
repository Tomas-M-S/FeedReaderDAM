using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Classes
{
    class RSSContact
    {
        protected long id { get; set; }
        protected bool active { get; set; }
        protected DateTime savedate { get; set; }
        protected string url { get; set; }
        protected string comment { get; set; }
        protected string type { get; set; }
        protected string title { get; set; }

        #region "Constructores"
        public RSSContact()
        {
            this.id = -1;
            this.active = false;
            this.savedate = new DateTime();
            this.url = "";
            this.comment = "";
            this.type = "";
            this.title = "";
        }

        public RSSContact(RSSContact RSStocopy)
        {
            this.id = RSStocopy.id;
            this.active = RSStocopy.active;
            this.savedate = new DateTime(RSStocopy.savedate.Ticks);
            this.url = RSStocopy.url;
            this.comment = RSStocopy.comment;
            this.type = RSStocopy.type;
            this.title = RSStocopy.title;
        }

        public RSSContact(DateTime fecha, string url, string comm, string type, string title)
        {
            this.id = -1;
            this.active = false;
            this.savedate = fecha;
            this.url = url;
            this.comment = comm;
            this.type = type;
            this.title = title;
        }

        public RSSContact(long id, bool active, DateTime fecha, string url, string comm, string type, string title)
        {
            this.id = id;
            this.active = active;
            this.savedate = fecha;
            this.url = url;
            this.comment = comm;
            this.type = type;
            this.title = title;
        }
        #endregion

        #region "CRUD from database"
        public int saveItem(RSSContact rsstosave)
        {
            string sqlsentence =
                "INSERT INTO RssDirections " +
                "(Id,Active,SaveDate,Direction,Comment,Type,Title) " +
                "VALUES (" +
                rsstosave.id + ",'" +
                rsstosave.active + "','" +
                rsstosave.savedate.ToShortDateString() + "','" +
                rsstosave.url + "','" +
                rsstosave.comment + "','" +
                rsstosave.type + "','" +
                rsstosave.title + "')";
            Console.WriteLine(sqlsentence);

            return 0;
        }

        public int updateItem(RSSContact rsstoupdate)
        {
            string sqlsentence =
                "UPDATE RssDirections " +
                "SET Active = '" + rsstoupdate.active + "', " +
                "SaveDate = '" + rsstoupdate.savedate.ToShortDateString() + "', " +
                "Direction = '" + rsstoupdate.url + "', " +
                "Comment = '" + rsstoupdate.comment + "' " +
                "Type = '" + rsstoupdate.type + "' " +
                "Title = '" + rsstoupdate.title + "' " +
                "WHERE Id = " + rsstoupdate.id;
            Console.WriteLine(sqlsentence);

            return 0;
        }

        public int deleteItem(long id)
        {
            string sqlsentence =
                "DELETE FROM RssDirections WHERE Id = " + id;
            Console.WriteLine(sqlsentence);

            return 0;
        }

        public RSSContact retrieveItem(long id)
        {
            string sqlsentence =
                "SELECT * FROM RssDirections WHERE Id = " + id;
            Console.WriteLine(sqlsentence);

            return null;
        }

        public List<RSSContact> retrieneAllItems()
        {
            string sqlsentence =
                "SELECT * FROM RssDirections";
            Console.WriteLine(sqlsentence);

            return null;
        }
        #endregion

        #region "Other methods"
        public RSSContact Clone(RSSContact RSStoclone)
        {
            return new RSSContact(RSStoclone);
        }

        public string ToString()
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
