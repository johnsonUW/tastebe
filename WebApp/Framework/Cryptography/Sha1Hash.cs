using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Framework.ExceptionHandling;

namespace Framework.Cryptography
{
    public static class Sha1Hash
    {
        public static string Hash(string textToBeHashed)
        {
            if (string.IsNullOrWhiteSpace(textToBeHashed))
                throw new ApiException(ApiErrorCode.InvalidData);
            var data = Encoding.UTF8.GetBytes(textToBeHashed);
            using (var sha1 = SHA1.Create())
            {
                var sha1Data = sha1.ComputeHash(data);
                var x = sha1Data.Select(s => s.ToString("x2"));
                return string.Join("", x);
            }
        }
    }
}