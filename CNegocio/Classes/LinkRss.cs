using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    /// <summary>
    /// Clase-entidad construida a partir de un objeto SyndicationLink.
    /// Representa un Link con sus datos.
    /// </summary>
    public class LinkRss
    {
        public string title { set; get; }
        public string uri { set; get; }

        // *****************
        // * CONSTRUCTORES *
        // *****************
        #region "Constructores"

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public LinkRss()
        {
            this.title =    String.Empty;
            this.uri =      String.Empty;
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="lnktocopy">(LinkRss) Objeto a copiar</param>
        public LinkRss(LinkRss lnktocopy)
        {
            this.title =    lnktocopy.title;
            this.uri =      lnktocopy.uri;
        }

        /// <summary>
        /// Constructor de objeto mediante un SyndicationLink
        /// </summary>
        /// <param name="lnk">(SyndicationLink) Objeto con los datos necesarios para iniciar un LinkRss</param>
        public LinkRss(SyndicationLink lnk)
        {
            this.title =    lnk.Title != null ? lnk.Title : String.Empty;
            this.uri =      lnk.Uri != null ? lnk.Uri.AbsoluteUri : String.Empty;
        }

        #endregion

        // *****************
        // * OTROS MÉTODOS *
        // *****************
        #region "Otros métodos"

        /// <summary>
        /// Método estático. Transforma un List<SyndicationLink> en List<LinkRss>.
        /// </summary>
        /// <param name="copytolist">(List<SyndicationLink>) List pasado como parámetro</param>
        /// <returns>(List<LinkRss>) List resultante</returns>
        public static List<LinkRss> NewListLinks(List<SyndicationLink> copytolist)
        {
            List<LinkRss> newlist = new List<LinkRss>();
            foreach (SyndicationLink item in copytolist)
            {
                newlist.Add(new LinkRss(item));
            }

            return newlist;
        }

        #endregion
    }
}
