using UnityEngine;
 
namespace Main.CodeBase.Infrastructure.Services.SceneLoadService
{
    public class LoadingCurtain : MonoBehaviour
    {
        private static readonly int Close = Animator.StringToHash("Close");
        private static readonly int Open = Animator.StringToHash("Open");
        
        [SerializeField] private Animator _animatorCircle;
        [SerializeField] private Animator _animatorGate;

        private void Awake()
        {
            _animatorCircle.updateMode = AnimatorUpdateMode.UnscaledTime;
            _animatorGate.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _animatorGate.SetTrigger(Close);
        }
        
        public void Hide()
        {
            _animatorGate.SetTrigger(Open);
        }
    }
}