using System;
using System.Collections.Generic;
using System.Text;
using CDatos.ConnectionWeb;
using CDatos.StorageData;
using System.Data.OleDb;

namespace TestingProject
{
    class TestingClass
    {
        static void Main(string[] args)
        {
            /*
            //string feedURI = "http://ep00.epimg.net/rss/elpais/portada.xml";
            //string feedURI = "http://ep00.etyrpimg.net/rss/elpais/portada.xml";
            string feedURI = "http://feeds.wired.com/wired/index";
            Uri siteUri = new Uri(feedURI);
            GetDataWeb rssR = new GetDataWeb(feedURI);

            String result = "";
            try
            {
                result = rssR.getStringFeed().ToString();
                Console.WriteLine(result);
            }
            catch (ExceptionMissingWeb ex)
            {
                Console.WriteLine(ex.Message);
            }*/

            /*
            DBManage manDB = new DBManage();
            manDB.OpenDB();
            OleDbDataReader data = manDB.executeDML("SELECT * FROM RssDirections");
            int i = 0;

            while (data.Read())
            {
                Console.WriteLine(data[i]);
                i++;
            }*/

            Console.Read();
        }
    }
}
