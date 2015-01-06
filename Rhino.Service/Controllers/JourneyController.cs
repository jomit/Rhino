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
    public class JourneyController : ApiController
    {
        public JourneyController()
        {
        }

        public Journey Get()
        {
            var techCMOStages = new List<JourneyStage>();
            techCMOStages.Add(new JourneyStage()
                {
                    Name = "Reach",
                    CustomerObjective = "Reach Me",
                    ExperienceObjective = "Reach target audience",
                    Strategy = "Lead generation campaign"
                });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Interest",
                CustomerObjective = "Interest Me",
                ExperienceObjective = "Share pain and value prop",
                Strategy = "Vision Video"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Relevance",
                CustomerObjective = "How can it work for me?",
                ExperienceObjective = "Convey scenarios & demo",
                Strategy = "Scenario videos & demos"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Scope",
                CustomerObjective = "Give me a proposal",
                ExperienceObjective = "Propose a solution",
                Strategy = "Solution config tool"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Compare",
                CustomerObjective = "How do you compare?",
                ExperienceObjective = "Stand above competitors",
                Strategy = "Comparision tools"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Decide",
                CustomerObjective = "Help me decide",
                ExperienceObjective = "Build case & remove blockers",
                Strategy = "Business value tools"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Purchase",
                CustomerObjective = "Purchase the solution",
                ExperienceObjective = "Order accuracy",
                Strategy = "Solution map"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Recieve",
                CustomerObjective = "Implement the solution",
                ExperienceObjective = "Ready to deliver value",
                Strategy = "Onboarding kit"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Adopt",
                CustomerObjective = "Realize value quickly",
                ExperienceObjective = "Adopt & expand",
                Strategy = "Adoption & community programs"
            });
            techCMOStages.Add(new JourneyStage()
            {
                Name = "Evangelize",
                CustomerObjective = "Evangelize",
                ExperienceObjective = "Customer references",
                Strategy = "Case study videos"
            });

            return new Journey()
            {
                Name = "Technical CMO Journey",
                Stages = techCMOStages
            };
        }
    }

    public class Journey
    {
        public string Name { get; set; }
        public List<JourneyStage> Stages { get; set; }
    }

    public class JourneyStage
    {
        public string Name { get; set; }
        public string CustomerObjective { get; set; }
        public string ExperienceObjective { get; set; }
        public string Strategy { get; set; }
    }
}
