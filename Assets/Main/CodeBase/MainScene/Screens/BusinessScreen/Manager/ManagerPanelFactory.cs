using Cysharp.Threading.Tasks;
using Main.CodeBase.Canvases;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.Manager
{
    public class ManagerPanelFactory : ITickable
    {
        private const float TimeToDestroy = 10;
        
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly AssetReferenceGameObject _panelPrefab;
        private readonly IconsRepository _iconsRepository;
        private readonly Transform _canvasOverlay;

        private ManagerPanelView _panel;
        private ManagerPanelController _controller;

        private bool _readyForDestroy;
        private float _timeToDestroyCounter;

        public ManagerPanelFactory(IAssetProvider assetProvider, DiContainer diContainer,
            AssetReferenceGameObject panelPrefab, OverlayCanvasTransform canvasOverlay,
            IconsRepository iconsRepository)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
            _panelPrefab = panelPrefab;
            _iconsRepository = iconsRepository;
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
                _panel = _diContainer.InstantiatePrefab(prefab, _canvasOverlay).GetComponent<ManagerPanelView>();
                _panel.Closed += OnPanelClosed;
                _controller = _diContainer.Instantiate<ManagerPanelController>();
                _controller.Initialize(_panel);
            }
            
            Sprite managerPortrait = await _assetProvider.Load<Sprite>(_iconsRepository
                .GetManagerPortrait(_controller.GetManagerType(businessType)));
            
            _controller.Build(businessType, managerPortrait);
            
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