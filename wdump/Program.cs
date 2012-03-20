using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using wdump.Library;

namespace wdump
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsParser = new ArgsParser();
            var message = argsParser.Validate(args);
    
            if (message == "")
            {
                execute(args);
            }
            else
            {
                Console.WriteLine(message);
            }
       }

        private static void execute(string[] args)
       {
            var result = extract(args);

            foreach (string s in result)
            {
                Console.Write(s);
            }
        }

        private static List<string> extract(string[] args)
        {
            List<string> result = null;
            WordDocument document = null;
            var parser = new ArgsParser();

            try
            {
                document = new WordDocument();
                var filters = new FilterApplyer(new List<Filter>() 
                              { new UnixFilter(),
                                new NoCarriageReturnFilter()
                              });

                var dumper = new WordDumper(document, filters);
                result = dumper.Dump(parser.Parse(args));
            }
            catch
            {
                if (document != null)
                    document.Finally();
            }
            return result;
        }

    }

}
