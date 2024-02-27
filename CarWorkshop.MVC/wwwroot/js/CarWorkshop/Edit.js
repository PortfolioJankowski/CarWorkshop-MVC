//kod JS, u¿ywam JQuery do obs³ugi zdarzeñ i wykonania asynchronicznych ¿¹dañ AJAX


//je¿eli html zosta³ za³adowany to wykona siê funkcja wewn¹trz
$(document).ready(function () {
    //gdy formularz zostanie przesk³any (id formularza to createCarServiceModal) to funkcja zostanie wywo³ana
    $("#createCarWorkshopServiceModal form").submit(function (event) {
        //strona nie bedzie prze³adowana po z³o¿eniu formularza
        event.preventDefault();
        //asynchroniczne ¿¹danie
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