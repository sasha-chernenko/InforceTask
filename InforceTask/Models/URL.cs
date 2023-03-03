using System.ComponentModel.DataAnnotations;

namespace InforceTask.Models
{
    public class URL
    {
        public int Id { get; set; }
        [Required]
        public string? Long { get; set; }
        public string? Short { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
