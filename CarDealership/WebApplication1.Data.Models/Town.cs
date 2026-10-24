using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Models
{
    public class Town
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
