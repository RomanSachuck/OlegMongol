using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.TimeService;
using Main.CodeBase.SaveData;
using Main.CodeBase.Utilities;
using YG;

namespace Main.CodeBase.Infrastructure.Services.APIService
{
    public class APIClientYG : IAPIClient
    {
        private readonly IStartingProgressConfigs _startingProgressConfigs;

        private PlayerProgress _playerProgress;

        public APIClientYG(IStartingProgressConfigs startingProgressConfigs, ITimeService timeService)
        {
            _startingProgressConfigs = startingProgressConfigs;
            timeService.OneSecondHasPassed += SavePlayerProgress;
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

        public UniTask<long> GetServerTime()
        {
            long time = YG2.ServerTime();
            return new UniTask<long>(time);
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
            YG2.saves.PlayerProgressJson = _playerProgress.ToJson();
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