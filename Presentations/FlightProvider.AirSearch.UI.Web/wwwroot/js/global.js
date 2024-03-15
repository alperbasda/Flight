var globalEvents = {
    formToJSON: function ($form) {
        var unindexed_array = $form.serializeArray();
        var indexed_array = {};

        $.map(unindexed_array, function (n, i) {
            indexed_array[n['name']] = n['value'];
        });

        return indexed_array;
    },
    JSONToform: function (form, obj) {
        for (var prop in obj) {
            $(form).find('[name="' + prop + '"]').val(obj[prop]);
        }
    },
    ajaxGet: function (route, params, callback) {
        $.ajax({
            url: route,
            type: 'GET',
            data: params,
            success: function (data) {
                if (data.errors != undefined) {
                    alert(data.errors);
                    return;
                }
                if (callback)
                    callback(data);
            },
            fail: function (ex) {
                alert("İşlem Sırasında Hata Oluştu");
            },
            error: function (ex) {
                alert("İşlem Sırasında Hata Oluştu");
            },
            complete: function () {

            }
        });
    },
    ajaxPost: function (route, params, callback) {
        $.ajax({
            contentType: 'application/json',
            url: route,
            type: 'POST',
            data: JSON.stringify(params),
            success: function (data) {
                if (data.errors != undefined) {
                    alert(data.errors);
                    return;
                }
                if (callback)
                    callback(data);
            },
            fail: function (ex) {
                alert("İşlem Sırasında Hata Oluştu");
                console.log(ex);
            },
            error: function (ex) {
                alert("İşlem Sırasında Hata Oluştu");
                console.log(ex);
            },
            complete: function () {

            }
        });
    },
    setDynamicDropdowns: function () {
        $('[data-dynamic-for]').each(function (index, item) {
            var url = $(item).attr('data-dynamic-for');
            if (url && url.length > 0) {

                $(item).select2({

                    //if item has parent
                    dropdownParent: $(item).attr('data-parent'),

                    ajax: {
                        url: Constants.baseApiUrl + Constants.airportSearchApiUrl +'search',
                        dataType: 'json',
                        data: function (params) {
                            var query = {
                                term: params.term,
                            };

                            // Query parameters will be ?searchterm=[term]
                            return query;
                        },
                        // Additional AJAX parameters go here; see the end of this chapter for the full code of this example
                        processResults: function (data) {
                            // Transforms the top-level key of the response object from 'items' to 'results'
                            return {

                                results: data.data.airports.map(function (value, label) {


                                    return {

                                        "id": value.name,
                                        "text": value.name

                                    };


                                })
                            };
                        },
                        delay: 250,
                    },
                    placeholder: 'Veri Seçin',
                    minimumInputLength: $(item).attr('data-min-length') != undefined
                        ? $(item).attr('data-min-length')
                        : 2,
                    templateSelection: formatState,
                    allowClear: true,
                    matcher: matchStart,
                    tags: true


                });






            }
        });

    },
    showErrorIfFormContainsRequiredEmptyElementReturnFalse: function (form) {
        var inputs = $(form).find('[data-required="true"]');
        
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].value.trim() === '') {
                alert("Lütfen Tüm Alanları Doldurun.");
                return false;
            }
        }
        return true;
    }
}
function formatState(state) {
    if (state.text.trim() != "") {
        return state.text;
    }

    var id = state.id.toUpperCase();
    var querySelector = document.querySelector('[data-fill-ref="' + id + '"]');
    var controller = "";
    if (querySelector != null) {
        controller = querySelector.dataset.fillController;
        var $state = $(
            '<span data-fill-controller="' + controller + '" data-fill-ref="' + id + '"> </span>'
        );
        pageEvents.setDynamicReferences();

    }

    // Use .text() instead of HTML string concatenation to avoid script injection issues
    return $state;
};


function matchStart(params, data) {
    // If there are no search terms, return all of the data
    if ($.trim(params.term) === '') {
        return data;
    }

    // Skip if there is no 'children' property
    if (typeof data.children === 'undefined') {
        return null;
    }

    // `data.children` contains the actual options that we are matching against
    var filteredChildren = [];
    $.each(data.children, function (idx, child) {
        if (child.text.toUpperCase().indexOf(params.term.toUpperCase()) == 0) {
            filteredChildren.push(child);
        }
    });

    // If we matched any of the timezone group's children, then set the matched children on the group
    // and return the group object
    if (filteredChildren.length) {
        var modifiedData = $.extend({}, data, true);
        modifiedData.children = filteredChildren;

        // You can return modified objects from here
        // This includes matching the `children` how you want in nested data sets
        return modifiedData;
    }

    // Return `null` if the term should not be displayed
    return null;
}