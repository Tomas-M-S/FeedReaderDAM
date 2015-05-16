using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Data.OleDb;
using CDatos.StorageData;

namespace CDatos.DBEntities
{
    /// <summary>
    /// Clase ItemDB intermediaria con la tabla StoredEntries de la base de datos.
    /// Representa un Item concreto guardado en la base de datos.
    /// </summary>
    /// <author>Tomás Martínez Sempere</author>
    /// <date>26/04/2015</date>
    public class ItemDB
    {
        // ************************
        // * PROPIEDADES PÚBLICAS *
        // ************************
        public int id { get; set; }
        public bool active { get; set; }
        public DateTime savedate { get; set; }
        public int idoriginrss { get; set; }
        public string content { get; set; }
        public XDocument contentXML
        {
            get { return XDocument.Parse(this.content); }
        }


        // *****************
        // * CONSTRUCTORES *
        // *****************
        #region "Constructores"
        
        /// <summary>
        /// Constructor vacío
        /// </summary>
        public ItemDB()
        {
            this.id =           -1;
            this.active =       false;
            this.savedate =     new DateTime();
            this.idoriginrss =  -1;
            this.content =      String.Empty;
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="itemcopy">(ItemDB) Objeto a copiar</param>
        public ItemDB(ItemDB itemcopy)
        {
            this.id =           itemcopy.id;
            this.active =       itemcopy.active;
            this.savedate =     new DateTime(itemcopy.savedate.Ticks);
            this.idoriginrss =  itemcopy.idoriginrss;
            this.content =      itemcopy.content;
        }

        /// <summary>
        /// Constructor con los parámetros básicos para la persistencia del objeto
        /// </summary>
        /// <param name="fecha">(DateTime) Fecha de alta</param>
        /// <param name="idOrigin">(int) FK, PK del feed de origen</param>
        /// <param name="content">(string) Cadena que representa el fichero XML del item</param>
        public ItemDB(DateTime fecha, int idOrigin, string content)
        {
            this.id =           -1;
            this.active =       false;
            this.savedate =     fecha;
            this.idoriginrss =  idOrigin;
            this.content =      content;
        }

        /// <summary>
        /// Constructor con todos los parámetros
        /// </summary>
        /// <param name="id">(int) Identificador (PK) en la base de datos</param>
        /// <param name="active">(bool) Estado del feed en la base de datos</param>
        /// <param name="fecha">(DateTime) Fecha de alta</param>
        /// <param name="idOrigin">(int) FK, PK del feed de origen</param>
        /// <param name="content">(string) Cadena que representa el fichero XML del item</param>
        public ItemDB(int id, bool active, DateTime fecha, int idOrigin, string content)
        {
            this.id =           id;
            this.active =       active;
            this.savedate =     fecha;
            this.idoriginrss =  idOrigin;
            this.content =      content;
        }

        #endregion


        // *******************
        // * ACCESOS A LA BD *
        // *******************
        #region "CRUD from database"

        /// <summary>
        /// Inserta en la base de datos un Item
        /// </summary>
        /// <param name="itemtosave">(ItemDB) Objeto ItemDB a guardar</param>
        /// <returns>(int) Resultado de la operación DDL (0: la operación no se efectuo, 1: La operación de efectuo)</returns>
        public static int saveItem(ItemDB itemtosave)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence = "INSERT INTO StoredEntries " +
                "(SaveDate,IdOrigin,Content) " +
                "VALUES ('" +
                itemtosave.savedate.ToString() + "'," +
                itemtosave.idoriginrss + ",'" +
                itemtosave.content + "')";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        /// <summary>
        /// Actualiza un en la base de datos un Item
        /// </summary>
        /// <param name="itemtosave">(ItemDB) Objeto ItemDB a actualizar</param>
        /// <returns>(int) Resultado de la operación DDL (0: la operación no se efectuo, 1: La operación de efectuo)</returns>
        public static int updateItem(ItemDB itemtoupdate)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence = "UPDATE StoredEntries " +
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

        /// <summary>
        /// Borra de la base de datos un Item
        /// </summary>
        /// <param name="id">(int)</param>
        /// <returns>(int) Resultado de la operación DDL (0: la operación no se efectuo, 1: La operación de efectuo)</returns>
        public static int deleteItem(int id)
        {
            DBManager manDB = new DBManager();
            int result = 0;
            string sqlsentence = "DELETE " +
                "FROM StoredEntries " +
                "WHERE Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            result = manDB.executeDDL(sqlsentence);
            manDB.CloseDB();

            return result;
        }

        /// <summary>
        /// Recupera un item determinado
        /// </summary>
        /// <param name="id">(int) Identificador del item que queremos recuperar</param>
        /// <returns>(DataTable) Objeto DataTable con el item recuperado</returns>
        public static DataTable retrieveItem(int id)
        {
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "etrs.Id AS ID, " +
                "etrs.SaveDate AS Fecha, " +
                "etrs.IdOrigin AS IdOrigen, " +
                "etrs.Content AS Comentario " +
                "FROM StoredEntries AS etrs " +
                "WHERE etrs.Active = TRUE " +
                "AND etrs.Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            return dt;
        }

        /// <summary>
        /// Recupera todos los items guardados
        /// </summary>
        /// <returns>(DataTable) DataTable con todos los items guardados</returns>
        public static DataTable retrieneAllItems()
        {
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "etrs.Id AS ID, " +
                "etrs.SaveDate AS Fecha, " +
                "etrs.IdOrigin AS IdOrigen, " +
                "etrs.Content AS Comentario " +
                "FROM StoredEntries AS etrs " +
                "WHERE etrs.Active = TRUE ";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            return dt;
        }

        /// <summary>
        /// Recupera una lista con los items guardados de un determinado feed
        /// </summary>
        /// <param name="idorigin">(int) Identificador del feed de origen de los items que queremos recuperar</param>
        /// <returns>(DateTable) Objeto DateTable con los items recuperados</returns>
        public static DataTable retrieveItemByOrigin(int idorigin)
        {
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "etrs.Id AS ID, " +
                "etrs.SaveDate AS Fecha, " +
                "etrs.IdOrigin AS IdOrigen, " +
                "etrs.Content AS Contenido " +
                "FROM StoredEntries AS etrs " +
                "WHERE etrs.Active = TRUE " +
                "AND etrs.IdOrigin = " + idorigin;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            return dt;
        }

        /// <summary>
        /// Recupera un item determinado
        /// </summary>
        /// <param name="id">(int) Identificador del item que queremos recuperar</param>
        /// <returns>(ItemDB) Objeto ItemDB con el item recuperado (vacío si no se recuperó ninguno)</returns>
        public static ItemDB retrieveObjectItemDB(int id)
        {
            ItemDB itemObject = new ItemDB();
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "etrs.Id AS ID, " +
                "etrs.SaveDate AS Fecha, " +
                "etrs.IdOrigin AS IdOrigen, " +
                "etrs.Content AS Comentario " +
                "FROM StoredEntries AS etrs " +
                "WHERE etrs.Active = TRUE " +
                "AND etrs.Id = " + id;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            // Adaptar DataTable a RSSContactDB
            itemObject.id = (int)dt.Rows[0]["ID"];
            itemObject.active = true;
            itemObject.savedate = (DateTime)dt.Rows[0]["Fecha"];
            itemObject.idoriginrss = (int)dt.Rows[0]["IdOrigen"];
            itemObject.content = (string)dt.Rows[0]["Contenido"];

            return itemObject;
        }
        
        /// <summary>
        /// Recupera todos los items guardados
        /// </summary>
        /// <returns>(List<ItemDB>) Lista con todos los items guardados</returns>
        public static List<ItemDB> retrieveListAllItem()
        {
            List<ItemDB> list = new List<ItemDB>();
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "etrs.Id AS ID, " +
                "etrs.SaveDate AS Fecha, " +
                "etrs.IdOrigin AS IdOrigen, " +
                "etrs.Content AS Contenido " +
                "FROM StoredEntries AS etrs " +
                "WHERE etrs.Active = TRUE ";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            foreach (DataRow row in dt.Rows)
            {
                ItemDB item = new ItemDB();
                item.id = (int)row["ID"];
                item.active = true;
                item.savedate = (DateTime)row["Fecha"];
                item.idoriginrss = (int)row["IdOrigen"];
                item.content = (string)row["Contenido"];
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Recupera una lista con los items guardados de un determinado feed
        /// </summary>
        /// <param name="idorigin">(int) Identificador del feed de origen de los items que queremos recuperar</param>
        /// <returns>(List<ItemDB>) Lista con los items recuperados</returns>
        public static List<ItemDB> retrieveListItemsById(int idorigin)
        {
            List<ItemDB> list = new List<ItemDB>();
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT " +
                "etrs.Id AS ID, " +
                "etrs.SaveDate AS Fecha, " +
                "etrs.IdOrigin AS IdOrigen, " +
                "etrs.Content AS Contenido " +
                "FROM StoredEntries AS etrs " +
                "WHERE etrs.Active = TRUE " +
                "AND etrs.IdOrigin = " + idorigin;
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            foreach (DataRow row in dt.Rows)
            {
                ItemDB item = new ItemDB();
                item.id = (int)row["ID"];
                item.active = true;
                item.savedate = (DateTime)row["Fecha"];
                item.idoriginrss = (int)row["IdOrigen"];
                item.content = (string)row["Contenido"];
                list.Add(item);
            }

            return list;
        }

        #endregion


        // *****************
        // * OTROS MÉTODOS *
        // *****************
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
