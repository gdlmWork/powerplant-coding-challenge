﻿using PowerplantAPI.Models;

namespace PowerplantAPI.Services
{
    public interface IProductionService
    {
        List<ProductionPlan> CalculateUnit(Payload payload);
    }
}
