using System;
using DG.Tweening;
using Main.CodeBase.Buttons;
using UnityEngine;

namespace Main.CodeBase.MainScene.Screens.BusinessScreen.Manager
{
    public class ManagerPanelView : MonoBehaviour
    {
        public event Action Closed;

        [SerializeField] private Transform _content;
        [SerializeField] private SimpleButton _closeButton;

        private float _posY;
        
        private void Awake()
        {
            _posY = _content.position.y;
        }

        private void OnEnable()
        {
            _closeButton.Click += Close;
        }

        private void OnDisable()
        {
            _closeButton.Click -= Close;
        }

        public void Open()
        {
            DOTween.Kill(transform);
            
            gameObject.SetActive(true);

            _content.DOMoveY(_posY, 0.5f).From(_posY - 9).SetId(transform).SetEase(Ease.OutBack);
        }
        
        private void Close()
        {
            DOTween.Kill(transform);
            
            DOTween.Sequence().SetId(transform)
                .Append(_content.DOMoveY(_posY - 9, 0.5f).SetId(transform).SetEase(Ease.InBack))
                .OnKill(() =>
                {
                    gameObject.SetActive(false);
                    Closed?.Invoke();
                });
        }
    }
}