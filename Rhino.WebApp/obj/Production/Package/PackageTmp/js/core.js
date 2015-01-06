var INDIGOSLATE = INDIGOSLATE || {};

INDIGOSLATE.core = (function () {
    "use strict";
    var methodType = { POST: "POST", GET: "GET" };
    var baseUrl = $("#baseUrl").text();
    var hash = "A2897A8A-F4FB-47A7-B59A-B7E527707574"; //TODO implement the server hash later

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
                url: baseUrl + options.url,
                type: type,
                dataType: "json",
                data: data,
                contentType: "application/json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("DeviceId", hash);
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

    return {
        callService: callService,
        methodType: methodType,
        baseUrl: baseUrl,
        hash: hash
    }
})();