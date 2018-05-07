using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess;
using Framework.Cryptography;
using Framework.ExceptionHandling;
using Framework.Models;
using Framework.Utils;
using Framework.Wechat;
using Microsoft.Ajax.Utilities;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UsersController : BaseController
    {
        [Route("{code}")]
        [HttpGet]
        public async Task<IHttpActionResult> Login(string code)
        {
            if (code.IsNullOrWhiteSpace()) return Ok(GetErrorModel(ApiErrorCode.SessionExpired));
            var session = await WeChatHttpClient.GetSessionAsync(code, ConfigurationManager.AppSettings.Get("WeChatMiniProgramAppId"), ConfigurationManager.AppSettings.Get("WeChatMiniProgramAppSecret"));
            using (var context = new TasteContext())
            {
                var user = context.Users.FirstOrDefault(u => string.Equals(u.UserId, session.OpenId));
                if (user != null)
                {
                    return Ok(user.UserId);
                }
                user = context.Users.Add(new User
                {
                    Name = session.OpenId,
                    UserId = session.OpenId
                });
                context.SaveChanges();
                return Ok(user.UserId);
            }
        }
    }
}
