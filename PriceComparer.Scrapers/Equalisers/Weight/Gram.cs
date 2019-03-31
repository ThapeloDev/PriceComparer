namespace PriceComparer.Scrapers.Equalisers.Weight
{
    public class Gram : AbstractUnit
    {
        public override void SetWeight(string weight)
        {
            var parsedWeight = decimal.Parse(weight);

            Gram= parsedWeight;
        }
    }
}
