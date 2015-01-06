 using Newtonsoft.Json;
using Rhino.Service.Models;
using Rhino.Service.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Rhino.Service.Controllers
{
    public class TagsController : ApiController
    {
        IStorage storage;
        public TagsController()
        {
            storage = new SQLAzureStorage();
        }

        public TagTree Get()
        {
            return new TagTree()
            {
                Tags = JsonConvert.SerializeObject(storage.GetTags())
            };
        }

        public bool Post([FromBody]TagNode node)
        {
            if (!string.IsNullOrEmpty(node.Branch))
            {
                storage.AddTagNode(node.Name, node.Branch);
            }
            else
            {
                storage.AddTagBranch(node.Name);
            }
            return true;
        }


        public bool Delete([FromBody]TagNode node)
        {
            if (!string.IsNullOrEmpty(node.Branch))
            {
                storage.DeleteTagNode(node.Name, node.Branch);
            }
            else
            {
                storage.DeleteTagBranch(node.Name);
            }
            return true;
        }
    }
}
