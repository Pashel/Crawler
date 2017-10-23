using System;
using System.Collections.Generic;
using System.Reflection;
using Crawler2.BLL.Contracts;
using NUnit.Framework;
using Crawler2.BLL.Services;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Crawler2.BLL.Domain;

namespace Crawler2.Tests.Services
{
    [TestFixture]
    class CrawlerTests
    {
        private Mock<IValidator> _validator;
        private Mock<IPageParser> _parser;
        private ICrawler _crawler;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<IValidator>();
            _validator.Setup(v => v.CheckWord(It.IsAny<string>())).Returns(new ValidationResult {Success = true});
            _validator.Setup(v => v.CheckDeep(It.IsAny<int>())).Returns(new ValidationResult { Success = true });
            _validator.Setup(v => v.CheckTimeLimit(It.IsAny<int>())).Returns(new ValidationResult { Success = true });
            _validator.Setup(v => v.CheckGroupSize(It.IsAny<int>())).Returns(new ValidationResult { Success = true });

            _parser = new Mock<IPageParser>();
            _parser.Setup(p => p.GetSubLinksAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<string>{"1", "2", "3", "4", "5"}));
            _parser.Setup(p => p.SearchWordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            //_crawler = new Crawler(_validator.Object, _parser.Object);

            var mock = new Mock<Crawler>(_validator.Object, _parser.Object);
            mock.Setup(c => c.JustGetContentAsync(It.IsAny<string>())).Returns(Task.FromResult("hello world"));
            _crawler = mock.Object;
        }

        [Test]
        public async Task StartAsync_ParserDoesNotFindWordAndDeepEquals1_ShouldReturn0()
        {
            // Arrange
            _parser.Setup(p => p.SearchWordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            // Act
            var result = await _crawler.StartAsync("test", 1, "home");

            // Assert
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public async Task StartAsync_ParserAlwaysFindWordAndDeepEquals1_ShouldReturn6()
        {
            // Arrange
            _parser.Setup(p => p.SearchWordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _crawler.StartAsync("test", 1, "home");

            // Assert
            Assert.AreEqual(result.Count, 6);
        }

        [Test]
        public async Task StartAsync_ParserAlwaysFindWordAndDeepEquals2_ShouldReturn31()
        {
            // Arrange
            _parser.Setup(p => p.SearchWordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _crawler.StartAsync("test", 2, "home");

            // Assert
            Assert.AreEqual(result.Count, 31);
        }

        [Test]
        public async Task StartAsync_ParserAlwaysFindWordAndDeepEquals3_ShouldReturn156()
        {
            // Arrange
            _parser.Setup(p => p.SearchWordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _crawler.StartAsync("test", 3, "home");

            // Assert
            Assert.AreEqual(result.Count, 156);
        }

        [Test]
        public async Task StartAsync_OriginalParserAndDeepEquals1_ShouldReturn6()
        {
            // Arrange
            var parser = new PageParser();
            _parser.Setup(p => p.SearchWordAsync(It.IsAny<string>(), It.IsAny<string>())).Returns<string, string>(
                (p1, p2) => parser.SearchWordAsync(p1, p2)
             );

            // Act
            var result = await _crawler.StartAsync("test", 1, "world");

            // Assert
            Assert.AreEqual(result.Count, 6);
        }

        [Test]
        public async Task StartAsync_OriginalParserAndDeepEquals2_ShouldReturn0()
        {
            // Arrange
            var parser = new PageParser();
            _parser.Setup(p => p.SearchWordAsync(It.IsAny<string>(), It.IsAny<string>())).Returns<string, string>(
                (p1, p2) => parser.SearchWordAsync(p1, p2)
             );

            // Act
            var result = await _crawler.StartAsync("test", 1, "moon");

            // Assert
            Assert.AreEqual(result.Count, 0);
        }
    }
}
