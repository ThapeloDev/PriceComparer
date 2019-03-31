using System.Collections.Generic;
using System.Linq;

namespace PriceComparer.Scrapers.Common
{
    public class PageNumber
    {
        private List<int> _processedPageNumbers;
        private bool _isIncreasedLastTime;

        public PageNumber()
        {
            _isIncreasedLastTime = true;
            _processedPageNumbers = new List<int>() { 5 };

            Current = 5;
            Determined = false;
        }

        public int Current { get; private set; }

        public bool Determined { get; private set; }

        public void Increase()
        {
            if (_isIncreasedLastTime)
                Current += 5;
            else
                Current += 1;

            _processedPageNumbers.Add(Current);

        }

        public void Decrease()
        {
            _processedPageNumbers.Remove(Current);

            if (Current > 2)
                Current -= 2;
            else
                Current -= 1;

            if (Current <= 0)
            {
                Determined = true;
            }
            if (_processedPageNumbers.Contains(Current))
            {
                Determined = true;
            }
            if (_processedPageNumbers.Any(p => p > Current))
            {
                Determined = true;
                Current = _processedPageNumbers.Where(p => p > Current).First();
            }
            else
            {
                _processedPageNumbers.Add(Current);

                _isIncreasedLastTime = false;
            }
        }
    }
}
