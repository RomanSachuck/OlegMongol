using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.CodeBase.Buttons
{
    public abstract class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        public abstract void OnPointerClick(PointerEventData eventData);

        public abstract void OnPointerEnter(PointerEventData eventData);

        public abstract void OnPointerExit(PointerEventData eventData);
    }
}