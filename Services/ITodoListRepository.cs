using System.Collections.Generic;
using ToDoProject.Models;

namespace ToDoProject.Services
{
    public interface ITodoListRepository
    {
        int Create(TodoList list);
        int Delete(int id);
        List<TodoList> getAll();
        TodoList getById(int id);
        int Update(int id, TodoList todoList);
    }
}
