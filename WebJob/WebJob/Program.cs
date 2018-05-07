using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            SyncNewDishesFromClover();
        }

        public static void SyncNewDishesFromClover()
        {
            string html = string.Empty;
            string url = @"https://tasteservice.chinacloudsites.cn/api/v1/admin/sync";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
                Console.WriteLine(html);
            }
        }
    }
}
