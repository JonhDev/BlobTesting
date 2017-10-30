using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.IO;
using System;

namespace BlobTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("autodefensasfotos");
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob blob = container.GetBlockBlobReference("blobTest");

            using (var fileStream = File.OpenRead("img_asalto.jpg"))
            {
                blob.UploadFromStream(fileStream);
            }
            Console.ReadKey();
        }
    }
}
