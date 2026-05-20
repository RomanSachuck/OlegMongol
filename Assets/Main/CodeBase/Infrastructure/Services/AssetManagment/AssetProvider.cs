using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Main.CodeBase.Infrastructure.Services.AssetManagment
{
    public class AssetsProvider : IAssetProvider
    {
        private readonly Dictionary<AssetReference, AsyncOperationHandle> _cachedHandles = new();
        private readonly Dictionary<AssetReference, object> _cachedAssets = new();
        private readonly Dictionary<string, AsyncOperationHandle> _cachedHandlesByPath = new();
        private readonly Dictionary<string, object> _cachedAssetsByPath = new();

        public async UniTask DownloadDependency(AssetReference assetReference)
        {
            AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(assetReference, true);
            await handle.Task;
        }

        public async UniTask DownloadDependency(string assetPath)
        {
            AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(assetPath, true);
            await handle.Task;
        }

        public async UniTask<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_cachedAssets.TryGetValue(assetReference, out var cachedAsset))
                return cachedAsset as T;
            
            if(_cachedHandles.TryGetValue(assetReference, out var cachedHandle))
                return await cachedHandle.Task as T;
            
            try
            {
                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
                _cachedHandles[assetReference] = handle;
                
                T asset = await handle.Task;
                
                _cachedAssets[assetReference] = asset;
                
                return asset;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to load asset {assetReference}: {e.Message}");
                throw;
            }
        }

        public async UniTask<T> Load<T>(string assetPath) where T : class
        {
            if (_cachedAssetsByPath.TryGetValue(assetPath, out var cachedAsset))
                return cachedAsset as T;
            
            if(_cachedHandlesByPath.TryGetValue(assetPath, out var cachedHandle))
                return await cachedHandle.Task as T;
            
            try
            {
                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetPath);
                _cachedHandlesByPath[assetPath] = handle;
                
                T asset = await handle.Task;
                
                _cachedAssetsByPath[assetPath] = asset;
                
                return asset;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to load asset from path {assetPath}: {e.Message}");
                throw;
            }
        }
        
        public void Release(AssetReference assetReference)
        {
            if (_cachedHandles.TryGetValue(assetReference, out var handle))
            {
                Addressables.Release(handle);
                _cachedHandles.Remove(assetReference);
                _cachedAssets.Remove(assetReference);
            }
        }
        
        public void Release(string assetPath)
        {
            if (_cachedHandlesByPath.TryGetValue(assetPath, out var handle))
            {
                Addressables.Release(handle);
                _cachedHandlesByPath.Remove(assetPath);
                _cachedAssetsByPath.Remove(assetPath);
            }
        }
        
        public void CleanUp()
        {
            foreach (var handle in _cachedHandles.Values)
                Addressables.Release(handle);
                
            foreach (var handle in _cachedHandlesByPath.Values)
                Addressables.Release(handle);
            
            _cachedHandles.Clear();
            _cachedHandlesByPath.Clear();
            _cachedAssets.Clear();
            _cachedAssetsByPath.Clear();
        }
    }
}