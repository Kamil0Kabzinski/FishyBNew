using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FishyBuisness_3.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("First name")]
        [Required(ErrorMessage = "The First Name field is required.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First name must start with a capital letter and only contain letters")]
        [StringLength(50, ErrorMessage = "The First Name field must be at most 50 characters.")]
        public string FistName { get; set; }

        [DisplayName("Last name")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First name must start with a capital letter and only contain letters")]
        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, ErrorMessage = "The Last Name field must be at most 50 characters.")]
        public string LastName { get; set; }
    }
}

