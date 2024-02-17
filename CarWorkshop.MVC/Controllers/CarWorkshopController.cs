using CarWorkshop.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {

        private readonly ICarWorkshopService _carWorkshopService;

        //tutaj mam dostęp do serwisów z kontenera zależności
        public CarWorkshopController(ICarWorkshopService carWorkshopService)
        {
            _carWorkshopService = carWorkshopService;
        }

        /* index czyli strona główna w konkretnym kontrolerze, żeby wyświetlić wszystkie auta
         trzeba będzie przekazac ich liste do tego View */
        public async Task<IActionResult> Index()
        {
            var carWorkshop = await _carWorkshopService.GetAll();
            return View(carWorkshop);
        }

        //akcja odpowiedzialna za zwrócenie formularza
        public ActionResult Create()
        {
            return View();
        }

        /* metoda, która będzie wysyłała posta z konkretnym warsztatem samochodowym
        warto pamiętać, że logika aplikacji ma swoje osobne miejsce a nie tutaj -> Services/CarWorkshopService
        dlatego przekazuje carWorkshopService przez konstruktor */
        [HttpPost]
        public async Task<IActionResult> Create(Application.CarWorkshop.CarWorkshopDto carWorkshop)
        {
            //jeżeli model nie został prawidłowo zwalidowany to zwróc mi widok, a jeżeli został to dodaje do bazy
            if (!ModelState.IsValid)
            {
                //zwracam Vidok z obiektem carWorkshop, który nie przeszedł walidacji, aby nie zniknęły mi dane z formularza
                return View(carWorkshop);
            }
            await _carWorkshopService.Create(carWorkshop);
            //tym niżej sie nie sugerować bo to tylko tymczasoe 
            return RedirectToAction(nameof(Index));
        }
    }
}
