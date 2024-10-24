using System.ComponentModel.DataAnnotations;

namespace WebApplication1Minimal.DBConnection.Model
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
