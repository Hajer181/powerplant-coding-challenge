using System.Collections.Generic;

namespace PowerPlantApplication.Models
{
    public class PayLoadRequest
    {
        public Fuels Fuels { get; set; }
        public int Load { get; set; }
        public List<PowerPlant> PowerPlants { get; set; }
    }
}