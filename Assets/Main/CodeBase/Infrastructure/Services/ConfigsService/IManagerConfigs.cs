using System.Collections.Generic;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public interface IManagerConfigs
    {
        void CacheManagersConfigs(IEnumerable<ManagerConfigs> configs);
        IEnumerable<ManagerType> GetAllManagers();
        float GetTimeToClick(ManagerType managerType, int level);
        ulong GetUpgradeCost(ManagerType managerType, int level);
    }
}