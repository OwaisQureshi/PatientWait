//different icons for place.types
//http://stackoverflow.com/questions/11775774/seperate-marker-for-each-location-type-in-google-places-api

var countries = {
    'ca': {
        center: { lat: 62, lng: -110.0 },
        zoom: 3
    }
};

var map, places, infoWindow;
var pnwcurrgoogledata; //Current Google Data
var markers = [];
var autocomplete;
var countryRestrict = { 'country': 'ca' };
var MARKER_PATH = 'https://developers.google.com/maps/documentation/javascript/images/marker_green';
var hostnameRegexp = new RegExp('^https?://.+?/');
var geocoder = new google.maps.Geocoder;

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

    $('#liTimeTab').click(function () {
        alert('Owais');
        //alert(
        //    geocodePlaceId(geocoder, $('#iw-placeid').text())
        //    );
    });

});

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

    var autocomplete1 = new google.maps.places.Autocomplete($("#txtStartLoc")[0], {});
    var autocomplete2 = new google.maps.places.Autocomplete($("#txtDestination")[0], {});
    //var autocomplete3 = new google.maps.places.Autocomplete($("#loc3")[0], {});

    //google.maps.event.addListener(autocomplete1, 'place_changed', function () {
    //    var place = autocomplete1.getPlace();
    //    console.log(place.address_components);
    //});


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
                if (results.length > 0) { pnwcurrgoogledata = results; }
                // Create a marker for each hotel found, and
                // assign a letter of the alphabetic to each marker icon.            
                //results.sort();
                window.res = results;
                results.sort(function (a, b) {
                    var textA = a.name.toUpperCase();
                    var textB = b.name.toUpperCase();
                    return (textA < textB) ? -1 : (textA > textB) ? 1 : 0;
                });
                //console.log("results");
                //console.log(results);

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
            //console.log(markers);
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
                $('#exampleModal').modal('show');
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

        if (place.opening_hours.weekday_text) {
            document.getElementById('iw-workinghours-row').style.display = '';
            var openingHoursText = place.opening_hours.weekday_text;
            openingHoursText = openingHoursText.join('<br/>')
            document.getElementById('iw-workinghours').innerHTML = openingHoursText;
        }
        else {
            document.getElementById('iw-workinghours-row').style.display = 'none';
        }

        if (place.opening_hours.open_now) {
            var opennow = place.opening_hours.open_now;
            //window.alert(opennow);
            if (opennow == 'true') {
                opennow = 'Open';
            }
            else {
                if (opennow == 'false') {
                    opennow = 'Closed';
                }
            }
            document.getElementById('iw-opennow-row').style.display = '';
            document.getElementById('iw-opennow').innerHTML = opennow;//place.opening_hours.open_now;
        }
        else {
            document.getElementById('iw-opennow-row').style.display = 'none';
        }

        if (place.place_id) {
            document.getElementById('iw-placeid-row').style.display = '';
            document.getElementById('iw-placeid').innerHTML = place.place_id;
        }
        else {
            document.getElementById('iw-placeid-row').style.display = 'none';
        }
    }

    //    initAutocomplete();

    //  function initAutocomplete() {
    // Create the autocomplete object, restricting the search to geographical location types.

    //}
}

    function geocodePlaceId(geocoder, placeId) {
        //var placeId = //document.getElementById('place-id').value;
        geocoder.geocode({ 'placeId': placeId }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    //infowindow.setContent(results[0].formatted_address);
                    return results[0].formatted_address;
                }
            }
        });
    }