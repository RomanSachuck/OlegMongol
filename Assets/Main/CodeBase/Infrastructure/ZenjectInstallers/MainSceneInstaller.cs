using Main.CodeBase.MainScene.BottomPanel;
using Main.CodeBase.MainScene.ScreenManagement;
using Main.CodeBase.MainScene.Screens.BusinessScreen;
using Main.CodeBase.MainScene.Screens.ClothesScreen;
using Main.CodeBase.MainScene.Screens.HousingScreen;
using Main.CodeBase.MainScene.Screens.InvestmentsScreen;
using Main.CodeBase.MainScene.Screens.RelaxScreen;
using Main.CodeBase.MainScene.SettingsPanel;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _mainCanvas;
        [SerializeField] private Transform _overlayCanvas;
        [SerializeField] private ScreensPrefabsRepository _screenPrefabsRepository;
        [SerializeField] private ScreenBackgroundsRepository _screenBackgroundsRepository;
        
        public override void InstallBindings()
        {
            BindScreenFactory();
            BindScreenManager();

            BindHouseBgLoader();
            
            BindBottomPanelController();
            BindSettingsPanelController();
            
            BindRelaxScreenController();
            BindBusinessScreenController();
            BindInvestmentsScreenController();
            BindHousingScreenController();
            BindClothesScreenController();
        }

        private void BindHouseBgLoader()
        {
            Container.Bind<HouseBgLoader>().AsSingle()
                .WithArguments(_screenBackgroundsRepository);
        }

        private void BindClothesScreenController()
        {
            Container.Bind<ClothesScreenController>().AsSingle();
        }

        private void BindHousingScreenController()
        {
            Container.Bind<HousingScreenController>().AsSingle();
        }

        private void BindInvestmentsScreenController()
        {
            Container.Bind<InvestmentsScreenController>().AsSingle();
        }

        private void BindBusinessScreenController()
        {
            Container.Bind<BusinessScreenController>().AsSingle();
        }

        private void BindRelaxScreenController()
        {
            Container.Bind<RelaxScreenController>().AsSingle();
        }

        private void BindSettingsPanelController()
        {
            Container.Bind<SettingsPanelController>().AsSingle();
        }

        private void BindBottomPanelController()
        {
            Container.Bind<BottomPanelController>().AsSingle();
        }

        private void BindScreenManager()
        {
            Container.Bind<ScreenManager>().AsSingle();
        }

        private void BindScreenFactory()
        {
            Container.Bind<ScreenFactory>().AsSingle()
                .WithArguments(_mainCanvas, _overlayCanvas, _screenPrefabsRepository);
        }
    }
}