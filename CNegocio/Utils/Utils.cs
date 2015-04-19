using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Data;
using CDatos.DBEntities;
using CNegocio.Classes;

namespace CNegocio.Utils
{
    public class Constants
    {
        public static int MODIFY_FEED = 0;
        public static int CREATE_FEED = 1;
    }

    public class Utils
    {
        #region "Access DB service ItemDB"
        public static int saveItem(Item itemToSave, DateTime dtnow, int ido)
        {
            ItemDB itmdb = new ItemDB();
            itmdb.savedate = dtnow;
            itmdb.idoriginrss = ido;
            itmdb.content = itemToSave.SyndItem.ToString();
            return ItemDB.saveItem(itmdb);
        }

        public static int updateItem(Item itemToUpdate, int id, DateTime dtnow, int ido)
        {
            ItemDB itmdb = new ItemDB();
            itmdb.savedate = dtnow;
            itmdb.idoriginrss = ido;
            itmdb.content = itemToUpdate.SyndItem.ToString();
            return ItemDB.saveItem(itmdb);
        }

        public static int deleteItem(int id)
        {
            return Utils.deleteItem(id);
        }

        public static DataTable retrieveItems(int idfeed)
        {
            return Utils.retrieveItems(idfeed);
        }
        #endregion

        #region "Access DB service RSSContactDB"
        public static int saveRssContact(DateTime fecha, string url, string comm, string type, string title)
        {
            RSSContactDB newRss = new RSSContactDB();
            newRss.savedate = fecha;
            newRss.url = url;
            newRss.comment = comm;
            newRss.type = type;
            newRss.title = title;

            return RSSContactDB.saveRss(newRss);
        }

        public static int updateRssContact(int id, bool active, DateTime fecha, string url, string comm, string type, string title)
        {
            RSSContactDB newRss = new RSSContactDB();
            newRss.id = id;
            newRss.active = active;
            newRss.savedate = fecha;
            newRss.url = url;
            newRss.comment = comm;
            newRss.type = type;
            newRss.title = title;

            return RSSContactDB.updateRss(newRss);
        }

        public static int deleteRssContact(int id)
        {
            return RSSContactDB.deleteRss(id);
        }

        public static DataTable retrieveRssContact()
        {
            return RSSContactDB.retrieveAllRss();
        }
        #endregion

        #region "Other methods"

        #endregion
    }
}
