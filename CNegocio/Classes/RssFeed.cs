using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    /// <summary>
    /// Clase-entidad construida a partir de un objeto SyndicationFeed.
    /// Representa una lectura RSS de un Feed. RSS o Atom.
    /// </summary>
    public class RssFeed
    {
        // Datos obligatorios del feed según el estándar RSS 2.0
        public string Title { set; get; }
        public string Description { set; get; }
        public List<LinkRss> Link { set; get; }

        // Datos opcionales del feed según el estándar RSS 2.0
        public string Language { set; get; }
        public string Copyright { set; get; }
        public List<PersonRss> autors { set; get; }
        public string LastBuildDate { set; get; }
        public List<CategoryRss> Categories { set; get; }
        public List<Item> Items { set; get; }

        // *****************
        // * CONSTRUCTORES *
        // *****************
        #region "Constructores"

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public RssFeed()
        {
            this.Title =            String.Empty;
            this.Description =      String.Empty;
            this.Link =             new List<LinkRss>();
            this.Language =         String.Empty;
            this.Copyright =        String.Empty;
            this.autors =           new List<PersonRss>();
            this.LastBuildDate =    String.Empty;
            this.Categories =       new List<CategoryRss>();
            this.Items =            new List<Item>();
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="rsscopy">(RssFeed) Objeto a copiar</param>
        public RssFeed(RssFeed rsscopy)
        {
            this.Title =            rsscopy.Title;
            this.Description =      rsscopy.Description;
            this.Link =             new List<LinkRss>(rsscopy.Link);
            this.Language =         rsscopy.Language;
            this.Copyright =        rsscopy.Copyright;
            this.autors =           new List<PersonRss>(rsscopy.autors);
            this.LastBuildDate =    rsscopy.LastBuildDate;
            this.Categories =       new List<CategoryRss>(rsscopy.Categories);
            this.Items =            new List<Item>(rsscopy.Items);
        }

        /// <summary>
        /// Constructor de objeto mediante un SyndicationFeed
        /// </summary>
        /// <param name="rss">(SyndicationFeed) Objeto con los datos necesarios para iniciar un RssFeed</param>
        public RssFeed(SyndicationFeed rss)
        {
            this.Title =            rss.Title != null ? rss.Title.Text : String.Empty;
            this.Description =      rss.Description != null ? rss.Description.Text : String.Empty;
            this.Link =             LinkRss.NewListLinks(rss.Links.ToList());
            this.Language =         rss.Language != null ? rss.Language : String.Empty;
            this.Copyright =        rss.Copyright != null ? rss.Copyright.Text : String.Empty;
            this.autors =           PersonRss.NewListPersons(rss.Authors.ToList());
            this.LastBuildDate =    rss.LastUpdatedTime.ToString();
            this.Categories =       CategoryRss.NewListCategories(rss.Categories.ToList());
            this.Items =            Item.NewListItems(rss.Items.ToList());
        }

        #endregion
    }
}
