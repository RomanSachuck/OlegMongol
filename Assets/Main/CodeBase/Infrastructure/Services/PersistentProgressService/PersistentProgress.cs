using System.Collections.Generic;
using System.Linq;
using Main.CodeBase.SaveData;
using Main.CodeBase.Systems.WalletSystem;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public class PersistentProgress : IPersistentProgress
    {
        private PlayerProgress _playerProgress;

        public void CachePlayerProgress(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }

        #region Wallet

        public IEnumerable<(Currency, ulong)> GetAllCurrencies()
        {
            List<(Currency, ulong)> result = new List<(Currency, ulong)>();

            foreach (CurrencySaveData currencyData in _playerProgress.WalletSaveData.Currencies) 
                result.Add(new (currencyData.Currency, currencyData.Value));
            
            return result;
        }

        public ulong GetValue(Currency currency)
        {
            return _playerProgress.WalletSaveData.Currencies
                .First(c => c.Currency == currency).Value;
        }

        public void SetValue(Currency currency, ulong value)
        {
            _playerProgress.WalletSaveData.Currencies
                .First(c => c.Currency == currency).Value = value;
        }

        #endregion
    }
}