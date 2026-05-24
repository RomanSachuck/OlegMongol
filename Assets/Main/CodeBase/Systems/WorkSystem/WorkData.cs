using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Systems.WorkSystem
{
    public class WorkData
    {
        public BusinessType BusinessType;
        public int ProductionValue;
        public ulong Price;
        public int CurrentCycleValue;
        public int FullCycleValue;

        public WorkData(BusinessType businessType, int productionValue, 
            ulong price, int currentCycleValue, int fullCycleValue)
        {
            BusinessType = businessType;
            ProductionValue = productionValue;
            Price = price;
            CurrentCycleValue = currentCycleValue;
            FullCycleValue = fullCycleValue;
        }
    }
}