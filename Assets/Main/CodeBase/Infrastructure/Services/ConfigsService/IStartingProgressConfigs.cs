using Cysharp.Threading.Tasks;
using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public interface IStartingProgressConfigs
    {
        void CacheStartingProgress(PlayerProgress startingProgress);
        UniTask<PlayerProgress> GetStartingProgress();
    }
}