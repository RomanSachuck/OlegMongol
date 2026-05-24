using Cysharp.Threading.Tasks;
using Main.CodeBase.Canvases;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.StaticData.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessUpgrade
{
    public class BusinessUpgradePanelFactory : ITickable
    {
        private const float TimeToDestroy = 10;
        
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly AssetReferenceGameObject _panelPrefab;
        private readonly Transform _canvasOverlay;

        private BusinessUpgradePanelView _panel;
        private BusinessUpgradePanelController _controller;

        private bool _readyForDestroy;
        private float _timeToDestroyCounter;

        public BusinessUpgradePanelFactory(IAssetProvider assetProvider, DiContainer diContainer,
            AssetReferenceGameObject panelPrefab, OverlayCanvasTransform canvasOverlay)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
            _panelPrefab = panelPrefab;
            _canvasOverlay = canvasOverlay.transform;
        }
        
        public void Tick()
        {
            if (_panel != null && _readyForDestroy)
            {
                _timeToDestroyCounter += Time.deltaTime;
            
                if(_timeToDestroyCounter >= TimeToDestroy)
                {
                    _panel.Closed -= OnPanelClosed;
                    Object.Destroy(_panel.gameObject);
                    _assetProvider.Release(_panelPrefab);
                }
            }
        }
        
        public async UniTaskVoid Create(BusinessType businessType)
        {
            if (_panel == null)
            {
                GameObject prefab = await _assetProvider.Load<GameObject>(_panelPrefab);
                _panel = _diContainer.InstantiatePrefab(prefab, _canvasOverlay).GetComponent<BusinessUpgradePanelView>();
                _panel.Closed += OnPanelClosed;
                _controller = _diContainer.Instantiate<BusinessUpgradePanelController>();
                _controller.Initialize(_panel);
            }
            
            _controller.Build(businessType);
            
            _panel.transform.SetAsLastSibling();
            _panel.Open();
            _readyForDestroy = false;
        }

        private void OnPanelClosed()
        {
            _timeToDestroyCounter = 0;
            _readyForDestroy = true;
        }
    }
}