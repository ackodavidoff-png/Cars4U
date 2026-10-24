using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Models
{
    public class Favorite
    {
        [ForeignKey(nameof(ApplicationUser))]
        public int UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public virtual Car Car { get; set; } = null!;
    }
}
