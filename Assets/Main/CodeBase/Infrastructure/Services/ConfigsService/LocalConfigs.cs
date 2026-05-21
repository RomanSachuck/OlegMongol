using Cysharp.Threading.Tasks;
using Main.CodeBase.SaveData;
using Main.CodeBase.StaticData.Repositories;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public class LocalConfigs : IConfigs
    {
        private readonly StartingProgressRepository _startingProgressRepository;

        public LocalConfigs(StartingProgressRepository startingProgressRepository)
        {
            _startingProgressRepository = startingProgressRepository;
        }
        
        public UniTask<PlayerProgress> GetStartingProgress()
        {
            return new UniTask<PlayerProgress>(_startingProgressRepository.PlayerProgress);
        }
    }
}