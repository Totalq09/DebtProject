$(document).ready(function () {
    var grid = $("#grid-data");
    $("#pesel-for-read").change(function () {
        grid.bootgrid("reload");
    });
    grid.bootgrid({
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
                SortDirection: "",
                PESEL: $("#pesel-for-read").val()
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
                    date.getHours() + ":";
                if (date.getMinutes() < 10) {
                    response.created += "0" + date.getMinutes();
                }
                else {
                    response.created += date.getMinutes();
                }
                if (response.updated !== null && response.updated !== "") {
                    var date_1 = new Date(response.updated);
                    response.updated = date_1.getDate() + '/' + (date_1.getMonth() + 1) + '/' + date_1.getFullYear() + " " +
                        date_1.getHours() + ":";
                    if (date_1.getMinutes() < 10) {
                        response.updated += "0" + date_1.getMinutes();
                    }
                    else {
                        response.updated += date_1.getMinutes();
                    }
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
        navigation: 1,
        labels: {
            noResults: "Brak rezultatów",
            loading: "Ładowanie",
            search: "Szukaj"
        }
    });
});
//# sourceMappingURL=loader-borrower.js.map