using System.Collections.Generic;
using ToDoProject.Models;
using System.Linq;

namespace ToDoProject.Services
{
    public class TodoListRepository : ITodoListRepository
    {
        Entities context= new Entities();
        public TodoListRepository(Entities _context)
        {
            context = _context;
        }
        public int Create(TodoList list)
        {
            context.todoList.Add(list);
            int raw = context.SaveChanges();
            return raw;
        }

        public int Delete(int id)
        {
            TodoList List = context.todoList.FirstOrDefault(s => s.ID == id);
            context.todoList.Remove(List);
            int raw = context.SaveChanges();
            return raw;
        }

        public List<TodoList> getAll()
        {
            return context.todoList.ToList();
        }

        public TodoList getById(int id)
        {
            return context.todoList.FirstOrDefault(s => s.ID == id);
        }

        public int Update(int id, TodoList todoList)
        {
            TodoList oldList = context.todoList.FirstOrDefault(s => s.ID == id);
            oldList.Item = todoList.Item;
            int raw = context.SaveChanges();
            return raw;
        }
    }
}
