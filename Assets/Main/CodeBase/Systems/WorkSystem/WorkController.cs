using System;
using System.Collections.Generic;
using System.Linq;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.Infrastructure.Services.TimeService;
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
        private readonly IHousePersistent _housePersistent;
        private readonly IClothesPersistent _clothesPersistent;
        private readonly Wallet _wallet;
        private readonly ITimeService _timeService;
        
        private readonly List<WorkData> _works;

        public WorkController(IBusinessPersistent businessPersistent, IBusinessConfigs businessConfigs,
            IHousePersistent housePersistent, IClothesPersistent clothesPersistent,
            Wallet wallet, ITimeService timeService)
        {
            _businessPersistent = businessPersistent;
            _businessConfigs = businessConfigs;
            _housePersistent = housePersistent;
            _clothesPersistent = clothesPersistent;
            _wallet = wallet;
            _timeService = timeService;
            _works = new List<WorkData>();
        }

        public void Initialize()
        {
            foreach (BusinessType businessType in _businessConfigs.GetAllBusinesses())
            {
                _works.Add(new WorkData(businessType, 1, 1, 0, 10)); 
                //Здесь должна быть инициализация нормальными значениями
            }
        }

        public (int, int) GetCycleValues(BusinessType businessType)
        {
            WorkData workData = _works.First(x => x.BusinessType == businessType);

            return new(workData.CurrentCycleValue, workData.FullCycleValue);
        }
        
        public void AddWorkClick(BusinessType businessType)
        {
            WorkData work = _works.First(w => w.BusinessType == businessType);
            work.CurrentCycleValue += 1;

            if (work.CurrentCycleValue >= work.FullCycleValue)
            {
                work.CurrentCycleValue = 0;
                ulong income = (ulong)work.ProductionAmount * work.Price;
                _wallet.Add(Currency.Dollar, income);
                IncomeReceived?.Invoke(businessType, income);
            }
            
            WorkCycleUpdated?.Invoke(businessType, work.CurrentCycleValue, work.FullCycleValue);
        }
    }
}