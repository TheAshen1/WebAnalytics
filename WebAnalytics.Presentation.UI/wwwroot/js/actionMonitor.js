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
                Product: platform.product
            };
            monitor.registerAction(action);

            $('track').on('click', function (e) {
                let description = $(this).prop('data-description');
                let action = {
                    ActionType: monitor.ActionType.Click,
                    Url: document.URL,
                    Description: description,
                    Platform: platform.name,
                    PlatformVersion: platform.version,
                    OS: platform.os.family,
                    OSVersion: platform.os.version,
                    OSArchitecture: platform.os.architecture,
                    Product: platform.product
                };
                monitor.registerAction(action);
            });
            // Maybe later
            //navigator.geolocation.getCurrentPosition(positionSuccess, positionError)

            //function positionSuccess(geo) {
            //    document.cookie = 'latitude=' + geo.coords.latitude;
            //    document.cookie = 'longitude=' + geo.coords.longitude;
            //    document.cookie = 'accuracy=' + geo.coords.accuracy;
            //}

            //function positionError(geo) {
            //    document.cookie = 'latitude=; expires=Thu, 01 Jan 1970 00:00:00 UTC;';
            //    document.cookie = 'longitude=; expires=Thu, 01 Jan 1970 00:00:00 UTC;';
            //    document.cookie = 'accuracy=; expires=Thu, 01 Jan 1970 00:00:00 UTC;';
            //}
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