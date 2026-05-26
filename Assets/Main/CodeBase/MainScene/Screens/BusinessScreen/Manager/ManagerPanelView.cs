using System;
using DG.Tweening;
using Main.CodeBase.Buttons;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.Manager
{
    public class ManagerPanelView : MonoBehaviour
    {
        public event Action UpgradeButtonClick;
        
        public event Action Closed;

        [SerializeField] private Transform _content;
        [SerializeField] private SimpleButton _closeButton;
        [SerializeField] private SimpleButton _upgradeButton;
        [SerializeField] private Image _managerPortrait;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private TextMeshProUGUI _skillsText;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private TextMeshProUGUI _buttonUpgradeText;

        private Localization _localization;
        private float _posY;

        private void Awake()
        {
            _posY = _content.position.y;
        }

        private void OnEnable()
        {
            _closeButton.Click += Close;
            _upgradeButton.Click += OnUpgradeButtonClick;
        }

        private void OnDisable()
        {
            _closeButton.Click -= Close;
            _upgradeButton.Click -= OnUpgradeButtonClick;
        }

        [Inject]
        private void Construct(Localization localization)
        {
            _localization = localization;
        }
        
        public void Build(ManagerPanelBuildData buildData)
        {
            _managerPortrait.sprite = buildData.ManagerPortrait;

            SetTexts(buildData);
        }

        public void Rebuild(ManagerPanelBuildData buildData)
        {
            SetTexts(buildData);
        }
        
        public void Open()
        {
            DOTween.Kill(transform);
            
            gameObject.SetActive(true);

            _content.DOMoveY(_posY, 0.5f).From(_posY - 9).SetId(transform).SetEase(Ease.OutBack);
        }
        
        private void SetTexts(ManagerPanelBuildData buildData)
        {
            _nameText.text = $"<b>{_localization.GetNameWord()}:</b> " +
                             $"{_localization.GetManagerName(buildData.ManagerType)}";
            
            _infoText.text = $"<b>{_localization.GetInfoWord()}:</b> " +
                             $"{_localization.GetManagerInfo(buildData.ManagerType)}";
            
            _skillsText.text = $"<b>{_localization.GetProfitWord()}:</b> " +
                               $"{_localization.GetManagerProfitText(buildData.ClickAmount)}";

            _statusText.text = $"{_localization.GetCurrentStatusText()}: " +
                               $"{_localization.GetManagerStatusText(buildData.Hired).ToLower()}";

            string buttonText = buildData.Hired 
                ? _localization.GetUpgradeWord().ToUpper() 
                : _localization.GetHireWord().ToUpper();
            _buttonUpgradeText.text = $"{buttonText} {buildData.Cost}$";
        }
        
        private void Close()
        {
            DOTween.Kill(transform);
            
            DOTween.Sequence().SetId(transform)
                .Append(_content.DOMoveY(_posY - 9, 0.5f).SetId(transform).SetEase(Ease.InBack))
                .OnKill(() =>
                {
                    gameObject.SetActive(false);
                    Closed?.Invoke();
                });
        }
        
        private void OnUpgradeButtonClick()
        {
            UpgradeButtonClick?.Invoke();
        }
    }
}