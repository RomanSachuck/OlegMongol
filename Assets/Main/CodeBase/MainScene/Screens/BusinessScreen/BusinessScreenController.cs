using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen
{
    public class BusinessScreenController : ScreenControllerAbstract
    {
        private readonly BusinessListFactory _businessListFactory;
        private readonly IBusinessConfigs _businessConfigs;

        private BusinessScreenView _view;

        public BusinessScreenController(BusinessListFactory businessListFactory, IBusinessConfigs businessConfigs)
        {
            _businessListFactory = businessListFactory;
            _businessConfigs = businessConfigs;
        }
        
        public override async UniTask Initialize(ScreenViewAbstract view)
        {
            _view = (BusinessScreenView) view;
            
            SpawnBusinessLists().Forget();
        }

        public override void Open()
        {
            _view.gameObject.SetActive(true);
        }

        public override void Close()
        {
            _view.gameObject.SetActive(false);
        }

        private async UniTaskVoid SpawnBusinessLists()
        {
            foreach (BusinessType businessType in _businessConfigs.GetAllBusinesses())
            {
                await _businessListFactory.CreateBusinessList(businessType);
            }
        }
    }
}