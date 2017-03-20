// This technique is adapted from following resource
//https://whereamirightnow.com/blog/implement-w3c-geolocation-api-javascript/

function geoFindMe() {
    var location = { lat: 0, long: 0 };
    navigator.geolocation.getCurrentPosition(success, error, geo_options);

    function success(position) {//is object literal then success
        var locationArry = [];
        var latitude = position.coords.latitude;
        var longitude = position.coords.longitude;
        var altitude = position.coords.altitude;
        var accuracy = position.coords.accuracy;

        location.lat = latitude;
        location.long = longitude;

        return location;
    }

    function error(error) { //if string then fail
        var err = "Unable to retrieve your location due to " + error.code + " : " + error.message;
        return err;
    };

    var geo_options = {
        enableHighAccuracy: true,
        maximumAge: 30000,
        timeout: 27000
    };
}

