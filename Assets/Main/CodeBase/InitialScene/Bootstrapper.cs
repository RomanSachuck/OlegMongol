using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.APIService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.InitialScene
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private LoadingSlider _loadingSlider;
        
        private IAPIClient _apiClient;
        private ISceneLoader _sceneLoader;
        private IPersistentProgress _persistentProgress;

        private int _loadingPercent = 25;

        [Inject]
        private void Construct(IAPIClient apiClient, ISceneLoader sceneLoader, IPersistentProgress persistentProgress)
        {
            _apiClient = apiClient;
            _sceneLoader = sceneLoader;
            _persistentProgress = persistentProgress;
        }

        private void Start()
        {
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            UpdateLoadingView();
            
            DontDestroyOnLoad(_loadingCurtain.gameObject);
            _sceneLoader.SetCurtain(_loadingCurtain);
            
            while (_apiClient.IsInitialized == false)
            {
                await UniTask.Delay(Random.Range(50, 300));
                UpdateLoadingView();
            }

            await InitPersistentProgress();
            
            UpdateLoadingView(true);
            
            LoadMainScene();
        }

        private async UniTask InitPersistentProgress()
        {
            _persistentProgress.CachePlayerProgress(await _apiClient.GetPlayerProgress());
        }

        private void LoadMainScene()
        {
            _sceneLoader.Load(SceneId.Main);
        }
        
        private void UpdateLoadingView(bool isFinished = false)
        {
            if (isFinished)
            {
                _loadingSlider.SetValue(100, false);
                return;
            }
            
            if (_loadingPercent >= 100)
            {
                _loadingPercent = Random.Range(21, 75);
                _loadingSlider.SetValue(_loadingPercent++, false);
                _loadingSlider.SetNextInfo();
                return;
            }
            
            _loadingSlider.SetValue(_loadingPercent++);
        }
    }
}