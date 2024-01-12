using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FishyBuisness_3.Models
{
	public class Fish
	{
        [DisplayName("Fish ID")]
        public int FishId { get; set; }

		[Required]
        [DisplayName("Fish name")]
		[MaxLength(60, ErrorMessage = "Max Length length should not be more than 60 characters" )]
		[MinLength(3, ErrorMessage = "Product name should be at least 3 characters")]
		public string FishName { get; set; }

        [MaxLength(60, ErrorMessage = "Max Length length should not be more than 30 characters")]
        [MinLength(3, ErrorMessage = "Product name should be at least 3 characters")]
        [DisplayName("Fish Description")]
        public string FishDescription { get; set; }

        [DisplayName("Spieces")]
        public string Spieces { get; set; }

        [DisplayName("Habitat")]
        public string Habitat { get; set; }
        [DisplayName("Envirnment ID")]
        public int EnvironmentId { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Can not be less than 0.1 and more than 100")]
        [DisplayName("Length")]
        public double Lenght { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Can not be less than 0.1 and more than 1000")]
        [DisplayName("Weigth")]
        public double Weight { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Can not be less than 0.1 and more than 100000")]
        [DisplayName("Price")]
      
        public double Price { get; set; }

        public Environment? Environment{ get; set; }

    }
}
