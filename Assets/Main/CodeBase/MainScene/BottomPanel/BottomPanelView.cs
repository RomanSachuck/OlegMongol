using System;
using System.Linq;
using Main.CodeBase.Buttons;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;

namespace Main.CodeBase.MainScene.BottomPanel
{
    public class BottomPanelView : MonoBehaviour
    {
        public event Action<ScreenType> Click;

        [SerializeField] private BottomPanelButton[] _buttons;

        private void OnEnable()
        {
            foreach (BottomPanelButton button in _buttons) 
                button.Click += OnClick;
        }

        private void OnDisable()
        {
            foreach (BottomPanelButton button in _buttons) 
                button.Click -= OnClick;
        }
        
        public void Select(ScreenType screenType)
        {
            _buttons.First(b => b.ScreenType == screenType).Select();
        }

        public void Unselect(ScreenType screenType)
        {
            _buttons.First(b => b.ScreenType == screenType).Unselect();
        }
        
        private void OnClick(ScreenType screenType)
        {
            Click?.Invoke(screenType);
        }
    }
}