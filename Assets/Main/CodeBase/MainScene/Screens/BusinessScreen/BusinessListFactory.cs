using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessList;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen
{
    public class BusinessListFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _parent;
        private readonly BusinessListsPrefabsRepository _prefabsRepository;
        private readonly DiContainer _container;

        public BusinessListFactory(IAssetProvider assetProvider, Transform parent, 
            BusinessListsPrefabsRepository prefabsRepository, DiContainer container)
        {
            _assetProvider = assetProvider;
            _parent = parent;
            _prefabsRepository = prefabsRepository;
            _container = container;
        }

        public async UniTask<BusinessListController> CreateBusinessList(BusinessType businessType)
        {
            AssetReferenceGameObject prefabRef = _prefabsRepository.GetPrefabRef(businessType);
            GameObject prefab = await _assetProvider.Load<GameObject>(prefabRef);
            BusinessListView view = Object.Instantiate(prefab, _parent).GetComponent<BusinessListView>();
            BusinessListController controller = _container.Instantiate<BusinessListController>();
            controller.Initialize(view);
            
            return controller;
        }
    }
}