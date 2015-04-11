using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    public class CategoryRss
    {
        public string name { set; get; }
        public string label { set; get; }
        public string scheme { set; get; }

        #region "Constructores"
        public CategoryRss()
        {
            this.name =     String.Empty;
            this.label =    String.Empty;
            this.scheme =   String.Empty;
        }

        public CategoryRss(CategoryRss ctgtocopy)
        {
            this.name =     ctgtocopy.name;
            this.label =    ctgtocopy.label;
            this.scheme =   ctgtocopy.scheme;
        }

        public CategoryRss(SyndicationCategory ctg)
        {
            this.name =     ctg.Name != null ? ctg.Name : String.Empty;
            this.label =    ctg.Label != null ? ctg.Label : String.Empty;
            this.scheme =   ctg.Scheme != null ? ctg.Scheme : String.Empty;
        }
        #endregion

        #region "Otros métodos"
        public static List<CategoryRss> NewListCategories(List<SyndicationCategory> copytolist)
        {
            List<CategoryRss> newlist = new List<CategoryRss>();
            foreach (SyndicationCategory item in copytolist)
            {
                newlist.Add(new CategoryRss(item));
            }

            return newlist;
        }
        #endregion
    }
}
