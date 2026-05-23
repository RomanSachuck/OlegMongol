using System;
using System.Collections.Generic;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class ClothesSaveData
    {
        public List<ClothesType> OpenedClothes;
        public ClothesType SelectedClothes;

        public ClothesSaveData(ClothesSaveData template)
        {
            OpenedClothes = new List<ClothesType>();

            foreach (ClothesType clothes in template.OpenedClothes) 
                OpenedClothes.Add(clothes);
            
            SelectedClothes = template.SelectedClothes;
        }
    }
}