using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using CDatos.StorageData;

namespace CDatos.DBEntities
{
    /// <summary>
    /// Clase RSSContactDB intermediaria con la tabla RssContact de la base de datos.
    /// Representa un feed concreto guardado en la base de datos.
    /// </summary>
    /// <author>Tomás Martínez Sempere</author>
    /// <date>26/04/2015</date>
    public class RSSContactDB
    {
        // ************************
        // * PROPIEDADES PÚBLICAS *
        // ************************
        public int id { get; set; }
        public bool active { get; set; }
        public DateTime savedate { get; set; }
        public string url { get; set; }
        public string comment { get; set; }
        public string type { get; set; }
        public string title { get; set; }


        // ******************
        // * CONSTRUCTORES: *
        // ******************
        // public RSSContactDB()
        // public RSSContactDB(RSSContactDB RSStocopy)
        // public RSSContactDB(DateTime fecha, string url, string comm, string type, string title)
        // public RSSContactDB(int id, bool active, DateTime fecha, string url, string comm, string type, string title)
        #region "Constructores"

        /// <summary>
        /// Constructor vacío
        /// </summary>
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

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="RSStocopy">(RSSContactDB) Objeto a copiar</param>
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

        /// <summary>
        /// Constructor con los parámetros básicos para la persistencia del objeto
        /// </summary>
        /// <param name="fecha">(DateTime) Fecha de alta</param>
        /// <param name="url">(sstring) URL con la dirección del feed</param>
        /// <param name="comm">(sstring) Breve comentario</param>
        /// <param name="type">(sstring) Tipo (Sóo puede ser: Blog, Periódico, Otro)</param>
        /// <param name="title">(sstring) Título</param>
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

        /// <summary>
        /// Constructor con todos los parámetros
        /// </summary>
        /// <param name="id">(int) Identificador (PK) en la base de datos</param>
        /// <param name="active">(bool) Estado del feed en la base de datos</param>
        /// <param name="fecha">(DateTime) Fecha de alta</param>
        /// <param name="url">(sstring) URL con la dirección del feed</param>
        /// <param name="comm">(sstring) Breve comentario</param>
        /// <param name="type">(sstring) Tipo (Sóo puede ser: Blog, Periódico, Otro)</param>
        /// <param name="title">(sstring) Título</param>
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


        // ********************
        // * ACCESOS A LA BD: *
        // ********************
        // public static int saveRss(RSSContactDB rsstosave)
        // public static int updateRss(RSSContactDB rsstoupdate)
        // public static int deleteRss(int id)
        // public static DataTable retrieveRss(int id)
        // public static DataTable retrieveAllRss()
        // public static RSSContactDB retrieveObjectRssDB(int id)
        // public static List<RSSContactDB> retrieveListAllRss()
        // public static List<RSSContactDB> retrieveListRssWithItems()
        #region "CRUD from database"

        /// <summary>
        /// Inserta un feed en la base de datos
        /// </summary>
        /// <param name="id">(RSSContactDB) objeto con los datos del feed que se pretengue almacenar</param>
        /// <returns>(int) Resultado de la operación DDL (0: la operación no se efectuo, 1: La operación de efectuo)</returns>
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

        /// <summary>
        /// Actualiza un feed determinado en la base de datos
        /// </summary>
        /// <param name="id">(int) Identificador del feed que se pretende actualizar</param>
        /// <returns>(int) Resultado de la operación DDL (0: la operación no se efectuo, 1: La operación de efectuo)</returns>

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

        /// <summary>
        /// Borra un feed determinado en la base de datos
        /// </summary>
        /// <param name="id">(int) Identificador del feed que se pretende borrar</param>
        /// <returns>(int) Resultado de la operación DDL (0: la operación no se efectuo, 1: La operación de efectuo)</returns>
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

        /// <summary>
        /// Recupera un feed específico de la base de datos
        /// </summary>
        /// <param name="id">(int) Identificador del feed que se pretende recuperar</param>
        /// <returns>(DataTable) Feed recuperado de la base de datos almacenado en un DataTable</returns>
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

        /// <summary>
        /// Recupera todos los feeds almacenados en la base de datos
        /// </summary>
        /// <returns>(DataTable) Listado con los feeds almacenados en un DataTable</returns>
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

        /// <summary>
        /// Recupera un feed específico de la base de datos
        /// </summary>
        /// <param name="id">(int) Identificador del feed que se pretende recuperar</param>
        /// <returns>(RSSContactDB) Feed recuperado de la base de datos (vacío si no se recuperó ninguno)</returns>
        public static RSSContactDB retrieveObjectRssDB(int id)
        {
            RSSContactDB rssObject = new RSSContactDB();
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

            // Adaptar DataTable a RSSContactDB
            rssObject.id = (int)dt.Rows[0]["ID"];
            rssObject.active = true;
            rssObject.savedate = (DateTime)dt.Rows[0]["Fecha"];
            rssObject.url = (string)dt.Rows[0]["URL"];
            rssObject.comment = (string)dt.Rows[0]["Comentario"];
            rssObject.type = (string)dt.Rows[0]["Tipo"];
            rssObject.title = (string)dt.Rows[0]["Titulo"];

            return rssObject;
        }

        /// <summary>
        /// Recupera todos los feeds almacenados en la base de datos
        /// </summary>
        /// <returns>(List<RSSContactDB>) Listado con los feeds</returns>
        public static List<RSSContactDB> retrieveListAllRss()
        {
            List<RSSContactDB> list = new List<RSSContactDB>();
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

            foreach (DataRow row in dt.Rows)
            {
                RSSContactDB rss = new RSSContactDB();
                rss.id = (int)row["ID"];
                rss.active = true;
                rss.savedate = (DateTime)row["Fecha"];
                rss.url = (string)row["URL"];
                rss.comment = (string)row["Comentario"];
                rss.type = (string)row["Tipo"];
                rss.title = (string)row["Titulo"];
                list.Add(rss);
            }

            return list;
        }

        /// <summary>
        /// Recupera todos los feeds que tienen items guardados
        /// </summary>
        /// <returns>(List<RSSContactDB>) Lista con los feeds que tienen items guardados</returns>
        public static List<RSSContactDB> retrieveListRssWithItems()
        {
            List<RSSContactDB> list = new List<RSSContactDB>();
            DBManager manDB = new DBManager();
            string sqlsentence = "SELECT DISTINCT " +
                "rssd.Id AS ID, " +
                "rssd.SaveDate AS Fecha, " +
                "rssd.Direction AS URL, " +
                "rssd.Comment AS Comentario, " +
                "rssd.Type AS Tipo, " +
                "rssd.Title AS Titulo " +
                "FROM RssDirections AS rssd, " +
                "StoredEntries AS etrs " +
                "WHERE rssd.Active = TRUE " +
                "AND etrs.Active = TRUE " +
                "AND rssd.Id = etrs.IdOrigin";
            //Console.WriteLine(sqlsentence);

            manDB.OpenDB();
            DataTable dt = manDB.executeDML(sqlsentence);
            manDB.CloseDB();

            foreach (DataRow row in dt.Rows)
            {
                RSSContactDB rss = new RSSContactDB();
                rss.id = (int)row["ID"];
                rss.active = true;
                rss.savedate = (DateTime)row["Fecha"];
                rss.url = (string)row["URL"];
                rss.comment = (string)row["Comentario"];
                rss.type = (string)row["Tipo"];
                rss.title = (string)row["Titulo"];
                list.Add(rss);
            }

            return list;
        }
        #endregion


        // ******************
        // * OTROS MÉTODOS: *
        // ******************
        // public static RSSContactDB Clone(RSSContactDB RSStoclone)
        // public override string ToString()
        #region "Other methods"

        /// <summary>
        /// Método para copiar objetos tipo RSSContactDB (devuelve una nueva instancia)
        /// </summary>
        /// <param name="RSStoclone">(RSSContactDB) Objeto a clonar</param>
        /// <returns>(RSSContactDB) Nueva instancia</returns>
        public static RSSContactDB Clone(RSSContactDB RSStoclone)
        {
            return new RSSContactDB(RSStoclone);
        }

        /// <summary>
        /// Método que devuelve una cadena que representa al objeto
        /// </summary>
        /// <returns>(String) Información del objeto</returns>
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
