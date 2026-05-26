using System.Collections.Generic;
using System.Linq;
using Main.CodeBase.SaveData;
using Main.CodeBase.StaticData.Configs;
using Main.CodeBase.StaticData.Repositories;
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

        #region Time
        
        public long TimeExitGame 
        {
            get => _playerProgress.TimeExitGame;
            set => _playerProgress.TimeExitGame = value;
        }
        
        #endregion
        
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

        #region House
        
        public IEnumerable<HouseType> GetOpenedHouses()
        {
            List<HouseType> result = new List<HouseType>();

            foreach (HouseType openedHouse in _playerProgress.HouseSaveData.OpenedHouses) 
                result.Add(openedHouse);
            
            return result;
        }

        public HouseType SelectedHouse
        {
            get => _playerProgress.HouseSaveData.SelectedHouse;
            set => _playerProgress.HouseSaveData.SelectedHouse = value;
        }

        public void AddOpenedHouse(HouseType house)
        {
            _playerProgress.HouseSaveData.OpenedHouses.Add(house);
        }
        
        #endregion
        
        #region Clothes
        
        public IEnumerable<ClothesType> GetOpenedClothes()
        {
            List<ClothesType> result = new List<ClothesType>();

            foreach (ClothesType openedClothes in _playerProgress.ClothesSaveData.OpenedClothes) 
                result.Add(openedClothes);
            
            return result;
        }

        public ClothesType SelectedClothes
        {
            get => _playerProgress.ClothesSaveData.SelectedClothes;
            set => _playerProgress.ClothesSaveData.SelectedClothes = value; 
        }
        public void AddOpenedClothes(ClothesType clothes)
        {
            _playerProgress.ClothesSaveData.OpenedClothes.Add(clothes);
        }
        
        #endregion

        #region Business
        
        public int GetWorkCycleValue(BusinessType businessType)
        {
            return GetBusinessInfoSaveData(businessType).WorkCycleValue;
        }

        public int GetProductionUpgradeLevel(BusinessType businessType)
        {
            return GetBusinessInfoSaveData(businessType)
                .ProductionUpgradeLevel;
        }

        public int GetPriceUpgradeLevel(BusinessType businessType)
        {
            return GetBusinessInfoSaveData(businessType)
                .PriceUpgradeLevel;
        }

        public int GetManagerLevel(BusinessType businessType)
        {
            return GetBusinessInfoSaveData(businessType)
                .ManagerLevel;
        }

        public void SetWorkCycleValue(BusinessType businessType, int value)
        {
            GetBusinessInfoSaveData(businessType)
                .WorkCycleValue = value;
        }

        public void SetProductionUpgradeLevel(BusinessType businessType, int value)
        {
            GetBusinessInfoSaveData(businessType)
                .ProductionUpgradeLevel = value;
        }

        public void SetPriceUpgradeLevel(BusinessType businessType, int value)
        {
            GetBusinessInfoSaveData(businessType)
                .PriceUpgradeLevel = value;
        }

        public void SetManagerLevel(BusinessType businessType, int value)
        {
            GetBusinessInfoSaveData(businessType)
                .ManagerLevel = value;
        }

        private BusinessInfoSaveData GetBusinessInfoSaveData(BusinessType businessType)
        {
            BusinessInfoSaveData business = _playerProgress.BusinessSaveData.Businesses
                .FirstOrDefault(b => b.BusinessType == businessType);

            if (business == null)
            {
                business = new BusinessInfoSaveData(businessType);
                _playerProgress.BusinessSaveData.Businesses.Add(business);
            }
            
            return business;
        }        
        
        #endregion
    }
}