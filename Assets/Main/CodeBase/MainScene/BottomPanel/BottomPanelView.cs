using System;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;

namespace Main.CodeBase.MainScene.BottomPanel
{
    public class BottomPanelView : MonoBehaviour
    {
        public event Action<ScreenType> Clicked;
    }
}