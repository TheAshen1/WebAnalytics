(function () {
    return function () {
        let monitor = this;

        $(document).ready(function () {
            let action = {
                ActionType: monitor.ActionType.PageNavigation,
                Url: document.URL,
                FromUrl: document.referrer,
                Platform: platform.name,
                PlatformVersion: platform.version,
                OS: platform.os.family,
                OSVersion: platform.os.version,
                OSArchitecture: platform.os.architecture,
                Device: deviceDetector.device
            };
            monitor.registerAction(action);

            $('.track').on('click', function (e) {
                let description = $(this).attr('data-description');
                let action = {
                    ActionType: monitor.ActionType.Click,
                    Url: document.URL,
                    Description: description,
                    Platform: platform.name,
                    PlatformVersion: platform.version,
                    OS: platform.os.family,
                    OSVersion: platform.os.version,
                    OSArchitecture: platform.os.architecture,
                    Device: deviceDetector.device
                };
                monitor.registerAction(action);
            });
        });

        registerAction = function (action) {
            $.ajax({
                url: "/api/ClientActions",
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