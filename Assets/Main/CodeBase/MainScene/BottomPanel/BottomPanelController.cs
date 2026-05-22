using Main.CodeBase.StaticData.Repositories;
using Main.CodeBase.Utilities;
using UnityEngine;

namespace Main.CodeBase.MainScene.BottomPanel
{
    public class BottomPanelController
    {
        public event UniAction<ScreenType> SelectedScreenChanged;
        
        private BottomPanelView _view;
        private ScreenType _selectedScreen = ScreenType.Relax;

        public void Initialize(BottomPanelView view)
        {
            _view = view;
            _view.Select(_selectedScreen);
            
            _view.Click += OnClick;
        }
        
        public void Destroy()
        {
            _view.Click -= OnClick;
        }
        
        private void OnClick(ScreenType screenType)
        {
            if (_selectedScreen == screenType)
                return;

            _view.Unselect(_selectedScreen);
            _selectedScreen = screenType;
            _view.Select(_selectedScreen);
            
            SelectedScreenChanged?.Invoke(screenType);
        }
    }
}