using PriceComparer.Domain.Products;

namespace PriceComparer.Scrapers.Equalisers
{
    public interface IEqualiser
    {
        UnitSize GetUnitSize(string unitText);
    }
}
