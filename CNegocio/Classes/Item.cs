using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    /// <summary>
    /// Clase-entidad construida a partir de un objeto SyndicationItem
    /// Representa un Item
    /// </summary>
    public class Item
    {
        // Todos los elementos de un Item son opcionales
        public string Title { set; get; }
        public string Description { set; get; }
        public List<LinkRss> Links { set; get; }
        public List<PersonRss> Authors { set; get; }
        public List<CategoryRss> Categories { set; get; }
        public string Pubdate { set; get; }
        public string Guid { set; get; }
        public SyndicationItem SyndItem { set; get; }

        // *****************
        // * CONSTRUCTORES *
        // *****************
        #region "Constructores"

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public Item()
        {
            this.Title =        String.Empty;
            this.Description =  String.Empty;
            this.Links =        new List<LinkRss>();
            this.Authors =      new List<PersonRss>();
            this.Categories =   new List<CategoryRss>();
            this.Pubdate =      String.Empty;
            this.Guid =         String.Empty;
            this.SyndItem =     new SyndicationItem();
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="itemtocopy">(Item) Objeto a copiar</param>
        public Item(Item itemtocopy)
        {
            this.Title =        itemtocopy.Title;
            this.Description =  itemtocopy.Description;
            this.Links =        itemtocopy.Links;
            this.Authors =      itemtocopy.Authors;
            this.Categories =   itemtocopy.Categories;
            this.Pubdate =      itemtocopy.Pubdate;
            this.Guid =         itemtocopy.Guid;
            this.SyndItem =     itemtocopy.SyndItem;
        }

        /// <summary>
        /// Constructor de objeto mediante un SyndicationItem
        /// </summary>
        /// <param name="itm">(SyndicationItem) Objeto con los datos necesarios para iniciar un Item</param>
        public Item(SyndicationItem itm)
        {
            this.Title =        itm.Title != null ? itm.Title.Text : String.Empty;
            this.Description =  itm.Summary != null ? itm.Summary.Text : String.Empty;
            this.Links =        LinkRss.NewListLinks(itm.Links.ToList());
            this.Authors =      PersonRss.NewListPersons(itm.Authors.ToList());
            this.Categories =   CategoryRss.NewListCategories(itm.Categories.ToList());
            this.Pubdate =      itm.PublishDate.ToString();
            this.Guid =         itm.Id != null ? itm.Id : String.Empty;
            this.SyndItem =     itm.Clone();
        }

        #endregion

        // *****************
        // * OTROS MÉTODOS *
        // *****************
        #region "Otros métodos"

        /// <summary>
        /// Método estático. Transforma un List<SyndicationItem> en List<Item>.
        /// </summary>
        /// <param name="copytolist">(List<SyndicationItem>) List pasado como parámetro</param>
        /// <returns>(List<Item>) List resultante</returns>
        public static List<Item> NewListItems(List<SyndicationItem> copytolist)
        {
            List<Item> listItems = new List<Item>();
            foreach (SyndicationItem itm in copytolist)
            {
                listItems.Add(new Item(itm));
            }

            return listItems;
        }

        /// <summary>
        /// Obtiene información relevante del objeto.
        /// </summary>
        /// <returns>(string) Cadena que representa al objeto</returns>
        public override string ToString()
        {
            string msg = "";

            if (this.Title != null)
                msg += this.Title;
            //if (this.Description != null)
            //    msg += Environment.NewLine + "Descripción: " + this.Description;
            //if (this.Links.Count > 0)
            //    foreach (LinkRss item in this.Links)
            //    {
            //        msg += Environment.NewLine + "Link: " + item.uri;
            //    }
            //if (this.Authors.Count > 0)
            //    foreach (PersonRss item in this.Authors)
            //    {
            //        msg += Environment.NewLine + "Link: " + item.name;
            //    }
            //if (this.Categories.Count > 0)
            //    foreach (CategoryRss item in this.Categories)
            //    {
            //        msg += Environment.NewLine + "Categoria: " + item.name;
            //    }
            if (this.Pubdate != null)
                msg += Environment.NewLine +  "Fecha/Hora: " + this.Pubdate;

            return msg;
        }

        #endregion
    }
}
