using System;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class PlayerProgress
    {
        public WalletSaveData WalletSaveData;
        public HouseSaveData HouseSaveData;
        public ClothesSaveData ClothesSaveData;
        
        public PlayerProgress(PlayerProgress template)
        {
            WalletSaveData = new WalletSaveData(template.WalletSaveData);
            HouseSaveData = new HouseSaveData(template.HouseSaveData);
            ClothesSaveData = new ClothesSaveData(template.ClothesSaveData);
        }
    }
}