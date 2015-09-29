module Scripts.Components{
        export enum OfficeSearchErrors {
                   CityNotFound= 0,
                   AjaxError= 1,
        };
       export class  OfficeSearchSettings {
                constructor() {

                }
                public OnComplete: any;
                public OnData: any;
                public OnError: any;
         }

        export class OfficeSearchComponent {
            public Settings: OfficeSearchSettings = null;
            public PreviousCity: string = null;
            public PreviousLat: number = null;
            public PreviousLong: number = null;

            constructor(public settings:  OfficeSearchSettings) {
                this.Settings = settings;
            }


            public getOfficesFromAjax(latitude: number, longitude: number, hasSupport: boolean, isOpenInWeekends: boolean)
            {
                    var instance = this;
                    $.ajax({
                        type: "GET",
                        url: "/Office/GetNearestOffices?latitude=" + latitude + "&longitude=" + longitude +
                            "&IsOpenInWeekends=" + isOpenInWeekends + "&isWithSupportDesk=" + hasSupport,
                        dataType: 'json',
                        cache: true,
                        ifModified: false,
                        success: (data, textStatus, jqXHR) => {
                            if (instance.Settings.OnData) {
                                instance.Settings.OnData(data,latitude,longitude);
                            }
                        }
                        , fail:(jqXHR, textStatus, error) => {
                            instance.Settings.OnError(OfficeSearchErrors.AjaxError);
                        }
                        , complete: () => {
                            instance.Settings.OnComplete();
                        }
                    });
            }

            public getNearestOffices(city: string,hasSupport:boolean,isOpenInWeekends:boolean) {
                var instance = this;
                if (instance.PreviousCity === city) {
                    instance.getOfficesFromAjax(instance.PreviousLat, instance.PreviousLong , hasSupport, isOpenInWeekends);
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
                    }
                    else {
                        instance.Settings.OnError(OfficeSearchErrors.CityNotFound);
                        instance.Settings.OnComplete();
                    }
                });
            }
        }
}