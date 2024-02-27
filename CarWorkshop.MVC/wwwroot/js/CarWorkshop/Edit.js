//kod JS, u�ywam JQuery do obs�ugi zdarze� i wykonania asynchronicznych ��da� AJAX


//je�eli html zosta� za�adowany to wykona si� funkcja wewn�trz
$(document).ready(function () {
    //gdy formularz zostanie przesk�any (id formularza to createCarServiceModal) to funkcja zostanie wywo�ana
    $("#createCarWorkshopServiceModal form").submit(function (event) {
        //strona nie bedzie prze�adowana po z�o�eniu formularza
        event.preventDefault();
        //asynchroniczne ��danie
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Created carworkshop service")
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    });
});