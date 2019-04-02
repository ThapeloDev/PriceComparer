namespace PriceComparer.Domain.Products
{
    public class Price
    {
        public Price(decimal regularPrice)
        {
            RegularPrice = regularPrice;
        }

        public Price(decimal regularPrice, decimal offerPrice)
        {
            RegularPrice = regularPrice;
            OfferPrice = offerPrice;
        }

        public Price(decimal regularPrice, string offerDescription)
        {
            RegularPrice = regularPrice;
            OfferDescription = offerDescription;
        }

        public decimal RegularPrice { get; set; }

        public decimal OfferPrice{ get; set; }

        public string OfferDescription { get; set; }
    }
}
