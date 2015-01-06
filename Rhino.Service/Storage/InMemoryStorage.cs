using Newtonsoft.Json;
using Rhino.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhino.Service.Storage
{
    public class InMemoryStorage : IStorage
    {
        public IEnumerable<Models.Tag> GetTags()
        {
            return new List<Tag>()
            {
                new Tag() {
                Tree = "English",
                Branch = "Audience",
                Nodes = JsonConvert.SerializeObject(new List<TagNode>()
                       {
                           new TagNode(){ Name="BDM" },
                           new TagNode(){ Name="TDM" },
                       })
                },
                new Tag() {
                Tree = "English",
                Branch = "Vendor",
                Nodes = JsonConvert.SerializeObject(new List<TagNode>()
                       {
                           new TagNode(){ Name="Microsoft" },
                           new TagNode(){ Name="VMWare" },
                           new TagNode(){ Name="Google" },
                       })
                },
                 new Tag() {
                Tree = "English",
                Branch = "Access",
                Nodes = JsonConvert.SerializeObject(new List<TagNode>()
                       {
                           new TagNode(){ Name="Private" },
                           new TagNode(){ Name="Public" },
                       })
                }
            };
        }

        public IEnumerable<Content> GetContent()
        {
            return new List<Content>()
            {
                new Content()
                {
                     Id = 1,
                     Title = "Vision Video 1",
                     Text=@"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
                            Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
                            Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
                            Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
                            Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                     FileLocation = "",
                     ThumbnailLocation = "",
                     Tags = "{\"BDM\":true,\"Microsoft\":true,\"Public\":true}",
                     CreatedDate =DateTime.Now
                },
                  new Content()
                {
                     Id = 1,
                     Title = "Video Vision 2",
                     Text=@"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
                            Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
                            Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
                            Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
                            Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                     FileLocation = "",
                     ThumbnailLocation = "",
                     Tags = "{\"TDM\":true,\"VMWare\":true,\"Private\":true}",
                     CreatedDate =DateTime.Now
                },
                  new Content()
                {
                     Id = 1,
                     Title = "Business Value Calc 1",
                     Text=@"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
                            Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
                            Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
                            Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
                            Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                     FileLocation = "",
                     ThumbnailLocation = "",
                     Tags = "{\"TDM\":true,\"Microsoft\":true,\"Public\":true}",
                     CreatedDate =DateTime.Now
                },
                new Content()
                {
                     Id = 1,
                     Title = "Value Business Calc 2",
                     Text=@"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
                            Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
                            Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
                            Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
                            Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                     FileLocation = "",
                     ThumbnailLocation = "",
                     Tags = "{\"TDM\":true,\"BDM\":true,\"Google\":true,\"Public\":true}",
                     CreatedDate =DateTime.Now
                },
                new Content()
                {
                     Id = 1,
                     Title = "Web environmnet 1",
                     Text=@"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
                            Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
                            Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
                            Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
                            Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                     FileLocation = "",
                     ThumbnailLocation = "",
                     Tags = "{\"TDM\":true,\"BDM\":true,\"Microsoft\":true,\"Private\":true}",
                     CreatedDate =DateTime.Now
                },
            };
        }


        public void AddTagBranch(string name)
        {
            throw new NotImplementedException();
        }

        public void AddTagNode(string name, string branchName)
        {
            throw new NotImplementedException();
        }


        public void DeleteTagBranch(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteTagNode(string name, string branchName)
        {
            throw new NotImplementedException();
        }


        public void AddContent(Content content)
        {
            throw new NotImplementedException();
        }


        public void DeleteContent(int id)
        {
            throw new NotImplementedException();
        }


        public void UpdateContent(Content content)
        {
            throw new NotImplementedException();
        }
    }
}