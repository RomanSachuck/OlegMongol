using Main.CodeBase.StaticData.Configs;
using UnityEngine;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.Manager
{
    public class ManagerPanelBuildData
    {
        public ManagerType ManagerType { get; private set; }
        public int ClickAmount { get; private set; }
        public ulong Cost { get; private set; }
        public bool Hired { get; private set; }
        public Sprite ManagerPortrait { get; private set; }

        public ManagerPanelBuildData(ManagerType managerType, int clickAmount, ulong cost,
            bool hired, Sprite managerPortrait)
        {
            ManagerType = managerType;
            ClickAmount = clickAmount;
            Cost = cost;
            Hired = hired;
            ManagerPortrait = managerPortrait;
        }
    }
}