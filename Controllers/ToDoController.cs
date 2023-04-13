using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{   
    public class todocontroller : Controller
    {
        private readonly ToDoDb _database;

        public todocontroller(ToDoDb db)
        {
            _database = db;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _database.ToDos.ToListAsync();
            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Add(string newItem)
        {
            if (newItem != null)
            {
                var todo = new Todoitem
                {
                    Name = newItem,
                    IsDone = false
                };
                _database.ToDos.Add(todo);
                await _database.SaveChangesAsync();
            }

            return RedirectToAction("ToDo");
        }

        public IActionResult Complete(IEnumerable<int> isComplete)
        {
            if (isComplete != null)
            {
                foreach (var item in isComplete)
                {
                    var todo = _database.ToDos.FirstOrDefault(t => t.Id == item);
                    if (todo != null)
                    {
                        todo.IsDone = true;
                    }
                }
                _database.SaveChanges();
            }
            return RedirectToAction("ToDo");
        }

        public IActionResult DeleteAll()
        {
            _database.ToDos.RemoveRange(_database.ToDos);
            _database.SaveChanges();
            return RedirectToAction("ToDo");
        }

    }
}
