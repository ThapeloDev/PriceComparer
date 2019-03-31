namespace PriceComparer.Domain.Products
{
    public class UnitSize
    {
        public UnitSize()
        {

        }

        public UnitSize(string unit, decimal size)
        {
            Unit = unit;
            Size = size;
        }

        public UnitSize(string unit)
        {
            Unit = unit;
        }

        public string Unit { get; set; }

        public decimal Size { get; set; }

        public static UnknownUnitSize Empty() => new UnknownUnitSize();
    }
}