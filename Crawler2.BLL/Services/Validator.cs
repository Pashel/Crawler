using Crawler2.BLL.Contracts;
using Crawler2.BLL.Domain;

namespace Crawler2.BLL.Services
{
    public class Validator : IValidator
    {
        public ValidationResult CheckDeep(int deep)
        {
            if (deep < 1 || deep > 5) {
                return new ValidationResult() {
                    Success = false,
                    Message = "Deep parametr should be between 1 and 5"
                };
            }

            return new ValidationResult() {
                Success = true
            };
        }

        public ValidationResult CheckWord(string word)
        {
            if (string.IsNullOrEmpty(word)) {
                return new ValidationResult() {
                    Success = false,
                    Message = "Word to search can't be empty"
                };
            }

            return new ValidationResult() {
                Success = true
            };
        }
    }
}
