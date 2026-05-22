using Main.CodeBase.MainScene.BottomPanel;
using Main.CodeBase.MainScene.ScreenManagement;
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
        public override void InstallBindings()
        {
            BindScreenFactory();
            BindScreenManager();

            BindBottomPanelController();
            BindSettingsPanelController();
            BindRelaxScreenController();
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