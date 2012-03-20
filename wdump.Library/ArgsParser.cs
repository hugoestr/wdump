using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;

namespace wdump.Library
{

    public class ArgsParser
    {
        public Dictionary<string, string> Parse(string[] input)
        {
           var result = new Dictionary<string, string>();

            for (var i = 0; i < input.Length; i++)
            {
                var s = input[i];

                if (i == 0)
                {
                    result.Add("file", s);
                }

                if (Regex.IsMatch(s, "-"))
                {
                    result.Add(Regex.Replace(s,"-", ""), "true");
                }
            }

            return result;
        }

        public string Validate(string[] args)
        {
            var message = "";
            if (args != null && args.Length > 0)
            {
                var file = args[0];
                var extension = Path.GetExtension(file);

                if (extension.ToLower() != ".doc")
                {
                    message += String.Format("{0} extension not allowed. You must use a word document.", extension);
                }

                if (!File.Exists(file))
                {
                    message += String.Format("File \"{0}\" doesn't exist.", file);
                }
            }
            else
            {
                message = "wdump: dumps contents of word document. ";
                message += "Usage: wdump <file> <options>";
                message += "Options:";
                message += "-u  : Transform returns to unix format";
                message += "-c  : Strip out carriage returns";
                message += "-j  : Output json representation of text, font, font-size, bold, and italic";
            }
            return message;
        }
    }
}
