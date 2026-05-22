using System;
using DG.Tweening;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.CodeBase.Buttons
{
    public class BottomPanelButton : CustomButton
    {
        public event Action<ScreenType> Click;
        
        private Vector3 _scale;
        private int _baseSiblingIndex;

        [field:SerializeField] public ScreenType ScreenType { get; private set; }
        
        private void Awake()
        {
            _scale = transform.localScale;
            _baseSiblingIndex = transform.GetSiblingIndex();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke(ScreenType);
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        { }

        public override void OnPointerExit(PointerEventData eventData)
        { }

        public void Select()
        {
            transform.SetAsLastSibling();
            
            DOTween.Kill(transform);
            
            transform.DOScale(_scale * 1.2f, 0.5f).SetEase(Ease.OutBack).SetId(transform);
        }

        public void Unselect()
        {
            transform.SetSiblingIndex(_baseSiblingIndex);
            
            DOTween.Kill(transform);

            transform.DOScale(_scale, 0.5f).SetEase(Ease.OutBack).SetId(transform);
        }
    }
}