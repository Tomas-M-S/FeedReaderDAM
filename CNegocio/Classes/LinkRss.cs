using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    public class LinkRss
    {
        public string title { set; get; }
        public string uri { set; get; }

        #region "Constructores"
        public LinkRss()
        {
            this.title =    String.Empty;
            this.uri =      String.Empty;
        }

        public LinkRss(LinkRss lnktocopy)
        {
            this.title =    lnktocopy.title;
            this.uri =      lnktocopy.uri;
        }

        public LinkRss(SyndicationLink lnk)
        {
            this.title =    lnk.Title != null ? lnk.Title : String.Empty;
            this.uri =      lnk.Uri != null ? lnk.Uri.AbsoluteUri : String.Empty;
        }
        #endregion

        #region "Otros métodos"
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
