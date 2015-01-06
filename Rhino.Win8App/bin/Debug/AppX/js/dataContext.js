/// <reference path="core.js" />
/// <reference path="lib/knockout-3.1.0.js" />
/// <reference path="lib/jquery-2.1.1.min.js" />

RHINO.dataContext = (function (RHINO) {
    function getJourneyStages() {
        return $.Deferred(function (defer) {
            RHINO.core.callService(
                {
                    api: "/journey"
                }).then(function (data) {
                    defer.resolve(data);
                }).fail(function (message) {
                    defer.resolve(message);
                });
        }).promise();
    };

    var getTags = function () {
        return $.Deferred(function (defer) {
            RHINO.core.callService({
                api: "/tags"
            }).then(function (data) {
                defer.resolve(data);
            }).fail(function (message) {
                defer.resolve(message);
            });
        }).promise();
    };

    return {
        getJourneyStages: getJourneyStages,
        getTags: getTags
    };

})(RHINO);