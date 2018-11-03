using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Demo;
using Interfaces;
using FluentAssertions;

namespace AvaloniaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OpenAnImageFile()
        {
            var dataServiceMock = new Mock<IDataService>();
        }

        [TestMethod]
        public void CallWebAPI()
        {

        }
    }
}
