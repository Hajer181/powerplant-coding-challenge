using PowerPlantApplication.Models;

namespace PowerPlantApplication.Services
{
    public class PowerPlantDetails
    {
        public PowerPlantDetails(PowerPlant powerPlant)
        {
            this.Efficiency = powerPlant.Efficiency;
            this.Name = powerPlant.Name;
            this.Pmin = powerPlant.Pmin;
        }

        public double CostMHW { get; set; }
        public double Efficiency { get; set; }
        public double EfficiencyPmax { get; set; }
        public string Name { get; set; }
        public int Pmin { get; set; }
    }
}