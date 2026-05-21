using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Main.CodeBase.InitialScene
{
    public class LoadingSimulator : MonoBehaviour
    {
        [SerializeField] private LoadingSlider _loadingSlider;
        
        private int _loadingPercent = 25;
        private bool _isActive;
        
        public async UniTaskVoid RunLoadingSimulation()
        {
            while (_isActive)
            {
                UpdateLoadingView();
                await UniTask.Delay(Random.Range(50, 300));
            }
        }

        public void FinishLoadingSimulation()
        {
            _isActive = false;
            UpdateLoadingView(true);
        }
        
        private void UpdateLoadingView(bool isFinished = false)
        {
            if (isFinished)
            {
                _loadingSlider.SetValue(100, false);
                return;
            }
            
            if (_loadingPercent >= 100)
            {
                _loadingPercent = Random.Range(21, 75);
                _loadingSlider.SetValue(_loadingPercent++, false);
                _loadingSlider.SetNextInfo();
                return;
            }
            
            _loadingSlider.SetValue(_loadingPercent++);
        }
    }
}