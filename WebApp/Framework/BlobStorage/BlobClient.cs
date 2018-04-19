using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Framework.BlobStorage
{
    public class BlobClient
    {
        private readonly CloudBlobContainer _container;
        public BlobClient()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            _container = blobClient.GetContainerReference(ConfigurationManager.AppSettings.Get("Container"));
        }
        public async Task<Uri> UploadBlobAsync(Stream stream)
        {
            var guid = Guid.NewGuid();
            var blockBlob = _container.GetBlockBlobReference(guid.ToString());
            await blockBlob.UploadFromStreamAsync(stream);
            return blockBlob.Uri;
        }

        public async Task DeleteBlobAsync(string uri)
        {
            var guidStr = uri.Split('/').LastOrDefault();
            Guid guid;
            if (Guid.TryParse(guidStr, out guid))
            {
                var blockBlob = _container.GetBlockBlobReference(guid.ToString());
                await blockBlob.DeleteIfExistsAsync();
            }
        }

        public async Task<Stream> DownloadBlobAsync(string uri)
        {
            var guidStr = uri.Split('/').LastOrDefault();
            Guid guid;
            if (!Guid.TryParse(guidStr, out guid)) return null;
            var blockBlob = _container.GetBlockBlobReference(guid.ToString());
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                return memoryStream;
            }
        }
    }
}