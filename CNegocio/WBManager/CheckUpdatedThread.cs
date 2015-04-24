using System;
using System.Threading;
using System.ServiceModel.Syndication;
using CDatos.ConnectionWeb;
using CDatos.DBEntities;

namespace CNegocio.WBManager
{
    public class CheckUpdatedThread
    {
        private volatile bool stop;
        private int range;
        private RSSContactDB rss;
        private DataWeb dw;
        private SyndicationFeed sf1;
        public event EventHandler<UpdatedEventArgs> updatedFeed;

        public CheckUpdatedThread(RSSContactDB rss, int range)
        {
            this.stop = false;
            this.range = range;
            this.rss = rss;
            this.dw = new DataWeb(this.rss.url);
            this.sf1 = null;
        }

        public void DoWork()
        {
            while (!this.stop)
            {
                SyndicationFeed sf2 = null;

                if (this.sf1 == null)
                {
                    try
                    {
                        this.sf1 = SyndicationFeed.Load(this.dw.XmlDoc);
                    }
                    catch (ExceptionMissingWeb ex)
                    {
                        // LANZAR EVENTO
                        UpdatedEventArgs args = new UpdatedEventArgs();
                        args.status = -1; // Enlace roto
                        args.rss = this.rss;
                        this.OnUpdateFeedReached(args);
                    }
                }
                else
                {
                    try
                    {
                        sf2 = SyndicationFeed.Load(this.dw.XmlDoc);
                    }
                    catch (ExceptionMissingWeb ex)
                    {
                        // LANZAR EVENTO
                        UpdatedEventArgs args = new UpdatedEventArgs();
                        args.status = -1; // Enlaze roto
                        args.rss = this.rss;
                        this.OnUpdateFeedReached(args);
                    }

                    if (sf1.LastUpdatedTime < sf2.LastUpdatedTime)
                    {
                        // LANZAR EVENTO
                        UpdatedEventArgs args = new UpdatedEventArgs();
                        args.status = 1; // Actualizado
                        args.rss = this.rss;
                        args.time1 = this.sf1.LastUpdatedTime.DateTime;
                        args.time2 = sf2.LastUpdatedTime.DateTime;
                        this.OnUpdateFeedReached(args);

                        this.sf1 = sf2.Clone(true);
                    }
                }

                Thread.Sleep(this.range);
            }
        }
        public void StopThread()
        {
            this.stop = true;
        }

        protected virtual void OnUpdateFeedReached(UpdatedEventArgs e)
        {
            EventHandler<UpdatedEventArgs> handler = updatedFeed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}