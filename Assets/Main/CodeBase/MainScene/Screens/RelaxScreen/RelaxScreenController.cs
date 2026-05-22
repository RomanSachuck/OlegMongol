using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;

namespace Main.CodeBase.MainScene.Screens.RelaxScreen
{
    public class RelaxScreenController : ScreenControllerAbstract
    {
        private readonly HouseBgLoader _bgLoader;
        private readonly IHousePersistent _housePersistent;
        
        private RelaxScreenView _view;

        public RelaxScreenController(HouseBgLoader bgLoader, IHousePersistent housePersistent)
        {
            _bgLoader = bgLoader;
            _housePersistent = housePersistent;
        }
        
        public override async UniTask Initialize(ScreenViewAbstract view)
        {
            base.Initialize(view).Forget();
            
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