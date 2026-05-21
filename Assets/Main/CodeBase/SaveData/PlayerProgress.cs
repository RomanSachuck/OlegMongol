using System;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class PlayerProgress
    {
        public WalletSaveData WalletSaveData;
        
        public PlayerProgress(PlayerProgress template)
        {
            WalletSaveData = new WalletSaveData(template.WalletSaveData);
        }
    }
}