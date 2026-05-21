using System.Collections.Generic;
using Main.CodeBase.Systems.WalletSystem;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IWalletPersistent
    {
        IEnumerable<(Currency, ulong)> GetAllCurrencies();
        ulong GetValue(Currency currency);
        void SetValue(Currency currency, ulong value);
    }
}