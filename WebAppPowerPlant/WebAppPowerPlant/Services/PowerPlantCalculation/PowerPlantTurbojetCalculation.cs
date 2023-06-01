namespace PowerPlantApplication.Models
{
    public class PowerPlantTurbojetCalculation : IPowerPlantTurbojetCalculation
    {
        public PowerPlantTurbojetCalculation()
        {
        }

        public double CoastOfMWh(Fuels fuels, PowerPlant powerPlant)
        {
            return fuels.KerosineEuroMWh / powerPlant.Efficiency;
        }

        public double EfficiencyPmax(PowerPlant powerPlant)
        {
            return powerPlant.Pmax * powerPlant.Efficiency;
        }
    }
}