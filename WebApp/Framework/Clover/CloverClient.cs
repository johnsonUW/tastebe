using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Framework.Utils;

namespace Framework.Clover
{
    public static class CloverClient
    {
        private static HttpClient GetClient(bool sandboxMode, string accessToken)
        {
            var addr = sandboxMode ? "https://apisandbox.dev.clover.com" : "https://api.clover.com";
            var client = new HttpClient
            {
                BaseAddress = new Uri(addr),
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return client;
        }

        public static async Task<List<CloverItemModel>> GetItemsAsync(string accessToken, string merchantId, bool sandboxMode = false)
        {
            try
            {
                if (accessToken == null) return new List<CloverItemModel>();
                var client = GetClient(sandboxMode, accessToken);
                var response = await client.GetAsync($"v3/merchants/{merchantId}/items");
                var model = await response.ProcessReponse<CloverMenuModel>();
                return model.Elements;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<CloverOrderCreatedResponseModel> CreateOrderAsync(List<CloverLineItemModel> items, string accessToken,
            string merchantId, bool sandboxMode = false)
        {
            var client = GetClient(sandboxMode, accessToken);
            var data = new CloverCreateOrderModel
            {
                LineItems = items,
                State = "open"
            }.ToJson();
            var response = await client.PostAsync($"v3/merchants/{merchantId}/orders", new StringContent(data));
            var model = await response.ProcessReponse<CloverOrderCreatedResponseModel>();
            return model;
        }
    }
}
