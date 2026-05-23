using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen
{
    public class WorkListFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _parent;
        private readonly BusinessListsPrefabsRepository _prefabsRepository;
        private readonly DiContainer _container;

        public WorkListFactory(IAssetProvider assetProvider, Transform parent, 
            BusinessListsPrefabsRepository prefabsRepository, DiContainer container)
        {
            _assetProvider = assetProvider;
            _parent = parent;
            _prefabsRepository = prefabsRepository;
            _container = container;
        }

        public async UniTask<WorkListController> CreateBusinessList(BusinessType businessType)
        {
            AssetReferenceGameObject prefabRef = _prefabsRepository.GetPrefabRef(businessType);
            GameObject prefab = await _assetProvider.Load<GameObject>(prefabRef);
            WorkListView view = _container.InstantiatePrefab(prefab, _parent).GetComponent<WorkListView>();
            WorkListController controller = _container.Instantiate<WorkListController>();
            controller.Initialize(businessType, view);
            
            return controller;
        }
    }
}