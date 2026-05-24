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
        
        #endregion
    }
}