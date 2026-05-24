using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.Systems.WorkSystem;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessUpgrade
{
    public class BusinessUpgradePanelController
    {
        private readonly WorkController _workController;

        public BusinessUpgradePanelController(WorkController workController)
        {
            _workController = workController;
        }
        
        public void Build(BusinessUpgradePanelView view, BusinessType businessType)
        {
            view.Build(CreatePanelBuildData(businessType));
        }
        
        private UpgradePanelBuildData CreatePanelBuildData(BusinessType businessType)
        {
            (int, int) cycleValues = _workController.GetCycleValues(businessType);
            
            return new UpgradePanelBuildData(businessType, cycleValues.Item1, cycleValues.Item2, 1, 0,
                1, 5, 50, 1, 3);
        }
    }
}