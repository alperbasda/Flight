
var airSearchEvents = {
    search: function () {
        if (!globalEvents.showErrorIfFormContainsRequiredEmptyElementReturnFalse($('form'))) {
            return;
        }
        airSearchEvents.showLoader();
        airSearchEvents.hideResultWrapper();
        var req = globalEvents.formToJSON($('form'));

        req["[1].Destination"] = req["[0].Origin"];
        req["[0].GroupId"] = 0;
        req["[1].GroupId"] = 1;
        req["[1].Origin"] = req["[0].Destination"];
        var requestData = [
            {
                Destination: req["[0].Destination"],
                Origin: req["[0].Origin"],
                DepartureDate: req["[0].DepartureDate"],
                GroupId: 0,
            },
            {
                Destination: req["[1].Destination"],
                Origin: req["[1].Origin"],
                DepartureDate: req["[1].DepartureDate"],
                GroupId: 1,
            }];

        globalEvents.ajaxPost(Constants.baseApiUrl + Constants.airSearchApiUrl + 'list', requestData, function (data) {
            if (data.statusCode != 200) {
                alert(errors);
            }
            else {
                airSearchEvents.fillResult(data.data);
            }
            airSearchEvents.hideLoader();
            airSearchEvents.showResultWrapper();
        });

    },
    fillResult: function (data) {
        $('div[data-group="0"]').children().remove();
        $('div[data-group="1"]').children().remove();
        $('#flightDetail').children().remove();
        $.each(data, function (index, item) {
            $('div[data-group="' + item.groupId + '"]').append('<button class="btn btn-primary m-2" data-group="' + item.groupId + '" data-v="' + item.flightNumber + '">' + item.flightNumber + '</button>');
        });


    },
    detail: function (clickedButton) {
        var _this = clickedButton;
        var group = $(_this).data("group");
        var req = {
            flightNumber: $(_this).data("v"),
            departureDate: $('[name="[' + group + '].DepartureDate"]').val(),
            destination: $('[name="[' + group + '].Destination"]').val(),
            origin: $('[name="[' + group + '].Origin"]').val()
        }
        globalEvents.ajaxPost(Constants.baseApiUrl + Constants.airSearchApiUrl + 'detail', req, function (data) {
            if (data.statusCode != 200) {
                alert(errors);
            }
            else {
                $('#flightDetail').children().remove();
                $('#flightDetail').append('<h5>Uçuş Numarası : ' + data.data.flightNumber + ' </h5>');
                $('#flightDetail').append('<h5>Kalkış : ' + data.data.arrivalDateTime + ' </h5>');
                $('#flightDetail').append('<h5>Varış : ' + data.data.departureDateTime + ' </h5>');
                $('#flightDetail').append('<h5>Bilet Fiyatı : ' + data.data.price + ' TL </h5>');
                
            }

        });
    },
    showLoader: function () {
        $("#loader").show();
    },
    hideLoader: function () {
        $("#loader").hide();
    },
    showResultWrapper: function () {
        $("#results").show();
    },
    hideResultWrapper: function () {
        $("#results").hide();
    }
}

$('#search_btn').click(function (e) {
    e.preventDefault();
    e.stopPropagation();
    airSearchEvents.search();
});

$(document).on('click', '[data-v]', function () {
    airSearchEvents.detail($(this));
});

$('[name="[0].DepartureDate"]').change(function () {
    var newValue = $(this).val();
    $('[name="[1].DepartureDate"]').attr('min', newValue);
});

$('[name="[1].DepartureDate"]').change(function () {
    var newValue = $(this).val();
    $('[name="[0].DepartureDate"]').attr('max', newValue);
});

$('[data-dynamic-for]').select2().on('change', function () {
    var selectedOption = $(this).val();
    
    $('[name="' + $(this).attr('data-dynamic-for') + '"]').val(selectedOption);
});