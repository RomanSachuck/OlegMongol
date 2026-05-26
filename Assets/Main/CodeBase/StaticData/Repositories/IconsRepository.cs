using System;
using System.Linq;
using Main.CodeBase.StaticData.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main.CodeBase.StaticData.Repositories
{
    [CreateAssetMenu(fileName = "IconsRepository", menuName = "Repositories/IconsRepository")]
    public class IconsRepository : ScriptableObject
    {
        [SerializeField] private ManagerPortrait[] _managerPortraits;
        
        public AssetReferenceSprite GetManagerPortrait(ManagerType managerType) =>
            _managerPortraits.First(m => m.ManagerType == managerType).SpriteRef;
    }

    [Serializable]
    public class ManagerPortrait
    {
        [field:SerializeField] public ManagerType ManagerType { get; private set; }
        [field:SerializeField] public AssetReferenceSprite SpriteRef { get; private set; }
    }
}