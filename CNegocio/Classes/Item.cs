using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    public class Item
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public List<LinkRss> Links { set; get; }
        public List<PersonRss> Authors { set; get; }
        public List<CategoryRss> Categories { set; get; }
        public string Pubdate { set; get; }
        public string Guid { set; get; }

        #region "Constructores"
        public Item()
        {
            this.Title =        String.Empty;
            this.Description =  String.Empty;
            this.Links =        new List<LinkRss>();
            this.Authors =      new List<PersonRss>();
            this.Categories =   new List<CategoryRss>();
            this.Pubdate =      String.Empty;
            this.Guid =         String.Empty;
        }

        public Item(Item itemtocopy)
        {
            this.Title =        itemtocopy.Title;
            this.Description =  itemtocopy.Description;
            this.Links =        itemtocopy.Links;
            this.Authors =      itemtocopy.Authors;
            this.Categories =   itemtocopy.Categories;
            this.Pubdate =      itemtocopy.Pubdate;
            this.Guid =         itemtocopy.Guid;
        }

        public Item(SyndicationItem itm)
        {
            this.Title =        itm.Title != null ? itm.Title.Text : String.Empty;
            this.Description =  itm.Summary != null ? itm.Summary.Text : String.Empty;
            this.Links =        LinkRss.NewListLinks(itm.Links.ToList());
            this.Authors =      PersonRss.NewListPersons(itm.Authors.ToList());
            this.Categories =   CategoryRss.NewListCategories(itm.Categories.ToList());
            this.Pubdate =      itm.PublishDate != null ? itm.PublishDate.ToString() : String.Empty;
            this.Guid =         itm.Id != null ? itm.Id : String.Empty;
        }
        #endregion

        #region "Otros métodos"
        public static List<Item> NewListItems(List<SyndicationItem> sItem)
        {
            List<Item> listItems = new List<Item>();
            foreach (SyndicationItem itm in sItem)
            {
                listItems.Add(new Item(itm));
            }

            return listItems;
        }
        #endregion
    }
}
