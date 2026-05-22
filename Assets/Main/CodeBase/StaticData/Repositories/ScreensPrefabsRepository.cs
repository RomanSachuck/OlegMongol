using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main.CodeBase.StaticData.Repositories
{
    [CreateAssetMenu(fileName = "ScreensPrefabsRepository", menuName = "Repositories/ScreensPrefabsRepository")]
    public class ScreensPrefabsRepository : ScriptableObject
    {
        [SerializeField] private ScreenPrefabReference[] _prefabs;
        
        [field:SerializeField] public AssetReferenceGameObject WalletViewRef {get; private set;}
        [field:SerializeField] public AssetReferenceGameObject BottomPanelRef {get; private set;}

        public AssetReferenceGameObject GetScreenPrefabRef(ScreenType screenType) => 
            _prefabs.First(s => s.ScreenType == screenType).PrefabRef;
    }
    
    [Serializable]
    public class ScreenPrefabReference
    {
        [field:SerializeField] public ScreenType ScreenType { get; private set; }
        [field:SerializeField] public AssetReferenceGameObject PrefabRef { get; private set; }
    }

    public enum ScreenType
    {
        Relax = 0,
        Business = 1,
        Investments = 2,
        Clothes = 3,
        Housing = 4,
    }
}