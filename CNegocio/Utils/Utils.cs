using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using System.Data;
using CDatos.DBEntities;
using CDatos.ConnectionWeb;
using CNegocio.Classes;

namespace CNegocio.Utils
{
    /// <summary>
    /// Esta clase almacena métodos estáticos destinados a servir como puente entre
    /// las entidades de la caba CDatos y la capa CVista.
    /// </summary>
    public class Utils
    {
        #region "Access DB service ItemDB"

        /// <summary>
        /// Inserta un Item en la base de datos.
        /// </summary>
        /// <param name="itemToSave">(Item) Objeto con los datos del Item</param>
        /// <param name="dtnow">(DateTime) Fecha de hoy</param>
        /// <param name="ido">(int) Id del feed de donde hemos obtenido el item</param>
        /// <returns>(int) Resultado de la operación (0 o 1)</returns>
        public static int saveItem(Item itemToSave, DateTime dtnow, int ido)
        {
            ItemDB itmdb = new ItemDB();
            itmdb.savedate = dtnow;
            itmdb.idoriginrss = ido;
            itmdb.content = itemToSave.Links[0].uri;
            return ItemDB.saveItem(itmdb);
        }

        /// <summary>
        /// Actualiza un Item en la base de datos.
        /// </summary>
        /// <param name="itemToUpdate">(Item) Objeto con los datos del Item</param>
        /// <param name="id">(int) Id del Item en la base de datos</param>
        /// <param name="dtnow">(DateTime) Fecha de hoy</param>
        /// <param name="ido">(int) Id del feed de donde hemos obtenido el item</param>
        /// <returns>(int) Resultado de la operación (0 o 1)</returns>
        public static int updateItem(Item itemToUpdate, int id, DateTime dtnow, int ido)
        {
            ItemDB itmdb = new ItemDB();
            itmdb.savedate = dtnow;
            itmdb.idoriginrss = ido;
            itmdb.content = itemToUpdate.Links[0].uri;
            return ItemDB.saveItem(itmdb);
        }

        /// <summary>
        /// Borra un Item de la base de datos.
        /// </summary>
        /// <param name="id">(int) Id del Item en la base de datos</param>
        /// <returns>(int) Resultado de la operación (0 o 1)</returns>
        public static int deleteItem(int id)
        {
            return ItemDB.deleteItem(id);
        }

        /// <summary>
        /// Recupera todos los Items activos en la base de datos.
        /// </summary>
        /// <returns>(DataTable) Resultado de la query</returns>
        public static DataTable retrieveAllItems()
        {
            return ItemDB.retrieneAllItems();
        }

        /// <summary>
        /// Recupera todos los items de un feed dato (se pasa por parámetro si Id).
        /// </summary>
        /// <param name="idfeed">(int) Id del feed para realizar la consulta</param>
        /// <returns>(DataTable) Resultado de la query</returns>
        public static DataTable retrieveItems(int idfeed)
        {
            return ItemDB.retrieveItemByOrigin(idfeed);
        }
        #endregion

        #region "Access DB service RSSContactDB"

        /// <summary>
        /// Guarda un feed en la base de datos.
        /// </summary>
        /// <param name="fecha">(DateTime) Fecha de hoy</param>
        /// <param name="url">(string) Dirección del feed</param>
        /// <param name="comm">(string)Comentario al feed</param>
        /// <param name="type">(string) Tipo de feed</param>
        /// <param name="title">(string) Título del feed</param>
        /// <returns>(int) con el resultado de la inserción (1 o 0)</returns>
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

        /// <summary>
        /// Actualiza el estado de un feed.
        /// </summary>
        /// <param name="id">(int) Id del feed que queremos actualizar</param>
        /// <param name="active">(bool) Feed activo/inactivo (borrado)</param>
        /// <param name="fecha">(DateTime) Fecha de hoy</param>
        /// <param name="url">(string) Dirección del feed</param>
        /// <param name="comm">(string)Comentario al feed</param>
        /// <param name="type">(string) Tipo de feed</param>
        /// <param name="title">(string) Título del feed</param>
        /// <returns>(int) con el resultado de la inserción (1 o 0)</returns>
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

        /// <summary>
        /// Borra un feed.
        /// </summary>
        /// <param name="id">(int) Identificador del feed que queremos borrar</param>
        /// <returns>(int) Resultado de la operación (1 o 0)</returns>
        public static int deleteRssContact(int id)
        {
            return RSSContactDB.deleteRss(id);
        }

        /// <summary>
        /// Recupera todos los feeds activos almacenados en la BD.
        /// </summary>
        /// <returns>(DateTable) Con el resultado de la query</returns>
        public static DataTable retrieveRssContact()
        {
            return RSSContactDB.retrieveAllRss();
        }

        #endregion

        #region "Others methods"

        /// <summary>
        /// Conecta con una dirección feed, optiene una lectura de un archivo XML, lo parsea y crea un objeto RssFeed
        /// </summary>
        /// <param name="uri">(string) Dirección del feed</param>
        /// <returns>(RssFeed) Objeto con la información del feed</returns>
        public static RssFeed retrieveRssFeedContact(string uri)
        {
            DataWeb dw = new DataWeb(uri);
            RssFeed contact = new RssFeed(SyndicationFeed.Load(dw.XmlDoc));
            return contact;
        }

        /// <summary>
        /// Este método se usa para cargar el combobox de la ventana ItemsDialog
        /// </summary>
        /// <returns>(List<string>) Lista con la información de los items</returns>
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
