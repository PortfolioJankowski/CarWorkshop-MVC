using CarWorkshop.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {

        private readonly ICarWorkshopService _carWorkshopService;

        public CarWorkshopController(ICarWorkshopService carWorkshopService)
        {
            _carWorkshopService = carWorkshopService;
        }

        //akcja odpowiedzialna za zwrócenie formularza
        public ActionResult Create()
        {
            return View();
        }

        //metoda, która będzie wysyłała posta z konkretnym warsztatem samochodowym
        //warto pamiętać, że logika aplikacji ma swoje osobne miejsce a nie tutaj -> Services/CarWorkshopService
        //-> dlatego przekazuje carWorkshopService przez konstruktor
        [HttpPost]
        public async Task<IActionResult> Create(Domain.Entities.CarWorkshop carWorkshop)
        {
            await _carWorkshopService.Create(carWorkshop);
            //tym niżej sie nie sugerować bo to tylko tymczasoe 
            return RedirectToAction(nameof(Create));
        }
    }
}
