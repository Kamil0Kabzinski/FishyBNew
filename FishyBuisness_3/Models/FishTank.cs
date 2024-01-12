using System.ComponentModel;

namespace FishyBuisness_3.Models
{
    public class FishTank
    {
        [DisplayName("Fish tank ID")]
        public int FishTankId { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Descirption { get; set; }

        [DisplayName("Capacity")]
        public int Capacity { get; set; }

        [DisplayName("Environment ID")]
        public int EnvironmentId {  get; set; }

        public Environment? Environment{ get; set; }
    }
}
