using System;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class PlayerProgress
    {
        public WalletSaveData WalletSaveData;
        public HouseSaveData HouseSaveData;
        
        public PlayerProgress(PlayerProgress template)
        {
            WalletSaveData = new WalletSaveData(template.WalletSaveData);
            HouseSaveData = new HouseSaveData(template.HouseSaveData);
        }
    }
}