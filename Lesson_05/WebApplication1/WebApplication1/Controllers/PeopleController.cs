using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.People;
using WebApplication1.Services;
using WebApplication1.Services.Abstract;

namespace WebApplication1.Controllers
{
    public class PeopleController : Controller
    {
        readonly IWebPeopleService _peopleService;
        readonly IMemoryCache _cache;

        public PeopleController(
            IWebPeopleService peopleService,
            IMemoryCache cache)
        {
            _peopleService = peopleService;
            _cache = cache;
        }

        // Default
        // [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 2 * 60, NoStore = false)]
        public IActionResult Index()
        {
            List<PersonModel> people = null;

            // Caching in memory
            //if (!_cache.TryGetValue("people", out people))
            //{
            //    people = _peopleService.GetAllPeople();
            //    var options = new MemoryCacheEntryOptions();
            //    options.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            //    // добавление данных в кеш
            //    _cache.Set("people", people, options);
            //}
            people = _peopleService.GetAllPeople();

            return View(new PeopleIndexViewModel
            {
                People = people,
                MaxPerson = people.FirstOrDefault(p => p.Birth.Ticks == people.Min((p) => p.Birth.Ticks)),
                MinPerson = people.FirstOrDefault(p => p.Birth.Ticks == people.Max((p) => p.Birth.Ticks)),
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePersonModel person)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Model is not valid!");
                return View(person);
            }
            _peopleService.CreateNewPerson(person);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid? id)
        {
            if (id is null || _peopleService.GetPersonById((Guid)id) is null)
            {
                return BadRequest("Person was not found");
            }
            _peopleService.RemovePersonById((Guid)id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid? id)
        {
            if (id is null || _peopleService.GetPersonById((Guid)id) is null)
            {
                return BadRequest("Person was not found");
            }
            return View(_peopleService.GetPersonById((Guid)id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PersonModel person)
        {
            if (_peopleService.GetPersonById((Guid)person.Id) is null)
            {
                return BadRequest("Person was not found");
            }

            _peopleService.UpdatePerson(person);
            return View(_peopleService.GetPersonById(person.Id));
        }

        public IActionResult Detail(Guid? id)
        {
            if (id is null || _peopleService.GetPersonById((Guid)id) is null)
            {
                return BadRequest("Person was not found");
            }
            return View(_peopleService.GetPersonById((Guid)id));
        }
    }
}
