using PriceComparer.Domain.Products;

namespace PriceComparer.Scrapers.Equalisers
{
    public class ChainedEqualizer : IEqualiser
    {
        public ChainedEqualizer(IEqualiser current, IEqualiser next)
        {
            Current = current;
            Next = next;
        }

        private IEqualiser Current { get; }

        private IEqualiser Next { get; }

        public UnitSize GetUnitSize(string unitText)
        {
            var unitSize = Current.GetUnitSize(unitText);

            return unitSize is UnknownUnitSize ? Next.GetUnitSize(unitText) : unitSize;
        }
    }

    public static class ChainConstruction
    {
        public static IEqualiser Then(this IEqualiser current, IEqualiser next) 
            => new ChainedEqualizer(current, next);
    }
}
