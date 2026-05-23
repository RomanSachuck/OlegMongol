using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessList
{
    public class BusinessListController
    {
        private readonly IBusinessConfigs _businessConfigs;
        private readonly IHousePersistent _housePersistent;
        private readonly IClothesPersistent _clothesPersistent;
        
        private BusinessListView _view;

        public BusinessListController(IBusinessConfigs businessConfigs, IHousePersistent housePersistent,
            IClothesPersistent clothesPersistent)
        {
            _businessConfigs = businessConfigs;
            _housePersistent = housePersistent;
            _clothesPersistent = clothesPersistent;
        }

        public void Initialize(BusinessListView view)
        {
            _view = view;
        }
    }
}