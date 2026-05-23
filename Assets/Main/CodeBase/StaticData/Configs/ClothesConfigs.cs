using System;
using UnityEngine;

namespace Main.CodeBase.StaticData.Configs
{
    [Serializable]
    public class ClothesConfigs
    {
        [field:SerializeField] public ClothesType ClothesType { get; private set; }
        [field:SerializeField] public ulong Price { get; private set; }
    }

    public enum ClothesType
    {
        Rag = 0,
        PrisonRobes = 1,
    }
}