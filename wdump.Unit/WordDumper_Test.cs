using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Moq;

using wdump.Library;
using Word = NetOffice.WordApi;

namespace wdump.Unit
{
    [TestFixture]
    public class WordDumper_Test
    {
        WordDumper w;
        Mock<IWordDocument> word;
        Mock<IFilterApplyer> filter;

        [SetUp]
        public void SetUp()
        {
            word = new Mock<IWordDocument>();
            filter = new Mock<IFilterApplyer>();
            w = new WordDumper(word.Object, filter.Object);
        }

        /* Todo here:
         * What I want: 
         * To get the structure from the word file, and return json
         * with basic structure
         * The input should be the document object
         */

  
        [Test]
        public void It_Should_Transform_To_Unix()
        {
            word.Setup(d => d.Read()).Returns(new List<string>());
            filter.Setup(f => f.Apply(It.IsAny<List<string>>(), It.IsAny<Dictionary<string, string>>()))
                  .Verifiable();

            w = new WordDumper(word.Object, filter.Object);

            w.Dump(new Dictionary<string, string> { { "file", "file.doc" } });

            filter.Verify();
        } 
    }
}
