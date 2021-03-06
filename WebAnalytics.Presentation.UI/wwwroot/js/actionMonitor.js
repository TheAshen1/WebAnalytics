﻿(function () {
    return function () {
        let monitor = this;

        $(document).ready(function () {
            let action = {
                ActionType: monitor.ActionType.PageNavigation,
                Url: document.URL,
                FromUrl: document.referrer,
            };
            monitor.registerAction(action);

            $('.track').on('click', function (e) {
                let description = $(this).attr('data-description');
                let action = {
                    ActionType: monitor.ActionType.Click,
                    Url: document.URL,
                    Description: description,
                };
                monitor.registerAction(action);
            });
        });

        registerAction = function (action) {
            $.ajax({
                url: "/api/Statistics",
                type: "POST",
                data: JSON.stringify(action),
                contentType: "application/json",
                success: function () {

                },
                error: function (e) {
                    console.error(e);
                }
            });
        }

        ActionType = {
            Click: "Click",
            PageNavigation: "PageNavigation"
        };
    };
})()();