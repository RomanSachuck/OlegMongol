using System;
using System.Collections.Generic;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.SaveData
{
    [Serializable]
    public class BusinessSaveData
    {
        public List<BusinessInfoSaveData> Businesses;

        public BusinessSaveData(BusinessSaveData template)
        {
            Businesses = new List<BusinessInfoSaveData>();

            if (template.Businesses != null)
            {
                foreach (BusinessInfoSaveData business in template.Businesses) 
                    Businesses.Add(new BusinessInfoSaveData(business));
            }
        }
    }

    [Serializable]
    public class BusinessInfoSaveData
    {
        public BusinessType BusinessType;
        public int WorkCycleValue;
        public int ProductionUpgradeLevel;
        public int PriceUpgradeLevel;
        public int ManagerLevel;

        public BusinessInfoSaveData(BusinessInfoSaveData template)
        {
            BusinessType = template.BusinessType;
            WorkCycleValue = template.WorkCycleValue;
            ProductionUpgradeLevel = template.ProductionUpgradeLevel;
            PriceUpgradeLevel = template.PriceUpgradeLevel;
            ManagerLevel = template.ManagerLevel;
        }

        public BusinessInfoSaveData(BusinessType businessType)
        {
            BusinessType = businessType;
            ProductionUpgradeLevel = 1;
            PriceUpgradeLevel = 1;
        }
    }
}