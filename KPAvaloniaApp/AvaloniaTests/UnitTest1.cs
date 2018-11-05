using NUnit.Framework;
using System;
using KPAvalonia;
using Interfaces;
using Moq;
using FluentAssertions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaTests
{
    public class UnitTest1
    {
        [Test]
        public void OpenImageFile()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(m => m.FindFileAsync()).ReturnsAsync("/fake/file");
            dataServiceMock.Setup(m => m.FileExists(It.IsAny<String>())).Returns(true);
            var vm = new AvaloniaViewModel(dataServiceMock.Object);

            vm.FindFile.Execute(this);

            Assert.IsTrue(vm.LoadImage.CanExecute(this));
        }

        [Test]
        [TestCase("Hurricane") ]
        [TestCase("Trump")]
        [TestCase(null)]
        public void CallWebAPI(string searchString)
        {
            var dataServiceMock = new Mock<IDataService>();
            var expectedNews = new ObservableCollection<NewsArticles>();
            expectedNews.Add(new NewsArticles(searchString, null, null, null, null, null, null));
            dataServiceMock.Setup(m => m.GetNews(searchString)).ReturnsAsync(expectedNews);

            var vm = new AvaloniaViewModel(dataServiceMock.Object);

            vm.SearchQuery = searchString;
            vm.GetNews.Execute(this);

            vm.Articles[0].Title.Should().Be(searchString);
        }
    }
}
