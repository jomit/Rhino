using Rhino.Service.Models;
using Rhino.Service.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Rhino.Service.Controllers
{
    public class ContentController : ApiController
    {
        IStorage storage;
        public ContentController()
        {
            storage = new SQLAzureStorage();
        }
        public IEnumerable<Content> Get()
        {
            return storage.GetContent();
        }

        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                var azureStorageHelper = new AzureStorageHelper();

                var newContent = new Content();
                foreach (var fileData in provider.FileData)
                {
                    string fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));
                    if (string.IsNullOrEmpty(fileName))
                        continue;

                    var blob = azureStorageHelper.UploadBlob(fileName, fileData.Headers.ContentType.MediaType, fileData.LocalFileName);
                    File.Delete(fileData.LocalFileName);
                    if (fileData.Headers.ContentDisposition.Name.Contains("contentFile"))
                    {
                        newContent.FileName = fileName;
                        newContent.FileLocation = blob.Uri.AbsoluteUri;
                        newContent.FileSize = blob.Properties.Length;
                        newContent.FileContentType = blob.Properties.ContentType;
                    }
                    else if (fileData.Headers.ContentDisposition.Name.Contains("contentThumbnail"))
                    {
                        newContent.ThumbnailName = fileName;
                        newContent.ThumbnailLocation = blob.Uri.AbsoluteUri;
                        newContent.ThumbnailSize = blob.Properties.Length;
                        newContent.ThumbnailContentType = blob.Properties.ContentType;
                    }
                }

                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "Title")
                        {
                            newContent.Title = val;
                        }
                        if (key == "Text")
                        {
                            newContent.Text = val;
                        }
                        if (key == "Tags")
                        {
                            newContent.Tags = val;
                        }
                        if (key == "UserName")
                        {
                            newContent.UserName = val;
                        }
                    }
                }
                newContent.IsApproved = false;
                newContent.CreatedDate = DateTime.Now;
                storage.AddContent(newContent);

                //var response = Request.CreateResponse(HttpStatusCode.Moved);
                //response.Headers.Location = new Uri("/Home/UploadContent", UriKind.Relative);
                //return response;
                return new HttpResponseMessage()
                {
                    Content = new StringContent("File Saved")
                };
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public bool Delete([FromBody]Content content)
        {
            if (content.Id <= 0)
                return false;

            storage.DeleteContent(content.Id);

            var azureStorageHelper = new AzureStorageHelper();
            if (!string.IsNullOrEmpty(content.FileName))
                azureStorageHelper.DeleteBlob(content.FileName);

            if (!string.IsNullOrEmpty(content.ThumbnailName))
                azureStorageHelper.DeleteBlob(content.ThumbnailName);

            return true;
        }

        public bool Put([FromBody]Content content)
        {
            if (content.Id <= 0)
                return false;

            storage.UpdateContent(content);
            return true;
        }
    }
}
