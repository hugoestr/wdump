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
    class ParagraphTest
    {
        Paragraph p;

        [SetUp]
        public void SetUp()
        {
            p = new Paragraph();
        }

        [Test]
        public void Should_Convert_Text_To_Json()
        {
            p.Text = "some text";
            var result = p.ToJson();
            var expected = "{ text : \"some text\", font : \"\", font_size : \"\", bold : false,";
            expected += " italic : false }";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Convert_Font_To_Json()
        {
            p.Text = "some text";
            p.Font = "Arial";
            var result = p.ToJson();
            var expected = "{ text : \"some text\", font : \"Arial\", font_size : \"\", bold : false,";
            expected += " italic : false }";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Convert_FontSize_To_Json()
        {
            p.Text = "some text";
            p.Font = "Arial";
            p.FontSize = "12";

            var result = p.ToJson();
            var expected = "{ text : \"some text\", font : \"Arial\", font_size : \"12\", bold : false,";
            expected += " italic : false }";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Convert_Bold_To_Json()
        {
            p.Text = "some text";
            p.Font = "Arial";
            p.FontSize = "12";
            p.Bold = true;

            var result = p.ToJson();
            var expected = "{ text : \"some text\", font : \"Arial\", font_size : \"12\", bold : true,";
            expected += " italic : false }";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Convert_Italic_To_Json()
        {
            p.Text = "some text";
            p.Font = "Arial";
            p.FontSize = "12";
            p.Bold = false;
            p.Italic = true;


            var result = p.ToJson();
            var expected = "{ text : \"some text\", font : \"Arial\", font_size : \"12\", bold : false,";
            expected += " italic : true }";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Convert_Underline_To_Json()
        {
            p.Text = "some text";
            p.Font = "Arial";
            p.FontSize = "12";
            p.Bold = false;
            p.Italic = true;

            var result = p.ToJson();
            var expected = "{ text : \"some text\", font : \"Arial\", font_size : \"12\", bold : false,";
            expected += " italic : true }";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Escape_Quotes()
        {
            p.Text = "some text that has \"quotes\"";
            p.Font = "Arial";
            p.FontSize = "12";
            p.Bold = false;
            p.Italic = true;

            var result = p.ToJson();
            var expected = "{ text : \"some text that has \\\"quotes\\\"\", font : \"Arial\", font_size : \"12\", bold : false,";
            expected += " italic : true }";

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}

