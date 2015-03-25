using System;
using System.Data;
using System.Data.OleDb;

namespace CDatos.StorageData
{
    public class DBManage
    {
        private OleDbConnection ConnectionWithDB;
        private OleDbDataReader Lector;
        private OleDbCommand Orden;
        private static string strConnection;

        public DBManage()
        {
            strConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\StorageData\DBRss.accdb";
            this.ConnectionWithDB = null;
            this.Lector = null;
            this.Orden = null;
        }

        public void OpenDB()
        {
            this.ConnectionWithDB = new OleDbConnection(strConnection);
            this.ConnectionWithDB.Open();
        }

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
        }

        public OleDbDataReader executeDML(string sqlSentence)
        {
            this.Orden = new OleDbCommand(sqlSentence, this.ConnectionWithDB);
            this.Lector = this.Orden.ExecuteReader();
            return this.Lector;
        }

        public int executeDDL(string sqlSentence)
        {
            this.Orden = new OleDbCommand(sqlSentence, this.ConnectionWithDB);
            int rows = this.Orden.ExecuteNonQuery();
            return rows;
        }
    }
}
