using System;
using System.Collections.Generic;
using System.Linq;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.Systems.WalletSystem;
using Zenject;

namespace Main.CodeBase.Systems.WorkSystem
{
    public class WorkController : ITickable

    {
        public event Action<BusinessType, int, int> WorkCycleUpdated;
        public event Action<BusinessType, ulong> IncomeReceived;

        private readonly IBusinessPersistent _businessPersistent;
        private readonly IBusinessConfigs _businessConfigs;
        private readonly IManagerConfigs _managerConfigs;
        private readonly Wallet _wallet;

        private readonly List<WorkData> _works;
        private readonly List<Manager> _managers;

        public WorkController(IBusinessPersistent businessPersistent, IBusinessConfigs businessConfigs,
            IManagerConfigs managerConfigs, Wallet wallet)
        {
            _businessPersistent = businessPersistent;
            _businessConfigs = businessConfigs;
            _managerConfigs = managerConfigs;
            _wallet = wallet;

            _works = new List<WorkData>();
            _managers = new List<Manager>();
        }

        public void Initialize()
        {
            foreach (BusinessType businessType in _businessConfigs.GetAllBusinesses())
            {
                int currentCycleValue = _businessPersistent.GetWorkCycleValue(businessType);
                int fullCycleValue = _businessConfigs.GetCycleSize(businessType);
                int managerLevel = _businessPersistent.GetManagerLevel(businessType);

                _works.Add(new WorkData(businessType, GetCurrentProductionValue(businessType),
                    GetCurrentProductPrice(businessType), currentCycleValue, fullCycleValue));

                if (managerLevel > 0)
                {
                    ManagerType managerType = GetManagerType(businessType);
                    Manager manager = new Manager(managerType, businessType);
                    manager.SetTimeToClick(_managerConfigs.GetTimeToClick(managerType, managerLevel));
                    manager.ManagerClicked += AddWorkClick;

                    _managers.Add(manager);
                }
            }
        }

        public void Tick()
        {
            foreach (Manager manager in _managers) 
                manager.UpdateTimer();
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

        public ManagerType GetManagerType(BusinessType businessType)
        {
            return _businessConfigs.GetManagerType(businessType);
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

            _businessPersistent.SetWorkCycleValue(businessType, work.CurrentCycleValue);
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