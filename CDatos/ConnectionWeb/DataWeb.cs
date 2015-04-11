using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Linq;
using System.Text;

namespace CDatos.ConnectionWeb
{
    public class DataWeb
    {
        public String feedURI { get; set; }

        public DataWeb(String feedURI)
        {
            this.feedURI = feedURI;
        }

        public XmlReader XmlDoc
        {
            get
            {
                try
                {
                    return XmlReader.Create(feedURI);
                }
                catch (WebException ex)
                {
                    throw new ExceptionMissingWeb("Probablemente el rss no está en línea. " + Environment.NewLine + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error desconocido: " + ex.Message, ex);
                }
            }
        }
    }
}
