using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.SaveData;
using Main.CodeBase.Utilities;
using YG;

namespace Main.CodeBase.Infrastructure.Services.APIService
{
    public class APIClientYG : IAPIClient
    {
        private readonly IStartingProgressConfigs _startingProgressConfigs;
        
        private PlayerProgress _playerProgress;

        public APIClientYG(IStartingProgressConfigs startingProgressConfigs)
        {
            _startingProgressConfigs = startingProgressConfigs;
        }

        public bool IsInitialized => YG2.isSDKEnabled;

        public void GamePlayStart()
        {
            YG2.GameplayStart();
        }

        public void GamePlayStop()
        {
            YG2.GameplayStop();
        }

        public async UniTask<PlayerProgress> LoadPlayerProgress()
        {
            string loadedProgress = YG2.saves.PlayerProgressJson;

            _playerProgress = string.IsNullOrEmpty(loadedProgress) 
                ? new PlayerProgress(await _startingProgressConfigs.GetStartingProgress()) 
                : loadedProgress.FromJson<PlayerProgress>();
            
            return _playerProgress;
        }

        public void SavePlayerProgress()
        {
            YG2.SaveProgress();
        }
    }
}

namespace YG
{
    public partial class SavesYG
    {
        public string PlayerProgressJson;
    }
}