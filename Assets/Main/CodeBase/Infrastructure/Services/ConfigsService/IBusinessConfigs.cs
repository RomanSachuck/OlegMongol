using System.Collections.Generic;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public interface IBusinessConfigs
    {
        void CacheBusinessConfigs(IEnumerable<BusinessConfigs> configs);
        IEnumerable<BusinessType> GetAllBusinesses();
        BusinessConfigs GetBusinessConfig(BusinessType businessType);
    }
}