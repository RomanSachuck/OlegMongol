using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IBusinessPersistent
    {
        public int GetWorkCycleValue(BusinessType businessType);
        public int GetProductionUpgradeLevel(BusinessType businessType);
        public int GetPriceUpgradeLevel(BusinessType businessType);
        public int GetManagerLevel(BusinessType businessType);
        public void SetWorkCycleValue(BusinessType businessType, int value);
        public void SetProductionUpgradeLevel(BusinessType businessType, int value);
        public void SetPriceUpgradeLevel(BusinessType businessType, int value);
        public void SetManagerLevel(BusinessType businessType, int value);
    }
}