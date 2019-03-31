using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceComparer.Domain;
using PriceComparer.Domain.Products;

namespace PriceComparer.Scrapers.Equalisers.Weight
{
    public class WeightEqualiser : IEqualiser
    {
        private Dictionary<string, Func<AbstractUnit>> _unitBuilder;

        public WeightEqualiser()
        {
            _unitBuilder = new Dictionary<string, Func<AbstractUnit>>();

            InitializeUnitBuilder();
        }

        public UnitSize GetUnitSize(string unitText)
        {
            if (unitText.IsEmpty())
                return UnitSize.Empty();

            var productUnit = unitText.GetLastWord().ToLower();
            var shouldHandle = _unitBuilder.ContainsKey(productUnit);

            if (!shouldHandle)
                return UnitSize.Empty();

            var unit = _unitBuilder[productUnit]();
            var secondLastWord = unitText.GetSecondLastWord();
            unit.SetWeight(secondLastWord);

            return new UnitSize(unit.DisplayValue, unit.Gram);
        }

        private void InitializeUnitBuilder()
        {
            _unitBuilder.Add("g", () => new Gram());
            _unitBuilder.Add("gram", () => new Gram());
            _unitBuilder.Add("kilo", () => new Kilo());
        }
    }
}
