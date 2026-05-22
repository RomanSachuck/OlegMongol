using UnityEngine;
using UnityEngine.UI;

namespace Main.CodeBase.MainScene.Screens.RelaxScreen
{
    public class RelaxScreenView : ScreenViewAbstract
    {
        [SerializeField] private Image _bgImage;
        
        public void SetBackground(Sprite bg)
        {
            _bgImage.sprite = bg;
        }
    }
}