using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using wdump.Library;

using NUnit.Framework;
using Moq;

namespace wdump.Unit
{
    [TestFixture]
    class ArgsParser_Test
    {
        ArgsParser p;

        [SetUp]
        public void SetUp()
        {
            p = new ArgsParser();
        }

        [Test]
        public void Should_Set_Switches_To_True()
        {
            var input = new string[] {"myfile.doc", "-u"};
            var result = p.Parse(input);

            Assert.That(result["u"], Is.Not.Null);
        }

        [Test]
        public void Should_Set_file_Value()
        {
            var input = new string[] { "myfile.doc", "-u" };
            var result = p.Parse(input);

            Assert.That(result["file"], Is.EqualTo("myfile.doc"));
        }
    }
}

