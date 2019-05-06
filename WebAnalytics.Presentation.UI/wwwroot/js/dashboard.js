(function () {
    return function () {

        let dashboard = this;

        $(document).ready(function () {
            $.ajax({
                url: "/api/ClientActions",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildGridAllActions(data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/ClientActions/PageViewStatistics",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildPageViewsGrid(data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

            $.ajax({
                url: "/api/ClientActions/ClickStatistics",
                type: "GET",
                success: function (data) {
                    if (data) {
                        dashboard.buildClickStatisticsGrid(data);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });
        });

        buildPageViewsGrid = function (data) {
            $("#pageViewsGrid").jsGrid({
                width: "auto",
                height: "600px",

                sorting: true,
                paging: true,

                data: data,

                fields: [
                    { name: "url", type: "string", width: "300px" },
                    { name: "count", type: "string", width: "150px" }
                ]
            });
        }
        
        buildClickStatisticsGrid = function (data) {
            $("#clickStatisticsGrid").jsGrid({
                width: "auto",
                height: "400px",

                sorting: true,
                paging: true,

                data: data,

                fields: [
                    { name: "description", type: "string", width: "300px" },
                    { name: "count", type: "string", width: "150px" }
                ]
            });
        }

        buildGridAllActions = function (data) {
            $("#allActionsGrid").jsGrid({
                width: "auto",
                height: "400px",

                sorting: true,
                paging: true,

                data: data,

                fields: [
                    { name: "id", type: "string", width: "200px" },
                    { name: "actionType", type: "string", width: "150px" },
                    { name: "url", type: "string", width: "400px" },
                    { name: "fromUrl", type: "string", width: "400px" },
                    { name: "dateTime", type: "string", width: "200px" },
                    { name: "description", type: "string", width: "400px" },
                    { name: "platform", type: "string" },
                    { name: "platformVersion", type: "string", width: "150px" },
                    { name: "os", type: "string" },
                    { name: "osVersion", type: "string" },
                    { name: "osArchitecture", type: "string", width: "150px" },
                    { name: "product", type: "string" },
                ]
            });
        }
    };
})()();
