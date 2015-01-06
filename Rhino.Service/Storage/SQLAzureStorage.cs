
using Newtonsoft.Json;
using Rhino.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rhino.Service.Storage
{
    public class SQLAzureStorage : IStorage
    {
        private const string DEFAULT_TREE = "English";
        private ISDB db;
        public SQLAzureStorage()
        {
            db = new ISDB();
        }
        public IEnumerable<Models.Tag> GetTags()
        {
            return db.Tags.Where(t => t.Tree == DEFAULT_TREE);
        }

        public IEnumerable<Models.Content> GetContent()
        {
            return db.Contents;
        }

        public void AddTagBranch(string name)
        {
            if (db.Tags.Any(t => t.Branch.ToLower() == name.ToLower()))
                return;

            db.Tags.Add(new Models.Tag()
            {
                Tree = DEFAULT_TREE,
                Branch = name
            });
            db.SaveChanges();

        }

        public void AddTagNode(string name, string branchName)
        {
            var currentBranch = db.Tags.SingleOrDefault(t => t.Branch == branchName);
            if (currentBranch == null)
                return;

            var currentNodes = new List<TagNode>();
            if (currentBranch.Nodes != null)
            {
                currentNodes = JsonConvert.DeserializeObject<List<TagNode>>(currentBranch.Nodes);
            }
            if (currentNodes.Any(t => t.Name.ToLower() == name.ToLower()))
                return;

            currentNodes.Add(new TagNode() { Name = name, Branch = branchName });
            currentBranch.Nodes = JsonConvert.SerializeObject(currentNodes);
            db.SaveChanges();
        }

        public void DeleteTagBranch(string name)
        {
            var selectedBranch = db.Tags.SingleOrDefault(t => t.Branch == name);
            if (selectedBranch == null)
                return;

            db.Tags.Remove(selectedBranch);
            db.SaveChanges();
        }

        public void DeleteTagNode(string name, string branchName)
        {
            var currentBranch = db.Tags.SingleOrDefault(t => t.Branch == branchName);
            if (currentBranch == null)
                return;

            var currentNodes = new List<TagNode>();
            if (currentBranch.Nodes != null)
            {
                currentNodes = JsonConvert.DeserializeObject<List<TagNode>>(currentBranch.Nodes);
            }

            var selectedNode = currentNodes.SingleOrDefault(t => t.Name == name);
            if (selectedNode == null)
                return;

            currentNodes.Remove(selectedNode);
            currentBranch.Nodes = JsonConvert.SerializeObject(currentNodes);
            db.SaveChanges();

        }

        public void AddContent(Content content)
        {
            db.Contents.Add(content);
            db.SaveChanges();
        }

        public void DeleteContent(int id)
        {
            var selectedContent = db.Contents.SingleOrDefault(c => c.Id == id);
            db.Contents.Remove(selectedContent);
            db.SaveChanges();
        }

        public void UpdateContent(Content content)
        {
            var selectedContent = db.Contents.SingleOrDefault(c => c.Id == content.Id);
            selectedContent.IsApproved = content.IsApproved;
            db.SaveChanges();
        }
    }
}