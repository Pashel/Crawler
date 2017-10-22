using Crawler2.BLL.Domain;

namespace Crawler2.BLL.Contracts
{
    public interface IValidator
    {
        ValidationResult CheckDeep(int deep);
        ValidationResult CheckWord(string word);
        ValidationResult CheckTimeLimit(int timeout);
        ValidationResult CheckGroupSize(int groupSize);
    }
}
