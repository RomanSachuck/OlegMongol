using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IPersistentProgress : IWalletPersistent, IHousePersistent, IClothesPersistent
    {
        void CachePlayerProgress(PlayerProgress playerProgress);
    }
}