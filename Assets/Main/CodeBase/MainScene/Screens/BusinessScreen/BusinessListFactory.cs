using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessList;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen
{
    public class BusinessListFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _parent;
        private readonly BusinessListsPrefabsRepository _prefabsRepository;

        public BusinessListFactory(IAssetProvider assetProvider, Transform parent, BusinessListsPrefabsRepository prefabsRepository)
        {
            _assetProvider = assetProvider;
            _parent = parent;
            _prefabsRepository = prefabsRepository;
        }

        public async UniTask<BusinessListController> CreateBusinessList(BusinessType businessType)
        {
            AssetReferenceGameObject prefabRef = _prefabsRepository.GetPrefabRef(businessType);
            GameObject prefab = await _assetProvider.Load<GameObject>(prefabRef);
            BusinessListView view = Object.Instantiate(prefab, _parent).GetComponent<BusinessListView>();
            return new BusinessListController(view);
        }
    }
}