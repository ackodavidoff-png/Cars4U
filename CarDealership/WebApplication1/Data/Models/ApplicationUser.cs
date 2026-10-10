using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebApplication1.Common.EntityConstraints;

namespace WebApplication1.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        [MinLength(UserFirstNameMinLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(UserLastNameMaxLength)]
        [MinLength(UserLastNameMinLength)]
        public string LastName { get; set; } = null!;
        [Required]
        [RegularExpression(PhoneNumberRegexPattern)]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
        public bool IsAdmin { get; set; }
        [Required]
        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }
        public virtual Town Town { get; set; } = null!;
    }
}
