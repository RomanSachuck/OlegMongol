using System;
using System.Collections.Generic;
using Main.CodeBase.Systems.WalletSystem;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class CurrencySaveData
    {
        public Currency Currency;
        public ulong Value;

        public CurrencySaveData(Currency currency, ulong value)
        {
            Currency = currency;
            Value = value;
        }
    }
    
    [Serializable]
    public class WalletSaveData
    {
        public List<CurrencySaveData> Currencies;

        public WalletSaveData(WalletSaveData template)
        {
            Currencies = new List<CurrencySaveData>();
            
            foreach (CurrencySaveData currency in template.Currencies) 
                Currencies.Add(new CurrencySaveData(currency.Currency, currency.Value));
        }
    }
}