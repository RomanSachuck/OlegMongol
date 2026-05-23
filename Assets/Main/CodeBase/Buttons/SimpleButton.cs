using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.CodeBase.Buttons
{
    public class SimpleButton : CustomButton
    {
        public event Action Click;
        
        private Vector3 _scale;

        private void Awake()
        {
            _scale = transform.localScale;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke();
            
            DOTween.Kill(transform);
            transform.localScale = _scale;

            DOTween.Sequence().SetId(transform)
                .Append(transform.DOScale(_scale * 0.9f, 0.1f)).SetId(transform)
                .Append(transform.DOScale(_scale, 0.1f)).SetId(transform);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        { }

        public override void OnPointerExit(PointerEventData eventData)
        { }
    }
}