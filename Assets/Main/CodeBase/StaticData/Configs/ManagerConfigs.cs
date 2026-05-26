using System;
using UnityEngine;

namespace Main.CodeBase.StaticData.Configs
{
    [Serializable]
    public class ManagerConfigs
    {
        [field:SerializeField] public ManagerType ManagerType { get; private set; }
        [field:SerializeField] public int BaseClickValue { get; private set; }
        [field:SerializeField] public int UpgradeCost { get; private set; }
        [field:SerializeField] public RateOfGrowth ClickRateOfGrowth { get; private set; }
        [field:SerializeField] public RateOfGrowth UpgradeCostRateOfGrowth { get; private set; }
    }

    public enum ManagerType
    {
        Empty = 0,
        MadPashka = 1,
        FedorBat = 2,
        RuslanGitelman = 3,
        GennadiyGorin = 4,
    }
}