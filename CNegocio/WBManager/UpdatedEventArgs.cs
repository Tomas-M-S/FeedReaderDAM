using System;
using CDatos.DBEntities;

namespace CNegocio.WBManager
{
    /// <summary>
    /// Clase heredera de EventArgs portadora de la información del evento
    /// </summary>
    public class UpdatedEventArgs : EventArgs
    {
        // Propiedades
        public int status { get; set; }
        public int id { get; set; }
        public string message { get; set; }
        public RSSContactDB rss { get; set; }
        public DateTime time1 { get; set; }
        public DateTime time2 { get; set; }
    }
}
