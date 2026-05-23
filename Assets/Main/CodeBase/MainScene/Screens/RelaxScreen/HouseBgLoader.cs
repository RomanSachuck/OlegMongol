using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main.CodeBase.MainScene.Screens.RelaxScreen
{
    public class HouseBgLoader
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ScreenBackgroundsRepository _bgRepository;
        
        private AssetReferenceSprite _loadedSpriteRef;

        public HouseBgLoader(IAssetProvider assetProvider, ScreenBackgroundsRepository bgRepository)
        {
            _assetProvider = assetProvider;
            _bgRepository = bgRepository;
        }

        public async UniTask<Sprite> LoadBg(HouseType houseType)
        {
            AssetReferenceSprite spriteRef = _bgRepository.GetHouseSpriteRef(houseType);

            if (_loadedSpriteRef != null && spriteRef != _loadedSpriteRef)
            {
                _assetProvider.Release(_loadedSpriteRef);
                _loadedSpriteRef = spriteRef;
            }
            
            return await _assetProvider.Load<Sprite>(spriteRef);
        }
    }
}