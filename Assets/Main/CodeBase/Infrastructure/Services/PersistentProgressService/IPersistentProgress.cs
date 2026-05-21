using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IPersistentProgress
    {
        void CachePlayerProgress(PlayerProgress playerProgress);
    }
}