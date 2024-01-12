using System.ComponentModel;

namespace FishyBuisness_3.Models
{
    public class Environment
    {
        [DisplayName("Environment ID")]
        public int EnvironmentId { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
