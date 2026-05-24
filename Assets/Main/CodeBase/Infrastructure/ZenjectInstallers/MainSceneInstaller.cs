using Main.CodeBase.Canvases;
using Main.CodeBase.MainScene.BottomPanel;
using Main.CodeBase.MainScene.ScreenManagement;
using Main.CodeBase.MainScene.SettingsPanel;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainCanvasTransform _mainCanvas;
        [SerializeField] private OverlayCanvasTransform _overlayCanvas;
        [SerializeField] private ScreensPrefabsRepository _screenPrefabsRepository;
        
        public override void InstallBindings()
        {
            BindCanvases();
            
            BindScreenFactory();
            BindScreenManager();
            
            BindBottomPanelController();
            BindSettingsPanelController();
        }

        private void BindCanvases()
        {
            Container.BindInterfacesAndSelfTo<MainCanvasTransform>().FromInstance(_mainCanvas).AsSingle();
            Container.BindInterfacesAndSelfTo<OverlayCanvasTransform>().FromInstance(_overlayCanvas).AsSingle();
        }

        private void BindSettingsPanelController()
        {
            Container.BindInterfacesAndSelfTo<SettingsPanelController>().AsSingle();
        }

        private void BindBottomPanelController()
        {
            Container.BindInterfacesAndSelfTo<BottomPanelController>().AsSingle();
        }

        private void BindScreenManager()
        {
            Container.BindInterfacesAndSelfTo<ScreenManager>().AsSingle();
        }

        private void BindScreenFactory()
        {
            Container.BindInterfacesAndSelfTo<ScreenFactory>().AsSingle()
                .WithArguments(_screenPrefabsRepository);
        }
    }
}