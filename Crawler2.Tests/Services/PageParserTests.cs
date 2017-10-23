using NUnit.Framework;
using Crawler2.BLL.Services;
using System.Threading.Tasks;

namespace Crawler2.Tests.Services
{
    [TestFixture]
    class PageParserTests
    {
        private string _content = "<html>" +
                                    "<body>" +
                                    "<p>Hello world hello hello</p>" +
                                    "<h1>This is the end of global worming!</h1>" +
                                    "<h2>happy new year!</h2>" +
                                    "<a href='https://tut.by/blabla'>Go</a>" +
                                    "<a href='http://google.com'>Go</a>" +
                                    "<a href='https://yandex.ru?sobaka=pes'>Go</a>" +
                                    "<a href='/kolbasa'>Go</a>" +
                                    "</body>" +
                                    "</html>";

        [Test]
        public async Task SearchWordAsync_FindExistingWord_ShouldReturnTrue()
        {
            // Arrange
            var parser = new PageParser();

            // Act
            var result = await parser.SearchWordAsync(_content, "hello");

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task SearchWordAsync_FindMissingWord_ShouldReturnFalse()
        {
            // Arrange
            var parser = new PageParser();

            // Act
            var result = await parser.SearchWordAsync(_content, "bmw");

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task GetSubLinksAsync_FindExistingLinks_ShouldReturn4()
        {
            // Arrange
            var parser = new PageParser();

            // Act
            var result = await parser.GetSubLinksAsync("https://google.com", _content);

            // Assert
            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public async Task GetSubLinksAsync_OnLinkIsRepeated_ShouldReturn4()
        {
            // Arrange
            var parser = new PageParser();
            _content += "<a href='https://tut.by/blabla'>Go</a>";

            // Act
            var result = await parser.GetSubLinksAsync("https://google.com", _content);

            // Assert
            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public async Task GetSubLinksAsync_OneLinkDoesNotContainDomain_DomainShouldBeAddedToLink()
        {
            // Arrange
            var parser = new PageParser();
            _content += "<a href='https://tut.by/blabla'>Go</a>";

            // Act
            var result = await parser.GetSubLinksAsync("https://google.com/cars", _content);

            // Assert
            Assert.IsTrue(result.Contains("https://google.com/kolbasa"));
        }

        [Test]
        public async Task GetSubLinksAsync_NoLinksExists_ShoulReturn0()
        {
            // Arrange
            var parser = new PageParser();
            _content = "hello<br>byby<hr>worlisreallybig<br>goodbye";

            // Act
            var result = await parser.GetSubLinksAsync("https://google.com/cars", _content);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
