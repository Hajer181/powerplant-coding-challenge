namespace PowerPlantApplication.Models
{
    public interface IPowerPlantGasfiredCalculation
    {
        double CoastOfMWh(Fuels fuels, PowerPlant powerPlant);

        double EfficiencyPmax(PowerPlant powerPlant);
    }
}