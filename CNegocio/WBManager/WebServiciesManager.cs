using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDatos.ConnectionWeb;

namespace CNegocio.WBManager
{
    public class WebServiciesManager
    {
        protected DataWeb datawebFeed { get; set; }

        public WebServiciesManager(string uri)
        {
            this.datawebFeed = new DataWeb(uri);
        }

        
    }
}
