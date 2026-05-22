using Cysharp.Threading.Tasks;

namespace Main.CodeBase.MainScene.Screens.ClothesScreen
{
    public class ClothesScreenController : ScreenControllerAbstract
    {
        private ClothesScreenView _view;
        
        public override async UniTask Initialize(ScreenViewAbstract view)
        {
            base.Initialize(view).Forget();
            
            _view = view as ClothesScreenView;
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