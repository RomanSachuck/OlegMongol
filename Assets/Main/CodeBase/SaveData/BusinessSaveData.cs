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
        public ManagerSaveData Manager;

        public BusinessInfoSaveData(BusinessInfoSaveData template)
        {
            BusinessType = template.BusinessType;
            WorkCycleValue = template.WorkCycleValue;
            ProductionUpgradeLevel = template.ProductionUpgradeLevel;
            PriceUpgradeLevel = template.PriceUpgradeLevel;
            Manager = new ManagerSaveData(template.Manager);
        }

        public BusinessInfoSaveData(BusinessType businessType)
        {
            BusinessType = businessType;
            ProductionUpgradeLevel = 1;
            PriceUpgradeLevel = 1;
        }
    }

    [Serializable]
    public class ManagerSaveData
    {
        public ManagerSaveData(ManagerSaveData template)
        {
            
        }
    }
}