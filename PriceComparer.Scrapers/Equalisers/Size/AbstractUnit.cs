namespace PriceComparer.Scrapers.Equalisers.Size
{
    public abstract class AbstractUnit
    {
        public string DisplayValue => "liter";

        public decimal Liters { get; protected set; }

        public abstract void SetSize(string size);
    }
}
