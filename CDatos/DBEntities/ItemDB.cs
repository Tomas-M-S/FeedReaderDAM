using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.OleDb;
using CDatos.StorageData;

namespace CDatos.DBEntities
{
    public class ItemDB
    {
        public int id { get; set; }
        public bool active { get; set; }
        public DateTime savedate { get; set; }
        public int idoriginrss { get; set; }
        public string content { get; set; }
        public XDocument contentXML
        {
            get { return XDocument.Parse(this.content); }
        }

        #region "Constructores"
        public ItemDB()
        {
            this.id =           -1;
            this.active =       false;
            this.savedate =     new DateTime();
            this.idoriginrss =  -1;
            this.content =      String.Empty;
        }

        public ItemDB(ItemDB itemcopy)
        {
            this.id =           itemcopy.id;
            this.active =       itemcopy.active;
            this.savedate =     new DateTime(itemcopy.savedate.Ticks);
            this.idoriginrss =  itemcopy.idoriginrss;
            this.content =      itemcopy.content;
        }

        public ItemDB(DateTime fecha, int idOrigin, string content)
        {
            this.id =           -1;
            this.active =       false;
            this.savedate =     fecha;
            this.idoriginrss =  idOrigin;
            this.content =      content;
        }

        public ItemDB(int id, bool active, DateTime fecha, int idOrigin, string content)
        {
            this.id =           id;
            this.active =       active;
            this.savedate =     fecha;
            this.idoriginrss =  idOrigin;
            this.content =      content;
        }
        #endregion

        #region "CRUD from database"
        public static int saveItem(ItemDB itemtosave)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence =
                "INSERT INTO StoredEntries " +
                "(Id,Active,SaveDate,IdOrigin,Content) " +
                "VALUES (" +
                itemtosave.savedate.ToString() + "'," +
                itemtosave.idoriginrss + ",'" +
                itemtosave.content + "')";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        public static int updateItem(ItemDB itemtoupdate)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence =
                "UPDATE StoredEntries " +
                "SET Active = '" + itemtoupdate.active + "', " +
                "SaveDate = '" + itemtoupdate.savedate.ToString() + "', " +
                "IdOrigin = " + itemtoupdate.idoriginrss + ", " +
                "Content = '" + itemtoupdate.content + "' " +
                "WHERE Id = " + itemtoupdate.id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        public static int deleteItem(long id)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence =
                "DELETE FROM StoredEntries WHERE Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        
        public static ItemDB retrieveItem(long id)
        {/*
            DBManager manDB = new DBManager();
            ItemDB item = null;
            OleDbDataReader itemDBansware;
            string sqlsentence =
                "SELECT * FROM StoredEntries WHERE Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            itemDBansware = manDB.executeDML(sqlsentence);
            while (itemDBansware.Read())
            {
                int col1 = itemDBansware.GetInt32(0);
                bool col2 = itemDBansware.GetBoolean(1);
                DateTime col3 = itemDBansware.GetDateTime(2);
                int col4 = itemDBansware.GetInt32(3);
                String col5 = itemDBansware.GetString(4);
                item = new ItemDB(col1, col2, col3, col4, col5);
            }
            manDB.CloseDB();
            itemDBansware.Close();

            return item;*/
            return null;
        }

        public static List<ItemDB> retrieneAllItems()
        {/*
            DBManager manDB = new DBManager();
            List<ItemDB> itemList = new List<ItemDB>();
            OleDbDataReader itemDBansware;
            string sqlsentence =
                "SELECT * FROM StoredEntries";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            itemDBansware = manDB.executeDML(sqlsentence);
            while (itemDBansware.Read())
            {
                int col1 = itemDBansware.GetInt32(0);
                bool col2 = itemDBansware.GetBoolean(1);
                DateTime col3 = itemDBansware.GetDateTime(2);
                int col4 = itemDBansware.GetInt32(3);
                String col5 = itemDBansware.GetString(4);
                itemList.Add(new ItemDB(col1, col2, col3, col4, col5));
            }
            manDB.CloseDB();
            itemDBansware.Close();

            return itemList;*/
            return null;
        }
        #endregion

        #region "Other methods"
        public ItemDB Clone(ItemDB itemtoclone)
        {
            return new ItemDB(itemtoclone);
        }

        public override string ToString()
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
