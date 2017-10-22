using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Crawler2.BLL.Contracts;

namespace Crawler2.BLL.Services
{
    public class PageParser : IPageParser
    {
        private const string HRefPattern = "href\\s*=\\s*[\"']\\s*((http|/[^/\"'])[^\"']*)[\"']";
        private readonly List<string> _viewed = new List<string>();

        public Task<List<string>> GetSubLinksAsync(string link, string page)
        {
            return Task.Run(() => {
                var match = Regex.Match(page,
                    HRefPattern,
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

                var url = new Uri(link);
                var mathes = new List<string>();

                while (match.Success) {
                    var subLink = match.Groups[1].Value;

                    // for next case: href="/home"
                    if (!subLink.StartsWith("http"))
                        subLink = $"{url.Scheme}://{url.Host}{subLink}";

                    // check if link was already viewed
                    if (!_viewed.Contains(subLink)) {
                        _viewed.Add(subLink);
                        mathes.Add(subLink);
                    }
                    match = match.NextMatch();
                }

                return mathes;
            });
        }

        public Task<bool> SearchWordAsync(string content, string word)
        {
            return Task.Run(() => {
                if (content.IndexOf(word, StringComparison.Ordinal) >= 0)
                    return true;
                return false;
            });
        }
    }
}