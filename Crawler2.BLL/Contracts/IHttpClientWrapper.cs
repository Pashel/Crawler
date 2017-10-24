using System.Threading.Tasks;

namespace Crawler2.BLL.Contracts
{
    public interface IHttpClientWrapper
    {
        Task<string> GetStringAsync(string link);
        int TimeLimitToDownload { set; }
    }
}
