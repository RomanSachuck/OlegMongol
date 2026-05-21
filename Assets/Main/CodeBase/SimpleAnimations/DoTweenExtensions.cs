using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.CodeBase.SimpleAnimations
{
    public static class DoTweenExtensions
    {
        public static void DoClickShake(this Transform transform, Vector3 startScale)
        {
            DOTween.Kill(transform);
            transform.localScale = startScale;
            
            DOTween.Sequence()
                .Append(transform.DOScale(transform.localScale * 0.9f, .3f)).SetId(transform)
                .Append(transform.DOScale(transform.localScale, .5f)).SetEase(Ease.OutBack).SetId(transform);
        }
        
        public static void DoSetShake(this Transform transform)
        {
            DOTween.Kill(transform);
            
            DOTween.Sequence()
                .Append(transform.DOScale(transform.localScale * 0.9f, .1f)).SetId(transform)
                .Append(transform.DOScale(transform.localScale, .3f)).SetEase(Ease.OutBack).SetId(transform);
        }
        
        public static void DoValue(this TextMeshProUGUI tmp, ulong oldValue, ulong newValue, 
            float duration, Action onKill = null, string prefix = "", string postfix = "")
        {
            DOTween.Kill(tmp);
            
            DOTween.To(() => oldValue, x => {
                    oldValue = x;
                    tmp.text = oldValue + postfix;
                }, newValue, duration)
                .SetEase(Ease.OutCubic)
                .SetId(tmp)
                .OnKill(() => onKill?.Invoke());
        }

        public static void DoValue(this Slider slider, float newValue, float duration)
        {
            DOTween.Kill(slider);
            
            DOTween.To(() => slider.value
                , x => slider.value = x, newValue, duration)
                .SetId(slider);
        }

        public static void DoVolume(this AudioSource audioSource, float newVolume, float duration)
        {
            DOTween.Kill(audioSource);
            
            DOTween.To(() => audioSource.volume
                    , x => audioSource.volume = x, newVolume, duration)
                .SetId(audioSource);
        }
    }
}