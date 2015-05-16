using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    /// <summary>
    /// Clase-entidad construida a partir de un objeto SyndicationPerson.
    /// Representa a una persona (autor o colaborador).
    /// </summary>
    public class PersonRss
    {
        public string name { set; get; }
        public string email { set; get; }
        public string uri { set; get; }

        // *****************
        // * CONSTRUCTORES *
        // *****************
        #region "Constructores"

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public PersonRss()
        {
            this.name =     String.Empty;
            this.email =    String.Empty;
            this.uri =      String.Empty;
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="prsntocopy">(PersonRss) Objeto a copiar</param>
        public PersonRss(PersonRss prsntocopy)
        {
            this.name =     prsntocopy.name;
            this.email =    prsntocopy.email;
            this.uri =      prsntocopy.uri;
        }

        /// <summary>
        /// Constructor de objeto mediante un SyndicationPerson
        /// </summary>
        /// <param name="prsn">(SyndicationPerson) Objeto con los datos necesarios para iniciar un PersonRss</param>
        public PersonRss(SyndicationPerson prsn)
        {
            this.name =     prsn.Name != null ? prsn.Name : String.Empty;
            this.email =    prsn.Email != null ? prsn.Email : String.Empty;
            this.uri =      prsn.Uri != null ? prsn.Uri : String.Empty;
        }

        #endregion

        // *****************
        // * OTROS MÉTODOS *
        // *****************
        #region "Otros métodos"

        /// <summary>
        /// Método estático. Transforma un List<SyndicationPerson> en List<PersonRss>.
        /// </summary>
        /// <param name="copytolist">(List<SyndicationPerson>) List pasado como parámetro</param>
        /// <returns>(List<PersonRss>) List resultante</returns>
        public static List<PersonRss> NewListPersons(List<SyndicationPerson> copytolist)
        {
            List<PersonRss> newlist = new List<PersonRss>();
            foreach (SyndicationPerson item in copytolist)
            {
                newlist.Add(new PersonRss(item));
            }

            return newlist;
        }

        #endregion
    }
}
