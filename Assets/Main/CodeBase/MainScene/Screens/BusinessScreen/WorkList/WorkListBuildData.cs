using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList
{
    public class WorkListBuildData
    {
        public BusinessType BusinessType { get; private set; }
        public int CurrenCycleValue { get; private set; }
        public int FullCycleValue { get; private set; }
        public ulong CurrentProductPrice { get; private set; }
        public ulong PassiveIncome { get; private set; }
        public int ProductionAmount { get; private set; }
        public ulong ProductionUpgradeCost { get; private set; }
        public ulong PriceUpgradeCost { get; private set; }
        public int ProductionUpgradeValue { get; private set; }
        public ulong PriceUpgradeValue { get; private set; }

        public WorkListBuildData(BusinessType businessType, int currenCycleValue, int fullCycleValue,
            ulong currentProductPrice, ulong passiveIncome, int productionAmount, ulong productionUpgradeCost,
            ulong priceUpgradeCost, int productionUpgradeValue, ulong priceUpgradeValue)
        {
            BusinessType = businessType;
            CurrenCycleValue = currenCycleValue;
            FullCycleValue = fullCycleValue;
            CurrentProductPrice = currentProductPrice;
            PassiveIncome = passiveIncome;
            ProductionAmount = productionAmount;
            ProductionUpgradeCost = productionUpgradeCost;
            PriceUpgradeCost = priceUpgradeCost;
            ProductionUpgradeValue = productionUpgradeValue;
            PriceUpgradeValue = priceUpgradeValue;
        }
    }
}