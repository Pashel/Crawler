using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Crawler2.BLL.Contracts;

namespace Crawler2.BLL.Services
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private static HttpClient _client;

        public int TimeLimitToDownload
        {
            set {
                _client.Timeout = TimeSpan.FromSeconds(value);
            }
        }

        public HttpClientWrapper()
        {
            _client = new HttpClient();
            // default time limit to download is 30 seconds
            _client.Timeout = TimeSpan.FromSeconds(30);
        }

        public Task<string> GetStringAsync(string link)
        {
            return _client.GetStringAsync(link);
        }
    }
}
