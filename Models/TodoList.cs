namespace ToDoProject.Models
{
    public class TodoList
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public ApplicationUser User { get; set; }
    }
}
