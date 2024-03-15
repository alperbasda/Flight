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
