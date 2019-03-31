namespace PriceComparer.Scrapers.Equalisers.Size
{
    public class Milliliter : AbstractUnit
    {
        public override void SetSize(string size)
        {
            var parsedSize = decimal.Parse(size);

            Liters = parsedSize / 1000;
        }
    }
}
