using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.ConnectionWeb
{
    public class GetDataWeb
    {
        protected String feedURI;
        protected XDocument XMLDoc;

        public GetDataWeb(String feedURI)
        {
            this.feedURI = feedURI;
            this.XMLDoc = new XDocument();
        }

        public XDocument getStringFeed()
        {
            try
            {
                this.XMLDoc = XDocument.Load(feedURI);
                return this.XMLDoc;
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new ExceptionMissingWeb("Probablemente el rss no está en línea. " + Environment.NewLine + ex.Message, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new Exception("Error desconocido: " + ex.Message, ex);
            }
        }
    }
}
