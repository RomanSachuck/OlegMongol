using System;
using System.Collections.Generic;
using System.Linq;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.Systems.WalletSystem;

namespace Main.CodeBase.Systems.WorkSystem
{
    public class WorkController
    {
        public event Action<BusinessType, int, int> WorkCycleUpdated;
        public event Action<BusinessType, ulong> IncomeReceived;
        
        private readonly IBusinessPersistent _businessPersistent;
        private readonly IBusinessConfigs _businessConfigs;
        private readonly Wallet _wallet;
        
        private readonly List<WorkData> _works;

        public WorkController(IBusinessPersistent businessPersistent, IBusinessConfigs businessConfigs,
            Wallet wallet)
        {
            _businessPersistent = businessPersistent;
            _businessConfigs = businessConfigs;
            _wallet = wallet;
            _works = new List<WorkData>();
        }

        public void Initialize()
        {
            foreach (BusinessType businessType in _businessConfigs.GetAllBusinesses())
            {
                _works.Add(new WorkData(businessType, GetCurrentProductionValue(businessType), 
                    GetCurrentProductPrice(businessType), 0, 10)); 
                //Здесь должна быть инициализация нормальными значениями
            }
        }

        public (int, int) GetCycleValues(BusinessType businessType)
        {
            WorkData workData = _works.First(x => x.BusinessType == businessType);

            return new(workData.CurrentCycleValue, workData.FullCycleValue);
        }

        public ulong GetCurrentProductPrice(BusinessType businessType)
        {
            int upgradeLevel = _businessPersistent.GetPriceUpgradeLevel(businessType);
            return _businessConfigs.GetCurrentProductPrice(businessType, upgradeLevel);
        }

        public int GetCurrentProductionValue(BusinessType businessType)
        {
            int upgradeLevel = _businessPersistent.GetProductionUpgradeLevel(businessType);
            return _businessConfigs.GetCurrentProductionValue(businessType, upgradeLevel);
        }
        
        public ulong GetCurrentPassiveIncome(BusinessType businessType)
        {
            return 0; //Реализовать после добавления менеджера
        }
        
        public ulong GetCurrentProductionUpgradeCost(BusinessType businessType)
        {
            int upgradeLevel = _businessPersistent.GetProductionUpgradeLevel(businessType);
            return _businessConfigs.GetCurrentProductionUpgradeCost(businessType, upgradeLevel);
        }
        
        public ulong GetCurrentPriceUpgradeCost(BusinessType businessType)
        {
            int upgradeLevel = _businessPersistent.GetPriceUpgradeLevel(businessType);
            return _businessConfigs.GetCurrentPriceUpgradeCost(businessType, upgradeLevel);
        }
        
        public int GetCurrentProductionUpgradeValue(BusinessType businessType)
        {
            int upgradeLevel = _businessPersistent.GetProductionUpgradeLevel(businessType);
            return _businessConfigs.GetCurrentProductionUpgradeValue(businessType, upgradeLevel);
        }

        public ulong GetCurrentPriceUpgradeValue(BusinessType businessType)
        {
            int upgradeLevel = _businessPersistent.GetPriceUpgradeLevel(businessType);
            return _businessConfigs.GetCurrentPriceUpgradeValue(businessType, upgradeLevel);
        }

        public bool TryUpgradeProduction(BusinessType businessType)
        {
            ulong upgradeCost = GetCurrentProductionUpgradeCost(businessType);

            if (_wallet.IsEnough(Currency.Dollar, upgradeCost))
            {
                _wallet.Spend(Currency.Dollar, upgradeCost);
                _businessPersistent.SetProductionUpgradeLevel(businessType, 
                    _businessPersistent.GetProductionUpgradeLevel(businessType) + 1);
                UpdateWorkData(businessType);
                return true; 
            }
            
            return false;
        }
        
        public bool TryUpgradePrice(BusinessType businessType)
        {
            ulong upgradeCost = GetCurrentPriceUpgradeCost(businessType);
            
            if (_wallet.IsEnough(Currency.Dollar, upgradeCost))
            {
                _wallet.Spend(Currency.Dollar, upgradeCost);
                _businessPersistent.SetPriceUpgradeLevel(businessType,
                    _businessPersistent.GetPriceUpgradeLevel(businessType) + 1);
                UpdateWorkData(businessType);
                return true;
            }
            
            return false;
        }

        public void AddWorkClick(BusinessType businessType)
        {
            WorkData work = _works.First(w => w.BusinessType == businessType);
            work.CurrentCycleValue += 1;

            if (work.CurrentCycleValue >= work.FullCycleValue)
            {
                work.CurrentCycleValue = 0;
                ulong income = (ulong)work.ProductionValue * work.Price;
                _wallet.Add(Currency.Dollar, income);
                IncomeReceived?.Invoke(businessType, income);
            }
            
            WorkCycleUpdated?.Invoke(businessType, work.CurrentCycleValue, work.FullCycleValue);
        }
        
        private void UpdateWorkData(BusinessType businessType)
        {
            WorkData workData = _works.First(w => w.BusinessType == businessType);
            workData.Price = GetCurrentProductPrice(businessType);
            workData.ProductionValue = GetCurrentProductionValue(businessType);
        }
    }
}