using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using WebApplication1.Data.Models.Enums;
using static WebApplication1.GCommon.EntityConstraints;

namespace WebApplication1.ViewModels
{
    public class CreateCarViewModel
    {
        [Required]
        [StringLength(CarBrandNameMaxLength, MinimumLength = CarBrandNameMinLength)]
        public string Brand { get; set; } = null!;
        [Required]
        [StringLength(CarModelNameMaxLength, MinimumLength = CarModelNameMinLength)]
        public string Model { get; set; } = null!;
        [Required]
        [Range(CarProductionYearMinValue, CarProductionYearMaxValue)]
        public int Year { get; set; }
        [Required]
        [Range(CarPriceMinValue, CarPriceMaxValue)]
        public double Price { get; set; }
        [Required]
        [Range(CarMileageMinValue, CarMileageMaxValue)]
        public int Mileage { get; set; }
        [Required]
        public EngineType EngineType { get; set; }
        [Required]
        public TransmissionType TransmissionType { get; set; }
        [Required]
        [Range(CarHorsepowerMinValue, CarHorsepowerMaxValue)]
        public int HorsePower { get; set; }
        [Required]
        public State State { get; set; }
        [StringLength(CarDescriptionMaxLength, MinimumLength = CarDescriptionMinLength)]
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        //[ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
