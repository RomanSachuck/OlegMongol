using Main.CodeBase.SimpleAnimations;
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
        }

        private void OnDestroy()
        {
            _wallet.Changed -= OnChanged;
        }

        private void OnChanged(Currency currency, ulong oldValue, ulong newValue)
        {
            _dollarsText.DoValue(oldValue, newValue, 0.5f);
        }
    }
}