using System;
using DG.Tweening;
using Main.CodeBase.Buttons;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList;
using TMPro;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessUpgrade
{
    public class BusinessUpgradePanelView : MonoBehaviour
    {
        public event Action ProductionUpgradeButtonClick;
        public event Action PriceUpgradeButtonClick;
        public event Action Closed;
        
        [SerializeField] private TextMeshProUGUI _businessNameText;
        [SerializeField] private TextMeshProUGUI _productPriceText;
        [SerializeField] private TextMeshProUGUI _passiveIncomeValueText;
        [SerializeField] private TextMeshProUGUI _productionValueText;
        [SerializeField] private TextMeshProUGUI _productionUpgradeButtonText;
        [SerializeField] private TextMeshProUGUI _priceUpgradeButtonText;
        [SerializeField] private TextMeshProUGUI _productionUpgradeValueText;
        [SerializeField] private TextMeshProUGUI _priceUpgradeValueText;
        
        [SerializeField] private SimpleButton _closeButton;
        [SerializeField] private SimpleButton _productionUpgradeButton;
        [SerializeField] private SimpleButton _priceUpgradeButton;
        
        [SerializeField] private WorkSlider _workSlider;
        
        [SerializeField] private Transform _content;
        
        private Localization _localization;
        private float _contentPosY;

        [Inject]
        private void Construct(Localization localization)
        {
            _localization = localization;
        }
        
        private void OnEnable()
        {
            _productionUpgradeButton.Click += OnProductionUpgradeButtonClick;
            _priceUpgradeButton.Click += OnPriceUpgradeButtonClick;
            _closeButton.Click += OnCloseButtonClick;
        }

        private void OnDisable()
        {
            _productionUpgradeButton.Click -= OnProductionUpgradeButtonClick;
            _priceUpgradeButton.Click -= OnPriceUpgradeButtonClick;
            _closeButton.Click -= OnCloseButtonClick;
        }

        private void Awake()
        {
            _contentPosY = _content.position.y;
        }

        public void Build(UpgradePanelBuildData buildData)
        {
            _businessNameText.text = $"<b>{_localization.GetBusinessWord().ToUpper()}:</b>  " +
                                     $"{_localization.GetBusinessName(buildData.BusinessType)}";
            
            _productPriceText.text = $"<b>{_localization.GetProductPriceText()} -</b>  {buildData.CurrentProductPrice}$";
            
            _passiveIncomeValueText.text = $"${buildData.PassiveIncome} / {_localization.GetSecondsShortWord()}";
            
            _productionValueText.text = $"{buildData.ProductionAmount} {_localization.GetPiecesShortWord()} " +
                                        $"/ {_localization.GetCycleWord().ToLower()}";

            _productionUpgradeButtonText.text = $"{_localization.GetBuyWord()} {buildData.ProductionUpgradeCost}$";

            _priceUpgradeButtonText.text = $"{_localization.GetBuyWord()} {buildData.PriceUpgradeCost}$";

            _productionUpgradeValueText.text = $"+{buildData.ProductionUpgradeValue} {_localization.GetPiecesShortWord()} " +
                                               $"/ {_localization.GetCycleWord().ToLower()}";

            _priceUpgradeValueText.text = $"+{buildData.PriceUpgradeValue}$ {_localization.GetProductPriceText().ToLower()}";
            
            _workSlider.SetValue(buildData.CurrenCycleValue, buildData.FullCycleValue);
        }
        
        public void Open()
        {
            DOTween.Kill(transform);
            
            gameObject.SetActive(true);
            
            DOTween.Sequence().SetId(transform)
                .Append(_content.DOMoveY(_contentPosY, 0.5f)
                    .From(_contentPosY - 6)
                    .SetEase(Ease.OutBack)
                    .SetId(transform));
        }

        private void Close()
        {
            DOTween.Kill(transform);
            
            DOTween.Sequence().SetId(transform)
                .Append(_content.DOMoveY(_contentPosY - 6, 0.5f)
                    .SetEase(Ease.InBack)
                    .SetId(transform))
                .OnKill(() =>
                {
                    gameObject.SetActive(false);
                    _content.DOMoveY(_contentPosY, 0.01f);
                    Closed?.Invoke();
                });
        }
        
        private void OnPriceUpgradeButtonClick()
        {
            PriceUpgradeButtonClick?.Invoke();
        }

        private void OnProductionUpgradeButtonClick()
        {
            ProductionUpgradeButtonClick?.Invoke();
        }
        
        private void OnCloseButtonClick()
        {
            Close();
        }
    }
}