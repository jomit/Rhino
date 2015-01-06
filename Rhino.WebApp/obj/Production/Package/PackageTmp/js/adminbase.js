/// <reference path="C:\Jomit\Projects\IndigoSlate\Rhino\Rhino.WebApp\js/core.js" />
/// <reference path="C:\Jomit\Projects\IndigoSlate\Rhino\Rhino.WebApp\js/lib/knockout-3.1.0.js" />
/// <reference path="C:\Jomit\Projects\IndigoSlate\Rhino\Rhino.WebApp\js/lib/sammy-0.7.5.min.js" />

(function (INDIGOSLATE) {
    "use strict";
    INDIGOSLATE.adminService = (function () {
        var callService = INDIGOSLATE.core.callService;
        var getTags = function () {
            return $.Deferred(function (defer) {
                callService({
                    url: "tags"
                }).then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();
        };
        var getContent = function () {
            return $.Deferred(function (defer) {
                callService({
                    url: "content"
                }).then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();
        };
        var addBranch = function (data) {
            return $.Deferred(function (defer) {
                callService({
                    url: "tags", type: "POST", data: data
                })
                .then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();
        };
        var addNode = function (data) {
            return $.Deferred(function (defer) {
                callService({
                    url: "tags", type: "POST", data: data
                })
                .then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();

        };
        var deleteBranch = function (data) {
            return $.Deferred(function (defer) {
                callService({
                    url: "tags", type: "DELETE", data: data
                })
                .then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();
        };
        var deleteNode = function (data) {
            return $.Deferred(function (defer) {
                callService({
                    url: "tags", type: "DELETE", data: data
                })
                .then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();
        };
        var deleteContent = function (data) {
            return $.Deferred(function (defer) {
                callService({
                    url: "content", type: "DELETE", data: data
                })
                .then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();
        };
        var uploadContent = function (data) {
            return $.Deferred(function (defer) {
                $.ajax({
                    url: INDIGOSLATE.core.baseUrl + "content",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: data,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("DeviceId", INDIGOSLATE.core.hash);
                    },
                    success: function (response) {
                        if (response)
                            defer.resolve(response);
                        else
                            defer.reject("Bad Response");
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        var response = { serviceError: true, jqXHR: jqXHR, textStatus: textStatus, errorThrown: errorThrown };
                        defer.reject(response);
                    }
                });
            }).promise();
        };
        var approveContent = function (data) {
            return $.Deferred(function (defer) {
                callService({
                    url: "content", type: "PUT", data: data
                })
                .then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
            }).promise();
        };

        return {
            getTags: getTags,
            getContent: getContent,
            addBranch: addBranch,
            addNode: addNode,
            deleteBranch: deleteBranch,
            deleteNode: deleteNode,
            deleteContent: deleteContent,
            uploadContent: uploadContent,
            approveContent: approveContent
        }
    })();

    INDIGOSLATE.adminViewModel = (function () {
        var service = INDIGOSLATE.adminService;
        var tags = ko.observableArray();
        var allContentsCache = ko.observableArray();
        var contents = ko.observableArray();
        var topNavigation = { Categories: { Title: "Categories", Url: "/Admin/Categories.html" }, Tags: { Title: "Tags", Url: "/Admin/Tags.html" }, Content: { Title: "Content", Url: "/Admin/Content.html" } };
        var navigationDisplay = ko.observableArray();
        var showLoading = ko.observable(true);
        var searchText = ko.observable();
        var tagForm = {
            newBranchName: ko.observable(),
            selectedBranchName: ko.observable(),
            newNodeName: ko.observable(),
        };
        var contentForm = {
            title: ko.observable(),
            text: ko.observable(),
            videoFile: ko.observable(),
            thumnailImage: ko.observable(),
            selectedTags: ko.observableArray(),
            tags: ko.observable(),
            userName: ko.observable()
        };

        for (var key in topNavigation) {
            if (topNavigation.hasOwnProperty(key)) {
                navigationDisplay.push(topNavigation[key]);
            }
        }

        var resetForms = function () {
            tagForm.newBranchName("");
            tagForm.newNodeName("");
            tagForm.selectedBranchName("");

            contentForm.title("");
            contentForm.text("");
            contentForm.selectedTags([]);
            contentForm.tags("");
            contentForm.videoFile("");
            contentForm.thumnailImage("");
            contentForm.userName("");
        };

        var initialize = function (filteredContent) {
            resetForms();
            service.getTags()
                   .then(function (data) {
                       if (data.serviceError) {
                           showLoading(false);
                           INDIGOSLATE.adminViewHelper.showErrorMessage(data.errorThrown);
                           return;
                       }
                       var tagList = JSON.parse(data.Tags);
                       tags.removeAll();
                       tagList.forEach(function (tag) {
                           tags.push({
                               "Branch": ko.observable(tag.Branch),
                               "Nodes": ko.observableArray(JSON.parse(tag.Nodes))
                           });
                       });
                   });

            if (filteredContent == undefined) {
                service.getContent()
                       .then(function (data) {
                           if (data.serviceError) {
                               showLoading(false);
                               INDIGOSLATE.adminViewHelper.showErrorMessage(data.errorThrown);
                               return;
                           }
                           contents(data);
                           allContentsCache(data);
                           showLoading(false);
                       });
            }
            else {
                contents(filteredContent);
            }
        };

        var addBranch = function () {
            var branchName = tagForm.newBranchName().trim();
            if (branchName == '') {
                return;
            }

            service.addBranch({ "Name": branchName })
                   .then(initialize);
        };

        var addNode = function () {
            var nodeName = tagForm.newNodeName().trim();
            if (nodeName == '' || tagForm.selectedBranchName() == undefined)
                return;

            var branchName = tagForm.selectedBranchName().Branch();
            service.addNode({ "Name": nodeName, "Branch": branchName })
                   .then(initialize);
        };

        var addContent = function () {
            INDIGOSLATE.adminViewHelper.hideAllMessages();

            if (contentForm.title() == '' ||
                contentForm.text() == '' ||
                contentForm.selectedTags().length < 1
                //contentForm.videoFile() == '' ||
                //contentForm.thumnailImage() == ''
                ) {
                INDIGOSLATE.adminViewHelper.showErrorMessage("You gotta give me all the data.");
                return;
            }
            var tagList = [];
            contentForm.selectedTags().forEach(function (selectedTag) {
                tagList.push({ Name: selectedTag.split("|")[0], Branch: selectedTag.split("|")[1] });
            });
            contentForm.tags(JSON.stringify(tagList));
            contentForm.userName($("#currentUserName").text());
            var data = new FormData(document.forms['mainForm']);
            showLoading(true);
            service.uploadContent(data)
                   .then(function (response) {
                       showLoading(false);
                       resetForms();
                       INDIGOSLATE.adminViewHelper.showSuccessMessage();
                   })
                    .fail(function (response) {
                        showLoading(false);
                        resetForms();
                        INDIGOSLATE.adminViewHelper.showErrorMessage(response.errorThrown);
                    });
        };

        var approveContent = function (data) {
            var answer = confirm("Just to confirm, this will approve this Content and make is available to public.");
            if (answer == false)
                return;

            service.approveContent({ "Id": data.Id, "IsApproved": true })
                   .then(initialize);
        };

        var deleteBranch = function (data) {
            var answer = confirm("Just to confirm, this will delete the Category and all the associated Tags.");
            if (answer == false)
                return;

            service.deleteBranch({ "Name": data.Branch() })
                   .then(initialize);
        };

        var deleteNode = function (parent, data) {
            var answer = confirm("Just to confirm, this will delete the Tag and all the Content Mappings.");
            if (answer == false)
                return;

            var branchName = parent.Branch();
            var nodeName = data.Name;
            service.deleteNode({ "Name": nodeName, "Branch": branchName })
                   .then(initialize);
        };

        var deleteContent = function (data) {
            var answer = confirm("Just to confirm, this will delete the Content and all the associated Files.");
            if (answer == false)
                return;

            service.deleteContent({ "Id": data.Id, "FileName": data.FileName, "ThumbnailName": data.ThumbnailName })
                   .then(initialize);
        };

        var currentNavigationTitle = function () {
            var link = location.href.toLowerCase();
            if (link.indexOf(topNavigation.Categories.Url.toLowerCase()) > -1)
                return topNavigation.Categories.Title;
            else if (link.indexOf(topNavigation.Tags.Url.toLowerCase()) > -1)
                return topNavigation.Tags.Title;
            else if (link.indexOf(topNavigation.Content.Url.toLowerCase()) > -1)
                return topNavigation.Content.Title;
            else
                return topNavigation.Tags.Title;
        };

        var goToNavigation = function (navigationItem) {
            location.href = navigationItem.Url;
        };

        var searchContent = function () {
            if (searchText() == undefined || searchText() == '') {
                initialize(allContentsCache());
                return;
            }
            var lowerCaseSearchText = searchText().toLowerCase();
            var filteredContent = ko.utils.arrayFilter(allContentsCache(), function (item) {
                return (item.Title.toLowerCase().indexOf(lowerCaseSearchText) > -1 || item.Text.indexOf(lowerCaseSearchText) > -1);
            });
            initialize(filteredContent);
        };

        return {
            initialize: initialize,
            topNavigation: topNavigation,
            navigationDisplay: navigationDisplay,
            currentNavigationTitle: currentNavigationTitle,
            goToNavigation: goToNavigation,
            showLoading: showLoading,
            addBranch: addBranch,
            addNode: addNode,
            addContent: addContent,
            deleteBranch: deleteBranch,
            deleteNode: deleteNode,
            deleteContent: deleteContent,
            approveContent: approveContent,
            searchContent: searchContent,
            tags: tags,
            contents: contents,
            tagForm: tagForm,
            contentForm: contentForm,
            searchText: searchText
        }
    })();

    INDIGOSLATE.adminViewHelper = (function () {
        function hideAllMessages() {
            $(".gotthedata").hide();
            $(".somethingwentwrong").hide();
        };

        function showErrorMessage(message) {
            if (message != undefined && message != '')
                $(".somethingwentwrong").text(message);

            hideAllMessages();
            $(".somethingwentwrong").show();
        };
        function showSuccessMessage(message) {
            if (message != undefined && message != '')
                $(".gotthedata").text(message);

            hideAllMessages();
            $(".gotthedata").show();
        };
        return {
            showErrorMessage: showErrorMessage,
            showSuccessMessage: showSuccessMessage,
            hideAllMessages: hideAllMessages
        }
    })();

    INDIGOSLATE.adminViewModel.initialize();

    ko.bindingHandlers.executeOnEnter = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var allBindings = allBindingsAccessor();
            $(element).keypress(function (event) {
                var keyCode = (event.which ? event.which : event.keyCode);
                if (keyCode === 13) {
                    allBindings.executeOnEnter.call(viewModel);
                    return false;
                }
                return true;
            });
        }
    };

    ko.applyBindings(INDIGOSLATE.adminViewModel);

})(INDIGOSLATE);


