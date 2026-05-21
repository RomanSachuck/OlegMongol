using System;
using Cysharp.Threading.Tasks;
using Main.CodeBase.SaveData;

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
    }
}