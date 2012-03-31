using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace wdump.Library
{
    public class Paragraph
    {
        public string Text { get; set; }
        public string Font { get; set; }
        public string FontSize { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }


        public string ToJson()
        {
            var result = "{ ";
            var format = "\"text\" : \"{0}\", \"font\" : \"{1}\", \"font_size\" : \"{2}\",";
            format += " \"bold\" : {3}, \"italic\" : {4}";
            result += String.Format(format, escapeQuotes(Text), Font, FontSize, 
                                    Bold.ToString().ToLower(),
                                    Italic.ToString().ToLower()); 
            result += " }";
            return result;
        }

        private string escapeQuotes(string text)
        {
            var result = Regex.Replace(text, "\"", "\\\"");
            return result;
        }
    }

    public interface IParagraphs
    {
        List<Paragraph> Items { get; set; }
        string ToJson();
    }

    public class Paragraphs : IParagraphs
    {
        public List<Paragraph> Items { get; set; }

        public string ToJson()
        {
            var result = "[ ";

            foreach (var p in Items)
            {
                result += p.ToJson() + ", ";
            }

            result = Regex.Replace(result, "}, $", "}");
            result += " ]";

            return result;
        }
    }
}