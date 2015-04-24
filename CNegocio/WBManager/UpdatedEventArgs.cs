using System;
using CDatos.DBEntities;

namespace CNegocio.WBManager
{
    public class UpdatedEventArgs : EventArgs
    {
        public int status { get; set; }
        public RSSContactDB rss { get; set; }
        public DateTime time1 { get; set; }
        public DateTime time2 { get; set; }
    }
}
