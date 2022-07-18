﻿using LP2MVCWithAuth.Data;
using LP2MVCWithAuth.Models;
using LP2MVCWithAuth.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace LP2MVCWithAuth.Controllers
{
    public class TarefasController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _database;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public TarefasController(ILogger<HomeController> logger, ApplicationDbContext database, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _database = database;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var tarefas = _database.ListaDeTarefas.ToList().Where(x => x.UserEmail == User.Identity.Name).OrderBy(x => x.DataEntrega);

                ListaTarefas listaTarefas = new ListaTarefas();

                if (tarefas.Count() > 0)
                {
                    foreach (var item in tarefas)
                    {
                        ListaTarefas tarefa = new ListaTarefas
                        {
                            DataEntrega = item.DataEntrega,
                            Descricao = item.Descricao,
                            Titulo = item.Titulo,
                            Id = item.Id,
                        };

                        listaTarefas.Tarefas.Add(tarefa);
                    }
                }

                // create a view for the tasks

                return View(listaTarefas);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ListaTarefas listaTarefas)
        {
            Tarefa tarefa = new Tarefa
            {
                DataEntrega = listaTarefas.DataEntrega,
                Descricao = listaTarefas.Descricao,
                Titulo = listaTarefas.Titulo,
                UserEmail = User.Identity.Name,
            };

            _database.ListaDeTarefas.Add(tarefa);
            _database.SaveChanges();

            var tarefaCriada = _database.ListaDeTarefas.Where(x => x.UserEmail == User.Identity.Name && x.Titulo == listaTarefas.Titulo).OrderBy(x => x.Id).Last();
            Data.Models.Location location = new Data.Models.Location
            {
                Latitude = listaTarefas.PontoDePartida.Latitude,
                Longitude = listaTarefas.PontoDePartida.Longitude,
                IsInitical = true,
                IdTarefa = tarefaCriada.Id,
            };

            foreach (var item in listaTarefas.Locations)
            {
                location = new Data.Models.Location
                {
                    Latitude = listaTarefas.PontoDePartida.Latitude,
                    Longitude = listaTarefas.PontoDePartida.Longitude,
                    IsInitical = false,
                    IdTarefa = tarefaCriada.Id,
                };
            }

            var tarefas = _database.ListaDeTarefas.ToList().Where(x => x.DataEntrega > DateTime.UtcNow).OrderBy(x => x.DataEntrega);

            if (tarefas.Count() > 0)
            {
                foreach (var item in tarefas)
                {
                    ListaTarefas tarefaa = new ListaTarefas
                    {
                        DataEntrega = item.DataEntrega,
                        Descricao = item.Descricao,
                        Titulo = item.Titulo,
                        Id = item.Id,
                    };

                    listaTarefas.Tarefas.Add(tarefaa);
                }
            }

            return RedirectToAction("Index", "Tarefas");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tarefa = _database.ListaDeTarefas.FirstOrDefault(x => x.Id == id);

            if (tarefa != null)
            {
                var tarefaModel = new ListaTarefas
                {
                    DataEntrega = tarefa.DataEntrega,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    Id = tarefa.Id,
                };

                var startLocation = _database.Location.Where(x => x.IsInitical == true).FirstOrDefault();
                if (startLocation != null)
                {
                    var locationModel = new Models.Location
                    {
                        IsInitical = true,
                        Latitude = startLocation.Latitude,
                        Longitude = startLocation.Longitude,
                        Id = startLocation.Id,
                    };

                    tarefaModel.PontoDePartida = locationModel;
                }

                var locations = _database.Location.Where(x => x.IsInitical == false).ToList();
                var locationsModel = new List<Models.Location>();

                foreach (var item in locations)
                {
                    var locationModel = new Models.Location
                    {
                        IsInitical = false,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        Id = item.Id,
                    };

                    locationsModel.Add(locationModel);
                }

                if (locationsModel.Count() > 0)
                {
                    tarefaModel.Locations = locationsModel;
                }

                return View(tarefaModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(ListaTarefas listaTarefas)
        {
            Tarefa tarefa = new Tarefa
            {
                Id = listaTarefas.Id,
                DataEntrega = listaTarefas.DataEntrega,
                Descricao = listaTarefas.Descricao,
                Titulo = listaTarefas.Titulo,
                UserEmail = User.Identity.Name,
            };

            _database.ListaDeTarefas.Update(tarefa);
            _database.SaveChanges();

            return View(listaTarefas);
        }

        [HttpPost]
        public IActionResult AddLocationByTarefa(Models.Location location)
        {
            Data.Models.Location locationToBeAdded = new Data.Models.Location
            {
                City = location.City,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                IdTarefa = location.IdTarefa,
                IsInitical = location.IsInitical,
                Country = location.Country,
                HouseNumber = location.HouseNumber,
                Id = location.Id,
                Postcode = location.Postcode,
                State = location.State,
                StreetName = location.StreetName,
            };

            _database.Location.Add(locationToBeAdded);
            _database.SaveChanges();

            var tarefas = _database.ListaDeTarefas.ToList().Where(x => x.DataEntrega > DateTime.UtcNow).OrderBy(x => x.DataEntrega);
            ListaTarefas listaTarefas = new ListaTarefas();

            if (tarefas.Count() > 0)
            {
                foreach (var item in tarefas)
                {
                    ListaTarefas tarefaa = new ListaTarefas
                    {
                        DataEntrega = item.DataEntrega,
                        Descricao = item.Descricao,
                        Titulo = item.Titulo,
                        Id = item.Id,
                    };

                    listaTarefas.Tarefas.Add(tarefaa);
                }
            }

            return View(listaTarefas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}