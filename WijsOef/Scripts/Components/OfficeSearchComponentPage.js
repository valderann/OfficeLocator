var Scripts;
(function (Scripts) {
    (function (Components) {
        var OfficeSearchComponentPage = (function () {
            function OfficeSearchComponentPage() {
                this.initialize();
            }
            OfficeSearchComponentPage.prototype.initialize = function () {
                var instance = this;
                instance.officeSearchComponent = new Scripts.Components.OfficeSearchComponent({
                    OnData: function (data, lat, long) {
                        if (data && data.Result && data.Result.length > 0) {
                            $("#OfficeTemplate").show();
                            OfficeSearchViewModel["Offices"](data.Result);
                            instance.drawMap(lat, long);
                        } else {
                            $("#officeErrors>div").hide();
                            $("#officeErrors").show();
                            $("#OfficeErrorNoResults").show();
                        }
                    },
                    OnComplete: function () {
                        $("#txtOfficeSearch").removeClass("loader-dropdown");
                        $("#txtOfficeSearch").prop("disabled", false);
                        if (window.history) {
                            window.history.replaceState(null, null, "?city=" + encodeURIComponent($("#txtOfficeSearch").val()) + "&HasSupportDesk=" + $("#chkHasSupportDesk").is(':checked') + "&IsOpenWeekend=" + $("#chkIsOpenWeekend").is(':checked'));
                        }
                    },
                    OnError: function (error) {
                        $("#officeErrors>div").hide();
                        $("#officeErrors").show();
                        if (error === 0 /* CityNotFound */) {
                            $("#OfficeErrorCityNotFound").show();
                        } else if (error === 1 /* AjaxError */) {
                            $("#OfficeErrorConnection").show();
                        }
                    }
                });

                ko.applyBindings(OfficeSearchViewModel, document.getElementById("OfficeTemplate"));

                $("#txtOfficeSearch").keypress(function (e) {
                    if (e.which === 13) {
                        instance.search();
                    }
                });

                $("#chkIsOpenWeekend,#chkHasSupportDesk").click(function () {
                    instance.search();
                });
            };

            OfficeSearchComponentPage.prototype.search = function () {
                $("#txtOfficeSearch").addClass("loader-dropdown");
                $("#txtOfficeSearch").prop("disabled", true);
                $("#OfficeTemplate").hide();
                $("#officeErrors").hide();
                var searchVal = $("#txtOfficeSearch").val();
                if (searchVal !== "") {
                    this.officeSearchComponent.getNearestOffices(searchVal, $("#chkHasSupportDesk").is(':checked'), $("#chkIsOpenWeekend").is(':checked'));
                }
            };

            OfficeSearchComponentPage.prototype.drawMap = function (lat, long) {
                var instance = this;
                var mapOptions = { zoom: 8 };
                instance.gmapOffices = new google.maps.Map(document.getElementById("gmap"), mapOptions);
                instance.gmapOffices.setCenter(new google.maps.LatLng(lat, long));
                var lIndex = 0;
                var markets = [];
                $(".office").each(function () {
                    var strLat = $(this).attr("data-lat");
                    var strLong = $(this).attr("data-long");
                    if (strLong !== "" && strLat !== "") {
                        var myLatlng = new google.maps.LatLng(parseFloat(strLat), parseFloat(strLong));

                        var lAddr = $(this).find(".offCity").html();
                        var lCity = $(this).find(".offStreet").html();

                        var contentString = '<div style="line-height:1.35;overflow:hidden;white-space:nowrap;"><b>' + lAddr + '<br/>' + lCity + '<br/>';
                        var infowindow = new google.maps.InfoWindow({
                            content: contentString
                        });

                        var marker = new google.maps.Marker({
                            position: myLatlng,
                            map: instance.gmapOffices,
                            value: lIndex,
                            title: lAddr
                        });
                        markets.push(marker);

                        var prevInfowindow;
                        google.maps.event.addListener(marker, 'click', function () {
                            if (prevInfowindow) {
                                prevInfowindow.close();
                            }
                            prevInfowindow = infowindow;
                            infowindow.open(instance.gmapOffices, marker);
                        });
                        lIndex += 1;
                    }
                });
            };
            return OfficeSearchComponentPage;
        })();
        Components.OfficeSearchComponentPage = OfficeSearchComponentPage;
    })(Scripts.Components || (Scripts.Components = {}));
    var Components = Scripts.Components;
})(Scripts || (Scripts = {}));

var OfficeSearchViewModel = {
    Offices: ko.observable(),
    ShowOfficesList: ko.observable(),
    NoResults: ko.observable(),
    NoCityFound: ko.observable()
};

$(document).ready(function () {
    var officeComponentPage = new Scripts.Components.OfficeSearchComponentPage();
    if ($("#txtOfficeSearch").val() !== "") {
        officeComponentPage.search();
    }
});
//# sourceMappingURL=OfficeSearchComponentPage.js.map
