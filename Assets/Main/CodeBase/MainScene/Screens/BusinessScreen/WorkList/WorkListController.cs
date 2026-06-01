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
        public event Action<BusinessType> OpenManagerPanelEvent;
        
        private readonly IBusinessConfigs _businessConfigs;
        private readonly IHousePersistent _housePersistent;
        private readonly IClothesPersistent _clothesPersistent;
        private readonly Localization _localization;
        private readonly WorkController _workController;

        private  BusinessType _businessType;
        private WorkListView _view;

        private bool _expanded;

        public WorkListController(IBusinessConfigs businessConfigs, 
            IHousePersistent housePersistent, IClothesPersistent clothesPersistent,
            Localization localization, WorkController workController)
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
            _view.ClickExpandButton += OnClickExpandButton;
            _view.ProductionUpgradeButtonClick += OnProductionUpgradeButtonClick;
            _view.PriceUpgradeButtonClick += OnPriceUpgradeButtonClick;
            _view.ManagerButtonClick += OnManagerButtonClick;

            _workController.WorkCycleUpdated += OnWorkCycleUpdated;
            _workController.IncomeReceived += OnIncomeReceived;
            _workController.ManagerUpgraded += OnManagerUpgraded;
        }

        public void Stop()
        {
            _view.ClickWorkButton -= OnClickWorkButton;
            _view.ClickExpandButton -= OnClickExpandButton;
            _view.ProductionUpgradeButtonClick -= OnProductionUpgradeButtonClick;
            _view.PriceUpgradeButtonClick -= OnPriceUpgradeButtonClick;
            _view.ManagerButtonClick -= OnManagerButtonClick;
            
            _workController.WorkCycleUpdated -= OnWorkCycleUpdated;
            _workController.IncomeReceived -= OnIncomeReceived;
            _workController.ManagerUpgraded -= OnManagerUpgraded;
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
        
        private void OnClickExpandButton()
        {
            _expanded = !_expanded;
            _view.SetExpand(_expanded);
        }

        private void OnClickWorkButton()
        {
            _workController.AddWorkClick(_businessType);
        }
        
        private void OnProductionUpgradeButtonClick()
        {
            if (_workController.TryUpgradeProduction(_businessType))
            {
                _view.Rebuild(CreateBuildData());
            }
        }

        private void OnPriceUpgradeButtonClick()
        {
            if (_workController.TryUpgradePrice(_businessType))
            {
                _view.Rebuild(CreateBuildData());
            }
        }
        
        private void OnManagerButtonClick()
        {
            OpenManagerPanelEvent?.Invoke(_businessType);
        }

        private void OnManagerUpgraded(BusinessType businessType)
        {
            if (businessType == _businessType)
            {
                _view.Rebuild(CreateBuildData());
            }
        }
        
        private void InitView()
        {
            BusinessConfigs config = _businessConfigs.GetBusinessConfig(_businessType);
            IEnumerable<HouseType> openedHouses = _housePersistent.GetOpenedHouses();
            IEnumerable<ClothesType> openedClothes = _clothesPersistent.GetOpenedClothes();
            bool businessUnlocked = IsUnlocked(config, openedClothes, openedHouses);
            string lockedTitle = businessUnlocked ? "" : CreateLockedTitle(config, openedClothes, openedHouses);
            
            _view.Initialize(businessUnlocked, lockedTitle, CreateBuildData());
        }

        private WorkListBuildData CreateBuildData()
        {
            (int, int) cycleValues = _workController.GetCycleValues(_businessType);
            
            return new WorkListBuildData(_businessType, cycleValues.Item1, cycleValues.Item2, 
                _workController.GetCurrentProductPrice(_businessType), _workController.GetCurrentPassiveIncome(_businessType),
                _workController.GetCurrentProductionValue(_businessType), _workController.GetCurrentProductionUpgradeCost(_businessType), 
                _workController.GetCurrentPriceUpgradeCost(_businessType), _workController.GetCurrentProductionUpgradeValue(_businessType), 
                _workController.GetCurrentPriceUpgradeValue(_businessType));
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