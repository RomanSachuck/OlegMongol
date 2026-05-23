using System;
using UnityEngine;

namespace Main.CodeBase.StaticData.Configs
{
    [Serializable]
    public class BusinessConfigs
    {
        [field:SerializeField] public BusinessType BusinessType { get; private set; }
        [field:SerializeField] public ulong BaseProductPrice { get; private set; }
        [field:SerializeField] public int CycleSize { get; private set; }
        [field:SerializeField] public ulong BaseProduction { get; private set; }
        [field:SerializeField] public HouseType RequiredHouse { get; private set; }
        [field:SerializeField] public ClothesType RequiredClothes { get; private set; }
    }
    
    public enum BusinessType
    {
        Bottle = 0,
        Scrap = 1,
    }
}