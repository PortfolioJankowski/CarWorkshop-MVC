using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshopQuery;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedNameQuery;
using CarWorkshop.Application.CarWorkshopService.Commands;
using CarWorkshop.MVC.Extensions;
using CarWorkshop.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        //tutaj mam dostęp do serwisów z kontenera zależności
        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /* index czyli strona główna w konkretnym kontrolerze, żeby wyświetlić wszystkie auta
         trzeba będzie przekazac ich liste do tego View */
        public async Task<IActionResult> Index()
        {
            var carWorkshop = await _mediator.Send(new GetAllCarWorkshopQuery());
            return View(carWorkshop);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
       
                return View();
        }
        //atrybut pozwalający nam zmienić Routing
        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }


        [Authorize(Roles = "Moderator")]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            if (!dto.IsEditable)
            {
                //jeżeli użytkownik nie jest zdolny do edycji zasobu to zostanie przekierowany
                return RedirectToAction("NoAccess", "Home");
            }
            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string endodedName, EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            //tym niżej sie nie sugerować bo to tylko tymczasoe 
            return RedirectToAction(nameof(Index));
        }

        /* metoda, która będzie wysyłała posta z konkretnym warsztatem samochodowym
        warto pamiętać, że logika aplikacji ma swoje osobne miejsce a nie tutaj -> Services/CarWorkshopService
        dlatego przekazuje carWorkshopService przez konstruktor */
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            //jeżeli model nie został prawidłowo zwalidowany to zwróc mi widok, a jeżeli został to dodaje do bazy
            if (!ModelState.IsValid)
            {
                //zwracam Vidok z obiektem carWorkshop, który nie przeszedł walidacji, aby nie zniknęły mi dane z formularza
                //nastąpiła zamiana z obiektu na komendę (wzorzec mediator)
                return View(command);
            }
            await _mediator.Send(command);

            this.SetNotification("success", $"Created workshop: {command.Name}");
            return RedirectToAction(nameof(Index));
        }

        /* Ciekawe rozwiązanie - nie zwracam całego widoku (w końcu modal jest partial View), 
         * informuje tylko o statusie błędu, albo pomyślnym dodaniu */
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            //jeżeli model nie został prawidłowo zwalidowany to zwróc mi widok, a jeżeli został to dodaje do bazy
            if (!ModelState.IsValid)
            {
                //informujemy o błędach
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Ok();
        }
    }
}
