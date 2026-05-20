using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Main.CodeBase.Infrastructure.Services.AssetManagment
{
    public interface IAssetProvider
    {
        UniTask DownloadDependency(AssetReference assetReference);
        UniTask DownloadDependency(string assetPath);
        UniTask<T> Load<T>(AssetReference assetReference) where T : class;
        UniTask<T> Load<T>(string assetPath) where T : class;
        void Release(AssetReference assetReference);
        void Release(string assetPath);
        void CleanUp();
    }
}