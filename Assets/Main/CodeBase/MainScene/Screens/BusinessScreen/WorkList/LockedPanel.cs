using TMPro;
using UnityEngine;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList
{
    public class LockedPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _requirementText;
        
        public void SetActive(bool isOpen, string lockedTitle)
        {
            gameObject.SetActive(isOpen);
            _requirementText.text = lockedTitle;
        }
    }
}