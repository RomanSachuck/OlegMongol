using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Systems.WorkSystem
{
    public class WorkData
    {
        public BusinessType BusinessType;
        public int ProductionAmount;
        public ulong Price;
        public int CurrentCycleValue;
        public int FullCycleValue;

        public WorkData(BusinessType businessType, int productionAmount, 
            ulong price, int currentCycleValue, int fullCycleValue)
        {
            BusinessType = businessType;
            ProductionAmount = productionAmount;
            Price = price;
            CurrentCycleValue = currentCycleValue;
            FullCycleValue = fullCycleValue;
        }
    }
}