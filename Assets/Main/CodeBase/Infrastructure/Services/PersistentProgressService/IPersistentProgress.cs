using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IPersistentProgress : IWalletPersistent
    {
        void CachePlayerProgress(PlayerProgress playerProgress);
    }
}