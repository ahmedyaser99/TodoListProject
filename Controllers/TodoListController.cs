using Microsoft.AspNetCore.Mvc;
using ToDoProject.Models;
using ToDoProject.Services;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Data;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ToDoProject.Controllers
{
    public class TodoListController : Controller
    {
        ITodoListRepository TodoRepository;
        
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoListController(ITodoListRepository _todoList,UserManager<ApplicationUser> userManager)
        {
            TodoRepository = _todoList;
            _userManager = userManager;
        }
        public IActionResult Add()
        {
            TodoList todo = new TodoList();
            return View(todo);
        }
        [HttpPost]
        public IActionResult SaveAdd(TodoList list)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    TodoRepository.Create(list);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException) { ModelState.AddModelError("", "Unable to save"); }
            return View("Add", list);
        }
        public IActionResult Edit(int id)
        {
            
            TodoList todo = TodoRepository.getById(id);
            return View(todo);
        }
        [HttpPost]// <form method="post"> only accept post method
        public IActionResult SaveEdit([FromRoute] int id, TodoList newTodo)
        {
            if (ModelState.IsValid)
            {
                TodoRepository.Update(id, newTodo);
                return RedirectToAction("Index");
            }
            
            return View("Edit", newTodo);

        }
        [Authorize]
        public IActionResult Index()
        {
           
            List<TodoList> todoListModel = TodoRepository.getAll();
                return View("Index", todoListModel);
            
        }
        public IActionResult Delete(int id)
        {
            try
            {
                TodoRepository.Delete(id);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);//
                //exception client
                return View("Index");
            }
        }

    }
}
