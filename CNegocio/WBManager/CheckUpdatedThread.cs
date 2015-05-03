using System;
using System.Threading;
using System.Xml;
using System.ServiceModel.Syndication;
using CDatos.ConnectionWeb;
using CDatos.DBEntities;
using CNegocio.Utils;
using System.Diagnostics;

namespace CNegocio.WBManager
{
    /// <summary>
    /// Clase con el hilo que comprobará el estado de un feed determinado pasado por parámetro
    /// </summary>
    /// <author>Tomás Martínez Sempere</author>
    /// <date>01/05/2015</date>
    /// <see cref="WebServiciesManager"/>
    /// <see cref="UpdatedEventArgs"/>
    public class CheckUpdatedThread
    {
        // Variables globales de clase
        private volatile bool stop;
        private int range;
        private RSSContactDB rss;
        private DataWeb dw;
        private SyndicationFeed sf1;
        public event EventHandler<UpdatedEventArgs> updatedFeed;

        /// <summary>
        /// Constructor de clase.
        /// </summary>
        /// <param name="rss">(RSSContactDB) Objeto con los datos del feed a contactar</param>
        /// <param name="range">(int) Intervalo de comprobación (milisegundos)</param>
        public CheckUpdatedThread(RSSContactDB rss, int range)
        {
            this.stop = false;
            this.range = range;
            this.rss = rss;
            this.dw = new DataWeb(this.rss.url);
            this.sf1 = null;
        }

        /// <summary>
        /// Método principal de la clase.
        /// Ejecuta un bucle en el cual comprueba, con un intervalo de diferencia, el estado del feed.
        /// </summary>
        public void RunLoop()
        {
            while (!this.stop)
            {
                SyndicationFeed sf2 = null;

                try
                {
                    if (this.sf1 == null)
                    {
                        this.sf1 = SyndicationFeed.Load(this.dw.XmlDoc);
                    }
                    else
                    {
                        sf2 = SyndicationFeed.Load(this.dw.XmlDoc);

                        if (sf1.LastUpdatedTime == null || sf2.LastUpdatedTime == null)
                        {
                            this.raiseEvent(Constants.FEED_OFFLINE, Constants.MSG_F_OFFLINE);
                        }
                        else
                        {
                            if (sf1.LastUpdatedTime < sf2.LastUpdatedTime)
                            {
                                this.raiseEvent(Constants.FEED_UPDATED, Constants.MSG_F_UPDATED);
                                this.sf1 = sf2.Clone(true);
                            }
                            else
                            {
                                this.raiseEvent(Constants.FEED_NO_CHANGES, Constants.MSG_F_NO_CHANGES);
                            }
                        }
                    }
                }
                catch (ExceptionMissingWeb ex)
                {
                    this.raiseEvent(Constants.FEED_OFFLINE, Environment.NewLine + ex.Message);
                }
                catch (XmlException ex)
                {
                    this.raiseEvent(Constants.FEED_OFFLINE, Environment.NewLine + ex.Message);
                }
                catch (Exception ex)
                {
                    this.raiseEvent(Constants.FEED_OFFLINE, Environment.NewLine + ex.Message);
                }
                
                // Esperamos a la próxima iteración
                Thread.Sleep(this.range);
            }
        }

        /// <summary>
        /// Lanza el evento con los datos del estado del feed.
        /// </summary>
        /// <param name="state">(int) Código con el estado del feed (0: Sin cambios, 1: Actualizado, -1: Sin conexión).</param>
        /// <param name="msg">(string) Mensaje comentando el estado (se muestra en el tooltip del dataGridView).</param>
        public void raiseEvent(int state, string msg)
        {
            UpdatedEventArgs args = new UpdatedEventArgs();
            args.status = state;
            args.id = this.rss.id;
            args.rss = this.rss;
            args.message = msg;
            this.OnUpdateFeedReached(args);
        }

        /// <summary>
        /// Pone la variable stop en true y hace el bucle de RunLoop termine.
        /// </summary>
        public void StopThread()
        {
            this.stop = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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