using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CDatos.DBEntities;

namespace CNegocio.WBManager
{
    /// <summary>
    /// Clase controladora de los hilos de comprobación
    /// </summary>
    /// <author>Tomás Martínez Sempere</author>
    /// <date>01/05/2015</date>
    /// <see cref="CheckUpdateThread"/>
    public class WebServiciesManager
    {
        // Variables globales de clase
        protected int range;
        protected List<RSSContactDB> rssList;
        protected List<Thread> listthread;

        // Propiedades públicas
        public List<CheckUpdatedThread> listwork { get; set; }

        /// <summary>
        /// Constructor de clase.
        /// </summary>
        /// <param name="range">(int) Intervalo de comprobación</param>
        public WebServiciesManager(int range)
        {
            this.range = range;
            this.rssList = RSSContactDB.retrieveListAllRss();
            this.listthread = new List<Thread>();
            this.listwork = new List<CheckUpdatedThread>();

            foreach (RSSContactDB rssitem in rssList)
            {
                CheckUpdatedThread workerObject = new CheckUpdatedThread(rssitem, range);
                listwork.Add(workerObject);
                //workerObject.updatedFeed += event_updatedFeed;

                Thread workerThread = new Thread(workerObject.RunLoop);
                listthread.Add(workerThread);
            }
        }

        /// <summary>
        /// Lanza un hilo para cada feed.
        /// </summary>
        public void LanzarHilos()
        {
            foreach (Thread item in listthread)
            {
                item.Start();
            }
            //Console.WriteLine("Connected");
        }

        /// <summary>
        /// Detiene el loop de cada hilo.
        /// </summary>
        public void DetenerHilos()
        {
            foreach (CheckUpdatedThread item in listwork)
            {
                item.StopThread();
            }
            //Console.WriteLine("Disconnected");
        }
    }
}
