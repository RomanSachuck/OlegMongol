using Cysharp.Threading.Tasks;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.MainScene
{
    public class Bootstrapper : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            HideLoadingCurtain().Forget();
        }

        private async UniTaskVoid HideLoadingCurtain()
        {
            await UniTask.Delay(500);
            _sceneLoader.HideCurtain();
        }
    }
}