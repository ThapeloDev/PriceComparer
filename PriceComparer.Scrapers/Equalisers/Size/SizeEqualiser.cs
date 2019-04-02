using PriceComparer.Domain;
using PriceComparer.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PriceComparer.Scrapers.Equalisers.Size
{
    public class SizeEqualiser : IEqualiser
    {
        private Dictionary<string, Func<AbstractUnit>> _unitBuilder;

        public SizeEqualiser()
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
            unit.SetSize(secondLastWord);

            return new UnitSize(unit.DisplayValue, unit.Liters);
        }

        private void InitializeUnitBuilder()
        {
            _unitBuilder.Add("l", () => new Liter());
            _unitBuilder.Add("liter", () => new Liter());
            _unitBuilder.Add("lt", () => new Liter());
            _unitBuilder.Add("ml", () => new Milliliter());
        }
    }
}
