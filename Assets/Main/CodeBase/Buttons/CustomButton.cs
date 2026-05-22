using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.CodeBase.Buttons
{
    public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action Click;
        public event Action Enter;
        public event Action Exit;
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            Enter?.Invoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            Exit?.Invoke();
        }
    }
}