using System;
using Main.CodeBase.Buttons;
using UnityEngine;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList
{
    public class WorkListView : MonoBehaviour
    {
        public event Action ClickUpgradeButton;
        public event Action ClickWorkButton;
        
        [SerializeField] private SimpleButton _upgradeButton;
        [SerializeField] private SimpleButton _workButton;
        [SerializeField] private WorkSlider _workSlider;
        [SerializeField] private LockedPanel _lockedPanel;
        
        public void Initialize(bool isUnlocked, string lockedTitle, int currentCycleValue, int fullCycleValue)
        {
            _workSlider.SetValue(currentCycleValue, fullCycleValue);
            _lockedPanel.SetActive(isUnlocked == false, lockedTitle);
            _upgradeButton.Click += OnClickUpgradeButton;
            _workButton.Click += OnClickWorkButton;
        }

        public void UpdateCycleValues(int currentValue, int fullValue)
        {
            _workSlider.SetValue(currentValue, fullValue);
        }
        
        private void OnDestroy()
        {
            _upgradeButton.Click -= OnClickUpgradeButton;
            _workButton.Click -= OnClickWorkButton;
        }
        
        private void OnClickUpgradeButton()
        {
            ClickUpgradeButton?.Invoke();
        }
        
        private void OnClickWorkButton()
        {
            ClickWorkButton?.Invoke();
        }
    }
}