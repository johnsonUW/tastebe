using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Framework.BlobStorage;
using Framework.ExceptionHandling;
using Framework.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/image")]
    public class UploadController : BaseController
    {
        private static readonly BlobClient BlobClient = new BlobClient();

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> UploadBlob()
        {
            var contents = await Request.Content.ReadAsMultipartAsync();
            var content = await contents.Contents.First().ReadAsStreamAsync();
            var url = await BlobClient.UploadBlobAsync(content);
            return Ok(url);
        }

        [Route]
        [HttpDelete]
        public async Task DeleteBlob([FromBody] string blobUrl)
        {
            await BlobClient.DeleteBlobAsync(blobUrl);
        }
    }
}
