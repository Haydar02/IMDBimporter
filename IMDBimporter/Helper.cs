using IMDBimporter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBimporter
{
    public class Helper
    {
        public List<Title> TitleReadFile()
        {
            // instance en new liste af title 
            List<Title> titles = new List<Title>();

            foreach (string line in
                System.IO.File.ReadLines
                (@"E:\title.basics (1).tsv\data.tsv")
                //skippe nr 1 og starte fra nr 2
                .Skip(1).Take(Range.All))


            {
                string[] values = line.Split("\t");
                if (values.Length == 9)
                {
                    titles.Add(new Title(
                         values[0], values[1], values[2], values[3],
                         ConvertToBool(values[4]), ConvertToInt(values[5]),
                         ConvertToInt(values[6]), ConvertToInt(values[7]),
                         values[8]
                         ));
                }
            }
            Console.WriteLine("Fil læst ind");
            Console.WriteLine("Antal Titler : " + titles.Count);
            return titles;
        }
        public List<TitleCrew> CrewReadFile() {

            List<TitleCrew> crews = new List<TitleCrew>();

            foreach (string line in
                System.IO.File.ReadLines
                (@"E:\title.crew.tsv\data.tsv").
                Skip(1).Take(Range.All)) 
            {
                string[] values = line.Split ("\t");

                if (values.Length ==3)
                {
                    crews.Add(new TitleCrew(
                        values[0], values[1], values[2]));
                }
            }
            Console.WriteLine("Fíl læst ind");
            Console.WriteLine("Antal Title Crew " + crews.Count);
            return crews;
        }




        public List<Name> NameReadFile()
        {
            List<Name> names = new List<Name>();

            foreach (string line in System.IO.File.ReadLines
                (@"E:\name.basics.tvs\data.tsv").Skip(1).Take(Range.All))
            {
                string[] values = line.Split("\t");
                if(values.Length == 6)
                {
                    names.Add(new Name(values[0], values[1],
                        ConvertToInt(values[2]), ConvertToInt(values[3]),
                        values[4]));
                }

            }
            Console.WriteLine("Fil Læst Ind");
            Console.WriteLine("Antal Personer "+ names.Count);
            return names;
        }

       

       static bool ConvertToBool(string input)
        {
            if (input == "0")
            {
                return false;
            }
            else if (input == "1")
            {
                return true;
            }
            throw new ArgumentException(
                "Kolonne er ikke 0 eller 1, men " + input);
        }

        static int? ConvertToInt(string input)
        {
            if (input.ToLower() == @"\n")
            {
                return null;
            }
            else
            {
                return int.Parse(input);
            }

        }

    }
}
