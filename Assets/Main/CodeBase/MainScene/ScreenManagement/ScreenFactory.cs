using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.MainScene.BottomPanel;
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

        public async UniTask<BottomPanelView> CreateBottomPanel()
        {
            AssetReferenceGameObject prefabRef = _prefabsRepository.BottomPanelRef;
            GameObject prefab = await _assetProvider.Load<GameObject>(prefabRef);
            return _container.InstantiatePrefab(prefab, _canvasOverlay).GetComponent<BottomPanelView>();
        }

        public async UniTask<WalletView> CreateWalletView()
        {
            AssetReferenceGameObject prefabRef = _prefabsRepository.WalletViewRef;
            GameObject prefab = await _assetProvider.Load<GameObject>(prefabRef);
            return _container.InstantiatePrefab(prefab, _canvasOverlay).GetComponent<WalletView>();
        }
    }
}