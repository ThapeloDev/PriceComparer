using System.Collections.Generic;
using System.Linq;

namespace PriceComparer.API.Controllers.Products
{
    public class SearchResults
    {
        public SearchResults()
        {
            Supermarkets = Enumerable.Empty<Supermarket>();
        }

        public string SearchTerm { get; set; }

        public IEnumerable<Supermarket> Supermarkets { get; set; }

        public string Duration { get; set; }
    }
}