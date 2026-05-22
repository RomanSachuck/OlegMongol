using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.RelaxScreen
{
    public class RelaxScreenController : ScreenControllerAbstract
    {
        private HouseBgLoader _bgLoader;
        private IHousePersistent _housePersistent;
        
        private RelaxScreenView _view;

        /*public RelaxScreenController(HouseBgLoader bgLoader, IHousePersistent housePersistent)
        {
            _bgLoader = bgLoader;
            _housePersistent = housePersistent;
        }*/

        [Inject]
        private void Construct(HouseBgLoader bgLoader, IHousePersistent housePersistent)
        {
            _bgLoader = bgLoader;
            _housePersistent = housePersistent;
        }
        
        public override async UniTask Initialize(ScreenViewAbstract view)
        {
            _view = (RelaxScreenView) view;

            _view.SetBackground(await _bgLoader.LoadBg(_housePersistent.SelectedHouse));
        }
        
        public override void Open()
        {
            _view.gameObject.SetActive(true);
        }

        public override void Close()
        {
            _view.gameObject.SetActive(false);
        }
    }
}