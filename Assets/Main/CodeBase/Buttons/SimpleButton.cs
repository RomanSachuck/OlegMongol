using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.CodeBase.Buttons
{
    public class SimpleButton : CustomButton
    {
        private Vector3 _scale;

        private void Awake()
        {
            _scale = transform.localScale;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            DOTween.Kill(transform);

            DOTween.Sequence().SetId(transform)
                .Append(transform.DOScale(_scale * 0.85f, 0.1f)).SetId(transform)
                .Append(transform.DOScale(_scale, 0.1f)).SetId(transform);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        { }

        public override void OnPointerExit(PointerEventData eventData)
        { }
    }
}