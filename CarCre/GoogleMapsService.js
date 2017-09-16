
function initGoogleService() {
    var self = {};

    var usersCurrentLatLon = null;

    self.askForLocation = function () {
        var geoSuccess = function (position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;
            var latLon = new google.maps.LatLng(lat, lon);
            usersCurrentLatLon = latLon;
        };
        var geoError = function (error) {
            console.log(error);
        };
        navigator.geolocation.getCurrentPosition(geoSuccess, geoError);
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
                throw "Error searchAndDisplay - no location passed in request or set in the GoogleService";
            }
        }
        if (!self.exists(request.radius)) {
            request.radius = 500;
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