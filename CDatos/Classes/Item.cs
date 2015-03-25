using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Classes
{
    public class Item
    {
        protected long id { get; set; }
        protected bool active { get; set; }
        protected DateTime savedate { get; set; }
        protected long idoriginrss { get; set; }
        protected string content { get; set; }

        #region "Constructores"
        public Item()
        {
            this.id = -1;
            this.active = false;
            this.savedate = new DateTime();
            this.idoriginrss = -1;
            this.content = "";
        }

        public Item(Item itemcopy)
        {
            this.id = itemcopy.id;
            this.active = itemcopy.active;
            this.savedate = new DateTime(itemcopy.savedate.Ticks);
            this.idoriginrss = itemcopy.idoriginrss;
            this.content = itemcopy.content;
        }

        public Item(DateTime fecha, long idOrigin, string content)
        {
            this.id = -1;
            this.active = false;
            this.savedate = fecha;
            this.idoriginrss = idOrigin;
            this.content = content;
        }

        public Item(long id, bool active, DateTime fecha, long idOrigin, string content)
        {
            this.id = id;
            this.active = active;
            this.savedate = fecha;
            this.idoriginrss = idOrigin;
            this.content = content;
        }
        #endregion

        #region "CRUD from database"
        public int saveItem(Item itemtosave)
        {
            string sqlsentence =
                "INSERT INTO StoredEntries " +
                "(Id,Active,SaveDate,IdOrigin,Content) " +
                "VALUES (" +
                itemtosave.id + ",'" +
                itemtosave.active + "','" +
                itemtosave.savedate.ToShortDateString() + "'," +
                itemtosave.idoriginrss + ",'" +
                itemtosave.content + "')";
            Console.WriteLine(sqlsentence);

            return 0;
        }

        public int updateItem(Item itemtoupdate)
        {
            string sqlsentence =
                "UPDATE StoredEntries " +
                "SET Active = '" + itemtoupdate.active + "', " +
                "SaveDate = '" + itemtoupdate.savedate.ToShortDateString() + "', " +
                "IdOrigin = " + itemtoupdate.idoriginrss + ", " +
                "Content = '" + itemtoupdate.content + "' " +
                "WHERE Id = " + itemtoupdate.id;
            Console.WriteLine(sqlsentence);

            return 0;
        }

        public int deleteItem(long id)
        {
            string sqlsentence =
                "DELETE FROM StoredEntries WHERE Id = " + id;
            Console.WriteLine(sqlsentence);

            return 0;
        }

        public Item retrieveItem(long id)
        {
            string sqlsentence =
                "SELECT * FROM StoredEntries WHERE Id = " + id;
            Console.WriteLine(sqlsentence);

            return null;
        }

        public List<Item> retrieneAllItems()
        {
            string sqlsentence =
                "SELECT * FROM StoredEntries";
            Console.WriteLine(sqlsentence);

            return null;
        }
        #endregion

        #region "Other methods"
        public Item Clone(Item itemtoclone)
        {
            return new Item(itemtoclone);
        }

        public string ToString()
        {
            string message = "{";

            message += "id=" + this.id + ", ";
            message += "active=" + this.active.ToString() + ", ";
            message += "fecha de alta=" + this.savedate.ToString() + ", ";
            message += "idOrigen=" + this.idoriginrss + ", ";
            message += "content=" + this.content + "}";

            return message;
        }
        #endregion
    }
}
