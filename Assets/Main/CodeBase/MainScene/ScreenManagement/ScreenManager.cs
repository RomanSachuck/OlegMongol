using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Main.CodeBase.Buttons;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using Main.CodeBase.MainScene.BottomPanel;
using Main.CodeBase.MainScene.Screens;
using Main.CodeBase.MainScene.Screens.RelaxScreen;
using Main.CodeBase.MainScene.SettingsPanel;
using Main.CodeBase.StaticData.Repositories;

namespace Main.CodeBase.MainScene.ScreenManagement
{
    public class ScreenManager
    {
        private readonly ScreenFactory _screenFactory;
        private readonly ILoadingCurtain _loadingCurtain;
        
        private readonly BottomPanelController _bottomPanelController;
        private readonly SettingsPanelController _settingsPanelController;
        private readonly RelaxScreenController _relaxScreenController;

        private readonly Dictionary<ScreenType, IScreenController> _screens;
        private IScreenController _openedScreen;
        
        public ScreenManager(ScreenFactory screenFactory, ILoadingCurtain loadingCurtain,
            BottomPanelController bottomPanelController, SettingsPanelController settingsPanelController,
            RelaxScreenController relaxScreenController)
        {
            _screenFactory = screenFactory;
            _loadingCurtain = loadingCurtain;
            _bottomPanelController = bottomPanelController;
            _settingsPanelController = settingsPanelController;
            _relaxScreenController = relaxScreenController;

            _screens = new Dictionary<ScreenType, IScreenController>()
            {
                { ScreenType.Relax, relaxScreenController },
            };
            
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
            SimpleButton button = await _screenFactory.CreateSettingsButton();
            _settingsPanelController.Initialize(button);
        }
        
        public async UniTask OpenScreen(ScreenType screenType)
        {
            if (_screens[screenType].Created == false)
            {
                _loadingCurtain.ShowCurtain();
                
                ScreenViewAbstract screenViewAbstract = await _screenFactory.CreateScreen(screenType);
                _screens[screenType].Initialize(screenViewAbstract);
                
                _loadingCurtain.HideCurtain();
            }
 
            _openedScreen?.Close();
            _screens[screenType].Open();
            _openedScreen = _screens[screenType];
        }
    }
}