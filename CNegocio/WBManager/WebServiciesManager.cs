using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CDatos.DBEntities;

namespace CNegocio.WBManager
{
    public class WebServiciesManager
    {
        protected int range;
        protected List<RSSContactDB> rssList;
        public List<CheckUpdatedThread> listwork { get; set; }
        protected List<Thread> listthread;

        public WebServiciesManager(int range)
        {
            this.rssList = RSSContactDB.retrieveListAllRss();
            this.range = range;
            this.listthread = new List<Thread>();
            this.listwork = new List<CheckUpdatedThread>();

            foreach (RSSContactDB rssitem in rssList)
            {
                CheckUpdatedThread workerObject = new CheckUpdatedThread(rssitem, range);
                listwork.Add(workerObject);
                //workerObject.updatedFeed += event_updatedFeed;

                Thread workerThread = new Thread(workerObject.DoWork);
                listthread.Add(workerThread);
            }
        }

        public void LanzarHilos()
        {
            foreach (Thread item in listthread)
            {
                item.Start();
            }
            //Console.WriteLine("Connected");
        }

        public void DetenerHilos()
        {
            foreach (CheckUpdatedThread item in listwork)
            {
                item.StopThread();
            }

            this.rssList = null;
            this.range = 0;
            this.listthread = null;
            this.listwork = null;
            //Console.WriteLine("Disconnected");
        }


        //public static void event_updatedFeed(object sender, UpdatedEventArgs e)
        //{
        //    //DataGridViewImageCell cell = (DataGridViewImageCell)dataGridView1.Rows[2].Cells[0];
        //    //cell.Value = Properties.Resources.greendot;

        //    Console.WriteLine("--------------------------");
        //    Console.WriteLine(e.rss.title);
        //    Console.WriteLine("Anterior: " + e.time1.ToString());
        //    Console.WriteLine("Nuevo: " + e.time2.ToString());
        //    Console.WriteLine("Feed actualizado: " + e.status.ToString());
        //    Console.WriteLine("Uri: " + e.rss.url);
        //    Console.WriteLine("--------------------------");
        //    //Environment.Exit(0);
        //}
    }
}
