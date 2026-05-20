using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Main.CodeBase.Infrastructure.Services.SceneLoadService
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ZenjectSceneLoader _zenjectSceneLoader;
        
        private LoadingCurtain _loadingCurtain;
        
        public SceneLoader(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        public void ShowCurtain()
        {
            _loadingCurtain.Show();
        }

        public void HideCurtain()
        {
            _loadingCurtain.Hide();
        }
        
        public void SetCurtain(LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }

        public void Load(SceneId sceneId) =>
            LoadScene(sceneId).Forget();

        private async UniTaskVoid LoadScene(SceneId sceneId, Action<DiContainer> callback = null)
        {
            AsyncOperation load = _zenjectSceneLoader.LoadSceneAsync((int)sceneId, LoadSceneMode.Single, callback);
            
            while (!load.isDone)
            {
                await UniTask.Yield();
            }
        }
    }
}