using System;
using System.Data;
using System.Data.OleDb;

namespace CDatos.StorageData
{
    public class DBManager
    {
        private OleDbConnection ConnectionWithDB;
        private OleDbDataReader Lector;
        private OleDbCommand Orden;
        private DataTable dtSalida;
        private static string strConnection;

        public DBManager()
        {
            strConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\StorageData\DBRss.accdb";
            this.ConnectionWithDB = null;
            this.Lector = null;
            this.Orden = null;
            this.dtSalida = null;
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
            if (this.dtSalida != null)
            {
                this.dtSalida.Dispose();
            }
        }

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
