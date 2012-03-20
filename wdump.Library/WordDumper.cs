using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = NetOffice.WordApi;

namespace wdump.Library
{
    public interface IWordDumper
    {
        List<string> Dump(Dictionary<string, string> args);
    }

    public class WordDumper: IWordDumper
    {
        IWordDocument document;
        IFilterApplyer filters;

        public WordDumper(IWordDocument d, IFilterApplyer f)
        {
            document = d;
            filters = f;
        }

        public List<string> Dump(Dictionary<string,string> args) 
        {
            var result = new List<string>();

            var file = args["file"];
            document.Open(file);

            if (args.Keys.ToList().Contains("j"))
            {
                var paragraphs = document.Paragraphs;
                result = new List<string>() {paragraphs.ToJson() }; 
            }
            else 
            {
                result = document.Read();
            }

            result = filters.Apply(result, args);

            return result;
        }
    }
}
