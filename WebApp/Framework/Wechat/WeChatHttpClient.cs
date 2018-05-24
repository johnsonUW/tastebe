using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Framework.ExceptionHandling;
using Framework.Logging;
using Framework.Utils;
using Framework.Wechat.Models;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Framework.Wechat
{
    public static class WeChatHttpClient
    {
        private static string _accessToken;
        private static readonly object AccessTokenLock = new object();
        private static HttpClient GetClient(string uri = "https://api.weixin.qq.com")
        {
            return new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
        }

        public static async Task<Stream> GetMiniProgramQrCodeAsync(string id, string appId, string appSecret)
        {
            var token = GetAccessTokenAsync(appId, appSecret);
            var client = GetClient();
            var data = new WechatQrCodeRequestModel
            {
                Path = $"pages/homepage/index?id={id}"
            };
            var content = JsonConvert.SerializeObject(data);
            var result = await client.PostAsync($"cgi-bin/wxaapp/createwxaqrcode?access_token={token}", new StringContent(content));
            var stream = await result.Content.ReadAsStreamAsync();

            return stream;
        }

        public static async Task<WeChatSession> GetSessionAsync(string code, string appId, string appSecret)
        {
            var client = GetClient();
            var result = await client.GetAsync($"sns/jscode2session?appid={appId}&secret={appSecret}&js_code={code}&grant_type=authorization_code");
            var str = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<WeChatSession>(str);
            response.CheckError();
            return response;
        }

        public static string GetAccessTokenAsync(string appId, string appSecret)
        {
            var token = _accessToken;
            lock (AccessTokenLock)
            {
                if (token == _accessToken)
                {
                    var client = GetClient();
                    var result = client.GetAsync(
                        $"cgi-bin/token?appid={appId}&secret={appSecret}&grant_type=client_credential").Result;
                    var str = result.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<WechatAccessTokenModel>(str);
                    response.CheckError();
                    _accessToken = response.AccessToken;
                    return response.AccessToken;
                }
                return _accessToken;
            }
        }
    }
}
