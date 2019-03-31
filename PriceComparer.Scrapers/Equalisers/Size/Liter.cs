namespace PriceComparer.Scrapers.Equalisers.Size
{
    public class Liter : AbstractUnit
    {
        public Liter()
        {
            
        }

        public override void SetSize(string size)
        {
            Liters = decimal.Parse(size);
        }
    }
}