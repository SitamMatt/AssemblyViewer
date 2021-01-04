using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Tests
{
    class AssemblyConverterFactoryTests
    {
        [Test]
        public void CreateTest()
        {
            var converter = new Mock<AssemblyConverter>();
            var factory = new Mock<IAssemblyConverterFactory>();
            factory.Setup(x => x.Create()).Returns(converter.Object);
            Assert.IsInstanceOf(typeof(AssemblyConverter), factory.Object.Create());
        }
    }
}
