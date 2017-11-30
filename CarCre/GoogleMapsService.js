
function initGoogleService() {
    var self = {};
    var geocoder = new google.maps.Geocoder();

    var usersCurrentLatLon = null;

    self.askForLocation = function (callback) {
        var geoSuccess = function (position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;
            var latLon = new google.maps.LatLng(lat, lon);
            usersCurrentLatLon = latLon;
            callback();
        };
        var geoError = function (error) {
            console.log(error);
        };
        navigator.geolocation.getCurrentPosition(geoSuccess, geoError);
    };

    self.processZipCode = function (zip, callback) {
        if (zip === undefined || zip === null || zip === "") {
            return;
        }
        geocoder.geocode( {'address': zip }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                lat = results[0].geometry.location.lat();
                lon = results[0].geometry.location.lng(); console.log(results);

                var latLon = new google.maps.LatLng(lat, lon);
                usersCurrentLatLon = latLon;
                callback();
            } else {
                alert("Zip Code was not successful for the following reason: " + status);
            }
        });
    };

    self.displayPosition = function (mapId, lat, lon, title) {
        if (title === undefined) {
            title = "Pin";
        }
        var location = new google.maps.LatLng(lat, lon);
        var map = new google.maps.Map(document.getElementById(mapId), {
            center: location,
            zoom: 15
        });
        new google.maps.Marker({
            map: map,
            title: title,
            position: location
        });
    };

    self.searchAndDisplay = function (mapId, request) {
        if (!self.exists(mapId)) {
            throw "Required map element ID not provided";
        }
        if (!self.exists(request)) {
            request = {};
        }
        if (!self.exists(request.location)) {
            if (self.exists(usersCurrentLatLon)) {
                request.location = usersCurrentLatLon;
            } else {
                alert("No location specified. Please allow location to be used or enter a zip code manually.");
                return;
            }
        }
        if (!self.exists(request.radius)) {
            request.radius = 5000;
        }
        if (!self.exists(request.type)) {
            request.type = ['car_repair'];
        }

        var map = new google.maps.Map(document.getElementById(mapId), {
            center: request.location,
            zoom: 15
        });
        var placesService = new google.maps.places.PlacesService(map);

        placesService.nearbySearch(request, function (results, status) {
            if (status == google.maps.places.PlacesServiceStatus.OK) {
                createMarkers(map, results);
            } else {
                alert(status);
            }
        });
    };

    self.getCurrentLatLon = function () {
        return usersCurrentLatLon;
    };

    self.exists = function (obj) {
        if (obj !== undefined && obj !== null) {
            return true;
        }
        return false;
    };

    var createMarkers = function (map, places) {
        var bounds = new google.maps.LatLngBounds();
        //var placesList = document.getElementById('places');

        for (var i = 0, place; place = places[i]; i++) {
            var image = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                title: place.name,
                position: place.geometry.location
            });

            //placesList.innerHTML += '<li>' + place.name + '</li>';

            bounds.extend(place.geometry.location);
        }
        map.fitBounds(bounds);
    };

    window.GoogleService = self;
};

//init();