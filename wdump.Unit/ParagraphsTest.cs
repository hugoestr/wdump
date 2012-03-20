using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Moq;

using wdump.Library;

namespace wdump.Unit
{
    [TestFixture]
    class ParagraphsTest
    {
        Paragraphs p;

        [SetUp]
        public void SetUp()
        {
            p = new Paragraphs();
            p.Items = new List<Paragraph>() { 
                new Paragraph() {Text = "some text", Font = "Arial", FontSize = "14"},
                new Paragraph() {Text = "more text", Font = "Times New Roman", 
                                FontSize = "12", Bold = true, Italic = true}
            };
        }

        [Test]
        public void Should_Convert_To_Json()
        {
            var result =  p.ToJson();

            var firstEntry = "{ text : \"some text\", font : \"Arial\","; 
                firstEntry += " font_size : \"14\", bold : false,";
                firstEntry += " italic : false }";

            var secondEntry = "{ text : \"more text\", font : \"Times New Roman\",";
                secondEntry += " font_size : \"12\", bold : true,";
                secondEntry += " italic : true }";

            var expected = String.Format("[ {0}, {1} ]", firstEntry, secondEntry);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}

