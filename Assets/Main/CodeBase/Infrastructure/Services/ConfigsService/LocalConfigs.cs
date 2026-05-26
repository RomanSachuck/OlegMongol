using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Main.CodeBase.SaveData;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public class LocalConfigs : IConfigs
    {
        #region StartingProgress
        
        private PlayerProgress _startingProgress;

        public void CacheStartingProgress(PlayerProgress startingProgress)
        {
            _startingProgress = startingProgress;
        }

        public UniTask<PlayerProgress> GetStartingProgress()
        {
            if (_startingProgress == null)
                throw new Exception("The StartingProgressConfigs are not cached!");
            
            return new UniTask<PlayerProgress>(_startingProgress);
        }
        
        #endregion
        
        #region Business
        
        private IEnumerable<BusinessConfigs> _businessConfigs;
        
        public void CacheBusinessConfigs(IEnumerable<BusinessConfigs> configs)
        {
            _businessConfigs = configs;
        }

        public IEnumerable<BusinessType> GetAllBusinesses()
        {
            return _businessConfigs.Select(c => c.BusinessType).ToArray();
        }

        public BusinessConfigs GetBusinessConfig(BusinessType businessType)
        {
            return _businessConfigs.First(c => c.BusinessType == businessType);
        }

        public ManagerType GetManagerType(BusinessType businessType)
        {
            return GetBusinessConfig(businessType).Manager;
        }

        public int GetCycleSize(BusinessType businessType)
        {
            return GetBusinessConfig(businessType).CycleSize;
        }

        public ulong GetCurrentProductPrice(BusinessType businessType, int upgradeLevel)
        {
            BusinessConfigs config = GetBusinessConfig(businessType);
            return CalculateCurrentValueForGrowth(config.PriceRateOfGrowth, config.BaseProductPrice, upgradeLevel);
        }

        public int GetCurrentProductionValue(BusinessType businessType, int upgradeLevel)
        {
            BusinessConfigs config = GetBusinessConfig(businessType);
            return (int)CalculateCurrentValueForGrowth(config.ProductionRateOfGrowth, config.BaseProduction, upgradeLevel);
        }

        public ulong GetCurrentPriceUpgradeCost(BusinessType businessType, int upgradeLevel)
        {
            BusinessConfigs config = GetBusinessConfig(businessType);
            return CalculateCurrentValueForGrowth(config.PriceUpgradeCostRateOfGrowth, 
                config.BaseProductPriceUpgradeCost, upgradeLevel);
        }

        public ulong GetCurrentProductionUpgradeCost(BusinessType businessType, int upgradeLevel)
        {
            BusinessConfigs config = GetBusinessConfig(businessType);
            return CalculateCurrentValueForGrowth(config.ProductionUpgradeCostRateOfGrowth, 
                config.BaseProductionUpgradeCost, upgradeLevel);
        }

        public int GetCurrentProductionUpgradeValue(BusinessType businessType, int upgradeLevel)
        {
            BusinessConfigs config = GetBusinessConfig(businessType);
            int currentProduction = (int)CalculateCurrentValueForGrowth(config.ProductionRateOfGrowth, 
                config.BaseProduction, upgradeLevel);
            int nextProduction = (int)CalculateCurrentValueForGrowth(config.ProductionRateOfGrowth, 
                config.BaseProduction, upgradeLevel + 1);
            
            return nextProduction - currentProduction;
        }

        public ulong GetCurrentPriceUpgradeValue(BusinessType businessType, int upgradeLevel)
        {
            BusinessConfigs config = GetBusinessConfig(businessType);
            ulong currentPrice = CalculateCurrentValueForGrowth(config.PriceRateOfGrowth, 
                config.BaseProductPrice, upgradeLevel);
            ulong nextPrice = CalculateCurrentValueForGrowth(config.PriceRateOfGrowth, 
                config.BaseProductPrice, upgradeLevel + 1);
            
            return nextPrice - currentPrice;
        }
        
        #endregion

        #region Managers

        private IEnumerable<ManagerConfigs> _managerConfigs;
        
        public void CacheManagersConfigs(IEnumerable<ManagerConfigs> configs)
        {
            _managerConfigs = configs;
        }

        public IEnumerable<ManagerType> GetAllManagers()
        {
            return _managerConfigs.Select(c => c.ManagerType).ToArray();
        }

        public float GetTimeToClick(ManagerType managerType, int level)
        {
            ManagerConfigs config = _managerConfigs.First(c => c.ManagerType == managerType);
            return 1f / CalculateCurrentValueForGrowth(config.ClickRateOfGrowth, (ulong)config.BaseClickValue, level);
        }

        #endregion
        
        private ulong CalculateCurrentValueForGrowth(RateOfGrowth rateOfGrowth, ulong baseValue, int upgradeLevel)
        {
            switch (rateOfGrowth)
            {
                case RateOfGrowth.VerySlow:
                    return baseValue + baseValue * (ulong)upgradeLevel;
                case RateOfGrowth.Slow:
                    return baseValue + baseValue * (ulong)upgradeLevel * (2 + (ulong)(upgradeLevel / 10));
                case RateOfGrowth.Middle:
                    return baseValue + baseValue * (ulong)upgradeLevel * (3 + (ulong)(upgradeLevel / 8));
                case RateOfGrowth.Fast:
                    return baseValue + baseValue * (ulong)upgradeLevel * (4 + (ulong)(upgradeLevel / 6));
                case RateOfGrowth.SuperFast:
                    return baseValue + baseValue * (ulong)upgradeLevel * (5 + (ulong)(upgradeLevel / 3));
                default:
                    throw new ArgumentOutOfRangeException(nameof(rateOfGrowth), rateOfGrowth, null);
            }
        }
    }
}