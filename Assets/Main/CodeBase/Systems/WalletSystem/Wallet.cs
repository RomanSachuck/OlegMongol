using System;
using System.Collections.Generic;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;

namespace Main.CodeBase.Systems.WalletSystem
{
    public class Wallet
    {
        public event Action<Currency, ulong, ulong> Changed;
        
        private IWalletPersistent _walletPersistent;
        private Dictionary<Currency, ulong> _currencies;

        public void Initialize(IWalletPersistent walletPersistent)
        {
            _walletPersistent = walletPersistent;
            
            _currencies = new Dictionary<Currency, ulong>();

            foreach ((Currency, ulong) currency in _walletPersistent.GetAllCurrencies()) 
                _currencies.Add(currency.Item1, currency.Item2);
        }

        public ulong GetValue(Currency currency) => 
            _currencies[currency];
        
        public void Add(Currency currency, ulong value)
        {
            if(value <= 0)
                throw new ArgumentException("Value must be greater than zero");
            
            _currencies[currency] += value;
            Changed?.Invoke(currency, _currencies[currency] - value, value);
        }

        public void Spend(Currency currency, ulong value)
        {
            if(value <= 0)
                throw new ArgumentException("Value must be greater than zero");
            
            _currencies[currency] -= value;
            Changed?.Invoke(currency, _currencies[currency] + value, value);
        }
    }
}