using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.MainScene.Screens.BusinessScreen.Manager;
using Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen
{
    public class BusinessScreenController : ScreenControllerAbstract
    {
        private readonly WorkListFactory _workListFactory;
        private readonly ManagerPanelFactory _managerPanelFactory;
        private readonly IBusinessConfigs _businessConfigs;
        private readonly List<WorkListController> _workListControllers;

        private BusinessScreenView _view;

        private bool _initialized;
        
        public BusinessScreenController(WorkListFactory workListFactory, 
            ManagerPanelFactory managerPanelFactory, IBusinessConfigs businessConfigs)
        {
            _workListFactory = workListFactory;
            _managerPanelFactory = managerPanelFactory;
            _businessConfigs = businessConfigs;
            _workListControllers = new List<WorkListController>();
        }
        
        public override async UniTask Initialize(ScreenViewAbstract view)
        {
            _view = (BusinessScreenView) view;
            
            SpawnBusinessLists().Forget();
        }

        public override void Open()
        {
            _view.gameObject.SetActive(true);

            if (_initialized)
            {
                foreach (WorkListController controller in _workListControllers)
                {
                    controller.OpenManagerPanelEvent += OpenManagerPanel;
                    controller.Start();
                }
            }
        }

        public override void Close()
        {
            _view.gameObject.SetActive(false);
            
            foreach (WorkListController controller in _workListControllers)
            {
                controller.OpenManagerPanelEvent -= OpenManagerPanel;
                controller.Stop();
            }
        }

        private async UniTaskVoid SpawnBusinessLists()
        {
            foreach (BusinessType businessType in _businessConfigs.GetAllBusinesses())
            {
                WorkListController controller = await _workListFactory.CreateBusinessList(businessType);
                controller.OpenManagerPanelEvent += OpenManagerPanel;
                controller.Start();
                _workListControllers.Add(controller);
            }
            
            _initialized = true;
        }

        private void OpenManagerPanel(BusinessType businessType)
        {
            _managerPanelFactory.Create(businessType).Forget();
        }
    }
}