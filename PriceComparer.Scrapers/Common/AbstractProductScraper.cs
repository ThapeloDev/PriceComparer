using PriceComparer.Domain;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Equalisers;
using PriceComparer.Scrapers.Equalisers.Count;
using PriceComparer.Scrapers.Equalisers.Size;
using PriceComparer.Scrapers.Equalisers.Weight;

namespace PriceComparer.Scrapers.Common
{
    public abstract class AbstractProductScraper
    {
        protected UnitSize GetUnitSize(string unitText)
        {
            if (unitText.IsEmpty())
                return new UnitSize();

            var equalizing = new SizeEqualiser()
                .Then(new CountEqualiser())
                .Then(new WeightEqualiser());

            return equalizing.GetUnitSize(unitText);
        }
    }
}