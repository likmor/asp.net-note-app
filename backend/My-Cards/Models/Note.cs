using Microsoft.VisualBasic;

namespace My_Cards.Models
{
    public class Note
    {
        public Note(string title, string description)
        {
            Title = title; 
            Description = description;
            CreationTime = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
