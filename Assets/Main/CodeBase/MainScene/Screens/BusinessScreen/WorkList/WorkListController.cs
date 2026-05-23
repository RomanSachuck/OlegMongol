using System;
using System.Collections.Generic;
using System.Linq;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.Systems.WorkSystem;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList
{
    public class WorkListController
    {
        public event Action<BusinessType> OpenBusinessUpgradePanelEvent;
        
        private readonly IBusinessConfigs _businessConfigs;
        private readonly IHousePersistent _housePersistent;
        private readonly IClothesPersistent _clothesPersistent;
        private readonly ILocalization _localization;
        private readonly WorkController _workController;

        private  BusinessType _businessType;
        private WorkListView _view;

        public WorkListController(IBusinessConfigs businessConfigs, 
            IHousePersistent housePersistent, IClothesPersistent clothesPersistent,
            ILocalization localization, WorkController workController)
        {
            _businessConfigs = businessConfigs;
            _housePersistent = housePersistent;
            _clothesPersistent = clothesPersistent;
            _localization = localization;
            _workController = workController;
        }

        public void Initialize(BusinessType businessType, WorkListView view)
        {
            _businessType = businessType;
            _view = view;
        }

        public void Start()
        {
            InitView();
            
            _view.ClickWorkButton += OnClickWorkButton;
            _view.ClickUpgradeButton += OnClickUpgradeButton;

            _workController.WorkCycleUpdated += OnWorkCycleUpdated;
            _workController.IncomeReceived += OnIncomeReceived;
        }
        
        public void Stop()
        {
            _view.ClickWorkButton -= OnClickWorkButton;
            _view.ClickUpgradeButton -= OnClickUpgradeButton;
            
            _workController.WorkCycleUpdated -= OnWorkCycleUpdated;
            _workController.IncomeReceived -= OnIncomeReceived;
        }
        
        private void OnWorkCycleUpdated(BusinessType businessType, int currentValue, int fullValue)
        {
            if (businessType == _businessType)
            {
                _view.UpdateCycleValues(currentValue, fullValue);
            }
        }
        
        private void OnIncomeReceived(BusinessType businessType, ulong income)
        {
            if (businessType == _businessType)
            {
                _view.ShowIncomeAnimation(income);
            }
        }
        
        private void OnClickUpgradeButton()
        {
            OpenBusinessUpgradePanelEvent?.Invoke(_businessType);
        }

        private void OnClickWorkButton()
        {
            _workController.AddWorkClick(_businessType);
        }

        private void InitView()
        {
            BusinessConfigs config = _businessConfigs.GetBusinessConfig(_businessType);
            IEnumerable<HouseType> openedHouses = _housePersistent.GetOpenedHouses();
            IEnumerable<ClothesType> openedClothes = _clothesPersistent.GetOpenedClothes();
            bool businessUnlocked = IsUnlocked(config, openedClothes, openedHouses);
            string lockedTitle = businessUnlocked ? "" : CreateLockedTitle(config, openedClothes, openedHouses);
            (int, int) cycleValues = _workController.GetCycleValues(_businessType);
            
            _view.Initialize(businessUnlocked, lockedTitle, cycleValues.Item1, cycleValues.Item2);
        }

        private string CreateLockedTitle(BusinessConfigs config, IEnumerable<ClothesType> openedClothes, 
            IEnumerable<HouseType> openedHouses)
        {
            if (openedHouses.Any(h => h == config.RequiredHouse) == false)
                return _localization.GetLockedTitleForHouse(config.RequiredHouse);
            if(openedClothes.Any(c => c == config.RequiredClothes) == false)
                return _localization.GetLockedTitleForClothes(config.RequiredClothes);

            return "";
        }

        private bool IsUnlocked(BusinessConfigs config, IEnumerable<ClothesType> openedClothes, 
            IEnumerable<HouseType> openedHouses)
        {
            return openedHouses.Any(h => h == config.RequiredHouse)
                   && openedClothes.Any(c => c == config.RequiredClothes);
        }
    }
}