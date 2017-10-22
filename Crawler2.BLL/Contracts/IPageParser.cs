using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler2.BLL.Contracts
{
    public interface IPageParser
    {
        Task<List<string>> GetSubLinksAsync(string link, string page);
        Task<bool> SearchWordAsync(string content, string word);
    }
}
