using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.CodeBase.Buttons
{
    public class BottomPanelButton : CustomButton
    {
        private float _posY;
        private Vector3 _scale;
        private int _baseSiblingIndex;
        
        private void Awake()
        {
            _posY = transform.position.y;
            _scale = transform.localScale;
            _baseSiblingIndex = transform.GetSiblingIndex();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            
            DOTween.Kill(transform);

            DOTween.Sequence().SetId(transform)
                .Append(transform.DOScale(_scale, 0.15f)).SetId(transform)
                .Append(transform.DOScale(_scale * 1.1f, 0.15f)).SetId(transform);
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            transform.SetAsLastSibling();
            
            DOTween.Kill(transform);
            
            transform.DOScale(_scale * 1.1f, .5f)
                .SetEase(Ease.OutBack).SetId(transform);
            transform.DOMoveY(_posY + 0.1f, .3f)
                .SetEase(Ease.OutBack).SetId(transform);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            transform.SetSiblingIndex(_baseSiblingIndex);
            
            DOTween.Kill(transform);
            
            transform.DOScale(_scale, .5f)
                .SetEase(Ease.OutBack).SetId(transform);
            transform.DOMoveY(_posY, .3f)
                .SetEase(Ease.OutBack).SetId(transform);
        }
    }
}