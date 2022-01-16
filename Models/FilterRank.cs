using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ModelSaber.Database
{
    public class FilterRank<T> where T : class
    {
        public FilterRank(T value, Regex[] filters, Func<T, string> filterFunc)
        {
            Value = value;
            Filters = filters;
            FilterFunc = filterFunc;
            CalculateRank();
        }

        public int Counts { get; set; }
        public T Value { get; set; }
        public Regex[] Filters { get; set; }
        public Func<T, string> FilterFunc { get; set; }

        public void CalculateRank()
        {
            var str = FilterFunc(Value);
            Counts = Filters.Select(t => t.Matches(str).Count).Sum();
        }

        public bool PassCheck()
        {
            if (Filters.Length < 4)
                return Counts >= Math.Ceiling(Filters.Length / 2F);
            // Want to just ignore the fraction loss so don't have to call Math.Floor as it does the same thing
            // ReSharper disable once PossibleLossOfFraction
            return Counts / 2 >= Math.Ceiling(Filters.Length / 2F);
        }
    }
}
