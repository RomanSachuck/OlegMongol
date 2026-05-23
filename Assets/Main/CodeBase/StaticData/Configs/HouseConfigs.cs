using System;
using UnityEngine;

namespace Main.CodeBase.StaticData.Configs
{
    [Serializable]
    public class HouseConfigs
    { 
        [field:SerializeField] public HouseType HouseType { get; private set; }
        [field:SerializeField] public ulong Price { get; private set; }
    }
    
    public enum HouseType
    {
        Street = 0,
        Bando = 1,
    }
}