(function () {
    return function () {
        let dashboard = this;

        $(document).ready(function () {
            window.gotoPage = dashboard.gotoPage;

            dashboard.gotoPage("clients",1);
            dashboard.gotoPage("actions",1);
            
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
                url: "/api/Statistics/DeviceUsage",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.loadDeviceUsageChart(data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/Statistics/DailyViews",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.loadDailyViewsChart(data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/Statistics/AverageTimeOnPage",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGrid("averageTimeOnPage", data);
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
            for (var i = 1; i <= totalPages; i++) {
                if (i === currentPage) {
                    paginator += `<a href='#' class="isDisabled">${i}</a>`;
                    continue;
                }
                paginator += `<a href='#' onclick="window.gotoPage('${gridId}',${i})">${i}</a>`;
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

        gotoPage = function (gridId, page) {
            let source = "";
            if (gridId === "actions") {
                source = "/api/Statistics/ActionsPage";
            }
            else if (gridId === "clients") {
                source = "/api/Statistics/ClientsPage";
            } else {
                return;
            }
            dashboard.loadGridPage(source, gridId, page);
        }

        loadDeviceUsageChart = function(data){
            let labels = [];
            let cleanData = [];
            for (let i = 0; i < data.length; i++) {
                labels.push(data[i].device);
                cleanData.push(data[i].count);
            }

            let options = {
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                responsive: false
            };
            let ctx = document.getElementById('deviceUsage').getContext('2d');
            let chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: labels,
                    datasets: [{
                        label: "# of clients",
                        data: cleanData,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: options
            });
        }

        loadDailyViewsChart = function (data) {
            let labels = [];
            let cleanData = [];
            for (let i = 0; i < data.length; i++) {
                labels.push(new Date(data[i].dateTime).toLocaleDateString());
                cleanData.push(data[i].count);
            }
            let options = {
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                responsive: false
            };
            let ctx = document.getElementById('dailyViews').getContext('2d');
            let chart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: "# of clients",
                        data: cleanData,
                        fill: true,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: options
            });
        }
    };
})()();
