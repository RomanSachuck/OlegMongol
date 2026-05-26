using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.Systems.WorkSystem;
using UnityEngine;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.Manager
{
    public class ManagerPanelController
    {
        private readonly WorkController _workController;
        
        private ManagerPanelView _view;
        private BusinessType _businessType;

        public ManagerPanelController(WorkController workController)
        {
            _workController = workController;
        }
        
        public void Initialize(ManagerPanelView view)
        {
            _view = view;
            _view.UpgradeButtonClick += OnManagerUpgradeButtonClick;
        }

        public void Build(BusinessType businessType, Sprite managerPortrait)
        {
            _businessType = businessType;
            
            _view.Build(CreateViewBuildData(businessType, managerPortrait));
        }

        public ManagerType GetManagerType(BusinessType businessType)
        {
            return _workController.GetManagerType(businessType);
        }
        
        private ManagerPanelBuildData CreateViewBuildData(BusinessType businessType, Sprite managerPortrait)
        {
            return new ManagerPanelBuildData(GetManagerType(businessType), 
                _workController.GetManagerClickAmount(businessType), _workController.GetManagerUpgradeCost(businessType),
                _workController.GetManagerLevel(businessType) > 0, managerPortrait);
        }
        
        private void OnManagerUpgradeButtonClick()
        {
            if (_workController.TryUpgradeManager(_businessType)) 
                _view.Rebuild(CreateViewBuildData(_businessType, null));
        }
    }
}