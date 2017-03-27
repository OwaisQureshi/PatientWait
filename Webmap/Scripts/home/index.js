//for autocomplete by google location use
//https://ubilabs.github.io/geocomplete/

$(document).ready(function () {
    stringFormat();
    //var apiKey = 'AIzaSyDttQ9SLD0V_2CM68YksJ18todsgtJn_i4';   

    //multiple routes ?
    //display routes on map ?
    $('#btnCalculate').click(function () {
        //send request to google api
        var units = 'imperial';
        var origins = $('#txtStartLoc').val();
        var dest = $('#txtDestination').val();

        $.ajax({
            url: getDistanceURL,
            type: "POST",
            data: { units: units, origin: origins, dest: dest },
            dataType: "json",
            success: function (result) {
                if (result) {
                    result = JSON.parse(result);
                    var htmlStr = '<td>' + result.rows[0].elements[0].duration.text + '</td>' + '<td>' + result.rows[0].elements[0].distance.text + '</td>'
                    $('#tblBody').html(htmlStr);
                }
                else {
                    console.log('In btnCalculate.click default');
                    console.log(result);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });

    });
});

function stringFormat() {
    if (!String.format) {
        String.format = function (format) {
            var args = Array.prototype.slice.call(arguments, 1);
            return format.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                  ? args[number]
                  : match
                ;
            });
        };
    }
}