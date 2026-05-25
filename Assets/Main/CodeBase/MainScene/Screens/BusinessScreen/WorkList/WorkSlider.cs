using DG.Tweening;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using Main.CodeBase.SimpleAnimations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList
{
    public class WorkSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _cycleText;
        
        private Localization _localization;

        [Inject]
        private void Construct(Localization localization)
        {
            _localization = localization;
        }
        
        public void SetValue(int currentValue, int fullValue)
        {
            _slider.maxValue = fullValue;
            
            DOTween.Kill(transform);
            
            if(currentValue != 0)
            {
                _slider.DoValue(currentValue, 0.2f);
                _cycleText.text = $"{currentValue}/{fullValue}{_localization.GetClickShortWord()}";
            }
            else
            {
                DOTween.Sequence()
                    .SetId(transform)
                    .AppendCallback(() =>
                    {
                        _slider.DoValue(10, 0.2f);
                        _cycleText.text = $"{fullValue}/{fullValue}{_localization.GetClickShortWord()}";
                    })
                    .SetId(transform)
                    .AppendInterval(0.1f)
                    .SetId(transform)
                    .AppendCallback(() =>
                    {
                        _slider.DoValue(currentValue, 0.2f);
                        _cycleText.text = $"{currentValue}/{fullValue}{_localization.GetClickShortWord()}";
                    })
                    .SetId(transform);
            }
            
            
        }
    }
}