using System;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class PlayerProgress
    {
        public long TimeExitGame;
        public WalletSaveData WalletSaveData;
        public HouseSaveData HouseSaveData;
        public ClothesSaveData ClothesSaveData;
        public BusinessSaveData BusinessSaveData;
        
        public PlayerProgress(PlayerProgress template)
        {
            TimeExitGame = template.TimeExitGame;
            WalletSaveData = new WalletSaveData(template.WalletSaveData);
            HouseSaveData = new HouseSaveData(template.HouseSaveData);
            ClothesSaveData = new ClothesSaveData(template.ClothesSaveData);
            BusinessSaveData = new BusinessSaveData(template.BusinessSaveData);
        }
    }
}