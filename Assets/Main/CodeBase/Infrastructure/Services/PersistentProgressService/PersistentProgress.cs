using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public class PersistentProgress : IPersistentProgress
    {
        private PlayerProgress _playerProgress;

        public void CachePlayerProgress(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }
    }
}