//kod JS, u�ywam JQuery do obs�ugi zdarze� i wykonania asynchronicznych ��da� AJAX
//je�eli html zosta� za�adowany to wykona si� funkcja wewn�trz
$(document).ready(function () {

    const RenderCarWorkshopServices = (services, container) => {
        container.empty();

        for (const service of services) {
            container.append(`
            <div class="card borded-secondary mb-3" style="max-width: 18rem;">
                <div class="card-header">${service.cost}</div>
                <div class="card-body">
                    <h5 class="card-title">${service.description}</h5>
                </div>
            </div>`)
        }
    }

    const LoadCarWorkshopServices = () => {
        const container = $("#services")
        const carWorkshopEncodedName = container.data("encodedName");

        $.ajax({
            url: `/CarWorkshop/${carWorkshopEncodedName}/CarWorkshopService`,
            type: 'get',
            success: function (data) {
                if (!data.length) {
                    container.html("There are no services for this car workshop")
                } else {
                    RenderCarWorkshopServices(data, container)
                }
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    }

    LoadCarWorkshopServices()
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