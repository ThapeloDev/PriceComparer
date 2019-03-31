using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceComparer.Domain;
using PriceComparer.Domain.Products;

namespace PriceComparer.Scrapers.Equalisers.Count
{
    public class CountEqualiser : IEqualiser
    {
        private List<string> _validUnitIdentifiers;

        public CountEqualiser()
        {
            _validUnitIdentifiers = new List<string>();

            InitializeUnitBuilder();
        }

        private void InitializeUnitBuilder()
        {
            _validUnitIdentifiers.Add("stuks");
        }

        public UnitSize GetUnitSize(string unitText)
        {
            if (unitText.IsEmpty())
                return UnitSize.Empty();

            var productUnit = unitText.GetLastWord().ToLower();

            var shouldHandle = _validUnitIdentifiers.Contains(productUnit);

            if (!shouldHandle)
                return UnitSize.Empty();

            var secondLastWord = unitText.GetSecondLastWord();

            return new UnitSize("stuks", decimal.Parse(secondLastWord));
        }
    }
}
