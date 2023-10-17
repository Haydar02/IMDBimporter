using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBimporter.Model
{
    public  class Name
    {
     
        public string nconst { get; set; }
        public string primaryName { get; set; }

        public int? brithYear { get; set; }

        public int? deathYear { get; set; }
       public List<string> primaryProfessions{ get; set; }

        public Name(string? nconst, string? primaryName, int? brithYear, int? deathYear, string primaryProfessionString)
        {
            this.nconst = nconst;
            this.primaryName = primaryName;
            this.brithYear = brithYear;
            this.deathYear = deathYear;
            primaryProfessions = primaryProfessionString.Split(',').ToList();
           
        }

    }
}
