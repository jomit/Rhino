var RHINO = RHINO || {};

RHINO.core = (function () {
    "use strict";
    var methodType = { POST: "POST", GET: "GET" };
    var serviceBaseUrl = "http://localhost:30104";
    var defaultUser = "test";
    var defaultSecret = "test";

    function getDeviceId() {
        var packageSpecificToken = Windows.System.Profile.HardwareIdentification.getPackageSpecificToken(null);
        var hardwareId = packageSpecificToken.id;

        var dataReader = Windows.Storage.Streams.DataReader.fromBuffer(hardwareId);
        var array = new Array(hardwareId.length);
        dataReader.readBytes(array);

        var internalId = '';
        for (var i = 0; i < array.length; i++) {
            internalId += array[i].toString();
        }
        return internalId
    };

    function callService(options) {
        return $.Deferred(function (defer) {
            var type = "GET";
            if (options.type) {
                type = options.type;
            }
            var data = "";
            if (options.data) {
                data = JSON.stringify(options.data);
            }
            $.ajax({
                url: serviceBaseUrl + options.api,
                type: type,
                cache: true,
                dataType: "json",
                data: data,
                async: true,
                contentType: "application/json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("DeviceId", getDeviceId());
                },
                success: function (data) {
                    if (data) {
                        defer.resolve(data);
                    }
                    else {
                        defer.reject("BadResponse");
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    var response = { serviceError: true, jqXHR: jqXHR, textStatus: textStatus, errorThrown: errorThrown };
                    defer.reject(response);
                }
            });

        }).promise();

    };

    function messageBox(title, message) {

        var msg = new Windows.UI.Popups.MessageDialog(
         message, title);

        msg.commands.append(
            new Windows.UI.Popups.UICommand("I got it", function () {
                msg.defaultCommandIndex = 0;
            }));

        // Set the command that will be invoked by default
        msg.defaultCommandIndex = 0;

        // Set the command to be invoked when escape is pressed
        msg.cancelCommandIndex = 1;
        msg.showAsync();
    }

    function initialize(callback) {

    }

    return {
        callService: callService,
        initialize: initialize,
        defaultUser: {
            user: defaultUser,
            secret: defaultSecret
        },
        messageBox: messageBox,
    };
})();