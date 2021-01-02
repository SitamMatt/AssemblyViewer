using Model.Data;
using Model.VisitorPattern;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Tests
{
    class AssemblyInfoServiceTests
    {

        [Test]
        public void AcceptRootTest()
        {
            var aiMock = new Mock<AssemblyInfo>();
            aiMock.Setup(x => x.Accept(It.IsAny<IVisitor>()));

            var visitorMock = new Mock<IVisitor>();

            var ais = new AssemblyInfoService(aiMock.Object);

            ais.AcceptRoot(visitorMock.Object);

            aiMock.Verify(x => x.Accept(It.IsAny<IVisitor>()), Times.Once);
        }

        [Test]
        public void AcceptTest()
        {
            var guid = Guid.NewGuid();
            var aiMock = new Mock<AssemblyInfo>();
            aiMock.Setup(x => x.Accept(It.IsAny<IVisitor>()));
            var aiRootMock = new Mock<AssemblyInfo>();
            aiRootMock.Setup(x => x.Accept(It.IsAny<IVisitor>()));
            aiRootMock.SetupGet(x => x.Lookup).Returns(new Dictionary<Guid, AsmComponent>()
            {
                {
                    guid,
                    aiMock.Object
                }
            });

            var visitorMock = new Mock<IVisitor>();

            var ais = new AssemblyInfoService(aiRootMock.Object);

            ais.Accept(guid, visitorMock.Object);

            aiRootMock.Verify(x => x.Lookup, Times.Once);
            aiMock.Verify(x => x.Accept(It.IsAny<IVisitor>()), Times.Once);
        }
    }
}
