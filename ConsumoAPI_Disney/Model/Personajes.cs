using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumoAPI_Disney.Model
{
    internal class Personajes
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public List<string> films { get; set; }
        public List<string> tvShows { get; set; }

        public string FilmsAsString => films != null && films.Count > 0 ? string.Join(", ", films) : "N/A";
        public string TvShowsAsString => tvShows != null && tvShows.Count > 0 ? string.Join(", ", tvShows) : "N/A";
    }
}
