using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    /// <summary>
    /// Clase-entidad construida a partir de un objeto SyndicationCategory
    /// Representa una Categoría de un Item
    /// </summary>
    public class CategoryRss
    {
        // Todos los elementos de una Categoría son opcionales
        public string name { set; get; }
        public string label { set; get; }
        public string scheme { set; get; }

        // *****************
        // * CONSTRUCTORES *
        // *****************
        #region "Constructores"

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public CategoryRss()
        {
            this.name =     String.Empty;
            this.label =    String.Empty;
            this.scheme =   String.Empty;
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="ctgtocopy">(CategoryRss) Objeto a copiar</param>
        public CategoryRss(CategoryRss ctgtocopy)
        {
            this.name =     ctgtocopy.name;
            this.label =    ctgtocopy.label;
            this.scheme =   ctgtocopy.scheme;
        }

        /// <summary>
        /// Constructor de objeto mediante un SyndicationCategory
        /// </summary>
        /// <param name="ctg">(SyndicationCategory) Objeto con los datos necesarios para iniciar un CategoryRss</param>
        public CategoryRss(SyndicationCategory ctg)
        {
            this.name =     ctg.Name != null ? ctg.Name : String.Empty;
            this.label =    ctg.Label != null ? ctg.Label : String.Empty;
            this.scheme =   ctg.Scheme != null ? ctg.Scheme : String.Empty;
        }

        #endregion

        // *****************
        // * OTROS MÉTODOS *
        // *****************
        #region "Otros métodos"

        /// <summary>
        /// Método estático. Transforma un List<SyndicationCategory> en List<CategoryRss>.
        /// </summary>
        /// <param name="copytolist">(List<SyndicationCategory>) List pasado como parámetro</param>
        /// <returns>(List<CategoryRss>) List resultante</returns>
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
