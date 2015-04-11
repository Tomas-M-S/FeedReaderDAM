using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    public class RssFeed
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public List<LinkRss> Link { set; get; }

        // Optional items
        public string Language { set; get; }
        public string Copyright { set; get; }
        public List<PersonRss> autors { set; get; }
        public string LastBuildDate { set; get; }
        public List<CategoryRss> Categories { set; get; }
        public List<Item> Items { set; get; }

        #region "Constructores"
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

        public RssFeed(SyndicationFeed rss)
        {
            this.Title =            rss.Title != null ? rss.Title.Text : String.Empty;
            this.Description =      rss.Description != null ? rss.Description.Text : String.Empty;
            this.Link =             LinkRss.NewListLinks(rss.Links.ToList());
            this.Language =         rss.Language != null ? rss.Language : String.Empty;
            this.Copyright =        rss.Copyright != null ? rss.Copyright.Text : String.Empty;
            this.autors =           PersonRss.NewListPersons(rss.Authors.ToList());
            this.LastBuildDate =    rss.LastUpdatedTime != null ? rss.LastUpdatedTime.ToString() : String.Empty;
            this.Categories =       CategoryRss.NewListCategories(rss.Categories.ToList());
            this.Items =            Item.NewListItems(rss.Items.ToList());
        }
        #endregion

        #region "Otros métodos"
        #endregion
    }
}
