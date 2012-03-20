using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wdump.Library
{
    public interface IFilterApplyer
    {
        List<string> Apply(List<string> text, Dictionary<string, string> options);
    }

    public class FilterApplyer : IFilterApplyer
    {
        List<Filter> filters;

        public FilterApplyer(List<Filter> fs)
        {
            filters = fs;
        }

        public List<string> Apply(List<string> text, Dictionary<string, string> options)
        {
            var result = new List<string>();

            foreach (var f in filters)
            {
                result = f.Transform(text, options.Keys.ToList());
            }

            return result;
        }
    }
}
