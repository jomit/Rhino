/// <reference path="../../js/lib/jquery-2.1.1.min.js" />
/// <reference path="../../js/lib/knockout-3.1.0.js" />
/// <reference path="../../js/core.js" />
/// <reference path="../../js/dataContext.js" />

(function () {
    "use strict";
    RHINO.homeViewModel = (function () {
        var journeyData = {
            customerName: ko.observable(""),
            audience: ko.observable(""),
            industry: ko.observable(""),
            productOrService: ko.observable(""),
            marketingGoal: ko.observable(""),
            selectedStages: ko.observableArray(""),
            selectedContent: ko.observableArray(""),
            solutionOverview: ko.observable(""),
            notes: ko.observable("")
        };
        var userDetails = {
            name: ko.observable(""),
            secret: ko.observable(""),
            isAuthenticated: ko.observable(false),
            hasFilledInRequirements: ko.observable(false),
            hasSelectedCategories: ko.observable(false)
        };

        //var journeyStages = ko.observableArray();
        var tags = ko.observableArray();

        function initialize() {
            tempPopulateSampleData();

            RHINO.dataContext
                 .getTags()
                 .then(function (data) {
                     var tagList = JSON.parse(data.Tags);
                     tags.removeAll();
                     tagList.forEach(function (tag) {
                         tags.push({
                             Branch: ko.observable(tag.Branch),
                             Nodes: ko.observableArray(JSON.parse(tag.Nodes)),
                             IsSelected: ko.observable(false)
                         });
                     });
                 });

            //RHINO.dataContext
            //     .getJourneyStages()
            //     .then(function (journeyData) {
            //         ko.utils.arrayForEach(journeyData.Stages, function (journeyStage) {
            //             journeyStages.push({
            //                 Name: ko.observable(journeyStage.Name),
            //                 CustomerObjective: ko.observable(journeyStage.CustomerObjective),
            //                 ExperienceObjective: ko.observable(journeyStage.ExperienceObjective),
            //                 Strategy: ko.observable(journeyStage.Strategy),
            //                 IsSelected: ko.observable(false)
            //             });
            //         });
            //     })
        };

        function validateUser() {
            if (userDetails.name() == RHINO.core.defaultUser.user && userDetails.secret() == RHINO.core.defaultUser.secret) {
                this.userDetails.isAuthenticated(true);
                return true;
            }
            return false;
        }

        function showCategories() {
            if (journeyData.customerName().trim() == "" && journeyData.audience().trim() == "" && journeyData.marketingGoal().trim() == "") {
                this.userDetails.hasFilledInRequirements(false);
                RHINO.core.messageBox("Something is Missing", "You atleast need to tell me the Customer Name, Audience and Marketing Goals.")
                return false;
            }
            this.userDetails.hasFilledInRequirements(true);
            return true;
        };

        function showTags() {
            this.userDetails.hasSelectedCategories(true);
            return true;
        };

        function categorySelected(data) {
            data.IsSelected(!data.IsSelected());
        };

        function tempPopulateSampleData() {
            userDetails.name("indigo");
            userDetails.secret("slate");
            journeyData.customerName("Microsoft");
            journeyData.audience("BDM");
            journeyData.marketingGoal("Some fluff !!!");
        };

        return {
            initialize: initialize,
            journeyData: journeyData,
            userDetails: userDetails,
            //journeyStages: journeyStages,
            tags: tags,
            validateUser: validateUser,
            showCategories: showCategories,
            showTags: showTags,
            categorySelected: categorySelected
        }

    })();

    WinJS.UI.Pages.define("/pages/home/home.html", {
        ready: function (element, options) {
            WinJS.Resources.processAll();

            RHINO.homeViewModel.initialize();
            ko.applyBindings(RHINO.homeViewModel);
        }
    });

})();