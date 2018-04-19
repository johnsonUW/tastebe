using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Framework.ExceptionHandling;

namespace Framework.Cryptography
{
    public static class Md5Hash
    {
        public static string Hash(string textToBeHashed)
        {
            if (string.IsNullOrWhiteSpace(textToBeHashed))
                throw new ApiException(ApiErrorCode.InvalidData);
            var data = Encoding.UTF8.GetBytes(textToBeHashed);
            using (var md5 = MD5.Create())
            {
                var md5Data = md5.ComputeHash(data);
                var x = md5Data.Select(s => s.ToString("x2"));
                return string.Join("", x);
            }
        }
    }
}