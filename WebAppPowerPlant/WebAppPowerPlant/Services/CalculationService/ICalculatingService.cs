using System.Collections.Generic;
using PowerPlantApplication.Models;

namespace PowerPlantApplication.Services
{
    public interface ICalculatingService
    {
        public IList<PayLoadResponse> ProductionPlan(IList<PowerPlant> powerPlants, Fuels fuels, double load);
    }
}