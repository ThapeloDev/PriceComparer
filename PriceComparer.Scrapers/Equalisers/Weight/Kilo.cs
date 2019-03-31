using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparer.Scrapers.Equalisers.Weight
{
    public class Kilo : AbstractUnit
    {
        public override void SetWeight(string weight)
        {
            Gram = decimal.Parse(weight) * 1000;
        }
    }
}
