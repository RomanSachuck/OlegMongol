using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.Systems.WorkSystem;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessUpgrade
{
    public class BusinessUpgradePanelController
    {
        private readonly WorkController _workController;
        
        private BusinessUpgradePanelView _view;
        private BusinessType _businessType;

        public BusinessUpgradePanelController(WorkController workController)
        {
            _workController = workController;
        }
        
        public void Initialize(BusinessUpgradePanelView view)
        {
            _view = view;
            
            _view.PriceUpgradeButtonClick += OnPriceUpgradeButtonClick;
            _view.ProductionUpgradeButtonClick += OnProductionUpgradeButtonClick;
        }
        
        public void Build(BusinessType businessType)
        {
            _businessType = businessType;
            
            _view.Build(CreatePanelBuildData());
        }

        private UpgradePanelBuildData CreatePanelBuildData()
        {
            (int, int) cycleValues = _workController.GetCycleValues(_businessType);
            
            return new UpgradePanelBuildData(_businessType, cycleValues.Item1, cycleValues.Item2, 
                _workController.GetCurrentProductPrice(_businessType), _workController.GetCurrentPassiveIncome(_businessType),
                _workController.GetCurrentProductionValue(_businessType), _workController.GetCurrentProductionUpgradeCost(_businessType), 
                _workController.GetCurrentPriceUpgradeCost(_businessType), _workController.GetCurrentProductionUpgradeValue(_businessType), 
                _workController.GetCurrentPriceUpgradeValue(_businessType));
        }
        
        private void OnProductionUpgradeButtonClick()
        {
            if (_workController.TryUpgradeProduction(_businessType))
            {
                _view.Build(CreatePanelBuildData());
            }
        }

        private void OnPriceUpgradeButtonClick()
        {
            if (_workController.TryUpgradePrice(_businessType))
            {
                _view.Build(CreatePanelBuildData());
            }
        }
    }
}