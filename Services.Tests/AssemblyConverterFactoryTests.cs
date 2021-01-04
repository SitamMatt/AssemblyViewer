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
            var factory = new AssemblyConverterFactory();
            Assert.IsInstanceOf(typeof(AssemblyConverter), factory.Create());
        }
    }
}
