namespace PriceComparer.Scrapers.Equalisers.Weight
{
    public abstract class AbstractUnit
    {
        public string DisplayValue => "gram";

        public decimal Gram { get; protected set; }

        public abstract void SetWeight(string weight);
    }
}