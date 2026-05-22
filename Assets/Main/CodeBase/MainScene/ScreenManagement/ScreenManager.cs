using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using Main.CodeBase.MainScene.BottomPanel;
using Main.CodeBase.StaticData.Repositories;

namespace Main.CodeBase.MainScene.ScreenManagement
{
    public class ScreenManager
    {
        private readonly ScreenFactory _screenFactory;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly BottomPanelController _bottomPanelController;

        public ScreenManager(ScreenFactory screenFactory, ILoadingCurtain loadingCurtain,
            BottomPanelController bottomPanelController)
        {
            _screenFactory = screenFactory;
            _loadingCurtain = loadingCurtain;
            _bottomPanelController = bottomPanelController;

            _bottomPanelController.SelectedScreenChanged += OpenScreen;
        }

        public void Destroy()
        {
            _bottomPanelController.SelectedScreenChanged -= OpenScreen;
            _bottomPanelController.Destroy();
        }

        public async UniTask CreateBottomPanel()
        {
            BottomPanelView view = await _screenFactory.CreateBottomPanel();
            _bottomPanelController.Initialize(view);
        }

        public async UniTask CreateWalletView()
        {
            await _screenFactory.CreateWalletView();
        }

        public async UniTask CreateSettingsButton()
        {
            
        }
        
        public UniTask OpenScreen(ScreenType screenType)
        {
            throw new System.NotImplementedException();
        }
    }
}