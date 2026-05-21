using Cysharp.Threading.Tasks;
using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.APIService
{
    public interface IAPIClient
    {
        bool IsInitialized { get; }
        void GamePlayStart();
        void GamePlayStop();
        UniTask<PlayerProgress> GetPlayerProgress();
    }
}