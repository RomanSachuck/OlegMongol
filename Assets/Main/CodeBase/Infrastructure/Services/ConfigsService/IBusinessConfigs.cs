using System.Collections.Generic;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public interface IBusinessConfigs
    {
        void CacheBusinessConfigs(IEnumerable<BusinessConfigs> configs);
        IEnumerable<BusinessType> GetAllBusinesses();
        BusinessConfigs GetBusinessConfig(BusinessType businessType);
        ManagerType GetManagerType(BusinessType businessType);
        int GetCycleSize(BusinessType businessType);
        ulong GetCurrentProductPrice(BusinessType businessType, int upgradeLevel);
        int GetCurrentProductionValue(BusinessType businessType, int upgradeLevel);
        ulong GetCurrentPriceUpgradeCost(BusinessType businessType, int upgradeLevel);
        ulong GetCurrentProductionUpgradeCost(BusinessType businessType, int upgradeLevel);
        int GetCurrentProductionUpgradeValue(BusinessType businessType, int upgradeLevel);
        ulong GetCurrentPriceUpgradeValue(BusinessType businessType, int upgradeLevel);
    }
}