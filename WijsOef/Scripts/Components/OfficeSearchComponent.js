var Scripts;
(function (Scripts) {
    (function (Components) {
        (function (OfficeSearchErrors) {
            OfficeSearchErrors[OfficeSearchErrors["CityNotFound"] = 0] = "CityNotFound";
            OfficeSearchErrors[OfficeSearchErrors["AjaxError"] = 1] = "AjaxError";
        })(Components.OfficeSearchErrors || (Components.OfficeSearchErrors = {}));
        var OfficeSearchErrors = Components.OfficeSearchErrors;
        ;
        var OfficeSearchSettings = (function () {
            function OfficeSearchSettings() {
            }
            return OfficeSearchSettings;
        })();
        Components.OfficeSearchSettings = OfficeSearchSettings;

        var OfficeSearchComponent = (function () {
            function OfficeSearchComponent(settings) {
                this.settings = settings;
                this.Settings = null;
                this.PreviousCity = null;
                this.PreviousLat = null;
                this.PreviousLong = null;
                this.Settings = settings;
            }
            OfficeSearchComponent.prototype.getOfficesFromAjax = function (latitude, longitude, hasSupport, isOpenInWeekends) {
                var instance = this;
                $.ajax({
                    type: "GET",
                    url: "/Office/GetNearestOffices?latitude=" + latitude + "&longitude=" + longitude + "&IsOpenInWeekends=" + isOpenInWeekends + "&isWithSupportDesk=" + hasSupport,
                    dataType: 'json',
                    cache: true,
                    ifModified: false,
                    success: function (data, textStatus, jqXHR) {
                        if (instance.Settings.OnData) {
                            instance.Settings.OnData(data, latitude, longitude);
                        }
                    },
                    fail: function (jqXHR, textStatus, error) {
                        instance.Settings.OnError(1 /* AjaxError */);
                    },
                    complete: function () {
                        instance.Settings.OnComplete();
                    }
                });
            };

            OfficeSearchComponent.prototype.getNearestOffices = function (city, hasSupport, isOpenInWeekends) {
                var instance = this;
                if (instance.PreviousCity === city) {
                    instance.getOfficesFromAjax(instance.PreviousLat, instance.PreviousLong, hasSupport, isOpenInWeekends);
                    return;
                }

                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'address': city }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var point = results[0].geometry.location;
                        var latitude = point.lat();
                        var longitude = point.lng();
                        instance.PreviousCity = city;
                        instance.PreviousLat = latitude;
                        instance.PreviousLong = longitude;
                        instance.getOfficesFromAjax(latitude, longitude, hasSupport, isOpenInWeekends);
                    } else {
                        instance.Settings.OnError(0 /* CityNotFound */);
                        instance.Settings.OnComplete();
                    }
                });
            };
            return OfficeSearchComponent;
        })();
        Components.OfficeSearchComponent = OfficeSearchComponent;
    })(Scripts.Components || (Scripts.Components = {}));
    var Components = Scripts.Components;
})(Scripts || (Scripts = {}));
//# sourceMappingURL=OfficeSearchComponent.js.map
