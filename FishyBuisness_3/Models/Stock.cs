using System.ComponentModel;

namespace FishyBuisness_3.Models
{
    public class Stock
    {
        [DisplayName("Stock ID")]
        public int StockId { get; set; }

        [DisplayName("Fish ID")]
        public int FishId { get; set; }

        [DisplayName("Fish Tank ID")]
        public int FishTankId { get; set; }

        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [DisplayName("Environment ID")]
        public int EnvironmentId {  get; set; }

        // Klucz obcy do modelu Product
        public Fish? Fish { get; set; }

        // Klucz obcy do modelu Warehouse
        public FishTank? FishTank { get; set; }

        public Environment? Environment { get; set; }

    }

}
