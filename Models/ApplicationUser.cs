using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public ICollection<TodoList> TodoList { get; set; }

    }
}
