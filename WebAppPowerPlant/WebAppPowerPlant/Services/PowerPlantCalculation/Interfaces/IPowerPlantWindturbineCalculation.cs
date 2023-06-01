namespace PowerPlantApplication.Models
{
    public interface IPowerPlantWindturbineCalculation
    {
        double CoastOfMWh(Fuels fuels);
        double EfficiencyPmax(Fuels fuels, PowerPlant powerPlant);
    }
}