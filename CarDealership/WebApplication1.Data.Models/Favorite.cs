using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Models
{
    public class Favorite
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        [ForeignKey(nameof(Car))]
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; } = null!;
    }
}
