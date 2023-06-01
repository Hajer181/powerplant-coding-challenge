namespace PowerPlantApplication.Models
{
    public class PowerPlantWindturbineCalculation : IPowerPlantWindturbineCalculation
    {
        public PowerPlantWindturbineCalculation()
        {

        }
        public double CoastOfMWh(Fuels fuels)
        {
            return 0;
        }

        public double EfficiencyPmax(Fuels fuels, PowerPlant powerPlant)
        {
            return powerPlant.Pmax * fuels.Wind / 100;
        }
    }
}