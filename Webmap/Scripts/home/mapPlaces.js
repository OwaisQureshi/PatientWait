$(document).ready(function () {
    //initMap();
    $("#btnClinicsInfo").click(function () {
        $("#clinicslist").show();
    });
    $("#btnDoctorsInfo").click(function () {
        $("#clinicslist").hide();
        $("#doctorlist").show();
        $("#clinicdetails").show();
    });
});

//different icons for place.types
//http://stackoverflow.com/questions/11775774/seperate-marker-for-each-location-type-in-google-places-api

var countries = {
    'ca': {
        center: { lat: 62, lng: -110.0 },
        zoom: 3
    }
};

var map, places, infoWindow;
var markers = [];
var autocomplete;
var countryRestrict = { 'country': 'ca' };
var MARKER_PATH = 'https://developers.google.com/maps/documentation/javascript/images/marker_green';
var hostnameRegexp = new RegExp('^https?://.+?/');

function initMap() {
    map = new google.maps.Map(document.getElementById('map_canvas'),
        {
            zoom: countries['ca'].zoom,//3
            center: countries['ca'].center,
            mapTypeControl: false,
            panControl: false,
            zoomControl: false,
            streetViewControl: false,
            mapTypeId: 'roadmap'
        });

    //get the infowindows ui
    infoWindow = new google.maps.InfoWindow({
        content: document.getElementById('info-content')
    });

    autocomplete = new google.maps.places.Autocomplete(
    /** @type {!HTMLInputElement} */(
        document.getElementById('txtSearch')), {
            types: ['(cities)'],
            componentRestrictions: countryRestrict
        });
    places = new google.maps.places.PlacesService(map);
    autocomplete.addListener('place_changed', onPlaceChanged);

    function onPlaceChanged() {
        var place = autocomplete.getPlace();
        if (place.geometry) {
            map.panTo(place.geometry.location);
            map.setZoom(15);
            search();
        } else {
            document.getElementById('txtSearch').placeholder = 'Search By City';
        }
    }

    // Search for hotels in the selected city, within the viewport of the map.
    function search() {
        var search = {
            bounds: map.getBounds(),
            //get them from checkboxes
            types: ['hospital', 'pharmacy', 'dentist', 'physiotherapist', 'doctor', 'health']
        };

        places.nearbySearch(search, function (results, status) {
            if (status === google.maps.places.PlacesServiceStatus.OK) {
                clearResults();
                clearMarkers();
                // Create a marker for each hotel found, and
                // assign a letter of the alphabetic to each marker icon.
                for (var i = 0; i < results.length; i++) {
                    var markerLetter = String.fromCharCode('A'.charCodeAt(0) + (i % 26));
                    var markerIcon = MARKER_PATH + markerLetter + '.png';
                    // Use marker animation to drop the icons incrementally on the map.
                    markers[i] = new google.maps.Marker({
                        position: results[i].geometry.location,
                        animation: google.maps.Animation.DROP,
                        icon: markerIcon
                    });
                    // If the user clicks a hotel marker, show the details of that hotel
                    // in an info window.
                    markers[i].placeResult = results[i];
                    google.maps.event.addListener(markers[i], 'click', showInfoWindow);
                    setTimeout(dropMarker(i), i * 100);
                    addResult(results[i], i);
                }
            }
        });
    }

    function clearMarkers() {
        for (var i = 0; i < markers.length; i++) {
            if (markers[i]) {
                markers[i].setMap(null);
            }
        }
        markers = [];
    }

    function dropMarker(i) {
        return function () {
            markers[i].setMap(map);
        };
    }

    function addResult(result, i) {
        var results = document.getElementById('results');
        var markerLetter = String.fromCharCode('A'.charCodeAt(0) + (i % 26));
        var markerIcon = MARKER_PATH + markerLetter + '.png';

        var tr = document.createElement('tr');
        tr.style.backgroundColor = (i % 2 === 0 ? '#F0F0F0' : '#FFFFFF');
        tr.onclick = function () {
            google.maps.event.trigger(markers[i], 'click');
        };

        var iconTd = document.createElement('td');
        var nameTd = document.createElement('td');
        var icon = document.createElement('img');
        icon.src = markerIcon;
        icon.setAttribute('class', 'placeIcon');
        icon.setAttribute('className', 'placeIcon');
        var name = document.createTextNode(result.name);
        iconTd.appendChild(icon);
        nameTd.appendChild(name);
        tr.appendChild(iconTd);
        tr.appendChild(nameTd);
        results.appendChild(tr);
    }

    function clearResults() {
        var results = document.getElementById('results');
        while (results.childNodes[0]) {
            results.removeChild(results.childNodes[0]);
        }
    }

    // Get the place details for a hotel. Show the information in an info window,
    // anchored on the marker for the hotel that the user selected.
    function showInfoWindow() {
        var marker = this;
        places.getDetails({ placeId: marker.placeResult.place_id },
            function (place, status) {
                if (status !== google.maps.places.PlacesServiceStatus.OK) {
                    return;
                }
                infoWindow.open(map, marker);
                buildIWContent(place);
            });
    }

    // Load the place information into the HTML elements used by the info window.
    function buildIWContent(place) {
        console.log("Place object print");
        console.log(place);
        document.getElementById('iw-icon').innerHTML = '<img class="hotelIcon" ' +
            'src="' + place.icon + '"/>';
        document.getElementById('iw-url').innerHTML = '<b><a href="' + place.url +
            '">' + place.name + '</a></b>';
        document.getElementById('iw-address').textContent = place.vicinity;

        if (place.formatted_phone_number) {
            document.getElementById('iw-phone-row').style.display = '';
            document.getElementById('iw-phone').textContent =
                place.formatted_phone_number;
        } else {
            document.getElementById('iw-phone-row').style.display = 'none';
        }

        // Assign a five-star rating to the hotel, using a black star ('&#10029;')
        // to indicate the rating the hotel has earned, and a white star ('&#10025;')
        // for the rating points not achieved.
        if (place.rating) {
            var ratingHtml = '';
            for (var i = 0; i < 5; i++) {
                if (place.rating < (i + 0.5)) {
                    ratingHtml += '&#10025;';
                } else {
                    ratingHtml += '&#10029;';
                }
                document.getElementById('iw-rating-row').style.display = '';
                document.getElementById('iw-rating').innerHTML = ratingHtml;
            }
        } else {
            document.getElementById('iw-rating-row').style.display = 'none';
        }

        // The regexp isolates the first part of the URL (domain plus subdomain)
        // to give a short URL for displaying in the info window.
        if (place.website) {
            var fullUrl = place.website;
            var website = hostnameRegexp.exec(place.website);
            if (website === null) {
                website = 'http://' + place.website + '/';
                fullUrl = website;
            }
            document.getElementById('iw-website-row').style.display = '';
            document.getElementById('iw-website').textContent = website;
        } else {
            document.getElementById('iw-website-row').style.display = 'none';
        }
    }

    // Create the search box and link it to the UI element.
    //var input = $('#txtSearch').get()[0];

    //var searchBox = new google.maps.places.SearchBox(input);//search box assigned
    ////map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);//search box attached on the map

    //// Bias the SearchBox results towards current map's viewport.
    //map.addListener('bounds_changed', function () {
    //    searchBox.setBounds(map.getBounds());
    //});

    //var markers = [];
    //// Listen for the event fired when the user selects a prediction and retrieve
    //// more details for that place.
    //searchBox.addListener('places_changed', function () {
    //    var places = searchBox.getPlaces();
    //    if (places.length == 0) {
    //        return;
    //    }

    //    // Clear out the old markers.
    //    markers.forEach(function (marker) {
    //        marker.setMap(null);
    //    });
    //    markers = [];

    //    // For each place, get the icon, name and location.
    //    var bounds = new google.maps.LatLngBounds();
    //    places.forEach(function (place) {
    //        if (!place.geometry) {
    //            console.log("Returned place contains no geometry");
    //            return;
    //        }
    //        var icon = {
    //            url: place.icon,
    //            size: new google.maps.Size(71, 71),
    //            origin: new google.maps.Point(0, 0),
    //            anchor: new google.maps.Point(17, 34),
    //            scaledSize: new google.maps.Size(25, 25)
    //        };

    //        // Create a marker for each place.
    //        markers.push(new google.maps.Marker({
    //            map: map,
    //            icon: icon,
    //            title: place.name,
    //            position: place.geometry.location
    //        }));

    //        if (place.geometry.viewport) {
    //            // Only geocodes have viewport.
    //            bounds.union(place.geometry.viewport);
    //        } else {
    //            bounds.extend(place.geometry.location);
    //        }
    //    });
    //    map.fitBounds(bounds);
    //});
}