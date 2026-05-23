using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Main.CodeBase.SaveData;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.ConfigsService
{
    public class LocalConfigs : IConfigs
    {
        #region StartingProgress
        
        private PlayerProgress _startingProgress;

        public void CacheStartingProgress(PlayerProgress startingProgress)
        {
            _startingProgress = startingProgress;
        }

        public UniTask<PlayerProgress> GetStartingProgress()
        {
            if (_startingProgress == null)
                throw new Exception("The StartingProgressConfigs are not cached!");
            
            return new UniTask<PlayerProgress>(_startingProgress);
        }
        
        #endregion
        
        #region Business
        
        private IEnumerable<BusinessConfigs> _businessConfigs;
        
        public void CacheBusinessConfigs(IEnumerable<BusinessConfigs> configs)
        {
            _businessConfigs = configs;
        }

        public IEnumerable<BusinessType> GetAllBusinesses()
        {
            return _businessConfigs.Select(c => c.BusinessType).ToArray();
        }

        public BusinessConfigs GetBusinessConfig(BusinessType businessType)
        {
            return _businessConfigs.First(c => c.BusinessType == businessType);
        }

        #endregion
    }
}