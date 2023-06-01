using System;
using System.Collections.Generic;
using System.Linq;
using PowerPlantApplication.Models;

namespace PowerPlantApplication.Services
{
    public class CalculatingService : ICalculatingService
    {
        private readonly IPowerPlantGasfiredCalculation _powerPlantGasfiredCalculation;

        private readonly IPowerPlantTurbojetCalculation _powerPlantTurbojetCalculation;

        private readonly IPowerPlantWindturbineCalculation _powerPlantWindturbineCalculation;

        public CalculatingService(IPowerPlantGasfiredCalculation powerPlantGasfiredCalculation,
                                  IPowerPlantTurbojetCalculation powerPlantTurbojetCalculation,
                                  IPowerPlantWindturbineCalculation powerPlantWindturbineCalculation)
        {
            _powerPlantGasfiredCalculation = powerPlantGasfiredCalculation;
            _powerPlantTurbojetCalculation = powerPlantTurbojetCalculation;
            _powerPlantWindturbineCalculation = powerPlantWindturbineCalculation;
        }

        public IList<PayLoadResponse> ProductionPlan(IList<PowerPlant> powerPlants, Fuels fuels, double load)
        {
            IList<PowerPlantDetails> powerPlantDetails = CalculatePowerPlantsDetails(powerPlants, fuels);
            return PayLoadResponses(powerPlantDetails, load);
        }

        private IList<PowerPlantDetails> CalculatePowerPlantsDetails(IList<PowerPlant> powerPlants, Fuels fuels)
        {
            IList<PowerPlantDetails> PowerPlantsDetails = new List<PowerPlantDetails>();

            foreach (PowerPlant powerPlant in powerPlants)
            {
                PowerPlantDetails powerPlantDetails = new PowerPlantDetails(powerPlant);

                switch (powerPlant.Type)
                {
                    case PowerPlantTypes.GASFIRED:
                        powerPlantDetails.CostMHW = _powerPlantGasfiredCalculation.CoastOfMWh(fuels, powerPlant);
                        powerPlantDetails.EfficiencyPmax = _powerPlantGasfiredCalculation.EfficiencyPmax(powerPlant);
                        break;

                    case PowerPlantTypes.TURBOJET:

                        powerPlantDetails.CostMHW = _powerPlantTurbojetCalculation.CoastOfMWh(fuels, powerPlant);
                        powerPlantDetails.EfficiencyPmax = _powerPlantTurbojetCalculation.EfficiencyPmax(powerPlant);
                        break;

                    case PowerPlantTypes.WINDTURBINE:

                        powerPlantDetails.CostMHW = _powerPlantWindturbineCalculation.CoastOfMWh(fuels);
                        powerPlantDetails.EfficiencyPmax = _powerPlantWindturbineCalculation.EfficiencyPmax(fuels, powerPlant);
                        break;
                }
                PowerPlantsDetails.Add(powerPlantDetails);
            }

            return OrderPowerPlantsDetails(PowerPlantsDetails);
        }

        private IList<PowerPlantDetails> OrderPowerPlantsDetails(IList<PowerPlantDetails> PowerPlantsDetails)
        {
            return PowerPlantsDetails.OrderBy(x => x.CostMHW)
                                     .ThenBy(c => c.Efficiency)
                                     .ThenByDescending(a => a.EfficiencyPmax).ToList();
        }

        private IList<PayLoadResponse> PayLoadResponses(IList<PowerPlantDetails> powerPlantsDetails, double load)
        {
            IList<PayLoadResponse> payLoadResponses = new List<PayLoadResponse>();

            foreach (PowerPlantDetails plantDetails in powerPlantsDetails)
            {
                double requestedLoad = load - plantDetails.EfficiencyPmax;

                if (load == 0)
                {
                    payLoadResponses.Add(
                        new PayLoadResponse
                        {
                            Name = plantDetails.Name,
                            P = 0
                        });
                }
                else
                {
                    if (requestedLoad > 0)
                    {
                        payLoadResponses.Add(
                            new PayLoadResponse
                            {
                                Name = plantDetails.Name,
                                P = plantDetails.EfficiencyPmax
                            });

                        load -= plantDetails.EfficiencyPmax;
                    }
                    if (requestedLoad < 0 && load > plantDetails.Pmin)
                    {
                        payLoadResponses.Add(
                            new PayLoadResponse
                            {
                                Name = plantDetails.Name,
                                P = Math.Round(load, 1)
                            });

                        load = 0;
                    }
                    if (requestedLoad < 0 && load < plantDetails.Pmin)
                    {
                        PayLoadResponse lastPowerPlant = payLoadResponses.LastOrDefault();
                        lastPowerPlant.P = lastPowerPlant.P - plantDetails.Pmin + load;

                        payLoadResponses.Add(
                            new PayLoadResponse
                            {
                                Name = plantDetails.Name,
                                P = plantDetails.Pmin
                            });

                        load = 0;
                    }
                }
            }

            return payLoadResponses;
        }
    }
}