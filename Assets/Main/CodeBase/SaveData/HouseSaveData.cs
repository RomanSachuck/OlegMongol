using System;
using System.Collections.Generic;
using Main.CodeBase.StaticData.Repositories;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class HouseSaveData
    {
        public List<HouseType> OpenedHouses;
        public HouseType SelectedHouse;
        
        public HouseSaveData(HouseSaveData template)
        {
            OpenedHouses = new List<HouseType>();

            foreach (HouseType houseType in template.OpenedHouses) 
                OpenedHouses.Add(houseType);
            
            SelectedHouse = template.SelectedHouse;
        }
    }
}