using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Main.CodeBase.Buttons;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using Main.CodeBase.MainScene.BottomPanel;
using Main.CodeBase.MainScene.Screens;
using Main.CodeBase.MainScene.Screens.BusinessScreen;
using Main.CodeBase.MainScene.Screens.ClothesScreen;
using Main.CodeBase.MainScene.Screens.HousingScreen;
using Main.CodeBase.MainScene.Screens.InvestmentsScreen;
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

        private readonly Dictionary<ScreenType, ScreenControllerAbstract> _screens;
        private ScreenControllerAbstract _openedScreen;
        
        public ScreenManager(ScreenFactory screenFactory, ILoadingCurtain loadingCurtain,
            BottomPanelController bottomPanelController, SettingsPanelController settingsPanelController,
            RelaxScreenController relaxScreenController, BusinessScreenController businessScreenController,
            InvestmentsScreenController investmentsScreenController, HousingScreenController housingScreenController,
            ClothesScreenController clothesScreenController)
        {
            _screenFactory = screenFactory;
            _loadingCurtain = loadingCurtain;
            _bottomPanelController = bottomPanelController;
            _settingsPanelController = settingsPanelController;

            _screens = new Dictionary<ScreenType, ScreenControllerAbstract>()
            {
                { ScreenType.Relax, relaxScreenController },
                { ScreenType.Business, businessScreenController },
                { ScreenType.Investments, investmentsScreenController },
                { ScreenType.Housing, housingScreenController },
                { ScreenType.Clothes, clothesScreenController },
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
                await UniTask.Delay(500);
                
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