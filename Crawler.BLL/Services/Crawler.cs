using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Crawler.BLL.Services
{
    class Crawler
    {
        private readonly string _word;
        private readonly int _deep;

        private static HttpClient _client;

        private List<string> _viewed = new List<string>();
        private List<string> _results = new List<string>();

        public int TimeLimitToDownload = 10;
        public int DownloadingGroupSize = 100;

        private const string HRefPattern = "href\\s*=\\s*[\"']\\s*((http|/[^/\"'])[^\"']*)[\"']";

        public Crawler(string word, int deep)
        {
            _word = word;
            _deep = deep;
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(TimeLimitToDownload);
        }

        public async Task<List<string>> StartAsync(string link)
        {
            string content = await GetContentAsync(link);
            await ParseContentAsync(link, content, _deep);
            return _results;
        }

        private async Task ParseSubLinksAsync(List<string> subLinks, int level)
        {
            var resultTasks = subLinks.Select(subLink => GetContentAsync(subLink)).ToList();
            await Task.WhenAll(resultTasks);

            var results = resultTasks.Select(task => task.Result).ToList();

            for (var i = 0; i < results.Count; i++)
            {
                await ParseContentAsync(subLinks[i], results[i], level - 1);
            }
        }

        private async Task ParseContentAsync(string link, string content, int level)
        {
            if (String.IsNullOrEmpty(content))
            {
                return;
            }

            bool isFound = await SearchWordAsync(content);
            if (isFound)
            {
                _results.Add(link);
            }

            if (level > 0)
            {
                var subLinks = await GetSubLinksAsync(link, content);
                // parse sublinks in separate groups
                while (subLinks.Count > DownloadingGroupSize)
                {
                    var group = subLinks.Take(DownloadingGroupSize).ToList();
                    subLinks = subLinks.Skip(DownloadingGroupSize).ToList();
                    await ParseSubLinksAsync(group, level);
                }
                // parse remain part of links
                if (subLinks.Count > 0)
                {
                    await ParseSubLinksAsync(subLinks, level);
                }
            }
        }

        private async Task<string> GetContentAsync(string link)
        {
            string content = "";
            try
            {
                content = await _client.GetStringAsync(link);
            }
            catch { }
            return content;
        }

        private Task<List<string>> GetSubLinksAsync(string link, string page)
        {
            return Task.Run(() => {
                var match = Regex.Match(page,
                    HRefPattern,
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

                var url = new Uri(link);
                var mathes = new List<string>();

                while (match.Success)
                {
                    var subLink = match.Groups[1].Value;

                    // for next case: href="/home"
                    if (!subLink.StartsWith("http"))
                    {
                        subLink = url.Scheme + "://" + url.Host + subLink;
                    }

                    // check if link was already viewed
                    if (!_viewed.Contains(subLink))
                    {
                        _viewed.Add(subLink);
                        mathes.Add(subLink);
                    }
                    match = match.NextMatch();
                }

                return mathes;
            });
        }

        private Task<bool> SearchWordAsync(string content)
        {
            return Task.Run(() => {
                if (content.IndexOf(_word) >= 0)
                {
                    return true;
                }
                return false;
            });
        }
    }
}
