(function () {
    return function () {
        let dashboard = this;

        $(document).ready(function () {
            window.gotoPage = dashboard.gotoPage;

            dashboard.gotoPage(1);

            $.ajax({
                url: "/api/Statistics/PageViews",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGrid("pageViews", data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/Statistics/Clicks",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGrid("clicks", data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/Statistics/Realtime",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGrid("onlineClients", data.onlineClients);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/Statistics/Total",
                type: "GET",
                success: function (data) {
                    if (data) {
                        $("#totalUniqueUsersCount").text(data.totalUniqueUsersCount);
                        $("#totalPageViewsCount").text(data.totalPageViewsCount);
                        $("#totalClicksCount").text(data.totalClicksCount);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/Statistics/Clients",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGrid("clients", data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/Statistics/Clients",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGrid("clients", data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });
            
            $.ajax({
                url: "/api/Statistics/DeviceUsage",
                type: "GET",
                success: function (data) {
                    if (data) {
                        let labels = [];
                        let cleanData = [];
                        for (let key in data) {
                            labels.push(key);
                            cleanData.push(data[key]);
                        }
                        var ctx = document.getElementById('deviceUsage').getContext('2d');
                        var myDoughnutChart = new Chart(ctx, {
                            type: 'doughnut',
                            data: {
                                dataset: [{
                                    data: cleanData
                                }]
                            },
                            options: Chart.defaults.doughnut
                        });
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });            
        });

        buildGrid = function (gridId, data) {
            if (data.length == 0) {
                return;
            }
            let header = "<tr>";
            for (var key in data[0]) {
                header += `<th scope="col">${key}</th>`;
            }
            header += "</tr>";
            $(`#${gridId} thead`).html(header);
            let body = "";
            for (var i = 0; i < data.length; i++) {
                body += `<tr>`;
                for (var key in data[i]) {
                    body += `<td>${data[i][key]}</td>`;
                }
                body += "</tr>";
            }
            $(`#${gridId} tbody`).html(body);
        }

        buildPaginator = function (gridId, currentPage, totalPages) {
            let paginator = "";
            //paginator += `<a href='#' onclick="">First page</a>`;
            //paginator += `<a href='#' onclick="">Prev</a>`;
            for (var i = 1; i < totalPages; i++) {
                if (i === currentPage) {
                    paginator += `<a href='#' class="isDisabled">${i}</a>`;
                    continue;
                }
                paginator += `<a href='#' onclick="window.gotoPage(${i})">${i}</a>`;
            }
            //paginator += `<a href='#' onclick="">Next</a>`;
            //paginator += `<a href='#' onclick="">Last page</a>`;
            $(`#${gridId} + .paginator`).html(paginator);
        }

        loadGridPage = function (sourceUrl, gridId, page) {
            $.ajax({
                url: `${sourceUrl}/${page}`,
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGrid(gridId, data.results);
                        dashboard.buildPaginator(gridId, page, data.pageCount);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });
        }

        gotoPage = function (page) {
            let sourseUrl = "/api/Statistics/ClientActionsPage";
            let gridId = "allActions";
            dashboard.loadGridPage(sourseUrl, gridId, page);
        }
    };
})()();
