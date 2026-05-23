using Main.CodeBase.SaveData;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IPersistentProgress : ITimePersistent, IWalletPersistent, IHousePersistent, IClothesPersistent, IBusinessPersistent
    {
        void CachePlayerProgress(PlayerProgress playerProgress);
    }
}