(function () {
    return function () {
        let monitor = this;

        $(document).ready(function () {
            let action = {
                ActionType: monitor.ActionType.PageNavigation,
                Url: document.URL,
                FromUrl: document.referrer,
            };
            monitor.registerAction(action);

            $(document).on('click', function (e) {
                var element = this;
                let action = {
                    ActionType: monitor.ActionType.Click,
                    Url: document.URL,
                    DomElementInfo: e.target.outerHTML
                };
                monitor.registerAction(action);
            });

        });

        registerAction = function (action) {
            $.ajax({
                url: "api/ClientActions",
                type: "POST",
                data: JSON.stringify(action),
                contentType: "application/json",
                success: function () {

                },
                error: function (xhr, message, error) {
                    console.error(error);
                }
            });
        }

        ActionType = {
            Click: "Click",
            PageNavigation: "PageNavigation"
        };
    };
})()();