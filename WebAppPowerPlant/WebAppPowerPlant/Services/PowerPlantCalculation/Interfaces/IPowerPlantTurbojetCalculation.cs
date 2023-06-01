namespace PowerPlantApplication.Models
{
    public interface IPowerPlantTurbojetCalculation
    {
        double CoastOfMWh(Fuels fuels, PowerPlant powerPlant);

        double EfficiencyPmax(PowerPlant powerPlant);
    }
}