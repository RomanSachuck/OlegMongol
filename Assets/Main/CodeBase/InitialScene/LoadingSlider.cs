using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.CodeBase.InitialScene
{
    public class LoadingSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private TextMeshProUGUI _loadingNumberText;
        [SerializeField] private TextMeshProUGUI _loadingInfoText;
        [SerializeField] private string[] _loadingInfo;
        
        private int _currentInfoIndex;
        
        public void SetValue(int value, bool withAnimation = true)
        {
            DOTween.Kill(transform);
            
            if (withAnimation)
                _slider.DOValue(value, 0.3f).SetId(transform);
            else
                _slider.value = value;
            
            _loadingNumberText.text = $"{value}%";
        }

        public void SetNextInfo()
        {
            _loadingInfoText.text = _loadingInfo[_currentInfoIndex++];
            
            if(_currentInfoIndex >= _loadingInfo.Length)
                _currentInfoIndex = 0;
        }
    }
}