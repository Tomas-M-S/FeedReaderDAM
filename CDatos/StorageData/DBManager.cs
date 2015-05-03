using System;
using System.Data;
using System.Data.OleDb;

namespace CDatos.StorageData
{
    /// <summary>
    /// Clase controladora de la comunicación con la base de datos
    /// </summary>
    /// <author>Tomás Martínez Sempere</author>
    /// <date>01/05/2015</date>
    public class DBManager
    {
        // Variables globales de la clase
        private OleDbConnection ConnectionWithDB;
        private OleDbDataReader Lector;
        private OleDbCommand Orden;
        private DataTable dtSalida;
        private static string strConnection;

        /// <summary>
        /// Constructor para la clase DBManager
        /// </summary>
        public DBManager()
        {
            //C:\Users\Tomas\Desktop\Martinez_Tomas_FeedReader\FeedReader\CDatos\bin\Debug\StorageData
            //strConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\StorageData\DBRss.accdb";
            //string path = System.IO.Directory.GetCurrentDirectory();
            //string path2 = Environment.CurrentDirectory;
            //Console.WriteLine(path2);
            strConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Tomas\Desktop\Martinez_Tomas_FeedReader\FeedReader\CDatos\bin\Debug\StorageData\DBRss.accdb";
            this.ConnectionWithDB = null;
            this.Lector = null;
            this.Orden = null;
            this.dtSalida = null;
        }

        /// <summary>
        /// Abre la conexión a la base de datos
        /// </summary>
        public void OpenDB()
        {
            this.ConnectionWithDB = new OleDbConnection(strConnection);
            this.ConnectionWithDB.Open();
        }

        /// <summary>
        /// Cierra la conexión a la base de datos
        /// </summary>
        public void CloseDB()
        {
            if (this.Lector != null)
            {
                this.Lector.Close();
            }
            if (this.ConnectionWithDB != null)
            {
                this.ConnectionWithDB.Close();
            }
            if (this.dtSalida != null)
            {
                this.dtSalida.Dispose();
            }
        }

        /// <summary>
        /// Ejecuta una consulta SQL (DML) sobre la base de datos del programa (select)
        /// </summary>
        /// <param name="sqlSentence">(string) Sentencia SQL a ejecutar</param>
        /// <returns>(DataTable) Con la el resultado de la consulta</returns>
        public DataTable executeDML(string sqlSentence)
        {
            try
            {
                this.Orden = new OleDbCommand(sqlSentence, this.ConnectionWithDB);
                this.Lector = this.Orden.ExecuteReader();
                this.dtSalida = new DataTable();
                this.dtSalida.Load(this.Lector);
                return this.dtSalida;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la sentencia DML: " + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta una sentencia SQL (DDL) sobre la base de datos de programa (inserts, deletes y updates)
        /// </summary>
        /// <param name="sqlSentence">(string) Sentencia SQL a ejecutar</param>
        /// <returns>(int) Con el resultado de la ejecución (el número de filas afectadas)</returns>
        public int executeDDL(string sqlSentence)
        {
            int rows = 0;
            try
            {
                this.Orden = new OleDbCommand(sqlSentence, this.ConnectionWithDB);
                rows = this.Orden.ExecuteNonQuery();
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la sentencia DDL: " + Environment.NewLine + ex.Message);
            }
        }
    }
}
