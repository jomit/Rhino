using Rhino.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Service.Storage
{
    public interface IStorage
    {
        IEnumerable<Tag> GetTags();

        IEnumerable<Content> GetContent();

        void AddTagBranch(string name);

        void AddTagNode(string name, string branchName);

        void AddContent(Content content);

        void DeleteTagBranch(string name);

        void DeleteTagNode(string name, string branchName);

        void DeleteContent(int id);

        void UpdateContent(Content content);
    }
}
