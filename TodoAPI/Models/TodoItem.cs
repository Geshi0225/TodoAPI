namespace TodoAPI.Models
{
    public class TodoItem
    {
        public long id {  get; set; }
        public string? name { get; set; }   
        public bool IsComplete { get; set; }    
    }
}
