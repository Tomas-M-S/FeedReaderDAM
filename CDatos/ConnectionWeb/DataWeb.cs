using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
using System.Text;

namespace CDatos.ConnectionWeb
{
    /// <summary>
    /// Clase DataWeb. Abre conexión para leer la uri informada.
    /// </summary>
    /// <author>Tomás Martínez Sempere</author>
    /// <date>01/05/2015</date>
    /// <see cref="ExceptionMissingWeb"/>
    public class DataWeb
    {
        // Propiedades públicas
        public String feedURI { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="feedURI">(string) Dirección URI con el Feed a conectar</param>
        public DataWeb(String feedURI)
        {
            this.feedURI = feedURI;
        }

        /// <summary>
        /// Propiedad virtual. Devuelve un XmlReader con el contenido del feed leído
        /// </summary>
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
                    throw new ExceptionMissingWeb("Probablemente el feed no está en línea. " + Environment.NewLine + ex.Message, ex);
                }
                catch (XmlException ex)
                {
                    throw new XmlException("El archivo obtenido no es un XML válido: " + Environment.NewLine + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error desconocido: " + Environment.NewLine + ex.Message, ex);
                }
            }
        }
    }
}
