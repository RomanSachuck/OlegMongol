using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.StaticData.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessUpgrade
{
    public class BusinessUpgradePanelFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly AssetReferenceGameObject _panelPrefab;

        private BusinessUpgradePanelView _panel;

        public BusinessUpgradePanelFactory(IAssetProvider assetProvider, DiContainer diContainer,
            AssetReferenceGameObject panelPrefab)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
            _panelPrefab = panelPrefab;
        }
        
        public async UniTaskVoid Create(BusinessType businessType, Transform parent)
        {
            if (_panel == null)
            {
                GameObject prefab = await _assetProvider.Load<GameObject>(_panelPrefab);
                _panel = _diContainer.InstantiatePrefab(prefab, parent).GetComponent<BusinessUpgradePanelView>();
            }
        }
    }
}