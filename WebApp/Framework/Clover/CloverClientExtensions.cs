using System.Net.Http;
using System.Threading.Tasks;
using Framework.ExceptionHandling;
using Framework.Utils;

namespace Framework.Clover
{
    public static class CloverClientExtensions
    {
        public static async Task<TOut> ProcessReponse<TOut>(this HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new ApiException(ApiErrorCode.CloverError, result);
            var model = result.FromJson<TOut>();
            return model;
        }
    }
}