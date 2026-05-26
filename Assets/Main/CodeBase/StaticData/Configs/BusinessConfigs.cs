using System;
using UnityEngine;

namespace Main.CodeBase.StaticData.Configs
{
    [Serializable]
    public class BusinessConfigs
    {
        [field:SerializeField] public BusinessType BusinessType { get; private set; }
        [field:SerializeField] public ManagerType Manager { get; private set; }
        [field:SerializeField] public int CycleSize { get; private set; }
        [field:SerializeField] public ulong BaseProduction { get; private set; }
        [field:SerializeField] public ulong BaseProductPrice { get; private set; }
        [field:SerializeField] public ulong BaseProductionUpgradeCost { get; private set; }
        [field:SerializeField] public ulong BaseProductPriceUpgradeCost { get; private set; }
        [field:SerializeField] public RateOfGrowth ProductionRateOfGrowth { get; private set; }
        [field:SerializeField] public RateOfGrowth PriceRateOfGrowth { get; private set; }
        [field:SerializeField] public RateOfGrowth ProductionUpgradeCostRateOfGrowth { get; private set; }
        [field:SerializeField] public RateOfGrowth PriceUpgradeCostRateOfGrowth { get; private set; }
        [field:SerializeField] public HouseType RequiredHouse { get; private set; }
        [field:SerializeField] public ClothesType RequiredClothes { get; private set; }
    }
    
    public enum BusinessType
    {
        Bottle = 0,
        Scrap = 1,
    }

    public enum RateOfGrowth
    {
        VerySlow = 0,
        Slow = 1,
        Middle = 2,
        Fast = 3,
        SuperFast = 4,
    }
}