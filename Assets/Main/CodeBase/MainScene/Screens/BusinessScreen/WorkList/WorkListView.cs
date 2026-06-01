using System;
using System.Globalization;
using DG.Tweening;
using Main.CodeBase.Buttons;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using Main.CodeBase.Utilities;
using TMPro;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList
{
    public class WorkListView : MonoBehaviour
    {
        public event Action ClickExpandButton;
        public event Action ClickWorkButton;
        public event Action ProductionUpgradeButtonClick;
        public event Action PriceUpgradeButtonClick;
        public event Action ManagerButtonClick;
        
        [Header("References")]
        [SerializeField] private RectTransform _panelRect;
        [SerializeField] private TextMeshProUGUI _expandText;
        [SerializeField] private TextMeshProUGUI _productPriceText;
        [SerializeField] private TextMeshProUGUI _passiveIncomeValueText;
        [SerializeField] private TextMeshProUGUI _productionValueText;
        [SerializeField] private TextMeshProUGUI _productionUpgradeButtonText;
        [SerializeField] private TextMeshProUGUI _priceUpgradeButtonText;
        [SerializeField] private TextMeshProUGUI _productionUpgradeValueText;
        [SerializeField] private TextMeshProUGUI _priceUpgradeValueText;
        [SerializeField] private SimpleButton _expandButton;
        [SerializeField] private SimpleButton _workButton;
        [SerializeField] private SimpleButton _productionUpgradeButton;
        [SerializeField] private SimpleButton _priceUpgradeButton;
        [SerializeField] private SimpleButton _managerButton;
        [SerializeField] private WorkSlider _workSlider;
        [SerializeField] private LockedPanel _lockedPanel;
        
        [Header("Settings")]
        [SerializeField] private float _expandedHeight;
        [SerializeField] private float _collapsedHeight;
        
        private Localization _localization;

        [Inject]
        private void Construct(Localization localization)
        {
            _localization = localization;
        }
        
        public void Initialize(bool isUnlocked, string lockedTitle, WorkListBuildData buildData)
        {
            _workSlider.SetValue(buildData.CurrenCycleValue, buildData.FullCycleValue, false);
            _lockedPanel.SetActive(isUnlocked == false, lockedTitle);
            
            SetTexts(buildData);
        }

        public void Rebuild(WorkListBuildData buildData)
        {
            SetTexts(buildData);
        }

        private void SetTexts(WorkListBuildData buildData)
        {
            string passiveIncomeValue = buildData.PassiveIncome.Item1 < 10 
                ? buildData.PassiveIncome.Item2.ToString("F1") 
                : buildData.PassiveIncome.Item1.ToFormatNumberString();
            
            _productPriceText.text = $"${buildData.CurrentProductPrice.ToFormatNumberString()} / {_localization.GetPiecesShortWord()}";
            
            _passiveIncomeValueText.text = $"${passiveIncomeValue} / {_localization.GetSecondsShortWord()}";
            
            _productionValueText.text = $"{buildData.ProductionAmount.ToFormatNumberString()} {_localization.GetPiecesShortWord()} " +
                                        $"/ {_localization.GetCycleWord().ToLower()}";

            _productionUpgradeButtonText.text = $"{_localization.GetBuyWord()} " +
                                                $"{buildData.ProductionUpgradeCost.ToFormatNumberString()}$";

            _priceUpgradeButtonText.text = $"{_localization.GetBuyWord()} " +
                                           $"{buildData.PriceUpgradeCost.ToFormatNumberString()}$";

            _productionUpgradeValueText.text = $"+{buildData.ProductionUpgradeValue.ToFormatNumberString()} " +
                                               $"{_localization.GetPiecesShortWord()} " +
                                               $"/ {_localization.GetCycleWord().ToLower()}";

            _priceUpgradeValueText.text = $"+{buildData.PriceUpgradeValue.ToFormatNumberString()}$ " +
                                          $"{_localization.GetProductPriceText().ToLower()}";
        }

        private void OnEnable()
        {
            _expandButton.Click += OnClickExpandButton;
            _workButton.Click += OnClickWorkButton;
            _productionUpgradeButton.Click += OnClickProductionUpgradeButton;
            _priceUpgradeButton.Click += OnClickPriceUpgradeButton;
            _managerButton.Click += OnClickManagerButton;
        }

        private void OnDisable()
        {
            _expandButton.Click -= OnClickExpandButton;
            _workButton.Click -= OnClickWorkButton;
            _productionUpgradeButton.Click -= OnClickProductionUpgradeButton;
            _priceUpgradeButton.Click -= OnClickPriceUpgradeButton;
            _managerButton.Click -= OnClickManagerButton;
        }

        public void SetExpand(bool isExpanded)
        {
            DOTween.Kill($"expand{transform}");
            
            Vector2 size = _panelRect.sizeDelta;
            size.y = isExpanded ? _expandedHeight : _collapsedHeight;
            
            _panelRect.DOSizeDelta(size, 0.4f).SetId($"expand{transform}");

            _expandText.text = isExpanded ? _localization.GetCollapseWord() : _localization.GetExpandWord();
        }
        
        public void UpdateCycleValues(int currentValue, int fullValue)
        {
            _workSlider.SetValue(currentValue, fullValue);
        }
        
        public void ShowIncomeAnimation(ulong income)
        {
            
        }
        
        private void OnClickExpandButton()
        {
            ClickExpandButton?.Invoke();
        }
        
        private void OnClickWorkButton()
        {
            ClickWorkButton?.Invoke();
        }
        
        private void OnClickPriceUpgradeButton()
        {
            PriceUpgradeButtonClick?.Invoke();
        }

        private void OnClickProductionUpgradeButton()
        {
            ProductionUpgradeButtonClick?.Invoke();
        }
        
        private void OnClickManagerButton()
        {
            ManagerButtonClick?.Invoke();
        }
    }
}