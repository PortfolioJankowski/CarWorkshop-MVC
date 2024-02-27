//kod JS, u¿ywam JQuery do obs³ugi zdarzeñ i wykonania asynchronicznych ¿¹dañ AJAX
//je¿eli html zosta³ za³adowany to wykona siê funkcja wewn¹trz
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