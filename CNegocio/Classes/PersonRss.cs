using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace CNegocio.Classes
{
    public class PersonRss
    {
        public string name { set; get; }
        public string email { set; get; }
        public string uri { set; get; }

        #region "Constructores"
        public PersonRss()
        {
            this.name =     String.Empty;
            this.email =    String.Empty;
            this.uri =      String.Empty;
        }

        public PersonRss(PersonRss prsntocopy)
        {
            this.name =     prsntocopy.name;
            this.email =    prsntocopy.email;
            this.uri =      prsntocopy.uri;
        }

        public PersonRss(SyndicationPerson prsn)
        {
            this.name =     prsn.Name != null ? prsn.Name : String.Empty;
            this.email =    prsn.Email != null ? prsn.Email : String.Empty;
            this.uri =      prsn.Uri != null ? prsn.Uri : String.Empty;
        }
        #endregion

        #region "Otros métodos"
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
