using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.APIService;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using Main.CodeBase.StaticData.Repositories;
using Main.CodeBase.Systems.WalletSystem;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.InitialScene
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Repositories")]
        [SerializeField] private StartingProgressRepository _startingProgressRepository;
        [SerializeField] private BusinessConfigsRepository _businessConfigsRepository;
        
        [Header("References")]
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private LoadingSimulator _loadingSimulator;
        
        private IAPIClient _apiClient;
        private ISceneLoader _sceneLoader;
        private IPersistentProgress _persistentProgress;
        private IConfigs _configs;
        
        private Wallet _wallet;

        [Inject]
        private void Construct(IAPIClient apiClient, ISceneLoader sceneLoader, 
            IPersistentProgress persistentProgress, IConfigs configs, Wallet wallet)
        {
            _apiClient = apiClient;
            _sceneLoader = sceneLoader;
            _persistentProgress = persistentProgress;
            _configs = configs;
            _wallet = wallet;
        }

        private void Start()
        {
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            _loadingSimulator.RunLoadingSimulation().Forget();
            
            SetLoadingCurtain();

            while (_apiClient.IsInitialized == false) 
                await UniTask.Yield();

            await InitConfigs();
            await InitPersistentProgress();

            InitWallet();
            
            _loadingSimulator.FinishLoadingSimulation();
            
            LoadMainScene();
        }

        private void InitWallet()
        {
            _wallet.Initialize(_persistentProgress);
        }

        private void SetLoadingCurtain()
        {
            DontDestroyOnLoad(_loadingCurtain.gameObject);
            _sceneLoader.SetCurtain(_loadingCurtain);
        }

        private UniTask InitConfigs()
        {
            _configs.CacheStartingProgress(_startingProgressRepository.PlayerProgress);
            _configs.CacheBusinessConfigs(_businessConfigsRepository.Configs);
            return new UniTask();
        }

        private async UniTask InitPersistentProgress()
        {
            _persistentProgress.CachePlayerProgress(await _apiClient.LoadPlayerProgress());
        }

        private void LoadMainScene()
        {
            _sceneLoader.Load(SceneId.Main);
        }
    }
}