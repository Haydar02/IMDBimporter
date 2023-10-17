using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IMDBimporter.Model
{
    public  class TitleCrew
    {
        

        public string? tconst { get; set; }
        public string? directors { get; set; }
        public string? writers { get; set; }
         


        public TitleCrew(string? tconst, string? directors, string? writers)
        {
            this.tconst = tconst;
            this.directors = directors;
            this.writers = writers;
        }
    }
}
