using Cysharp.Threading.Tasks;
using Main.CodeBase.Buttons;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.MainScene.BottomPanel;
using Main.CodeBase.MainScene.Screens;
using Main.CodeBase.StaticData.Repositories;
using Main.CodeBase.Systems.WalletSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Main.CodeBase.MainScene.ScreenManagement
{
    public class ScreenFactory
    {
        private readonly Transform _mainCanvas;
        private readonly Transform _canvasOverlay;
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;
        private readonly ScreensPrefabsRepository _prefabsRepository;

        public ScreenFactory(Transform mainCanvas, Transform canvasOverlay, 
            ScreensPrefabsRepository prefabsRepository, IAssetProvider assetProvider, 
            DiContainer container)
        {
            _mainCanvas = mainCanvas;
            _canvasOverlay = canvasOverlay;
            _prefabsRepository = prefabsRepository;
            _assetProvider = assetProvider;
            _container = container;
        }

        public async UniTask<ScreenViewAbstract> CreateScreen(ScreenType screenType)
        {
            return await InstantiatePrefabForComponent
                <ScreenViewAbstract>(_prefabsRepository.GetScreenPrefabRef(screenType), _mainCanvas);
        }
        
        public async UniTask<BottomPanelView> CreateBottomPanel()
        {
            return await InstantiatePrefabForComponent
                <BottomPanelView>(_prefabsRepository.BottomPanelRef, _canvasOverlay);
        }

        public async UniTask<WalletView> CreateWalletView()
        {
            return await InstantiatePrefabForComponent
                <WalletView>(_prefabsRepository.WalletViewRef, _canvasOverlay);
        }

        public async UniTask<SimpleButton> CreateSettingsButton()
        {
            return await InstantiatePrefabForComponent
                <SimpleButton>(_prefabsRepository.SettingsButton, _canvasOverlay);
        }
        
        private async UniTask<T> InstantiatePrefabForComponent<T>(AssetReferenceGameObject prefabRef, Transform parent)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(prefabRef);
            return _container.InstantiatePrefab(prefab, parent).GetComponent<T>();
        }
    }
}