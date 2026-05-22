using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main.CodeBase.StaticData.Repositories
{
    [CreateAssetMenu(fileName = "ScreenBackgroundsRepository", menuName = "Repositories/ScreenBackgroundsRepository")]
    public class ScreenBackgroundsRepository : ScriptableObject
    {
        [SerializeField] private HouseBgRef[] _houseBgRefs;
        
        public AssetReferenceSprite GetHouseSpriteRef(HouseType houseType) =>
            _houseBgRefs.First(h => h.HouseType == houseType).SpriteRef;
    }

    [Serializable]
    public class HouseBgRef
    {
        [field:SerializeField]public HouseType HouseType { get; private set; }
        [field:SerializeField]public AssetReferenceSprite SpriteRef { get; private set; }
    }
    
    public enum HouseType
    {
        Street = 0,
        Bando = 1,
    }
}