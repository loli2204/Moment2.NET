using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItem> _tasks = new List<TodoItem>
        {
            new TodoItem { Id = 1, TaskName = "Sample Task 1", IsCompleted = false },
            new TodoItem { Id = 2, TaskName = "Sample Task 2", IsCompleted = true }
        };

        public IActionResult Index()
        {
            ViewBag.Message = "Your To-Do List";
            ViewData["Title"] = "To Do List";
            return View(_tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem todoItem)
        {
            todoItem.Id = _tasks.Count + 1;
            _tasks.Add(todoItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem todoItem)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == todoItem.Id);
            if (task != null)
            {
                task.TaskName = todoItem.TaskName;
                task.IsCompleted = todoItem.IsCompleted;
            }
            return RedirectToAction("Index");
        }
    }
}
