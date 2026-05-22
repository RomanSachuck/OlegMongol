using Cysharp.Threading.Tasks;

namespace Main.CodeBase.MainScene.Screens.HousingScreen
{
    public class HousingScreenController : ScreenControllerAbstract
    {
        private HousingScreenView _view;
        
        public override async UniTask Initialize(ScreenViewAbstract view)
        {
            _view = view as HousingScreenView;
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