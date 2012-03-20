using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace wdump.Library
{
    public abstract class Filter
    {
        public string Name { get; set;}
        public abstract List<string> Transform(List<string> input, List<string> options);
    }


    public class UnixFilter : Filter
    {
        public UnixFilter()
        {
            Name = "u";
        }
        
        public override List<string> Transform(List<string> input, List<string> options)
        {
            var result = (from s in input select s).ToList();

            if (options.Contains(Name))
            {
                result = toUnix(input);
            }

            return result;
        }

        private List<string> toUnix(List<string> input)
        {
            var result = new List<string>();

            foreach (var s in input)
            {
                var line = Regex.Replace(s, "\r", "\n");
                result.Add(line);
            }

            return result;
        }
    }

    public class NoCarriageReturnFilter : Filter
    {
        public NoCarriageReturnFilter()
        {
            Name = "c";
        }

        public override List<string> Transform(List<string> input, List<string> options)
        {
            var result = (from s in input select s).ToList();

            if (options.Contains(Name))
            {
                result = removeCarriageReturn(input);
            }

            return result;
        }

        private List<string> removeCarriageReturn(List<string> input)
        {
            var result = new List<string>();

            foreach (var s in input)
            {
                var line = Regex.Replace(s, "\r", "");
                result.Add(line);
            }

            return result;
        }
    }

}
