using Main.CodeBase.SimpleAnimations;
using Main.CodeBase.Utilities;
using TMPro;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.Systems.WalletSystem
{
    public enum Currency
    {
        Dollar = 0,
    }
    
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dollarsText;
        
        private Wallet _wallet;
        
        [Inject]
        private void Construct(Wallet wallet)
        {
            _wallet = wallet;
            _wallet.Changed += OnChanged;

            OnChanged(Currency.Dollar, _wallet.GetValue(Currency.Dollar), _wallet.GetValue(Currency.Dollar));
        }

        private void OnDestroy()
        {
            _wallet.Changed -= OnChanged;
        }

        private void OnChanged(Currency currency, ulong oldValue, ulong newValue)
        {
            _dollarsText.text = $"${newValue.ToFormatNumberString()}";
        }
    }
}