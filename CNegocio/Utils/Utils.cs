using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Data;
using CDatos.DBEntities;
using CDatos.ConnectionWeb;
using CNegocio.Classes;

namespace CNegocio.Utils
{
    /// <summary>
    /// Clase con diversas constantes usadas en el programa
    /// </summary>
    public class Constants
    {
        public static int MODIFY_FEED = 0;
        public static int CREATE_FEED = 1;

        public static int FEED_UPDATED = 1;
        public static int FEED_NO_CHANGES = 0;
        public static int FEED_OFFLINE = -1;

        public static string MSG_F_UPDATED = "Feed actualizado";
        public static string MSG_F_NO_CHANGES = "Feed sin cambios";
        public static string MSG_F_OFFLINE = "No se pudo contactar con el feed";
    }

    /// <summary>
    /// 
    /// </summary>
    public class Utils
    {
        #region "Access DB service ItemDB"
        public static int saveItem(Item itemToSave, DateTime dtnow, int ido)
        {
            ItemDB itmdb = new ItemDB();
            itmdb.savedate = dtnow;
            itmdb.idoriginrss = ido;
            itmdb.content = itemToSave.Links[0].uri;
            return ItemDB.saveItem(itmdb);
        }

        public static int updateItem(Item itemToUpdate, int id, DateTime dtnow, int ido)
        {
            ItemDB itmdb = new ItemDB();
            itmdb.savedate = dtnow;
            itmdb.idoriginrss = ido;
            itmdb.content = itemToUpdate.Links[0].uri;
            return ItemDB.saveItem(itmdb);
        }

        public static int deleteItem(int id)
        {
            return Utils.deleteItem(id);
        }

        public static DataTable retrieveItems(int idfeed)
        {
            return ItemDB.retrieveItemByOrigin(idfeed);
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

        public static RssFeed retrieveRssFeedContact(string uri)
        {
            DataWeb dw = new DataWeb(uri);
            RssFeed contact = new RssFeed(SyndicationFeed.Load(dw.XmlDoc));
            return contact;
        }

        public static List<string> retrieveFeedsWithItems()
        {
            List<string> list = new List<string>();

            foreach (RSSContactDB item in RSSContactDB.retrieveListRssWithItems())
            {
                string itemmsg = "";
                itemmsg += item.id + " - " + item.title;
                list.Add(itemmsg);
            }

            return list;
        }
        #endregion
    }
}
