namespace PowerPlantApplication.Models
{
    public class PowerPlantGasfiredCalculation : IPowerPlantGasfiredCalculation
    {
        private const double MWHCO2 = 0.3;

        public PowerPlantGasfiredCalculation()
        {
        }

        public double CoastOfMWh(Fuels fuels, PowerPlant powerPlan)
        {
            return (fuels.GasEuroMWh / powerPlan.Efficiency) - CoastOfRuning(fuels);
        }

        public double EfficiencyPmax(PowerPlant powerPlan)
        {
            return powerPlan.Pmax * powerPlan.Efficiency;
        }

        private double CoastOfRuning(Fuels fuels)
        {
            return MWHCO2 * fuels.Co2EuroTon;
        }
    }
}