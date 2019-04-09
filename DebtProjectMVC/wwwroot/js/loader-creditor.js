$(document).ready(function () {
    function addRowForNew() {
        var date = new Date();
        var dateS = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + " " +
            date.getHours() + ":";
        if (date.getMinutes() < 10) {
            dateS += "0" + date.getMinutes();
        }
        else {
            dateS += date.getMinutes();
        }
        var rowForNew = $(" \n            <tr id=\"rowForNew\"> \n                <td></td>\n                <td><input id=\"getValue\" type=\"number\" step=\"0.01\"></td>\n                <td><input id=\"getName\" type=\"text\"></td>\n                <td><input id=\"getSurname\" type=\"text\"></td>\n                <td><input id=\"getPESEL\" type=\"number\"></td>\n                <td id=\"dateToUpdate\">" + dateS + "</td>\n                <td></td>\n                <td></td>\n                <td>Otwarty</td>\n                <td><button type=\"button\" class=\"btn btn-xs btn-default command-add\"><span class=\"fa fa-plus\"></span></button></td>\n            </tr> \n            ");
        $("#grid-data-creditor > tbody").prepend(rowForNew);
        setInterval(function () {
            try {
                var date_1 = new Date();
                var dateS_1 = date_1.getDate() + '/' + (date_1.getMonth() + 1) + '/' + date_1.getFullYear() + " " +
                    date_1.getHours() + ":";
                if (date_1.getMinutes() < 10) {
                    dateS_1 += "0" + date_1.getMinutes();
                }
                else {
                    dateS_1 += date_1.getMinutes();
                }
                $("#dateToUpdate").html(dateS_1);
            }
            catch (_a) {
            }
        }, 2000);
    }
    var grid = $("#grid-data-creditor").bootgrid({
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
                    date.getHours() + ":";
                if (date.getMinutes() < 10) {
                    response.created += "0" + date.getMinutes();
                }
                else {
                    response.created += date.getMinutes();
                }
                if (response.updated != null && response.updated != "") {
                    var date_2 = new Date(response.updated);
                    response.updated = date_2.getDate() + '/' + (date_2.getMonth() + 1) + '/' + date_2.getFullYear() + " " +
                        date_2.getHours() + ":";
                    if (date_2.getMinutes() < 10) {
                        response.updated += "0" + date_2.getMinutes();
                    }
                    else {
                        response.updated += date_2.getMinutes();
                    }
                }
            });
            return response;
        },
        formatters: {
            "commands": function (column, row) {
                var id = row["id"];
                return "<button type=\"button\" class=\"btn btn-xs btn-default command-edit\" data-row-id=\"" + id + "\" + data-row-inner-id=\"" + row.id + "\"><span class=\"fa fa-pencil\"></span></button> " +
                    "<button type=\"button\" class=\"btn btn-xs btn-default command-delete\" data-row-id=\"" + id + "\"><span class=\"fa fa-trash-o\"></span></button>";
            }
        },
        ajaxSettings: {
            method: "POST",
            contentType: "application/json"
        },
        url: "/api/for-creditor",
        sorting: true,
        multiSort: false,
        selection: true,
        navigation: 1,
        labels: {
            noResults: "Brak rezultatów",
            loading: "Ładowanie",
            search: "Szukaj"
        }
    }).on("loaded.rs.jquery.bootgrid", function (e) {
        addRowForNew();
        grid.find(".command-add").on("click", function (e) {
            var data = {
                value: $("#getValue").val(),
                name: $("#getName").val(),
                surname: $("#getSurname").val(),
                PESEL: $("#getPESEL").val()
            };
            $.ajax({
                url: "/api/post-new-debt",
                method: "POST",
                dataType: "json",
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function () {
                    grid.bootgrid("reload");
                },
                complete: function () {
                    grid.bootgrid("reload");
                }
            });
        });
        grid.find(".command-delete").on("click", function (e) {
            var data = {
                id: $(this).data("row-id")
            };
            $.ajax({
                url: "/api/delete-debt",
                method: "POST",
                dataType: "json",
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function () {
                    grid.bootgrid("reload");
                },
                complete: function () {
                    grid.bootgrid("reload");
                }
            });
        });
        grid.find(".command-edit").on("click", function (e) {
            handleEdit(e, $(this).data("row-inner-id"), $(this));
        });
    });
    function handleEdit(e, innerId, el) {
        var date = new Date();
        var dateS = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + " " +
            date.getHours() + ":";
        if (date.getMinutes() < 10) {
            dateS += "0" + date.getMinutes();
        }
        else {
            dateS += date.getMinutes();
        }
        setInterval(function () {
            try {
                var date_3 = new Date();
                var dateS_2 = date_3.getDate() + '/' + (date_3.getMonth() + 1) + '/' + date_3.getFullYear() + " " +
                    date_3.getHours() + ":";
                if (date_3.getMinutes() < 10) {
                    dateS_2 += "0" + date_3.getMinutes();
                }
                else {
                    dateS_2 += date_3.getMinutes();
                }
                $("#dateToUpdate").html(dateS_2);
            }
            catch (_a) {
            }
        }, 2000);
        var row = $("#grid-data-creditor tr:has(td button[data-row-id=" + innerId + "])")[0];
        var children = row.children;
        children.item(1).innerHTML = "<td><input id=\"getCurrentValue\" type=\"number\" step=\"0.01\" value=\"" + children.item(1).innerHTML + "\"></td>";
        children.item(2).innerHTML = "<td><input id=\"getCurrentName\" type=\"text\" value=\"" + children.item(2).innerHTML + "\"></td>";
        children.item(3).innerHTML = "<td><input id=\"getCurrentSurname\" type=\"text\" value=\"" + children.item(3).innerHTML + "\"></td>";
        children.item(4).innerHTML = "<td><input id=\"getCurrentPESEL\" type=\"number\" value=\"" + children.item(4).innerHTML + "\"></td>";
        children.item(6).innerHTML = "<td><td id=\"dateToUpdate\">" + dateS + "</td></td>";
        children.item(7).innerHTML = "<td><input id=\"getCurrentPartValue\" type=\"number\" step=\"0.01\" value=\"" + children.item(7).innerHTML + "\"></td>";
        var confirmButton = $("<button type=\"button\" class=\"btn btn-xs btn-default command-check\"><span class=\"fa fa-check\"></span></button> ");
        $(confirmButton).click(function () {
            var data = {
                id: innerId,
                value: $("#getCurrentValue").val(),
                name: $("#getCurrentName").val(),
                surname: $("#getCurrentSurname").val(),
                PESEL: $("#getCurrentPESEL").val(),
                returnedValue: $("#getCurrentPartValue").val()
            };
            $.ajax({
                url: "/api/update-debt",
                method: "POST",
                dataType: "json",
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function () {
                    grid.bootgrid("reload");
                    $(confirmButton).detach();
                    $(el).show();
                },
                complete: function () {
                    grid.bootgrid("reload");
                    $(confirmButton).detach();
                    $(el).show();
                }
            });
        });
        confirmButton.insertAfter($(el));
        $(el).hide();
    }
});
//# sourceMappingURL=loader-creditor.js.map