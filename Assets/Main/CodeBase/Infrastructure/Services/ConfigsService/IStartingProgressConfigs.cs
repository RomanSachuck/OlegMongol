using Cysharp.Threading.Tasks;
using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public interface IStartingProgressConfigs
    {
        UniTask<PlayerProgress> GetStartingProgress();
    }
}