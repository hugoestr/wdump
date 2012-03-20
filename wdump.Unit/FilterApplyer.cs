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
    class FilterApplyerTest
    {
        FilterApplyer fa;
        Mock<Filter> filter;

        [SetUp]
        public void SetUp()
        {
            filter = new Mock<Filter>();
            fa = new FilterApplyer(new List<Filter>() { filter.Object});
        }

        [Test]
        public void Should_Apply_Filter_When_Args_Exist()
        {
            filter.Setup(f => f.Transform(It.IsAny<List<string>>(),
                                          It.IsAny<List<string>>())).Verifiable();

            fa = new FilterApplyer(new List<Filter>(){filter.Object});

            var input = new List<string> { "one", "two" };
            var args = new Dictionary<string, string> { { "u", "true" } };

            fa.Apply(input, args);

            filter.Verify();
        }
    }
}

