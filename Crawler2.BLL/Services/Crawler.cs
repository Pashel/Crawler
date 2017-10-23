using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Crawler2.BLL.Contracts;

namespace Crawler2.BLL.Services
{
    public class Crawler : ICrawler
    {
        public static HttpClient _client;

        private readonly IPageParser _parser;
        private readonly IValidator _validator;
        private readonly List<string> _results = new List<string>();

        private int _deep;
        private string _word;

        public int TimeLimitToDownload
        {
            set {
                var result = _validator.CheckTimeLimit(value);
                if (!result.Success) throw new ValidationException(result.Message);
                _client.Timeout = TimeSpan.FromSeconds(value);
            }
        }

        private int _downloadGroupSize;
        public int DownloadingGroupSize
        {
            get { return _downloadGroupSize; }
            set {
                var result = _validator.CheckGroupSize(value);
                if (!result.Success) throw new ValidationException(result.Message);
                _downloadGroupSize = value;
            }
        }

        public Crawler(IValidator validator, IPageParser parser)
        {
            _client = new HttpClient();
            _validator = validator;
            _parser = parser;

            // set default downloading properties for crawler
            TimeLimitToDownload = 10;
            DownloadingGroupSize = 100;
        }

        public async Task<List<string>> StartAsync(string link, int deep, string word)
        {
            var result = _validator.CheckDeep(deep);
            if (!result.Success) throw new ValidationException(result.Message);

            result = _validator.CheckWord(word);
            if (!result.Success) throw new ValidationException(result.Message);

            // save parameters
            _deep = deep;
            _word = word;

            // start searching process
            var content = await JustGetContentAsync(link);
            await ParseContentAsync(link, content, _deep);
            return _results;
        }

        public virtual async Task<string> JustGetContentAsync(string link)
        {
            var content = "";
            try {
                content = await _client.GetStringAsync(link);
            }
            catch {
                // ignored
            }
            return content;
        }

        private async Task ParseSubLinksAsync(List<string> subLinks, int level)
        {
            var resultTasks = subLinks.Select(subLink => JustGetContentAsync(subLink)).ToList();
            await Task.WhenAll(resultTasks);

            var results = resultTasks.Select(task => task.Result).ToList();

            for (var i = 0; i < results.Count; i++) {
                await ParseContentAsync(subLinks[i], results[i], level - 1);
            }
        }

        private async Task ParseContentAsync(string link, string content, int level)
        {
            if (string.IsNullOrEmpty(content)) return;

            var isFound = await _parser.SearchWordAsync(content, _word);
            if (isFound) _results.Add(link);

            if (level > 0) {
                var subLinks = await _parser.GetSubLinksAsync(link, content);

                // parse sublinks in separate groups
                while (subLinks.Count > DownloadingGroupSize) {
                    var group = subLinks.Take(DownloadingGroupSize).ToList();
                    subLinks = subLinks.Skip(DownloadingGroupSize).ToList();
                    await ParseSubLinksAsync(group, level);
                }

                // parse remain part of links
                if (subLinks.Count > 0) await ParseSubLinksAsync(subLinks, level);
            }
        }
    }
}