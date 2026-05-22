using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using Main.CodeBase.MainScene.ScreenManagement;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.MainScene
{
    public class Bootstrapper : MonoBehaviour
    {
        private ScreenManager _screenManager;
        private ILoadingCurtain _loadingCurtain;

        [Inject]
        private void Construct(ScreenManager screenManager, ILoadingCurtain loadingCurtain)
        {
            _screenManager = screenManager;
            _loadingCurtain = loadingCurtain;
        }

        private void Start()
        {
            BuildScene().Forget();
        }

        private void OnDestroy()
        {
            _screenManager.Destroy();
        }

        private async UniTaskVoid BuildScene()
        {
            await _screenManager.CreateBottomPanel();
            await _screenManager.CreateWalletView();
            //await _screenManager.CreateSettingsButton();
            //await _screenManager.OpenScreen(ScreenType.Relax);
            
            _loadingCurtain.HideCurtain();
        }
    }
}