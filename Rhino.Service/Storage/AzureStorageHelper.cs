using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Rhino.Service.Storage
{
    internal class AzureStorageHelper : IDisposable
    {
        CloudBlobContainer blobContainer;
        public AzureStorageHelper()
        {
            blobContainer = GetContainer();
        }

        public CloudBlockBlob UploadBlob(string fileName, string contentType, string localFileName)
        {
            var blob = blobContainer.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = contentType;
            blob.UploadFromFile(localFileName, FileMode.OpenOrCreate);

            return blob;
        }

        public void DeleteBlob(string fileName)
        {
            var blob = blobContainer.GetBlockBlobReference(fileName);
            if (blob != null)
                blob.Delete();
        }

        private CloudBlobContainer GetContainer()
        {
            var storageConnection = ConfigurationManager.ConnectionStrings["IndigoSlateAzureStorage"];
            var blobContainer = ConfigurationManager.AppSettings["IndigSlateContentBlobContainer"];

            if (storageConnection == null || string.IsNullOrEmpty(blobContainer))
                throw new ArgumentNullException("Missing configuration for Azure Storage or Blob Container");

            var storageAccount = CloudStorageAccount.Parse(storageConnection.ConnectionString);
            return storageAccount.CreateCloudBlobClient().GetContainerReference(blobContainer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (blobContainer != null)
                {
                    blobContainer = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}