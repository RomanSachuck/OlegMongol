using UnityEngine;

namespace Main.CodeBase.Utilities
{
    [ExecuteInEditMode]
    public class UIScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private RectTransform _content;

        private Vector2 _lastContainerSize;
        private Vector2 _lastContentSize;

        void Start()
        {
            _lastContainerSize = _container.rect.size;
            _lastContentSize = _content.rect.size;
            FitToContainer();
        }

        void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && _container && _content && _lastContentSize != _content.rect.size)
            {
                _lastContentSize = _content.rect.size;
                FitToContainer();
                return;
            }
#endif

            Vector2 currentSize = _container.rect.size;
            if (currentSize != _lastContainerSize)
            {
                _lastContainerSize = currentSize;
                FitToContainer();
            }
        }

        void FitToContainer()
        {
            Vector2 containerSize = _container.rect.size;
            Vector2 contentSize = _content.rect.size;

            if (contentSize.x > containerSize.x || contentSize.y > containerSize.y)
            {
                float scaleX = containerSize.x / contentSize.x;
                float scaleY = containerSize.y / contentSize.y;
                float scale = Mathf.Min(scaleX, scaleY);

                _content.localScale = Vector3.one * scale;
            }
            else
            {
                _content.localScale = Vector3.one;
            }
        }
    }
}