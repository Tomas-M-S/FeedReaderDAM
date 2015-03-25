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
