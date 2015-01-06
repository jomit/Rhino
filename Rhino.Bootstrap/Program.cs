using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Rhino.Service.Controllers;
using Rhino.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Bootstrap
{
    class Program
    {
        static void Main(string[] args)
        {
            PopulateTags();
        }

        private static void PopulateTags()
        {
            var allBranches = new List<string>() { "Deliverables - Journey", "Reach", "Interest", "Relevance", "Scope", "Compare", "Decide", "Purchase", "Receive", "Adopt", "Evangelize", "Unify", "Integrate", "Manage" };
            var allTags = new List<string>() { "Deliverables - Journey,Market Strategy", "Deliverables - Journey,CX Journey Map", "Deliverables - Journey,CX Architecture", "Deliverables - Journey,CX Strategy", "Deliverables - Journey,P&M Framework", "Deliverables - Journey,Capability Frameworks", "Deliverables - Journey,Creative Direction", "Reach,Lead Gen Campaign", "Reach,Lead Gen Survey", "Interest,Vision Video", "Interest,Interactive Infographic", "Interest,Intro Sales Presentation", "Interest,Customer Collateral", "Relevance,Scenario Videos", "Relevance,Scenario Deck", "Relevance,Sales Discussion Guide", "Relevance,Demo", "Relevance,Sales Demo Training", "Relevance,Scenario Interest Survey", "Scope,Solution Config Tool", "Scope,Proactive Proposal ", "Compare,Comparison Tools", "Compare,Competitive Landscape Video", "Compare,Sales Compete Guide", "Decide,Business Value Toolkit", "Decide,Scenario Articles", "Decide,Buinsess Value Guide", "Purchase,Order Interface Spec", "Purchase,Solution Ordering Map", "Purchase,Order Form", "Purchase,Purchase Automation", "Receive,Onboarding Kit", "Receive,Soln Fulfillment Templates", "Adopt,User Adoption Program", "Adopt,Tips & Tricks Series", "Adopt,Interactive Newsletter", "Adopt,Contest creation", "Adopt,Community Program Design", "Evangelize,Case Study Video", "Evangelize,Evidence Infographic", "Evangelize,Evidence Datasheet", "Evangelize,Evidence Program Design", "Unify,Unified Digital Environment", "Integrate,Sales & Mktg Automation", "Integrate,Experience Playbook", "Integrate,Account Planning Tools", "Manage,Performance Dashboard", "Manage,CX Datamart", "Manage,CX Control Plan", "Manage,CX Assessment", "Manage,Customer Journey Dashboard" };
            var tagService = new TagsController();

            Console.WriteLine("Creating Branches");
            Console.WriteLine("----------------------");
            foreach (var branch in allBranches)
            {
                Console.WriteLine(branch);
                tagService.Post(new TagNode() { Name = branch });
            }
            Console.WriteLine("");
            Console.WriteLine("Creating Tags");
            Console.WriteLine("----------------------");
            foreach (var tag in allTags)
            {
                Console.WriteLine(tag);
                var temp = tag.Split(',');
                tagService.Post(new TagNode() { Branch = temp[0], Name = temp[1] });
            }
            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        private static void ChangeAzureStorageVersion()
        {
            var storageConnection = ConfigurationManager.ConnectionStrings["IndigoSlateAzureStorage"];
            var storageAccount = CloudStorageAccount.Parse(storageConnection.ConnectionString);
            var client = storageAccount.CreateCloudBlobClient();
            var properties = client.GetServiceProperties();
            properties.DefaultServiceVersion = "2013-08-15";
            client.SetServiceProperties(properties);
            Console.WriteLine(properties.DefaultServiceVersion);
            Console.ReadLine();
        }
    }
}
