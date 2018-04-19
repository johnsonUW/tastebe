using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Framework.ExceptionHandling;

namespace Framework.Cryptography
{
    public static class Sha256Hash
    {
        public static string Hash(string textToBeHashed)
        {
            if (string.IsNullOrWhiteSpace(textToBeHashed))
                throw new ApiException(ApiErrorCode.InvalidData);
            var data = Encoding.UTF8.GetBytes(textToBeHashed);
            using (var sha256 = SHA256.Create())
            {
                var sha256Data = sha256.ComputeHash(data);
                var x = sha256Data.Select(s => s.ToString("x2"));
                return string.Join("", x);
            }
        }
    }
}