$(document).ready(function () {
    $("#grid-data").bootgrid({
        ajax: true,
        post: function () {
            /* To accumulate custom parameter with the request object */
            return {
                id: "b0df282a-0d67-40e5-8558-c9e93b7befed"
            };
        },
        requestHandler: function (request) {
            var model = {
                Current: 1,
                RowCount: 1,
                Search: "",
                SortBy: "",
                SortDirection: ""
            };
            model.Current = request.current;
            model.RowCount = request.rowCount;
            model.Search = request.searchPhrase;
            for (var key in request.sort) {
                model.SortBy = key;
                model.SortDirection = request.sort[key];
            }
            return JSON.stringify(model);
        },
        responseHandler: function (response) {
            $.each(response.rows, function (index, response) {
                var date = new Date(response.created);
                response.created = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + " " +
                    date.getHours() + ":" + date.getMinutes();
                if (response.updated != null && response.updated != "") {
                    var date_1 = new Date(response.updated);
                    response.updated = date_1.getDate() + '/' + (date_1.getMonth() + 1) + '/' + date_1.getFullYear() + " " +
                        date_1.getHours() + ":" + date_1.getMinutes();
                }
            });
            return response;
        },
        ajaxSettings: {
            method: "POST",
            contentType: "application/json"
        },
        url: "/api/for-borrower",
        sorting: true,
        multiSort: false,
        selection: true,
        navigation: 1
    });
});
//# sourceMappingURL=loader.js.map