using System;
using System.Linq;
using Main.CodeBase.StaticData.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main.CodeBase.StaticData.Repositories
{
    [CreateAssetMenu(fileName = "BusinessListsPrefabsRepository", menuName = "Repositories/BusinessListsPrefabsRepository")]
    public class BusinessListsPrefabsRepository : ScriptableObject
    {
        [SerializeField] private BusinessListPrefabReference[] _prefabs;

        public AssetReferenceGameObject GetPrefabRef(BusinessType businessType) =>
            _prefabs.First(b => b.BusinessType == businessType).PrefabRef;
    }

    [Serializable]
    public class BusinessListPrefabReference
    {
        [field:SerializeField] public BusinessType BusinessType { get; private set; }
        [field:SerializeField] public AssetReferenceGameObject PrefabRef { get; private set; }
    }
}