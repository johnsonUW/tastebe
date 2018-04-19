using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Framework.Clover;
using Framework.Wechat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var x = await CloverClient.CreateOrderAsync(new List<CloverLineItemModel>
            {
                new CloverLineItemModel
                {
                    Name = "test",
                    PriceInPennies = 12,
                    Printed = true,
                    UnitQuantity = 2
                }
            }, "02c5cac5-06ba-c31b-effd-9286db53d8a0", "HS4VTV8MXDMDM", true);
            Console.WriteLine(x.Total);
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            var x = await CloverClient.GetItemsAsync("02c5cac5-06ba-c31b-effd-9286db53d8a0", "HS4VTV8MXDMDM", true);
            Console.WriteLine(x.First().Name);
        }

        [TestMethod]
        public async Task QrCode()
        {
            var stream = await WeChatHttpClient.GetMiniProgramQrCodeAsync("1", "wx5322fd632feee9b2",
                "e0704e11c8c3da3e30ec3369eb895343");
            using (var fileStream = File.Create("C:\\qrcodes\\1.png"))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }
    }
}
