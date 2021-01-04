using Model.Data;
using Moq;
using NUnit.Framework;
using Services.Factory;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Tests
{
    class AssemblyInfoServiceCreatorTests
    {
        [Test]
        public void CreateTest()
        {
            var creator = new AssemblyInfoServiceCreator();
            var aInfo = new Mock<AssemblyInfo>();

            Assert.IsInstanceOf(typeof(AssemblyInfoService), creator.Create(aInfo.Object));
        }
    }
}
