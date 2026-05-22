using Main.CodeBase.StaticData.Repositories;
using Main.CodeBase.Utilities;

namespace Main.CodeBase.MainScene.BottomPanel
{
    public class BottomPanelController
    {
        public event UniAction<ScreenType> SelectedScreenChanged;
        
        private BottomPanelView _view;

        public void Initialize(BottomPanelView view)
        {
            _view = view;
            _view.Clicked += OnClicked;
        }
        
        public void Destroy()
        {
            _view.Clicked -= OnClicked;
        }
        
        private void OnClicked(ScreenType screenType)
        {
            //Валидация клика
            SelectedScreenChanged?.Invoke(screenType);
        }
    }
}