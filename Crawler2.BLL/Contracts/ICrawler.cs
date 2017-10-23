using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler2.BLL.Contracts
{
    public interface ICrawler
    {
        Task<List<string>> StartAsync(string link, int deep, string word);
        Task<string> JustGetContentAsync(string link);
        int TimeLimitToDownload { set; }
        int DownloadingGroupSize { get; set; }
    }
}
