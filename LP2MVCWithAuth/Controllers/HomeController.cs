using LP2MVCWithAuth.Data;
using LP2MVCWithAuth.Models;
using LP2MVCWithAuth.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LP2MVCWithAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _database;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tarefas = _database.ListaDeTarefas.ToList().Where(x => x.DataEntrega > DateTime.UtcNow).OrderBy(x => x.DataEntrega);

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

            return View(listaTarefas);
        }

        /*[HttpPost]
        public IActionResult Index(ListaTarefas listaTarefas)
        {
            Tarefa tarefa = new Tarefa
            {
                DataEntrega = listaTarefas.DataEntrega,
                Descricao = listaTarefas.Descricao,
                Titulo = listaTarefas.Titulo,
            };

            Data.Models.Location location  = new Data.Models.Location
            {
                City = "teste",
                Latitude = -270266545,
                Longitude = -5114491030,
                IdTarefa = 5,
                IsInitical = true,
            };

            _database.ListaDeTarefas.Add(tarefa);
            _database.Location.Add(location);
            _database.SaveChanges();

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

            return View(listaTarefas);
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}